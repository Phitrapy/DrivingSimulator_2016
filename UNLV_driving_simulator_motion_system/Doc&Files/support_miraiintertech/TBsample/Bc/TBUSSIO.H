//------------------------------------------------------------------------
//
//      Termi-BUS SIO Serial Communication DLL
//
//          DLL.Name: TMBSCOM.DLL
//
//      Termi-BUS SIO Serial Comminication DLL API Function Prototypes
//
//          FileName: TBUSSIO.H
//
//      Copyright(C) 1998..2003 Dyadic Systems Co.,Ltd.
//
//------------------------------------------------------------------------
#ifndef TBUSSIO_H_INCLUDED
#define TBUSSIO_H_INCLUDED

#include <windows.h>

#ifdef	MAKE_DLL
	#define	TBUSSIOAPI	__declspec(dllexport)
#else
	#define TBUSSIOAPI	__declspec(dllimport)
#endif


#ifdef __cplusplus
extern "C" {
#endif

typedef struct {
	int		address[32];
	long	data[32];
} COMPACK;

TBUSSIOAPI int WINAPI init_sio_tbus(
	LPCTSTR port,  // Port Name ( "COM1","COM2"etc. )
	int baud,      // BaudRate ( 4,5,6,11H,13H,14H )
	int nrt,       // Retry
	int *axes_info //
	);

TBUSSIOAPI int WINAPI init_sio( void );

TBUSSIOAPI int WINAPI get_axes( unsigned short *axes );

TBUSSIOAPI int WINAPI get_sio_error( void );

TBUSSIOAPI int WINAPI read_svmem( int axis, int address, long *dst );

TBUSSIOAPI int WINAPI write_param( int axis, const COMPACK *src );

 TBUSSIOAPI int WINAPI read_param( int axis, COMPACK *dst );

TBUSSIOAPI int WINAPI write_point( int axis, int point, const COMPACK *src );

TBUSSIOAPI int WINAPI read_point( int axis, int point, COMPACK *dst );

TBUSSIOAPI int WINAPI load_param( int axis );

TBUSSIOAPI int WINAPI save_param( int axis );

TBUSSIOAPI int WINAPI save_point( int axis, int point );

TBUSSIOAPI int WINAPI get_write_count( int axis );

TBUSSIOAPI int WINAPI write_fzone( int axis, long zone );

TBUSSIOAPI int WINAPI write_rzone( int axis, long zone );

TBUSSIOAPI int WINAPI select_svparm( int axis, int gain_sel, int svparm );

TBUSSIOAPI int WINAPI write_inpos( int axis, long width );

TBUSSIOAPI int WINAPI write_trqlim( int axis, int l_dynamic, int l_static );

TBUSSIOAPI int WINAPI write_velocity( int axis, int vcmd, int acmd );

TBUSSIOAPI int WINAPI move_point( int axis, int point );

TBUSSIOAPI int WINAPI hmove_point( int axis, int point );

TBUSSIOAPI int WINAPI move_abs( int axis, long position );

TBUSSIOAPI int WINAPI hmove_abs( int axis, long position );

TBUSSIOAPI int WINAPI follow_position( int axis );

TBUSSIOAPI int WINAPI move_jog( int axis, long distance );

TBUSSIOAPI int WINAPI hmove_jog( int axis, long distance );

TBUSSIOAPI int WINAPI move_inc( int axis, long distance );

TBUSSIOAPI int WINAPI hmove_inc( int axis, long distance );

TBUSSIOAPI int WINAPI move_org( int axis, int mode );

TBUSSIOAPI int WINAPI hmove_org( int axis, int mode );

TBUSSIOAPI int WINAPI move_rotate( int axis, int dir, int vcmd, int acmd );

TBUSSIOAPI int WINAPI start_preload_command( int axis );

TBUSSIOAPI int WINAPI clear_preload_command( int axis );

TBUSSIOAPI int WINAPI write_position( int axis, long position );

TBUSSIOAPI int WINAPI set_son( int axis );

TBUSSIOAPI int WINAPI set_soff( int axis );

TBUSSIOAPI int WINAPI reset_alarm( int axis );

TBUSSIOAPI int WINAPI reset_memory( int axis );

TBUSSIOAPI int WINAPI config_pio( int axis, int mode );

TBUSSIOAPI int WINAPI get_status( int axis, char *param );

TBUSSIOAPI int WINAPI check_run( int axis );

TBUSSIOAPI int WINAPI check_son( int axis );

TBUSSIOAPI int WINAPI check_org( int axis );

TBUSSIOAPI int WINAPI check_pfin( int axis );

TBUSSIOAPI int WINAPI check_alrm( int axis );

TBUSSIOAPI int WINAPI check_status( int axis );

TBUSSIOAPI int WINAPI reset_stwrf( int axis );

TBUSSIOAPI int WINAPI check_stwrf( int axis );

TBUSSIOAPI int WINAPI init_tmbs_config(
	LPCTSTR port,      // Port Name ( "COM1","COM2"etc. )
	int baud,          // Baudrate( 4,5,6,7,11H,12H,13H,14H )
	int nrt,           // Retry
	BOOL reset,        // 
	BOOL automatic,    // 
	int *axes_info     // 
	);

TBUSSIOAPI int WINAPI init_tmbs( void );

TBUSSIOAPI int WINAPI close_tmbs( void	);

TBUSSIOAPI int WINAPI reopen_tmbs( void );

TBUSSIOAPI int WINAPI get_tmbs_state( void );

TBUSSIOAPI int WINAPI get_current_baud( void );

TBUSSIOAPI int WINAPI get_com_errlog( void );

TBUSSIOAPI int WINAPI write_svmem( int axis, int address, const long *src );

TBUSSIOAPI int WINAPI memcmd_form0( char *func_id, int axis, DWORD com_data, LPDWORD res_data, LPDWORD warning );

TBUSSIOAPI int WINAPI memcmd_form1( char *func_id, int axis, int bank, int point, unsigned char *res, LPDWORD warning );

TBUSSIOAPI int WINAPI hmemcmd_form1( char *func_id, int axis, int bank, int point, unsigned char *res, LPDWORD warning );

TBUSSIOAPI int WINAPI memcmd_form2( char *func_id, int axis, int bank, unsigned char *res, LPDWORD warning );

TBUSSIOAPI int WINAPI memcmd_form3( char *func_id, int axis, int bank, int point, LPDWORD res_data, LPDWORD warning );

TBUSSIOAPI int WINAPI dircmd_form0( char func_id, int axis, int data, unsigned char *res, LPDWORD warning );

TBUSSIOAPI int WINAPI hdircmd_form0( char func_id, int axis, int data, unsigned char *res, LPDWORD warning );

TBUSSIOAPI int WINAPI dircmd_form1( char func_id, int axis, unsigned char *res, LPDWORD warning );

TBUSSIOAPI int WINAPI hdircmd_form1( char func_id, int axis, unsigned char *res, LPDWORD warning );

TBUSSIOAPI int WINAPI dircmd_form2( char func_id, int axis, int data0, int data1, unsigned char *res, LPDWORD warning );

TBUSSIOAPI int WINAPI dircmd_form3( char func_id, int axis, int data0, int data1, unsigned char *res, LPDWORD warning );

TBUSSIOAPI int WINAPI dircmd_form4( char func_id, int axis, int data, unsigned char *res, LPDWORD warning );

TBUSSIOAPI int WINAPI hdircmd_form4( char func_id, int axis, int data, unsigned char *res, LPDWORD warning );

TBUSSIOAPI int WINAPI dircmd_form5( char func_id, int axis, int data, unsigned char *res, LPDWORD warning );

TBUSSIOAPI int WINAPI dircmd_form6( char func_id, int axis, int data, unsigned char *res, LPDWORD warning );

TBUSSIOAPI int WINAPI dircmd_form7( char func_id, int axis, int data0, int data1, int data2, unsigned char *res, LPDWORD warning );

TBUSSIOAPI int WINAPI dircmd_form8( char func_id, int axis, int data0, int data1, unsigned char *res, LPDWORD warning );

TBUSSIOAPI int WINAPI cmd_unform( int axis, int mode, char *cmd_str, LPDWORD res_data, LPDWORD warning );

TBUSSIOAPI int WINAPI get_trx_buf( char *rx_ptr, char *tx_ptr );


#ifdef __cplusplus
}
#endif

#define	SIO_DONE			1	//
#define SIO_ERROR			0	//
#define SIO_COMUSED			-1	//
#define SIO_TIMEOUT			-2	//
#define SIO_NOINIT			-3	//
#define SIO_INVALID_AXIS	-4	//
#define SIO_INVALID_PARAM	-5	//
#define SIO_NOTSUPORT_TO	-6	//
#define SIO_NOTSUPORT_DTR	-7	//
#define SIO_NOTSUPORT_BAUD	-8	//
#define SIO_NOTSUPORT_PARA	-9	//
#define SIO_NO_CONFIGFILE	-10	//
#define SIO_RESOURCE		-11	//
#define SIO_COMFAILED		-12	//

#define TMBS_PO_ID        0
#define TMBS_PI_ID        1
#define TMBS_ALARM_ID     2
#define TMBS_STAT_ID      3

// PO
#define TMBS_PM_CODE   0x0f
#define TMBS_PM1_BIT   0x01
#define TMBS_PM2_BIT   0x02
#define TMBS_PM4_BIT   0x04
#define TMBS_PM8_BIT   0x08
#define TMBS_PFIN_BIT  0x10
#define TMBS_ZFIN_BIT  0x20
#define TMBS_ZONE_BIT  0x40
#define TMBS_ALM_BIT   0x80

// PI
#define TMBS_PC_CODE   0x0f
#define TMBS_PC1_BIT   0x01
#define TMBS_PC2_BIT   0x02
#define TMBS_PC4_BIT   0x04
#define TMBS_PC8_BIT   0x08
#define TMBS_CSTR_BIT  0x10
#define TMBS_INHP_BIT  0x20
#define TMBS_INHN_BIT  0x40
#define TMBS_ILK_BIT   0x80

// STAT
#define TMBS_PON_BIT   0x01
#define TMBS_SON_BIT   0x02
#define TMBS_RUN_BIT   0x04
#define TMBS_ZFST_BIT  0x08
#define TMBS_HOLD_BIT  0x10
#define TMBS_LIMP_BIT  0x20
#define TMBS_LIMN_BIT  0x40
#define TMBS_WARN_BIT  0x80

#define	TMBS_BAUD_9600   0x04 //   9600 bps
#define	TMBS_BAUD_19200  0x05 //  19200 bps
#define	TMBS_BAUD_38400  0x06 //  38400 bps
#define	TMBS_BAUD_14400  0x11 //  14400 bps
#define	TMBS_BAUD_57600  0x13 //  57600 bps
#define	TMBS_BAUD_115200 0x14 // 115200 bps

#define	TMBS_NO_EXIST    0
#define	TMBS_INITIAL     1
#define	TMBS_INIT_ERROR  2
#define	TMBS_OPENING     3
#define	TMBS_RUNNING     4

#define	TMBS_BANK0_BASE   0x00000000 // Bank#0
#define	TMBS_BANK1_BASE   0x00000400 // Bank#1
#define	TMBS_BANK26_BASE  0x00006800 // Bank#26
#define	TMBS_BANK27_BASE  0x00006c00 // Bank#27
#define	TMBS_BANK28_BASE  0x00007000 // Bank#28
#define	TMBS_BANK29_BASE  0x00007400 // Bank#29
#define	TMBS_BANK30_BASE  0x00007800 // Bank#30
#define	TMBS_BANK31_BASE  0x00007c00 // Bank#31

#define	TMBS_WRCOUNT_OFFSET  0x00000015


#endif // TBUSSIO_H_INCLUDED

