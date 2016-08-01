/*
  user procedure
  name: tbus_ini
*/

#include "tbussio.h"
#include <tasy0def.h>
#include <grus0034.h>
#include "tbustyp.h"
#include "tbusvar.h"

void ISADLL USP_tbus_ini (str_arg *arg)
{
   struct TermiBUS_sio *sio = &TermiBUS_com;
   int i;
   
   if ( TBUS_EN )
   {
      if ( sio->reset )
      {
         if ( init_sio() )
         {
            for ( i = 0 ; i < 16 ; i++ )
            {
               TermiBUS[ i ].comm_en = 1;
               reset_stwrf( i );
            }
            sio->reset = 0;
            INIT_OK = 1;
         }
         else
         {
            sio->reset = 1;
            INIT_OK = 0;
         }
      }
      else
      {
         for ( i = 0 ; i < 16 ; i++ )
         {
            TermiBUS[ i ].comm_ok = 1;
            reset_stwrf( i );
         }
         sio->reset = 0;
         INIT_OK = 1;
      }
   }
   else
   {
      for ( i = 0 ; i < 16 ; i++ )
      {
         TermiBUS[ i ].comm_en = 0;
         TermiBUS[ i ].comm_ok = 1;
         TermiBUS[ i ].move = 0;
         TermiBUS[ i ].pfin = 1;
         TermiBUS[ i ].zfin = 0;
         TermiBUS[ i ].last_son = 0;
         TermiBUS[ i ].cur_spd = 0;
         TermiBUS[ i ].cur_pos = 0;
      }
      sio->reset = 1;
      sio->exe_cnt = 0;
      INIT_OK = 0;
   }
}

UFP ISADLL uspdef_tbus_ini (char *name)
{
   sys_strcpy (name, "TBUS_INI");
   return (USP_tbus_ini);
}
