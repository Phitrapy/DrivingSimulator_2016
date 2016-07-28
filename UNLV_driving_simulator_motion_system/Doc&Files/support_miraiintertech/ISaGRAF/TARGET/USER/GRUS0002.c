/*
  user procedure
  name: get_pfin
*/

#include "tbussio.h"
#include <tasy0def.h>
#include <grus0002.h>
#include "tbustyp.h"
#include "tbusvar.h"

void ISADLL USP_get_pfin (str_arg *arg)
{
   struct TermiBUS_var *axst = &( TermiBUS[ AX_NO ] );
   struct TermiBUS_sio *sio = &TermiBUS_com;
   int cur_comm, sio_err;
   
   if ( ( !axst->comm_en ) | ( axst->move ) )
   {
      axst->move = 0;
      PFIN = 0;
   }
   else
   {
      if ( !check_stwrf ( AX_NO ) )
      {
         cur_comm = check_status( AX_NO );
         axst->comm_ok = ( axst->comm_ok && cur_comm );
         
         if ( !cur_comm )
         {
            sio_err = get_sio_error();
            axst->comm_ok = cur_comm;
	     }
         ++sio->exe_cnt;
      }
      PFIN = axst->pfin = check_pfin ( AX_NO );
   }
}

UFP ISADLL uspdef_get_pfin (char *name)
{
   sys_strcpy (name, "GET_PFIN");
   return (USP_get_pfin);
}
