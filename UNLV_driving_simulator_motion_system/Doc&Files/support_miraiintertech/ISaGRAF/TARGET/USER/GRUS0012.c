/* 
  user procedure
  name: chg_curp
*/

#include "tbussio.h"
#include <tasy0def.h>
#include <grus0012.h>
#include "tbustyp.h"
#include "tbusvar.h"

void ISADLL USP_chg_curp (str_arg *arg)
{
   struct TermiBUS_var *axst = &( TermiBUS[ AX_NO ] );
   struct TermiBUS_sio *sio = &TermiBUS_com;
   int cur_comm;
   
   if ( axst->comm_en )
   {
      cur_comm = write_position ( AX_NO, POSITION );
      axst->comm_ok = ( axst->comm_ok && cur_comm );
      ++sio->exe_cnt;
      DONE = cur_comm;
   }
   else
      DONE = 0;
}

UFP ISADLL uspdef_chg_curp (char *name)
{
   sys_strcpy (name, "CHG_CURP");
   return (USP_chg_curp);
}
