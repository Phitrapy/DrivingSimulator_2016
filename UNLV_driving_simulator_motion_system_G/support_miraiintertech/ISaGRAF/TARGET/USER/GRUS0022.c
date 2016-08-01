/*
  user procedure
  name: chg_tlim
*/

#include "tbussio.h"
#include <tasy0def.h>
#include <grus0022.h>
#include "tbustyp.h"
#include "tbusvar.h"

void ISADLL USP_chg_tlim (str_arg *arg)
{
   struct TermiBUS_var *axst = &( TermiBUS[ AX_NO ] );
   struct TermiBUS_sio *sio = &TermiBUS_com;
   int cur_comm;
   
   if ( axst->comm_en )
   {
      cur_comm = write_trqlim ( AX_NO, TLIM_DY, TLIM_ST );
      axst->comm_ok = ( axst->comm_ok && cur_comm );
      ++sio->exe_cnt;
      DONE = cur_comm;
   }
   else
      DONE = 0;
}

UFP ISADLL uspdef_chg_tlim (char *name)
{
   sys_strcpy (name, "CHG_TLIM");
   return (USP_chg_tlim);
}
