/*
  user procedure
  name: mon_pos
*/

#include "tbussio.h"
#include <tasy0def.h>
#include <grus0032.h>
#include "tbustyp.h"
#include "tbusvar.h"

#define TBSADR_PNOW 0x00007400


void ISADLL USP_mon_pos (str_arg *arg)
{
   struct TermiBUS_var *axst = &( TermiBUS[ AX_NO ] );
   struct TermiBUS_sio *sio = &TermiBUS_com;
   int cur_comm, pos_buff, sio_err;
   
   if ( axst->comm_en )
   {
      cur_comm = read_svmem ( AX_NO, TBSADR_PNOW, &pos_buff );
      axst->comm_ok = ( axst->comm_ok && cur_comm );
      
      if ( cur_comm )
         axst->cur_pos = pos_buff;
      else
      {
         sio_err = get_sio_error();
         axst->comm_ok = cur_comm;
	  }
      ++sio->exe_cnt;
   }
   POS = axst->cur_pos;
}

UFP ISADLL uspdef_mon_pos (char *name)
{
   sys_strcpy (name, "MON_POS");
   return (USP_mon_pos);
}
