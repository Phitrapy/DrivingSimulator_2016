/*
  user procedure
  name: chg_prfl
*/

#include "tbussio.h"
#include <tasy0def.h>
#include <grus0023.h>
#include "tbustyp.h"
#include "tbusvar.h"

void ISADLL USP_chg_prfl (str_arg *arg)
{
   struct TermiBUS_var *axst = &( TermiBUS[ AX_NO ] );
   struct TermiBUS_sio *sio = &TermiBUS_com;
   int cur_comm;
   
   if ( axst->comm_en )
   {
      cur_comm = write_velocity ( AX_NO, VELOCITY, ACCEL );
      axst->comm_ok = ( axst->comm_ok && cur_comm );
      ++sio->exe_cnt;
      DONE = cur_comm;
   }
   else
      DONE = 0;
}

UFP ISADLL uspdef_chg_prfl (char *name)
{
   sys_strcpy (name, "CHG_PRFL");
   return (USP_chg_prfl);
}
