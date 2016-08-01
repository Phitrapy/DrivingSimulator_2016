/*
  user procedure
  name: move_inc
*/

#include "tbussio.h"
#include <tasy0def.h>
#include <grus0010.h>
#include "tbustyp.h"
#include "tbusvar.h"

void ISADLL USP_move_inc (str_arg *arg)
{
   struct TermiBUS_var *axst = &( TermiBUS[ AX_NO ] );
   struct TermiBUS_sio *sio = &TermiBUS_com;
   int cur_comm, sio_err;
   
   if ( axst->comm_en )
   {
      cur_comm = move_inc ( AX_NO, DISTANCE );
      axst->comm_ok = ( axst->comm_ok && cur_comm );
      
      if ( cur_comm )
      {
         axst->move = 1;
         axst->pfin = 0;
      }
      else
      {
         sio_err = get_sio_error();
         axst->comm_ok = cur_comm;
	  }
      ++sio->exe_cnt;
      DONE = cur_comm;
   }
   else
      DONE = 0;
}

UFP ISADLL uspdef_move_inc (char *name)
{
   sys_strcpy (name, "MOVE_INC");
   return (USP_move_inc);
}
