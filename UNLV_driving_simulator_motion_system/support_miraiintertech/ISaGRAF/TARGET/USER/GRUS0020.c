/*
  user procedure
  name: chg_gain
*/

#include "tbussio.h"
#include <tasy0def.h>
#include <grus0020.h>
#include "tbustyp.h"
#include "tbusvar.h"

void ISADLL USP_chg_gain (str_arg *arg)
{
   struct TermiBUS_var *axst = &( TermiBUS[ AX_NO ] );
   struct TermiBUS_sio *sio = &TermiBUS_com;
   int cur_comm;
   
   if ( axst->comm_en )
   {
      cur_comm = select_svparm ( AX_NO, SELECT, GAIN );
      axst->comm_ok = ( axst->comm_ok && cur_comm );
      ++sio->exe_cnt;
      DONE = cur_comm;
   }
   else
      DONE = 0;
}

UFP ISADLL uspdef_chg_gain (char *name)
{
   sys_strcpy (name, "CHG_GAIN");
   return (USP_chg_gain);
}
