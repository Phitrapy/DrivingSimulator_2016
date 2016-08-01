/*
  user procedure
  name: rst_svon
*/

#include "tbussio.h"
#include <tasy0def.h>
#include <grus0026.h>
#include "tbustyp.h"
#include "tbusvar.h"

void ISADLL USP_rst_svon (str_arg *arg)
{
   struct TermiBUS_var *axst = &( TermiBUS[ AX_NO ] );
   struct TermiBUS_sio *sio = &TermiBUS_com;
   int cur_comm;
   
   if ( axst->comm_en )
   {
      cur_comm = set_soff ( AX_NO );
      axst->comm_ok = ( axst->comm_ok && cur_comm );
      ++sio->exe_cnt;
      DONE = cur_comm;
   }
   else
      DONE = 0;
}

UFP ISADLL uspdef_rst_svon (char *name)
{
   sys_strcpy (name, "RST_SVON");
   return (USP_rst_svon);
}
