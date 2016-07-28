/*
  user procedure interface
  name: chg_tlim
*/

typedef long  T_BOO;
typedef long  T_ANA;
typedef float T_REAL;
typedef long  T_TMR;
typedef char  *T_MSG;

typedef struct {
   /* CALL   */ T_ANA  _ax_no;
   /* CALL   */ T_ANA  _tlim_dy;
   /* CALL   */ T_ANA  _tlim_st;
   /* RETURN */ T_BOO  _done;
} str_arg;

#define AX_NO            (arg->_ax_no)
#define TLIM_DY          (arg->_tlim_dy)
#define TLIM_ST          (arg->_tlim_st)
#define DONE             (arg->_done)
