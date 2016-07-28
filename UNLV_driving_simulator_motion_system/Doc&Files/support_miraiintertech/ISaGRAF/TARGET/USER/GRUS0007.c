/*
  user procedure
  name: get_pwon
*/

#include "tbussio.h"
#include <tasy0def.h>
#include <grus0007.h>
#include "tbustyp.h"
#include "tbusvar.h"

void ISADLL USP_get_pwon (str_arg *arg)
{
   char stat[4];
   struct TermiBUS_var *axst = &( TermiBUS[ AX_NO ] );
   struct TermiBUS_sio *sio = &TermiBUS_com;
   
   if ( axst->comm_en )
   {
      if ( !check_stwrf ( AX_NO ) )
      {
         axst->comm_ok = ( axst->comm_ok && check_status( AX_NO ) );
         ++sio->exe_cnt;
      }
      if ( axst->comm_ok = ( axst->comm_ok && get_status( AX_NO, stat ) ) )
         SV_PWRON = ( ( stat[TMBS_STAT_ID] & TMBS_PON_BIT ) == TMBS_PON_BIT );
      else
         SV_PWRON = 0;
   }
   else
      SV_PWRON = 0;
}

UFP ISADLL uspdef_get_pwon (char *name)
{
   sys_strcpy (name, "GET_PWON");
   return (USP_get_pwon);
}
