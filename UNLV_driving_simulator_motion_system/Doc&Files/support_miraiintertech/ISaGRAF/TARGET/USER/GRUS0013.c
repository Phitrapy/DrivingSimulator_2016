/*
  user procedure
  name: flw_trgp
*/

#include "tbussio.h"
#include <tasy0def.h>
#include <grus0013.h>
#include "tbustyp.h"
#include "tbusvar.h"

void ISADLL USP_flw_trgp (str_arg *arg)
{
   struct TermiBUS_var *axst = &( TermiBUS[ AX_NO ] );
   struct TermiBUS_sio *sio = &TermiBUS_com;
   int cur_comm;
   
   if ( axst->comm_en )
   {
      cur_comm = follow_position ( AX_NO );
      axst->comm_ok = ( axst->comm_ok && cur_comm );
      ++sio->exe_cnt;
      DONE = cur_comm;
   }
   else
      DONE = 0;
}

UFP ISADLL uspdef_flw_trgp (char *name)
{
   sys_strcpy (name, "FLW_TRGP");
   return (USP_flw_trgp);
}
