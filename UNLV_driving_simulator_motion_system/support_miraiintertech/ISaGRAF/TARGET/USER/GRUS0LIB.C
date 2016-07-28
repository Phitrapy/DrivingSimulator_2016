/*************************************************************************
* File        : grus0lib.c
* Description : User function Samples library  
* Creation    : CJI 11-dec-96
**************************************************************************
* Modif       : 
**************************************************************************/
#include <tasy0def.h>

UFP ISADLL uspdef_rst_sval (char *name) ;
UFP ISADLL uspdef_get_pfin (char *name) ;
UFP ISADLL uspdef_get_zfin (char *name) ;
UFP ISADLL uspdef_get_svrn (char *name) ;
UFP ISADLL uspdef_get_svon (char *name) ;
UFP ISADLL uspdef_get_sval (char *name) ;
UFP ISADLL uspdef_get_pwon (char *name) ;
UFP ISADLL uspdef_get_zone (char *name) ;
UFP ISADLL uspdef_move_abs (char *name) ;
UFP ISADLL uspdef_move_inc (char *name) ;
UFP ISADLL uspdef_move_tch (char *name) ;
UFP ISADLL uspdef_chg_curp (char *name) ;
UFP ISADLL uspdef_flw_trgp (char *name) ;
UFP ISADLL uspdef_srch_org (char *name) ;
UFP ISADLL uspdef_rotate_f (char *name) ;
UFP ISADLL uspdef_rotate_r (char *name) ;
UFP ISADLL uspdef_exe_jog (char *name) ;
UFP ISADLL uspdef_chg_zonf (char *name) ;
UFP ISADLL uspdef_chg_zonr (char *name) ;
UFP ISADLL uspdef_chg_gain (char *name) ;
UFP ISADLL uspdef_chg_ipos (char *name) ;
UFP ISADLL uspdef_chg_tlim (char *name) ;
UFP ISADLL uspdef_chg_prfl (char *name) ;
UFP ISADLL uspdef_get_svac (char *name) ;
UFP ISADLL uspdef_set_svon (char *name) ;
UFP ISADLL uspdef_rst_svon (char *name) ;
UFP ISADLL uspdef_son_soff (char *name) ;
UFP ISADLL uspdef_all_pfin (char *name) ;
UFP ISADLL uspdef_all_zfin (char *name) ;
UFP ISADLL uspdef_mon_spd (char *name) ;
UFP ISADLL uspdef_get_tber (char *name) ;
UFP ISADLL uspdef_mon_pos (char *name) ;
UFP ISADLL uspdef_tbus_cnt (char *name) ;
UFP ISADLL uspdef_tbus_ini (char *name) ;
UFP ISADLL uspdef_tini_cfg (char *name) ;

ISADLL UFP_LIST USPDEF[] = 
   {
   uspdef_rst_sval,
   uspdef_get_pfin,
   uspdef_get_zfin,
   uspdef_get_svrn,
   uspdef_get_svon,
   uspdef_get_sval,
   uspdef_get_pwon,
   uspdef_get_zone,
   uspdef_move_abs,
   uspdef_move_inc,
   uspdef_move_tch,
   uspdef_chg_curp,
   uspdef_flw_trgp,
   uspdef_srch_org,
   uspdef_rotate_f,
   uspdef_rotate_r,
   uspdef_exe_jog,
   uspdef_chg_zonf,
   uspdef_chg_zonr,
   uspdef_chg_gain,
   uspdef_chg_ipos,
   uspdef_chg_tlim,
   uspdef_chg_prfl,
   uspdef_get_svac,
   uspdef_set_svon,
   uspdef_rst_svon,
   uspdef_son_soff,
   uspdef_all_pfin,
   uspdef_all_zfin,
   uspdef_mon_spd,
   uspdef_get_tber,
   uspdef_mon_pos,
   uspdef_tbus_cnt,
   uspdef_tbus_ini,
   uspdef_tini_cfg,
   NULL 
   } ;


/* eof */

