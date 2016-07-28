/*
  user procedure
  name: son_soff
*/

#include "tbussio.h"
#include <tasy0def.h>
#include <grus0027.h>
#include "tbustyp.h"
#include "tbusvar.h"

void ISADLL USP_son_soff (str_arg *arg)
{
   char stat[4];
   int pwr_on;
   struct TermiBUS_var *axst = &( TermiBUS[ AX_NO ] );
   struct TermiBUS_sio *sio = &TermiBUS_com;
   int cur_son = SVON;
   
   if ( axst->comm_en )
   {
      if ( axst->last_son != cur_son )
      {
         if ( cur_son )
            axst->comm_ok = ( axst->comm_ok && set_son( AX_NO ) );
         else
            axst->comm_ok = ( axst->comm_ok && set_soff( AX_NO ) );         
         
         ++sio->exe_cnt;
      }
      else if ( !check_stwrf ( AX_NO ) )
      {
         axst->comm_ok = ( axst->comm_ok && check_status( AX_NO ) );
         ++sio->exe_cnt;
      }
      axst->last_son = cur_son;
      
      if ( axst->comm_ok = ( axst->comm_ok && get_status( AX_NO, stat ) ) )
         pwr_on = ( ( stat[ TMBS_STAT_ID ] & TMBS_PON_BIT ) == TMBS_PON_BIT );
      else
         pwr_on = 0;
         
      SVAC = ( check_run( AX_NO ) 
            && pwr_on
            && check_son( AX_NO ) );
   }
   else
      SVAC = 0;
}

UFP ISADLL uspdef_son_soff (char *name)
{
   sys_strcpy (name, "SON_SOFF");
   return (USP_son_soff);
}
