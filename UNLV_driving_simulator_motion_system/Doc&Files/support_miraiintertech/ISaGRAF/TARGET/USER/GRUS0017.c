/*
  user procedure
  name: exe_jog
*/

#include "tbussio.h"
#include <tasy0def.h>
#include <grus0017.h>
#include "tbustyp.h"
#include "tbusvar.h"

void ISADLL USP_exe_jog (str_arg *arg)
{
   struct TermiBUS_var *axst = &( TermiBUS[ AX_NO ] );
   struct TermiBUS_sio *sio = &TermiBUS_com;
   int cur_comm;
   
   if ( axst->comm_en )
   {
      if ( EN == 1 )
      {
         if ( DIR == 0 )    /* Forward */
            cur_comm = move_jog( AX_NO, ( VELOCITY * EXEC_TIME ) / 1000 );
         else                /* Reverse */
            cur_comm = move_jog( AX_NO, -( ( VELOCITY * EXEC_TIME ) / 1000 ) );
         
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
         DONE = 1;  /* if no execute command, return TRUE */
   }
   else
      DONE = 0;
}

UFP ISADLL uspdef_exe_jog (char *name)
{
   sys_strcpy (name, "EXE_JOG");
   return (USP_exe_jog);
}
