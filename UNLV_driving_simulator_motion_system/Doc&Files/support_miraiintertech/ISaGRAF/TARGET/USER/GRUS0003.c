/*
  user procedure
  name: get_zfin
*/

#include "tbussio.h"
#include <tasy0def.h>
#include <grus0003.h>
#include "tbustyp.h"
#include "tbusvar.h"

void ISADLL USP_get_zfin (str_arg *arg)
{
   struct TermiBUS_var *axst = &( TermiBUS[ AX_NO ] );
   struct TermiBUS_sio *sio = &TermiBUS_com;
   
   if ( ( !axst->comm_en ) | ( axst->move ) )
   {
      axst->move = 0;
      ZFIN = 0;
   }
   else
   {
      if ( !check_stwrf ( AX_NO ) )
      {
         axst->comm_ok = ( axst->comm_ok && check_status( AX_NO ) );
         ++sio->exe_cnt;
      }
      axst->pfin = check_pfin ( AX_NO );
      ZFIN = axst->zfin = check_org ( AX_NO );
   }
}

UFP ISADLL uspdef_get_zfin (char *name)
{
   sys_strcpy (name, "GET_ZFIN");
   return (USP_get_zfin);
}
