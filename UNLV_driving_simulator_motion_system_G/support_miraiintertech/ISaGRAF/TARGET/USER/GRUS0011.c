/*
  user procedure
  name: move_tch
*/

#include "tbussio.h"
#include <tasy0def.h>
#include <grus0011.h>
#include "tbustyp.h"
#include "tbusvar.h"

void ISADLL USP_move_tch (str_arg *arg)
{
   struct TermiBUS_var *axst = &( TermiBUS[ AX_NO ] );
   struct TermiBUS_sio *sio = &TermiBUS_com;
   int cur_comm;
   
   if ( axst->comm_en )
   {
      cur_comm = move_point ( AX_NO, POINT_NO );
      axst->comm_ok = ( axst->comm_ok && cur_comm );
      
      if ( cur_comm )
      {
         axst->move = 1;
         axst->pfin = 0;
      }
      ++sio->exe_cnt;
      DONE = cur_comm;
   }
   else
      DONE = 0;
}

UFP ISADLL uspdef_move_tch (char *name)
{
   sys_strcpy (name, "MOVE_TCH");
   return (USP_move_tch);
}
