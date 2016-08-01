/*
  user procedure
  name: tini_cfg
*/

#include "tbussio.h"
#include <tasy0def.h>
#include <grus0035.h>
#include "tbustyp.h"
#include "tbusvar.h"

//-----------------------------------
//       tini_cfg   Function
//-----------------------------------
void ISADLL USP_tini_cfg (str_arg *arg)
{
   struct TermiBUS_sio *sio = &TermiBUS_com;
   int i;
   
   if ( TBUS_EN )
   {
      if ( sio->reset )
      {
         if ( init_tmbs_config(
                             sio->port,  // COM port name
                             0x14,       // 115200bps
                             sio->retry, // maximum retry
                             0,          // not reset
                             1,          // baud automatic
                             sio->axes   // axes descripter
                               ) )
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
      sio->port[0] = 'C';
      sio->port[1] = 'O';
      sio->port[2] = 'M';
      sio->port[4] = '\0';

      switch ( COM_PNO )
      {
	     case ( 2 ):
            sio->port[3] = '2';
		    break;
         case ( 3 ):
            sio->port[3] = '3';
		    break;
         case ( 4 ):
            sio->port[3] = '4';
		    break;
         default:
            sio->port[3] = '1';
		    break;
      }
      
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
         sio->axes[ i ] = -1;
      }
      sio->reset = 1;
      sio->exe_cnt = 0;
      sio->retry = RETRY;
      
      if ( EN0 ) sio->axes[0]  = 0;
      if ( EN1 ) sio->axes[1]  = 0;
      if ( EN2 ) sio->axes[2]  = 0;
      if ( EN3 ) sio->axes[3]  = 0;
      if ( EN4 ) sio->axes[4]  = 0;
      if ( EN5 ) sio->axes[5]  = 0;
      if ( EN6 ) sio->axes[6]  = 0;
      if ( EN7 ) sio->axes[7]  = 0;
      if ( EN8 ) sio->axes[8]  = 0;
      if ( EN9 ) sio->axes[9]  = 0;
      if ( ENA ) sio->axes[10] = 0;
      if ( ENB ) sio->axes[11] = 0;
      if ( ENC ) sio->axes[12] = 0;
      if ( END ) sio->axes[13] = 0;
      if ( ENE ) sio->axes[14] = 0;
      if ( ENF ) sio->axes[15] = 0;
      
      INIT_OK = 0;
   }
}

UFP ISADLL uspdef_tini_cfg (char *name)
{
   sys_strcpy (name, "TINI_CFG");
   return (USP_tini_cfg);
}

