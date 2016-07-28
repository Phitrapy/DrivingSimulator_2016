/*
  user procedure
  name: all_zfin
*/

#include "tbussio.h"
#include <tasy0def.h>
#include <grus0029.h>
#include "tbustyp.h"
#include "tbusvar.h"

//-----------------------------------
//     Local Function Prototype
//-----------------------------------
static int z_fin( int ax_no );

//-----------------------------------
//       all_zfin   Function
//-----------------------------------
void ISADLL USP_all_zfin (str_arg *arg)
{
   ZFIN = 0;
   
   if ( EN0 )
   {
      if ( !z_fin ( 0 ) )
         return;
   }
   if ( EN1 )
   {
      if ( !z_fin ( 1 ) )
         return;
   }
   if ( EN2 )
   {
      if ( !z_fin ( 2 ) )
         return;
   }
   if ( EN3 )
   {
      if ( !z_fin ( 3 ) )
         return;
   }
   if ( EN4 )
   {
      if ( !z_fin ( 4 ) )
         return;
   }
   if ( EN5 )
   {
      if ( !z_fin ( 5 ) )
         return;
   }
   if ( EN6 )
   {
      if ( !z_fin ( 6 ) )
         return;
   }
   if ( EN7 )
   {
      if ( !z_fin ( 7 ) )
         return;
   }
   if ( EN8 )
   {
      if ( !z_fin ( 8 ) )
         return;
   }
   if ( EN9 )
   {
      if ( !z_fin ( 9 ) )
         return;
   }
   if ( ENA )
   {
      if ( !z_fin ( 10 ) )
         return;
   }
   if ( ENB )
   {
      if ( !z_fin ( 11 ) )
         return;
   }
   if ( ENC )
   {
      if ( !z_fin ( 12 ) )
         return;
   }
   if ( END )
   {
      if ( !z_fin ( 13 ) )
         return;
   }
   if ( ENE )
   {
      if ( !z_fin ( 14 ) )
         return;
   }
   if ( ENF )
   {
      if ( !z_fin ( 15 ) )
         return;
   }
   ZFIN = 1;
}

UFP ISADLL uspdef_all_zfin (char *name)
{
   sys_strcpy (name, "ALL_ZFIN");
   return (USP_all_zfin);
}

//-----------------------------------
//         Local Function
//-----------------------------------
static int z_fin ( int ax_no )
{
   struct TermiBUS_var *axst = &( TermiBUS[ ax_no ] );
   struct TermiBUS_sio *sio = &TermiBUS_com;
   
   if ( ( !axst->comm_en ) | ( axst->move ) )
   {
      axst->move = 0;
      return( 0 );
   }
   else if ( !axst->zfin )
   {
      if ( !check_stwrf ( ax_no ) )
      {
         axst->comm_ok = ( axst->comm_ok && check_status( ax_no ) );
         ++sio->exe_cnt;
      }
      axst->pfin = check_pfin ( ax_no );
      return( axst->zfin = check_org ( ax_no ) );
   }
   else
      return( 1 );
}

