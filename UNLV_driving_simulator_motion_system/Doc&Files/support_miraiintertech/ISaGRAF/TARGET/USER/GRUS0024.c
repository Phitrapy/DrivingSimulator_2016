/*
  user procedure
  name: get_svac
*/

#include "tbussio.h"
#include <tasy0def.h>
#include <grus0024.h>
#include "tbustyp.h"
#include "tbusvar.h"

void ISADLL USP_get_svac (str_arg *arg)
{
   char stat[4];
   int pwr_on;
   struct TermiBUS_var *axst = &( TermiBUS[ AX_NO ] );
   struct TermiBUS_sio *sio = &TermiBUS_com;
   int cur_comm;
   
   if ( axst->comm_en )
   {
      if ( !check_stwrf ( AX_NO ) )
      {
         cur_comm = check_status( AX_NO );
         axst->comm_ok = ( axst->comm_ok && cur_comm );
         ++sio->exe_cnt;
      }
      
      if ( axst->comm_ok = ( axst->comm_ok && get_status( AX_NO, stat ) ) )
         pwr_on = ( ( stat[ TMBS_STAT_ID ] & TMBS_PON_BIT ) == TMBS_PON_BIT );
      else
         pwr_on = 0;
         
      SV_ACTIVE = ( check_run( AX_NO )
                 && pwr_on
                 && check_son( AX_NO ) );
   }
   else
      SV_ACTIVE = 0;
}

UFP ISADLL uspdef_get_svac (char *name)
{
   sys_strcpy (name, "GET_SVAC");
   return (USP_get_svac);
}
