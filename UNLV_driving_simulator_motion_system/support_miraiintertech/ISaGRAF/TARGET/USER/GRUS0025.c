/*
  user procedure
  name: set_svon
*/

#include "tbussio.h"
#include <tasy0def.h>
#include <grus0025.h>
#include "tbustyp.h"
#include "tbusvar.h"

void ISADLL USP_set_svon (str_arg *arg)
{
   struct TermiBUS_var *axst = &( TermiBUS[ AX_NO ] );
   struct TermiBUS_sio *sio = &TermiBUS_com;
   int cur_comm;
   
   if ( axst->comm_en )
   {
      cur_comm = set_son ( AX_NO );
      axst->comm_ok = ( axst->comm_ok && cur_comm );
      ++sio->exe_cnt;
      DONE = cur_comm;
   }
   else
      DONE = 0;
}

UFP ISADLL uspdef_set_svon (char *name)
{
   sys_strcpy (name, "SET_SVON");
   return (USP_set_svon);
}
