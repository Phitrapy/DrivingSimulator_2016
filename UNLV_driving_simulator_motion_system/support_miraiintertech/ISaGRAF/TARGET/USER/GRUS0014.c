/*
  user procedure
  name: srch_org
*/

#include "tbussio.h"
#include <tasy0def.h>
#include <grus0014.h>
#include "tbustyp.h"
#include "tbusvar.h"

void ISADLL USP_srch_org (str_arg *arg)
{
   struct TermiBUS_var *axst = &( TermiBUS[ AX_NO ] );
   struct TermiBUS_sio *sio = &TermiBUS_com;
   int cur_comm, sio_err;
   
   if ( axst->comm_en )
   {
      cur_comm = move_org ( AX_NO, MODE );
      axst->comm_ok = ( axst->comm_ok && cur_comm );
      
      if ( cur_comm )
      {
         axst->move = 1;
         axst->pfin = 0;
         axst->zfin = 0;
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

UFP ISADLL uspdef_srch_org (char *name)
{
   sys_strcpy (name, "SRCH_ORG");
   return (USP_srch_org);
}
