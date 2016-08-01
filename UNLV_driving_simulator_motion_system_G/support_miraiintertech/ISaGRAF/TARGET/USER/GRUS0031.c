/*
  user procedure
  name: get_tber
*/

#include "tbussio.h"
#include <tasy0def.h>
#include <grus0031.h>
#include "tbustyp.h"
#include "tbusvar.h"

void USP_get_tber (str_arg *arg)
{
   if ( GET )
      TBER = get_sio_error();
   else
      TBER = 0;
}

UFP uspdef_get_tber (char *name)
{
   sys_strcpy (name, "GET_TBER");
   return (USP_get_tber);
}
