/*
  user procedure
  name: mon_spd
*/

#include "tbussio.h"
#include <tasy0def.h>
#include <grus0030.h>
#include "tbustyp.h"
#include "tbusvar.h"

#define TBSADR_VNOW 0x00007401


void ISADLL USP_mon_spd (str_arg *arg)
{
   struct TermiBUS_var *axst = &( TermiBUS[ AX_NO ] );
   struct TermiBUS_sio *sio = &TermiBUS_com;
   int cur_comm, spd_buff, sio_err;
   
   if ( axst->comm_en )
   {
      cur_comm = read_svmem ( AX_NO, TBSADR_VNOW, &spd_buff );
      axst->comm_ok = ( axst->comm_ok && cur_comm );
      
      if ( cur_comm )
         axst->cur_spd = ( short )spd_buff;
      else
      {
         sio_err = get_sio_error();
         axst->comm_ok = cur_comm;
	  }
      ++sio->exe_cnt;
   }
   SPD = ( int )axst->cur_spd;
}

UFP ISADLL uspdef_mon_spd (char *name)
{
   sys_strcpy (name, "MON_SPD");
   return (USP_mon_spd);
}
