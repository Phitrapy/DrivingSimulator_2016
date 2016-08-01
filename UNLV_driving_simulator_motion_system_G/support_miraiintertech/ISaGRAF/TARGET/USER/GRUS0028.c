/*
  user procedure
  name: all_pfin
*/

#include "tbussio.h"
#include <tasy0def.h>
#include <grus0028.h>
#include "tbustyp.h"
#include "tbusvar.h"

//-----------------------------------
//     Local Function Prototype
//-----------------------------------
static int p_fin( int ax_no );

//-----------------------------------
//       all_pfin   Function
//-----------------------------------
void ISADLL USP_all_pfin (str_arg *arg)
{
   PFIN = 0;
   
   if ( EN0 )
   {
      if ( !p_fin ( 0 ) )
         return;
   }
   if ( EN1 )
   {
      if ( !p_fin ( 1 ) )
         return;
   }
   if ( EN2 )
   {
      if ( !p_fin ( 2 ) )
         return;
   }
   if ( EN3 )
   {
      if ( !p_fin ( 3 ) )
         return;
   }
   if ( EN4 )
   {
      if ( !p_fin ( 4 ) )
         return;
   }
   if ( EN5 )
   {
      if ( !p_fin ( 5 ) )
         return;
   }
   if ( EN6 )
   {
      if ( !p_fin ( 6 ) )
         return;
   }
   if ( EN7 )
   {
      if ( !p_fin ( 7 ) )
         return;
   }
   if ( EN8 )
   {
      if ( !p_fin ( 8 ) )
         return;
   }
   if ( EN9 )
   {
      if ( !p_fin ( 9 ) )
         return;
   }
   if ( ENA )
   {
      if ( !p_fin ( 10 ) )
         return;
   }
   if ( ENB )
   {
      if ( !p_fin ( 11 ) )
         return;
   }
   if ( ENC )
   {
      if ( !p_fin ( 12 ) )
         return;
   }
   if ( END )
   {
      if ( !p_fin ( 13 ) )
         return;
   }
   if ( ENE )
   {
      if ( !p_fin ( 14 ) )
         return;
   }
   if ( ENF )
   {
      if ( !p_fin ( 15 ) )
         return;
   }
   PFIN = 1;
}

UFP ISADLL uspdef_all_pfin (char *name)
{
   sys_strcpy (name, "ALL_PFIN");
   return (USP_all_pfin);
}

//-----------------------------------
//         Local Function
//-----------------------------------
static int p_fin ( int ax_no )
{
   struct TermiBUS_var *axst = &( TermiBUS[ ax_no ] );
   struct TermiBUS_sio *sio = &TermiBUS_com;
   int cur_comm, sio_err;
   
   if ( ( !axst->comm_en ) | ( axst->move ) )
   {
      axst->move = 0;
      return( 0 );
   }
   else if ( !axst->pfin )
   {
      if ( !check_stwrf ( ax_no ) )
      {
         cur_comm = check_status( ax_no );
         axst->comm_ok = ( axst->comm_ok && cur_comm );
         
         if ( !cur_comm )
         {
            sio_err = get_sio_error();
            axst->comm_ok = cur_comm;
	     }
         ++sio->exe_cnt;
      }
      return( axst->pfin = check_pfin ( ax_no ) );
   }
   else
      return( 1 );
}

