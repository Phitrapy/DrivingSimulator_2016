/*
  user procedure
  name: get_svrn
*/

#include "tbussio.h"
#include <tasy0def.h>
#include <grus0004.h>
#include "tbustyp.h"
#include "tbusvar.h"

void ISADLL USP_get_svrn (str_arg *arg)
{
   struct TermiBUS_var *axst = &( TermiBUS[ AX_NO ] );
   struct TermiBUS_sio *sio = &TermiBUS_com;
   
   if ( axst->comm_en )
   {
      if ( !check_stwrf ( AX_NO ) )
      {
         axst->comm_ok = ( axst->comm_ok && check_status( AX_NO ) );
         ++sio->exe_cnt;
      }
      SV_RUN = check_run ( AX_NO );
   }
   else
      SV_RUN = 0;
}

UFP ISADLL uspdef_get_svrn (char *name)
{
   sys_strcpy (name, "GET_SVRN");
   return (USP_get_svrn);
}
