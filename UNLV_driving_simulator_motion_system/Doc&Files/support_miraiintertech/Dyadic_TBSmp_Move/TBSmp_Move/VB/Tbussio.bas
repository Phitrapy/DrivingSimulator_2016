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

'//----------------------------------------------------------------
'//        メモリブロック転送コマンドのパラメータデータ型
'//----------------------------------------------------------------
Type COMPACK
    address(31) As Long      'オフセット値の配列( -1は処理をスキップ )
    data(31) As Long         'データの配列
End Type

'//----------------------------------------------------------------
'//                 軸構成指定情報伝達配列の定義
'//----------------------------------------------------------------
Public axes_info(16) As Long

'//--------------------------------------------------------------------
'//  旧バージョン互換アプリケーションプログラムインターフェイス関数
'//--------------------------------------------------------------------

'//----------------------------------------------------------------
'//                         通信起動ＡＰＩ
'//----------------------------------------------------------------

'// Termi-BUS SIO の通信起動関数 ( 通信条件直接指定 )
'TBUSSIOAPI int WINAPI init_sio_tbus(
'    LPCTSTR port,  // 通信ポートファイル名 ( "COM1","COM2"etc. )
'    int baud,      // ボーレイト指定コード ( 4,5,6,11H,13H,14H )
'    int nrt,       // コマンド再送最大回数
'    int *axes_info // 軸構成指定情報
'    );

Public Declare Function init_sio_tbus Lib "TMBSCOM.DLL" Alias "_init_sio_tbus@16" _
    (ByVal port As String, _
    ByVal baud As Long, _
    ByVal nrt As Long, _
    axes_info As Long) As Long

'// Termi-BUS SIO の通信起動関数 ( TBUSSIO.INIファイル指定 )
'TBUSSIOAPI int WINAPI init_sio( void );

Public Declare Function init_sio Lib "TMBSCOM.DLL" Alias "_init_sio@0" () As Long

'//----------------------------------------------------------------
'//                    通信動作状態確認ＡＰＩ
'//----------------------------------------------------------------

'// 現在の軸構成情報を得る関数
'TBUSSIOAPI int WINAPI get_axes(
'    unsigned short *axes    // 軸構成情報格納領域へのポインタ
'    );
Public Declare Function get_axes Lib "TMBSCOM.DLL" Alias "_get_axes@4" _
    (axes As Integer) As Long

'// 現在の通信エラーコードを得る関数
Public Declare Function get_sio_error Lib "TMBSCOM.DLL" Alias "_get_sio_error@0" () As Long

'//----------------------------------------------------------------
'//                   仮想メモリアクセスＡＰＩ
'//----------------------------------------------------------------
'// (R4) 仮想メモリ空間からのランダムアクセス読み出し関数
Public Declare Function read_svmem Lib "TMBSCOM.DLL" Alias "_read_svmem@12" _
    (ByVal axis As Long, _
    ByVal address As Long, _
    dst As Long) As Long

'// EEPROM 共通パラメータ一括書き込み( src -> EEPROM )関数
Public Declare Function write_param Lib "TMBSCOM.DLL" Alias "_write_param@8" _
    (ByVal axis As Long, _
    src As COMPACK) As Long

'// EEPROM 共通パラメータ読み出し( EEPROM -> dst )関数
Public Declare Function read_param Lib "TMBSCOM.DLL" Alias "_read_param@8" _
    (ByVal axis As Long, _
    dst As COMPACK) As Long

'// EEPROM ポイントデータ一括書き込み( src -> EEPROM )関数
Public Declare Function write_point Lib "TMBSCOM.DLL" Alias "_write_point@12" _
    (ByVal axis As Long, _
    ByVal Point As Long, _
    src As COMPACK) As Long

'// EEPROM ポイントデータ読み出し( EEPROM -> dst )関数
Public Declare Function read_point Lib "TMBSCOM.DLL" Alias "_read_point@12" _
    (ByVal axis As Long, _
    ByVal Point As Long, _
    dst As COMPACK) As Long

'// 実行領域共通パラメータロード( EEPROM -> Bank#30 )関数
Public Declare Function load_param Lib "TMBSCOM.DLL" Alias "_load_param@4" _
    (ByVal axis As Long) As Long

'// 実行領域共通パラメータセーブ( Bank#30 -> EEPROM )関数
Public Declare Function save_param Lib "TMBSCOM.DLL" Alias "_save_param@4" _
    (ByVal axis As Long) As Long

'// 実行領域ポイントデータセーブ( Bank#31 -> EEPROM )関数
Public Declare Function save_point Lib "TMBSCOM.DLL" Alias "_save_point@8" _
    (ByVal axis As Long, _
    ByVal Point As Long) As Long

'// 共通パラメータ領域(Bank#0)の書き込み回数読み出し関数
Public Declare Function get_write_count Lib "TMBSCOM.DLL" Alias "_get_write_count@4" _
    (ByVal axis As Long) As Long


'//----------------------------------------------------------------
'//                    軸動作パラメータ変更ＡＰＩ
'//----------------------------------------------------------------

'// (z) 正転側ＺＯＮＥ出力境界値を変更する関数
'   axis：軸番号
'   zone：正転側ゾーン境界値
Public Declare Function write_fzone Lib "TMBSCOM.DLL" Alias "_write_fzone@8" _
    (ByVal axis As Long, _
    ByVal zone As Long) As Long

'// (z) 逆転側ＺＯＮＥ出力境界値を変更する関数
'   axis：軸番号
'   zone：逆転側ゾーン境界値
Public Declare Function write_rzone Lib "TMBSCOM.DLL" Alias "_write_rzone@8" _
    (ByVal axis As Long, _
    ByVal zone As Long) As Long

'// (g) 移動時又は停止時のサーボゲインパラメータを変更する関数
'   axis    ：軸番号
'   gain_sel：停止時移動時の選択 0=移動時, 1=停止時
'   svparm  ：サーボゲインパラメータ
Public Declare Function select_svparm Lib "TMBSCOM.DLL" Alias "_select_svparm@12" _
    (ByVal axis As Long, _
    ByVal gain_sel As Long, _
    ByVal sv_parm As Long) As Long

'// (i) 位置決め完了検出幅を変更する関数
'   axis ：軸番号
'   width：位置決め完了検出幅
Public Declare Function write_inpos Lib "TMBSCOM.DLL" Alias "_write_inpos@8" _
    (ByVal axis As Long, _
    ByVal width As Long) As Long

'// (l) 停止時及び移動時の電流制限値を変更する関数
'   axis     ：軸番号
'   l_dynamic：停止時電流制限値
'   l_static ：移動時電流制限値
Public Declare Function write_trqlim Lib "TMBSCOM.DLL" Alias "_write_trqlim@12" _
    (ByVal axis As Long, _
    ByVal l_dynamic As Long, _
    ByVal l_static As Long) As Long

'// (v) 速度／加速度指令値設定関数
'   axis：軸番号
'   vcmd：速度指令値
'   acmd：加速度指令値
Public Declare Function write_velocity Lib "TMBSCOM.DLL" Alias "_write_velocity@12" _
    (ByVal axis As Long, _
    ByVal vcmd As Long, _
    ByVal acmd As Long) As Long


'//------------------------------------------------------------------
'//                        軸動作指令ＡＰＩ
'//------------------------------------------------------------------

'// (Q3) ポイント番号指定ＰＴＰ動作指令関数
'   axis ：軸番号
'   point：ポイント番号
Public Declare Function move_point Lib "TMBSCOM.DLL" Alias "_move_point@8" _
    (ByVal axis As Long, _
    ByVal Point As Long) As Long

'// (a) 絶対位置指定ＰＴＰ動作指令関数
'TBUSSIOAPI int WINAPI move_abs(
'    int axis,      // 軸番号
'    long position  // 絶対座標系での目標位置
'    );

Public Declare Function move_abs Lib "TMBSCOM.DLL" Alias "_move_abs@8" _
    (ByVal axis As Long, _
    ByVal position As Long) As Long

'// (d) 残移動指令キャンセル（即時停止）指令関数
'TBUSSIOAPI int WINAPI follow_position(
'    int axis      // 軸番号
'    );

Public Declare Function follow_position Lib "TMBSCOM.DLL" Alias "_follow_position@4" _
    (ByVal axis As Long) As Long

'// (j) ＪＯＧ動作指令関数
'TBUSSIOAPI int WINAPI move_jog(
'    int axis,      // 軸番号
'    long distance  // ＪＯＧ指令繰り返し周期当たりの移動量
'    );

Public Declare Function move_jog Lib "TMBSCOM.DLL" Alias "_move_jog@8" _
    (ByVal axis As Long, _
    ByVal distance As Long) As Long

'// (m) 相対移動量位置指定ＰＴＰ動作指令関数
'TBUSSIOAPI int WINAPI move_inc(
'    int axis,      // 軸番号
'    long distance  // 移動量
'    );

Public Declare Function move_inc Lib "TMBSCOM.DLL" Alias "_move_inc@8" _
    (ByVal axis As Long, _
    ByVal distance As Long) As Long

'// (o) 原点復帰動作指令関数
'TBUSSIOAPI int WINAPI move_org(
'    int axis,      // 軸番号
'    int mode       // 原点復帰動作パターン指定コード
'    );

Public Declare Function move_org Lib "TMBSCOM.DLL" Alias "_move_org@8" _
    (ByVal axis As Long, _
    ByVal mode As Long) As Long

'// (v) 無限回転指令関数
'TBUSSIOAPI int WINAPI move_rotate(
'    int axis,      // 軸番号
'    int dir,       // 回転方向 0=正転, 1=逆転
'    int vcmd,      // 速度指令値
'    int acmd       // 加速度指令値
'    );

Public Declare Function move_rotate Lib "TMBSCOM.DLL" Alias "_move_rotate@16" _
    (ByVal axis As Long, _
    ByVal dir As Long, _
    ByVal vcmd As Long, _
    ByVal acmd As Long) As Long


'//----------------------------------------------------------------
'//                 サーボシステム状態変更ＡＰＩ
'//----------------------------------------------------------------

'// (b) 現在位置変更（座標系シフト）指令関数
'TBUSSIOAPI int WINAPI write_position(
'    int axis,      // 軸番号
'    long position  // 絶対座標系での現在位置
'    );

Public Declare Function write_position Lib "TMBSCOM.DLL" Alias "_write_position@8" _
    (ByVal axis As Long, _
    ByVal position As Long) As Long

'// (q) サーボＯＮ指令関数
'TBUSSIOAPI int WINAPI set_son(
'    int axis      // 軸番号
'    );

Public Declare Function set_son Lib "TMBSCOM.DLL" Alias "_set_son@4" _
    (ByVal axis As Long) As Long

'// (q) サーボＯＦＦ指令関数
'TBUSSIOAPI int WINAPI set_soff(
'    int axis      // 軸番号
'    );

Public Declare Function set_soff Lib "TMBSCOM.DLL" Alias "_set_soff@4" _
    (ByVal axis As Long) As Long

'// (r) アラームリセット指令関数
'TBUSSIOAPI int WINAPI reset_alarm(
'    int axis      // 軸番号
'    );

Public Declare Function reset_alarm Lib "TMBSCOM.DLL" Alias "_reset_alarm@4" _
    (ByVal axis As Long) As Long

'// (r) 不揮発性メモリ初期化指令関数
'TBUSSIOAPI int WINAPI reset_memory(
'    int axis      // 軸番号
'    );

Public Declare Function reset_memory Lib "TMBSCOM.DLL" Alias "_reset_memory@4" _
    (ByVal axis As Long) As Long

'// (s) ＰＩＯ機能選択フラグ設定関数
'TBUSSIOAPI int WINAPI config_pio(
'    int axis,      // 軸番号
'    int mode       // ＰＩＯ機能選択フラグ
'    );

Public Declare Function config_pio Lib "TMBSCOM.DLL" Alias "_config_pio@8" _
    (ByVal axis As Long, _
    ByVal mode As Long) As Long


'//----------------------------------------------------------------
'//               サーボシステム内部状態確認ＡＰＩ
'//----------------------------------------------------------------

'//  サーボシステム内部状態を得る関数
'TBUSSIOAPI int WINAPI get_status(
'    int axis,      // 軸番号
'    char *param    // サーボシステム内部状態データ格納領域へのポインタ
'    );

Public Declare Function get_status Lib "TMBSCOM.DLL" Alias "_get_status@8" _
    (ByVal axis As Long, _
    param As Byte) As Long

'// サーボシステム RUN 状態 (RUNビット) を得る関数
'TBUSSIOAPI int WINAPI check_run(
'    int axis      // 軸番号
'    );

Public Declare Function check_run Lib "TMBSCOM.DLL" Alias "_check_run@4" _
    (ByVal axis As Long) As Long

'// サーボ ON/OFF 状態 (SONビット) を得る関数
'TBUSSIOAPI int WINAPI check_son(
'    int axis      // 軸番号
'    );

Public Declare Function check_son Lib "TMBSCOM.DLL" Alias "_check_son@4" _
    (ByVal axis As Long) As Long

'// 原点復帰完了状態 (ZFINビット) を得る関数
'TBUSSIOAPI int WINAPI check_org(
'    int axis      // 軸番号
'    );

Public Declare Function check_org Lib "TMBSCOM.DLL" Alias "_check_org@4" _
    (ByVal axis As Long) As Long

'// 位置決め完了状態 (PFINビット) を得る関数
'TBUSSIOAPI int WINAPI check_pfin(
'    int axis      // 軸番号
'    );

Public Declare Function check_pfin Lib "TMBSCOM.DLL" Alias "_check_pfin@4" _
    (ByVal axis As Long) As Long

'// アラーム状態 (ALMビット) を得る関数
'TBUSSIOAPI int WINAPI check_alrm(
'    int axis      // 軸番号
'    );

Public Declare Function check_alarm Lib "TMBSCOM.DLL" Alias "_check_alrm@4" _
    (ByVal axis As Long) As Long

'// (n) サーボシステム内部状態ポーリング関数
'TBUSSIOAPI int WINAPI check_status(
'    int axis      // 軸番号
'    );

Public Declare Function check_status Lib "TMBSCOM.DLL" Alias "_check_status@4" _
    (ByVal axis As Long) As Long

'// ダイレクトレスポンスデータ書き込みフラグをクリアする関数
'TBUSSIOAPI int WINAPI reset_stwrf(
'    int axis      // 軸番号
'    );

Public Declare Function reset_stwrf Lib "TMBSCOM.DLL" Alias "_reset_stwrf@4" _
    (ByVal axis As Long) As Long

'// ダイレクトレスポンスデータ書き込みフラグを調べる関数
'TBUSSIOAPI int WINAPI check_stwrf(
'    int axis      // 軸番号
'    );

Public Declare Function check_stwrf Lib "TMBSCOM.DLL" Alias "_check_stwrf@4" _
    (ByVal axis As Long) As Long


'//----------------------------------------------------------------------
'//       新規追加アプリケーションプログラムインターフェイス関数
'//----------------------------------------------------------------------

'//----------------------------------------------------------------
'//                         通信起動ＡＰＩ
'//----------------------------------------------------------------

'// Termi-BUS SIO の新通信起動関数 ( 通信条件直接指定 )
'TBUSSIOAPI int WINAPI init_tmbs_config(
'    LPCTSTR port,      // 通信ポートファイル名 ( "COM1","COM2"etc. )
'    int baud,          // ボーレイト指定コード( 4,5,6,7,11H,12H,13H,14H )
'    int nrt,           // コマンド再送最大回数
'    BOOL reset,        // TRUE で再送回数オーバのとき通信初期化に戻る
'    BOOL automatic,    // TRUE でボーレイト自動設定
'    int *axes_info     // 軸構成指定情報
'    );

Public Declare Function init_tmbs_config Lib "TMBSCOM.DLL" Alias "_init_tmbs_config@24" _
    (ByVal port As String, _
    ByVal baud As Long, _
    ByVal nrt As Long, _
    ByVal reset As Long, _
    ByVal automatic As Long, _
    axes_info As Long) As Long

'// Termi-BUS SIO の新通信起動関数 ( TBUSSIO.INIファイル指定 )
'TBUSSIOAPI int WINAPI init_tmbs( void );

Public Declare Function init_tmbs Lib "TMBSCOM.DLL" Alias "_init_tmbs@0" () As Long

'// Termi-BUS SIO の全ての資源を閉じて終了する関数
'TBUSSIOAPI int WINAPI close_tmbs( void );

Public Declare Function close_tmbs Lib "TMBSCOM.DLL" Alias "_close_tmbs@0" () As Long

'// close_tmbs 関数で閉じた Termi-BUS を再度オープンする関数
'TBUSSIOAPI int WINAPI reopen_tmbs( void );

Public Declare Function reopen_tmbs Lib "TMBSCOM.DLL" Alias "_reopen_tmbs@0" () As Long

'//----------------------------------------------------------------
'//                      通信状態確認ＡＰＩ
'//----------------------------------------------------------------

'// Termi-BUS SIO 通信処理実行状態を得る関数
'TBUSSIOAPI int WINAPI get_tmbs_state( void );

Public Declare Function get_tmbs_state Lib "TMBSCOM.DLL" Alias "_get_tmbs_state@0" () As Long

'// 現在のボーレイト設定値を得る関数
'TBUSSIOAPI int WINAPI get_current_baud( void );

Public Declare Function get_current_baud Lib "TMBSCOM.DLL" Alias "_get_current_baud@0" () As Long

'// COM ポートの通信エラーログを得る関数
'TBUSSIOAPI int WINAPI get_com_errlog( void );

Public Declare Function get_com_errlog Lib "TMBSCOM.DLL" Alias "_get_com_errlog@0" () As Long

'//----------------------------------------------------------------
'//                   仮想メモリアクセスＡＰＩ
'//----------------------------------------------------------------

'// 仮想メモリ空間ランダムアクセス書き込み関数
'TBUSSIOAPI int WINAPI write_svmem(
'    int axis,           // 軸番号
'    int address,        // 仮想メモリ空間の書き込み先アドレス
'    const long *src     // 書き込みデータ格納領域へのポインタ
'    );

Public Declare Function write_svmem Lib "TMBSCOM.DLL" Alias "_write_svmem@12" _
    (ByVal axis As Long, _
    ByVal address As Long, _
    src As Long) As Long

'//----------------------------------------------------------------
'//                低レベルコマンド送受信ＡＰＩ
'//----------------------------------------------------------------

'// 低レベルメモリコマンド送受信関数 ( R4,W4,T4 形式 )
'TBUSSIOAPI int WINAPI memcmd_form0(
'    char *func_id,      // メモリコマンド機能コード文字列へのポインタ
'    int axis,           // 軸番号
'    DWORD com_data,     // コマンドデータ
'    LPDWORD res_data,   // レスポンスデータ格納領域へのポインタ
'    LPDWORD warning     // ワーニングコード格納領域へのポインタ
'    );

Public Declare Function memcmd_form0 Lib "TMBSCOM.DLL" Alias "_memcmd_form0@20" _
    (ByVal func_id As String, _
    ByVal axis As Long, _
    ByVal com_data As Long, _
    res_data As Long, _
    warning As Long) As Long

'// 低レベルメモリコマンド送受信関数 ( Q1,Q3 形式 )
'TBUSSIOAPI int WINAPI memcmd_form1(
'    char *func_id,      // メモリコマンド機能コード文字列へのポインタ
'    int axis,           // 軸番号
'    int bank,           // コマンドデータ( バンク番号 )
'    int point,          // コマンドデータ( ポイント番号 )
'    unsigned char *res, // レスポンスデータ格納領域へのポインタ
'    LPDWORD warning     // ワーニングコード格納領域へのポインタ
'    );

Public Declare Function memcmd_form1 Lib "TMBSCOM.DLL" Alias "_memcmd_form1@24" _
    (ByVal func_id As String, _
    ByVal axis As Long, _
    ByVal bank As Long, _
    ByVal Point As Long, _
    Res As Byte, _
    warning As Long) As Long

'// 低レベルメモリコマンド送受信関数 ( Q2,Q4 形式 )
'TBUSSIOAPI int WINAPI memcmd_form2(
'    char *func_id,      // メモリコマンド機能コード文字列へのポインタ
'    int axis,           // 軸番号
'    int bank,           // コマンドデータ( バンク番号 )
'    unsigned char *res, // レスポンスデータ格納領域へのポインタ
'    LPDWORD warning     // ワーニングコード格納領域へのポインタ
'    );

Public Declare Function memcmd_form2 Lib "TMBSCOM.DLL" Alias "_memcmd_form2@20" _
    (ByVal func_id As String, _
    ByVal axis As Long, _
    ByVal bank As Long, _
    Res As Byte, _
    warning As Long) As Long

'// 低レベルメモリコマンド送受信関数 ( V5,V6 形式 )
'TBUSSIOAPI int WINAPI memcmd_form3(
'    char *func_id,      // メモリコマンド機能コード文字列へのポインタ
'    int axis,           // 軸番号
'    int bank,           // コマンドデータ( バンク番号 )
'    int point,          // コマンドデータ( ポイント番号 )
'    LPDWORD res_data,   // レスポンスデータ格納領域へのポインタ
'    LPDWORD warning     // ワーニングコード格納領域へのポインタ
'    );

Public Declare Function memcmd_form3 Lib "TMBSCOM.DLL" Alias "_memcmd_form3@24" _
    (ByVal func_id As String, _
    ByVal axis As Long, _
    ByVal bank As Long, _
    ByVal Point As Long, _
    res_data As Long, _
    warning As Long) As Long

'// 低レベルダイレクトコマンド送受信関数 ( a,b,i,j,m 形式 )
'TBUSSIOAPI int WINAPI dircmd_form0(
'    char func_id,       // ダイレクトコマンド機能コード文字
'    int axis,           // 軸番号
'    int data,           // コマンドデータ
'    unsigned char *res, // レスポンスデータ格納領域へのポインタ
'    LPDWORD warning     // ワーニングコード格納領域へのポインタ
'    );

Public Declare Function dircmd_form0 Lib "TMBSCOM.DLL" Alias "_dircmd_form0@20" _
    (ByVal func_id As Byte, _
    ByVal axis As Long, _
    ByVal data As Long, _
    Res As Byte, _
    warning As Long) As Long

'// 低レベルダイレクトコマンド送受信関数 ( d,n,t 形式 )
'TBUSSIOAPI int WINAPI dircmd_form1(
'    char func_id,       // ダイレクトコマンド機能コード文字
'    int axis,           // 軸番号
'    unsigned char *res, // レスポンスデータ格納領域へのポインタ
'    LPDWORD warning     // ワーニングコード格納領域へのポインタ
'    );

Public Declare Function dircmd_form1 Lib "TMBSCOM.DLL" Alias "_dircmd_form1@16" _
    (ByVal func_id As Byte, _
    ByVal axis As Long, _
    Res As Byte, _
    warning As Long) As Long

'// 低レベルダイレクトコマンド送受信関数 ( g 形式 )
'TBUSSIOAPI int WINAPI dircmd_form2(
'    char func_id,       // ダイレクトコマンド機能コード文字
'    int axis,           // 軸番号
'    int data0,          // コマンドデータ ( 種別選択コード )
'    int data1,          // コマンドデータ ( ゲイン番号 )
'    unsigned char *res, // レスポンスデータ格納領域へのポインタ
'    LPDWORD warning     // ワーニングコード格納領域へのポインタ
'    );

Public Declare Function dircmd_form2 Lib "TMBSCOM.DLL" Alias "_dircmd_form2@24" _
    (ByVal func_id As Byte, _
    ByVal axis As Long, _
    ByVal data0 As Long, _
    ByVal data1 As Long, _
    Res As Byte, _
    warning As Long) As Long

'// 低レベルダイレクトコマンド送受信関数 ( l 形式 )
'TBUSSIOAPI int WINAPI dircmd_form3(
'    char func_id,       // ダイレクトコマンド機能コード文字
'    int axis,           // 軸番号
'    int data0,          // コマンドデータ ( 停止時電流制限値 )
'    int data1,          // コマンドデータ ( 移動時電流制限値 )
'    unsigned char *res, // レスポンスデータ格納領域へのポインタ
'    LPDWORD warning     // ワーニングコード格納領域へのポインタ
'    );

Public Declare Function dircmd_form3 Lib "TMBSCOM.DLL" Alias "_dircmd_form3@24" _
    (ByVal func_id As Byte, _
    ByVal axis As Long, _
    ByVal data0 As Long, _
    ByVal data1 As Long, _
    Res As Byte, _
    warning As Long) As Long

'// 低レベルダイレクトコマンド送受信関数 ( o, r 形式 )
'TBUSSIOAPI int WINAPI dircmd_form4(
'    char func_id,       // ダイレクトコマンド機能コード文字
'    int axis,           // 軸番号
'    int data,           // コマンドデータ
'    unsigned char *res, // レスポンスデータ格納領域へのポインタ
'    LPDWORD warning     // ワーニングコード格納領域へのポインタ
'    );

Public Declare Function dircmd_form4 Lib "TMBSCOM.DLL" Alias "_dircmd_form4@20" _
    (ByVal func_id As Byte, _
    ByVal axis As Long, _
    ByVal data As Long, _
    Res As Byte, _
    warning As Long) As Long

'// 低レベルダイレクトコマンド送受信関数 ( q 形式 )
'TBUSSIOAPI int WINAPI dircmd_form5(
'    char func_id,       // ダイレクトコマンド機能コード文字
'    int axis,           // 軸番号
'    int data,           // コマンドデータ
'    unsigned char *res, // レスポンスデータ格納領域へのポインタ
'    LPDWORD warning     // ワーニングコード格納領域へのポインタ
'    );

Public Declare Function dircmd_form5 Lib "TMBSCOM.DLL" Alias "_dircmd_form5@20" _
    (ByVal func_id As Byte, _
    ByVal axis As Long, _
    ByVal data As Long, _
    Res As Byte, _
    warning As Long) As Long

'// 低レベルダイレクトコマンド送受信関数 ( s 形式 )
'TBUSSIOAPI int WINAPI dircmd_form6(
'    char func_id,       // ダイレクトコマンド機能コード文字
'    int axis,           // 軸番号
'    int data,           // コマンドデータ
'    unsigned char *res, // レスポンスデータ格納領域へのポインタ
'    LPDWORD warning     // ワーニングコード格納領域へのポインタ
'    );

Public Declare Function dircmd_form6 Lib "TMBSCOM.DLL" Alias "_dircmd_form6@20" _
    (ByVal func_id As Byte, _
    ByVal axis As Long, _
    ByVal data As Long, _
    Res As Byte, _
    warning As Long) As Long

'// 低レベルダイレクトコマンド送受信関数 ( v 形式 )
'TBUSSIOAPI int WINAPI dircmd_form7(
'    char func_id,       // ダイレクトコマンド機能コード文字
'    int axis,           // 軸番号
'    int data0,          // コマンドデータ ( 選択コード )
'    int data1,          // コマンドデータ ( 速度指令 )
'    int data2,          // コマンドデータ ( 加速度指令 )
'    unsigned char *res, // レスポンスデータ格納領域へのポインタ
'    LPDWORD warning     // ワーニングコード格納領域へのポインタ
'    );

Public Declare Function dircmd_form7 Lib "TMBSCOM.DLL" Alias "_dircmd_form7@28" _
    (ByVal func_id As Byte, _
    ByVal axis As Long, _
    ByVal data0 As Long, _
    ByVal data1 As Long, _
    ByVal data2 As Long, _
    Res As Byte, _
    warning As Long) As Long

'// 低レベルダイレクトコマンド送受信関数 ( z 形式 )
'TBUSSIOAPI int WINAPI dircmd_form8(
'    char func_id,       // ダイレクトコマンド機能コード文字
'    int axis,           // 軸番号
'    int data0,          // コマンドデータ ( 領域信号境界値 )
'    int data1,          // コマンドデータ ( 境界の方向 )
'    unsigned char *res, // レスポンスデータ格納領域へのポインタ
'    LPDWORD warning     // ワーニングコード格納領域へのポインタ
'    );

Public Declare Function dircmd_form8 Lib "TMBSCOM.DLL" Alias "_dircmd_form8@24" _
    (ByVal func_id As Byte, _
    ByVal axis As Long, _
    ByVal data0 As Long, _
    ByVal data1 As Long, _
    Res As Byte, _
    warning As Long) As Long

'// 低レベルコマンド送受信関数 ( 汎用 形式なし )
'TBUSSIOAPI int WINAPI cmd_unform(
'       int axis,         // 軸番号
'       int mode,         // mode 0:MemoryCommand, 1:DirectCommand
'       char *cmd_str,    // コマンド文字列 (11文字) へのポインタ
'       LPDWORD res_data, // レスポンスデータ格納領域へのポインタ
'       LPDWORD warning   // ワーニングコード格納領域へのポインタ
'       );

Public Declare Function cmd_unform Lib "TMBSCOM.DLL" Alias "_cmd_unform@20" _
    (ByVal axis As Long, _
    ByVal mode As Long, _
    cmd_str As String, _
    res_data As Long, _
    warning As Long) As Long

'//----------------------------------------------------------------
'//                   旧バージョン互換定数
'//----------------------------------------------------------------
'//------------------------------------------------------------
'//              各関数からの成功／失敗返り値
'//------------------------------------------------------------
'#define SIO_DONE            1   // 正常終了
'#define SIO_ERROR           0   // エラー

Public Const SIO_DONE As Long = 1
Public Const SIO_ERROR As Long = 0

'//-------------------------------------------------
'//   get_sio_error() 関数から返されるエラーコード
'//-------------------------------------------------
Public Const SIO_COMUSED As Long = -1           'COMポートがすでに使われている
Public Const SIO_TIMEOUT As Long = -2           'レスポンスのタイムアウト
Public Const SIO_NOINIT As Long = -3            '初期化されていません
Public Const SIO_INVALID_AXIS As Long = -4      '環境設定に無い軸が指定された
Public Const SIO_INVALID_PARAM As Long = -5     '正しくないパラメータが渡された
Public Const SIO_NOTSUPORT_TO As Long = -6      '通信タイムアウトがサポートされていない
Public Const SIO_NOTSUPORT_DTR As Long = -7     'DTR制御がサポートされていない
Public Const SIO_NOTSUPORT_BAUD As Long = -8    'サポートされていないボーレイトが指定された
Public Const SIO_NOTSUPORT_PARA As Long = -9    'COMパラメータの変更がサポートされていない
Public Const SIO_NO_CONFIGFILE As Long = -10    '環境設定ファイルが見つからない
Public Const SIO_RESOURCE As Long = -11         'リソースデータが読み込めない


'//-------------------------------------------------
'//                   新規追加定数
'//-------------------------------------------------
'//-------------------------------------------------
'//   get_sio_error() 関数から返されるエラーコード
'//-------------------------------------------------
Public Const SIO_COMFAILED As Long = -12    'COM ポートのオープンに失敗しました

'//-------------------------------------------------
'//   サーボシステム内部状態データのビット抽出用定数
'//-------------------------------------------------
'// インデックス
Public Const TMBS_PO_ID As Long = 0
Public Const TMBS_PI_ID As Long = 1
Public Const TMBS_ALARM_ID As Long = 2
Public Const TMBS_STAT_ID As Long = 3

'// PO ビットマップ
Public Const TMBS_PM_CODE As Long = &HF&
Public Const TMBS_PM1_BIT As Long = &H1&
Public Const TMBS_PM2_BIT As Long = &H2&
Public Const TMBS_PM4_BIT As Long = &H4&
Public Const TMBS_PM8_BIT As Long = &H8&
Public Const TMBS_PFIN_BIT As Long = &H10&
Public Const TMBS_ZFIN_BIT As Long = &H20&
Public Const TMBS_ZONE_BIT As Long = &H40&
Public Const TMBS_ALM_BIT As Long = &H80&

'// PI ビットマップ
Public Const TMBS_PC_CODE As Long = &HF&
Public Const TMBS_PC1_BIT As Long = &H1&
Public Const TMBS_PC2_BIT As Long = &H2&
Public Const TMBS_PC4_BIT As Long = &H4&
Public Const TMBS_PC8_BIT As Long = &H8&
Public Const TMBS_CSTR_BIT As Long = &H10&
Public Const TMBS_INHP_BIT As Long = &H20&
Public Const TMBS_INHN_BIT As Long = &H40&
Public Const TMBS_ILK_BIT As Long = &H80&

'// STAT ビットマップ
Public Const TMBS_SON_BIT As Long = &H1&
Public Const TMBS_RUN_BIT As Long = &H2&
Public Const TMBS_ZFST_BIT As Long = &H4&
Public Const TMBS_HOLD_BIT As Long = &H8&
Public Const TMBS_LIMP_BIT As Long = &H10&
Public Const TMBS_LIMN_BIT As Long = &H20&
Public Const TMBS_ILST_BIT As Long = &H40&
Public Const TMBS_WARN_BIT As Long = &H80&

'//-------------------------------------------------
'//       Termi-BUS SIO ボーレイト設定コード
'//-------------------------------------------------
Public Const TMBS_BAUD_9600 As Long = &H4&      '  9600 bps
Public Const TMBS_BAUD_19200 As Long = &H5&     ' 19200 bps
Public Const TMBS_BAUD_38400 As Long = &H6&     ' 38400 bps
Public Const TMBS_BAUD_14400 As Long = &H11&    ' 14400 bps
Public Const TMBS_BAUD_57600 As Long = &H13&    ' 57600 bps
Public Const TMBS_BAUD_115200 As Long = &H14&   '115200 bps

'//-------------------------------------------------
'//       Termi-BUS SIO 通信処理実行状態コード
'//-------------------------------------------------
Public Const TMBS_NO_EXIST As Long = 0      '通信処理は開始されていません
Public Const TMBS_INITIAL As Long = 1       '通信初期化要求待ち状態
Public Const TMBS_INIT_ERROR As Long = 2    '通信初期化エラー状態
Public Const TMBS_OPENING As Long = 3       '通信初期化実行状態
Public Const TMBS_RUNNING As Long = 4       '通信確立状態
