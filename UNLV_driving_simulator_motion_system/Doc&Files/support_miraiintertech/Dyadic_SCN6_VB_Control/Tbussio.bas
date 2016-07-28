Attribute VB_Name = "Tbussio"
'//------------------------------------------------------------------------
'//
'//      Termi-BUS SIO Serial Communication Library
'//
'//          DLL.Name: TMBSCOM.DLL
'//
'//      Termi-BUS SIO API Declaration for Visual Basic
'//
'//          FileName: TBUSSIO.BAS
'//
'//      Copyright(C) 1999,2003 Dyadic Systems Co.,Ltd.
'//
'//      Ver.4.10  14th/Apr./2004      By M.Tsurumi
'//
'//------------------------------------------------------------------------
Option Explicit

Type COMPACK
    address(31) As Long
    data(31) As Long
End Type

Public axes_info(16) As Long

Public Declare Function init_sio_tbus Lib "TMBSCOM.DLL" Alias "_init_sio_tbus@16" _
    (ByVal port As String, _
    ByVal baud As Long, _
    ByVal nrt As Long, _
    axes_info As Long) As Long


Public Declare Function init_sio Lib "TMBSCOM.DLL" Alias "_init_sio@0" () As Long

Public Declare Function get_axes Lib "TMBSCOM.DLL" Alias "_get_axes@4" _
    (axes As Integer) As Long

Public Declare Function get_sio_error Lib "TMBSCOM.DLL" Alias "_get_sio_error@0" () As Long

Public Declare Function read_svmem Lib "TMBSCOM.DLL" Alias "_read_svmem@12" _
    (ByVal axis As Long, _
    ByVal address As Long, _
    dst As Long) As Long

Public Declare Function write_param Lib "TMBSCOM.DLL" Alias "_write_param@8" _
    (ByVal axis As Long, _
    src As COMPACK) As Long

Public Declare Function read_param Lib "TMBSCOM.DLL" Alias "_read_param@8" _
    (ByVal axis As Long, _
    dst As COMPACK) As Long

Public Declare Function write_point Lib "TMBSCOM.DLL" Alias "_write_point@12" _
    (ByVal axis As Long, _
    ByVal Point As Long, _
    src As COMPACK) As Long

Public Declare Function read_point Lib "TMBSCOM.DLL" Alias "_read_point@12" _
    (ByVal axis As Long, _
    ByVal Point As Long, _
    dst As COMPACK) As Long

Public Declare Function load_param Lib "TMBSCOM.DLL" Alias "_load_param@4" _
    (ByVal axis As Long) As Long

Public Declare Function save_param Lib "TMBSCOM.DLL" Alias "_save_param@4" _
    (ByVal axis As Long) As Long

Public Declare Function save_point Lib "TMBSCOM.DLL" Alias "_save_point@8" _
    (ByVal axis As Long, _
    ByVal Point As Long) As Long

Public Declare Function get_write_count Lib "TMBSCOM.DLL" Alias "_get_write_count@4" _
    (ByVal axis As Long) As Long

Public Declare Function write_fzone Lib "TMBSCOM.DLL" Alias "_write_fzone@8" _
    (ByVal axis As Long, _
    ByVal zone As Long) As Long

Public Declare Function write_rzone Lib "TMBSCOM.DLL" Alias "_write_rzone@8" _
    (ByVal axis As Long, _
    ByVal zone As Long) As Long

Public Declare Function select_svparm Lib "TMBSCOM.DLL" Alias "_select_svparm@12" _
    (ByVal axis As Long, _
    ByVal gain_sel As Long, _
    ByVal sv_parm As Long) As Long

Public Declare Function write_inpos Lib "TMBSCOM.DLL" Alias "_write_inpos@8" _
    (ByVal axis As Long, _
    ByVal width As Long) As Long

Public Declare Function write_trqlim Lib "TMBSCOM.DLL" Alias "_write_trqlim@12" _
    (ByVal axis As Long, _
    ByVal l_dynamic As Long, _
    ByVal l_static As Long) As Long

Public Declare Function write_velocity Lib "TMBSCOM.DLL" Alias "_write_velocity@12" _
    (ByVal axis As Long, _
    ByVal vcmd As Long, _
    ByVal acmd As Long) As Long

Public Declare Function move_point Lib "TMBSCOM.DLL" Alias "_move_point@8" _
    (ByVal axis As Long, _
    ByVal Point As Long) As Long

Public Declare Function hmove_point Lib "TMBSCOM.DLL" Alias "_hmove_point@8" _
    (ByVal axis As Long, _
    ByVal Point As Long) As Long

Public Declare Function move_abs Lib "TMBSCOM.DLL" Alias "_move_abs@8" _
    (ByVal axis As Long, _
    ByVal position As Long) As Long

Public Declare Function hmove_abs Lib "TMBSCOM.DLL" Alias "_hmove_abs@8" _
    (ByVal axis As Long, _
    ByVal position As Long) As Long

Public Declare Function follow_position Lib "TMBSCOM.DLL" Alias "_follow_position@4" _
    (ByVal axis As Long) As Long

Public Declare Function move_jog Lib "TMBSCOM.DLL" Alias "_move_jog@8" _
    (ByVal axis As Long, _
    ByVal distance As Long) As Long

Public Declare Function hmove_jog Lib "TMBSCOM.DLL" Alias "_hmove_jog@8" _
    (ByVal axis As Long, _
    ByVal distance As Long) As Long

Public Declare Function move_inc Lib "TMBSCOM.DLL" Alias "_move_inc@8" _
    (ByVal axis As Long, _
    ByVal distance As Long) As Long

Public Declare Function hmove_inc Lib "TMBSCOM.DLL" Alias "_hmove_inc@8" _
    (ByVal axis As Long, _
    ByVal distance As Long) As Long

Public Declare Function move_org Lib "TMBSCOM.DLL" Alias "_move_org@8" _
    (ByVal axis As Long, _
    ByVal mode As Long) As Long

Public Declare Function hmove_org Lib "TMBSCOM.DLL" Alias "_hmove_org@8" _
    (ByVal axis As Long, _
    ByVal mode As Long) As Long

Public Declare Function move_rotate Lib "TMBSCOM.DLL" Alias "_move_rotate@16" _
    (ByVal axis As Long, _
    ByVal dir As Long, _
    ByVal vcmd As Long, _
    ByVal acmd As Long) As Long

Public Declare Function start_preload_command Lib "TMBSCOM.DLL" Alias "_start_preload_command@4" _
    (ByVal axis As Long) As Long

Public Declare Function clear_preload_command Lib "TMBSCOM.DLL" Alias "_clear_preload_command@4" _
    (ByVal axis As Long) As Long

Public Declare Function write_position Lib "TMBSCOM.DLL" Alias "_write_position@8" _
    (ByVal axis As Long, _
    ByVal position As Long) As Long

Public Declare Function set_son Lib "TMBSCOM.DLL" Alias "_set_son@4" _
    (ByVal axis As Long) As Long

Public Declare Function set_soff Lib "TMBSCOM.DLL" Alias "_set_soff@4" _
    (ByVal axis As Long) As Long

Public Declare Function reset_alarm Lib "TMBSCOM.DLL" Alias "_reset_alarm@4" _
    (ByVal axis As Long) As Long

Public Declare Function reset_memory Lib "TMBSCOM.DLL" Alias "_reset_memory@4" _
    (ByVal axis As Long) As Long

Public Declare Function config_pio Lib "TMBSCOM.DLL" Alias "_config_pio@8" _
    (ByVal axis As Long, _
    ByVal mode As Long) As Long

Public Declare Function get_status Lib "TMBSCOM.DLL" Alias "_get_status@8" _
    (ByVal axis As Long, _
    param As Byte) As Long

Public Declare Function check_run Lib "TMBSCOM.DLL" Alias "_check_run@4" _
    (ByVal axis As Long) As Long

Public Declare Function check_son Lib "TMBSCOM.DLL" Alias "_check_son@4" _
    (ByVal axis As Long) As Long

Public Declare Function check_org Lib "TMBSCOM.DLL" Alias "_check_org@4" _
    (ByVal axis As Long) As Long

Public Declare Function check_pfin Lib "TMBSCOM.DLL" Alias "_check_pfin@4" _
    (ByVal axis As Long) As Long

Public Declare Function check_alarm Lib "TMBSCOM.DLL" Alias "_check_alrm@4" _
    (ByVal axis As Long) As Long

Public Declare Function check_status Lib "TMBSCOM.DLL" Alias "_check_status@4" _
    (ByVal axis As Long) As Long

Public Declare Function reset_stwrf Lib "TMBSCOM.DLL" Alias "_reset_stwrf@4" _
    (ByVal axis As Long) As Long

Public Declare Function check_stwrf Lib "TMBSCOM.DLL" Alias "_check_stwrf@4" _
    (ByVal axis As Long) As Long

Public Declare Function init_tmbs_config Lib "TMBSCOM.DLL" Alias "_init_tmbs_config@24" _
    (ByVal port As String, _
    ByVal baud As Long, _
    ByVal nrt As Long, _
    ByVal reset As Long, _
    ByVal automatic As Long, _
    axes_info As Long) As Long

Public Declare Function init_tmbs Lib "TMBSCOM.DLL" Alias "_init_tmbs@0" () As Long

Public Declare Function close_tmbs Lib "TMBSCOM.DLL" Alias "_close_tmbs@0" () As Long

Public Declare Function reopen_tmbs Lib "TMBSCOM.DLL" Alias "_reopen_tmbs@0" () As Long

Public Declare Function get_tmbs_state Lib "TMBSCOM.DLL" Alias "_get_tmbs_state@0" () As Long

Public Declare Function get_current_baud Lib "TMBSCOM.DLL" Alias "_get_current_baud@0" () As Long

Public Declare Function get_com_errlog Lib "TMBSCOM.DLL" Alias "_get_com_errlog@0" () As Long

Public Declare Function write_svmem Lib "TMBSCOM.DLL" Alias "_write_svmem@12" _
    (ByVal axis As Long, _
    ByVal address As Long, _
    src As Long) As Long

Public Declare Function memcmd_form0 Lib "TMBSCOM.DLL" Alias "_memcmd_form0@20" _
    (ByVal func_id As String, _
    ByVal axis As Long, _
    ByVal com_data As Long, _
    res_data As Long, _
    warning As Long) As Long

Public Declare Function memcmd_form1 Lib "TMBSCOM.DLL" Alias "_memcmd_form1@24" _
    (ByVal func_id As String, _
    ByVal axis As Long, _
    ByVal bank As Long, _
    ByVal Point As Long, _
    Res As Byte, _
    warning As Long) As Long

Public Declare Function memcmd_form2 Lib "TMBSCOM.DLL" Alias "_memcmd_form2@20" _
    (ByVal func_id As String, _
    ByVal axis As Long, _
    ByVal bank As Long, _
    Res As Byte, _
    warning As Long) As Long

Public Declare Function memcmd_form3 Lib "TMBSCOM.DLL" Alias "_memcmd_form3@24" _
    (ByVal func_id As String, _
    ByVal axis As Long, _
    ByVal bank As Long, _
    ByVal Point As Long, _
    res_data As Long, _
    warning As Long) As Long

Public Declare Function dircmd_form0 Lib "TMBSCOM.DLL" Alias "_dircmd_form0@20" _
    (ByVal func_id As Byte, _
    ByVal axis As Long, _
    ByVal data As Long, _
    Res As Byte, _
    warning As Long) As Long

Public Declare Function dircmd_form1 Lib "TMBSCOM.DLL" Alias "_dircmd_form1@16" _
    (ByVal func_id As Byte, _
    ByVal axis As Long, _
    Res As Byte, _
    warning As Long) As Long

Public Declare Function dircmd_form2 Lib "TMBSCOM.DLL" Alias "_dircmd_form2@24" _
    (ByVal func_id As Byte, _
    ByVal axis As Long, _
    ByVal data0 As Long, _
    ByVal data1 As Long, _
    Res As Byte, _
    warning As Long) As Long

Public Declare Function dircmd_form3 Lib "TMBSCOM.DLL" Alias "_dircmd_form3@24" _
    (ByVal func_id As Byte, _
    ByVal axis As Long, _
    ByVal data0 As Long, _
    ByVal data1 As Long, _
    Res As Byte, _
    warning As Long) As Long

Public Declare Function dircmd_form4 Lib "TMBSCOM.DLL" Alias "_dircmd_form4@20" _
    (ByVal func_id As Byte, _
    ByVal axis As Long, _
    ByVal data As Long, _
    Res As Byte, _
    warning As Long) As Long

Public Declare Function dircmd_form5 Lib "TMBSCOM.DLL" Alias "_dircmd_form5@20" _
    (ByVal func_id As Byte, _
    ByVal axis As Long, _
    ByVal data As Long, _
    Res As Byte, _
    warning As Long) As Long

Public Declare Function dircmd_form6 Lib "TMBSCOM.DLL" Alias "_dircmd_form6@20" _
    (ByVal func_id As Byte, _
    ByVal axis As Long, _
    ByVal data As Long, _
    Res As Byte, _
    warning As Long) As Long

Public Declare Function dircmd_form7 Lib "TMBSCOM.DLL" Alias "_dircmd_form7@28" _
    (ByVal func_id As Byte, _
    ByVal axis As Long, _
    ByVal data0 As Long, _
    ByVal data1 As Long, _
    ByVal data2 As Long, _
    Res As Byte, _
    warning As Long) As Long

Public Declare Function dircmd_form8 Lib "TMBSCOM.DLL" Alias "_dircmd_form8@24" _
    (ByVal func_id As Byte, _
    ByVal axis As Long, _
    ByVal data0 As Long, _
    ByVal data1 As Long, _
    Res As Byte, _
    warning As Long) As Long

Public Declare Function cmd_unform Lib "TMBSCOM.DLL" Alias "_cmd_unform@20" _
    (ByVal axis As Long, _
    ByVal mode As Long, _
    ByVal cmd_str As String, _
    res_data As Long, _
    warning As Long) As Long

Public Declare Function get_trx_buf Lib "TMBSCOM.DLL" Alias "_get_trx_buf@8" _
    (ByVal rx_ptr As Long, _
    ByVal tx_ptr As Long) As Long

Public Const SIO_DONE As Long = 1
Public Const SIO_ERROR As Long = 0
Public Const SIO_COMUSED As Long = -1
Public Const SIO_TIMEOUT As Long = -2
Public Const SIO_NOINIT As Long = -3
Public Const SIO_INVALID_AXIS As Long = -4
Public Const SIO_INVALID_PARAM As Long = -5
Public Const SIO_NOTSUPORT_TO As Long = -6
Public Const SIO_NOTSUPORT_DTR As Long = -7
Public Const SIO_NOTSUPORT_BAUD As Long = -8
Public Const SIO_NOTSUPORT_PARA As Long = -9
Public Const SIO_NO_CONFIGFILE As Long = -10
Public Const SIO_RESOURCE As Long = -11
Public Const SIO_COMFAILED As Long = -12

'//
Public Const TMBS_PO_ID As Long = 0
Public Const TMBS_PI_ID As Long = 1
Public Const TMBS_ALARM_ID As Long = 2
Public Const TMBS_STAT_ID As Long = 3

'// PO
Public Const TMBS_PM_CODE As Long = &HF&
Public Const TMBS_PM1_BIT As Long = &H1&
Public Const TMBS_PM2_BIT As Long = &H2&
Public Const TMBS_PM4_BIT As Long = &H4&
Public Const TMBS_PM8_BIT As Long = &H8&
Public Const TMBS_PFIN_BIT As Long = &H10&
Public Const TMBS_ZFIN_BIT As Long = &H20&
Public Const TMBS_ZONE_BIT As Long = &H40&
Public Const TMBS_ALM_BIT As Long = &H80&

'// PI
Public Const TMBS_PC_CODE As Long = &HF&
Public Const TMBS_PC1_BIT As Long = &H1&
Public Const TMBS_PC2_BIT As Long = &H2&
Public Const TMBS_PC4_BIT As Long = &H4&
Public Const TMBS_PC8_BIT As Long = &H8&
Public Const TMBS_CSTR_BIT As Long = &H10&
Public Const TMBS_INHP_BIT As Long = &H20&
Public Const TMBS_INHN_BIT As Long = &H40&
Public Const TMBS_ILK_BIT As Long = &H80&

'// STAT
Public Const TMBS_SON_BIT As Long = &H1&
Public Const TMBS_RUN_BIT As Long = &H2&
Public Const TMBS_ZFST_BIT As Long = &H4&
Public Const TMBS_HOLD_BIT As Long = &H8&
Public Const TMBS_LIMP_BIT As Long = &H10&
Public Const TMBS_LIMN_BIT As Long = &H20&
Public Const TMBS_ILST_BIT As Long = &H40&
Public Const TMBS_WARN_BIT As Long = &H80&

Public Const TMBS_BAUD_9600 As Long = &H4&      '  9600 bps
Public Const TMBS_BAUD_19200 As Long = &H5&     ' 19200 bps
Public Const TMBS_BAUD_38400 As Long = &H6&     ' 38400 bps
Public Const TMBS_BAUD_14400 As Long = &H11&    ' 14400 bps
Public Const TMBS_BAUD_57600 As Long = &H13&    ' 57600 bps
Public Const TMBS_BAUD_115200 As Long = &H14&   '115200 bps

Public Const TMBS_NO_EXIST As Long = 0
Public Const TMBS_INITIAL As Long = 1
Public Const TMBS_INIT_ERROR As Long = 2
Public Const TMBS_OPENING As Long = 3
Public Const TMBS_RUNNING As Long = 4
