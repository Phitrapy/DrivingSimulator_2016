/*
  user procedure
  name: get_sval
*/

#include "tbussio.h"
#include <tasy0def.h>
#include <grus0006.h>
#include "tbustyp.h"
#include "tbusvar.h"

void ISADLL USP_get_sval (str_arg *arg)
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
      SV_ALRM = check_alrm ( AX_NO );
   }
   else
      SV_ALRM = 0;
}

UFP ISADLL uspdef_get_sval (char *name)
{
   sys_strcpy (name, "GET_SVAL");
   return (USP_get_sval);
}
