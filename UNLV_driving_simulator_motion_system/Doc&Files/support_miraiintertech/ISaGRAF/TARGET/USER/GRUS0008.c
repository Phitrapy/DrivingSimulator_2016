/*
  user procedure
  name: get_zone
*/

#include "tbussio.h"
#include <tasy0def.h>
#include <grus0008.h>
#include "tbustyp.h"
#include "tbusvar.h"

void ISADLL USP_get_zone (str_arg *arg)
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
         IN_ZONE = ( ( stat[ TMBS_PO_ID ] & TMBS_ZONE_BIT ) == TMBS_ZONE_BIT );
      else
         IN_ZONE = 0;
   }
   else
      IN_ZONE = 0;
}

UFP ISADLL uspdef_get_zone (char *name)
{
   sys_strcpy (name, "GET_ZONE");
   return (USP_get_zone);
}
