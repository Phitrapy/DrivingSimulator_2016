/*
  user procedure
  name: rotate_r
*/

#include "tbussio.h"
#include <tasy0def.h>
#include <grus0016.h>
#include "tbustyp.h"
#include "tbusvar.h"

void ISADLL USP_rotate_r (str_arg *arg)
{
   struct TermiBUS_var *axst = &( TermiBUS[ AX_NO ] );
   struct TermiBUS_sio *sio = &TermiBUS_com;
   int cur_comm; 
   
   if ( axst->comm_en )
   {
      cur_comm = move_rotate ( AX_NO, 0, VELOCITY, ACCEL ); 
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

UFP ISADLL uspdef_rotate_r (char *name)
{
   sys_strcpy (name, "ROTATE_R");
   return (USP_rotate_r);
}
