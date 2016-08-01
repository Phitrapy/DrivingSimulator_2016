/*
  user procedure interface
  name: chg_gain
*/

typedef long  T_BOO;
typedef long  T_ANA;
typedef float T_REAL;
typedef long  T_TMR;
typedef char  *T_MSG;

typedef struct {
   /* CALL   */ T_ANA  _ax_no;
   /* CALL   */ T_ANA  _select;
   /* CALL   */ T_ANA  _gain;
   /* RETURN */ T_BOO  _done;
} str_arg;

#define AX_NO            (arg->_ax_no)
#define SELECT           (arg->_select)
#define GAIN             (arg->_gain)
#define DONE             (arg->_done)
