/*
  user procedure
  name: tbus_cnt
*/

#include "tbussio.h"
#include <tasy0def.h>
#include <grus0033.h>
#include "tbustyp.h"
#include "tbusvar.h"

void ISADLL USP_tbus_cnt (str_arg *arg)
{
   struct TermiBUS_sio *sio = &TermiBUS_com;
   
   CNT = sio->exe_cnt;
   
   if ( CLR )
      sio->exe_cnt = 0;
}

UFP ISADLL uspdef_tbus_cnt (char *name)
{
   sys_strcpy (name, "TBUS_CNT");
   return (USP_tbus_cnt);
}
