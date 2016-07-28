/*
  user procedure
  name: rst_sval
*/

#include "tbussio.h"
#include <tasy0def.h>
#include <grus0001.h>
#include "tbustyp.h"
#include "tbusvar.h"

void ISADLL USP_rst_sval (str_arg *arg)
{
   struct TermiBUS_var *axst = &( TermiBUS[ AX_NO ] );
   struct TermiBUS_sio *sio = &TermiBUS_com;
   int cur_comm;
   
   if ( axst->comm_en )
   {
      cur_comm = reset_alarm ( AX_NO );
      axst->comm_ok = ( axst->comm_ok && cur_comm );
      ++sio->exe_cnt;
      DONE = cur_comm;
   }
   else
      DONE = 0;
}

UFP ISADLL uspdef_rst_sval (char *name)
{
   sys_strcpy (name, "RST_SVAL");
   return (USP_rst_sval);
}
