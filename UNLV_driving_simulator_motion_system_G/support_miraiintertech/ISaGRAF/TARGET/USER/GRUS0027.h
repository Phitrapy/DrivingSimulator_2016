/*
  user procedure interface
  name: son_soff
*/

typedef long  T_BOO;
typedef long  T_ANA;
typedef float T_REAL;
typedef long  T_TMR;
typedef char  *T_MSG;

typedef struct {
   /* CALL   */ T_BOO  _svon;
   /* CALL   */ T_ANA  _ax_no;
   /* RETURN */ T_BOO  _svac;
} str_arg;

#define SVON             (arg->_svon)
#define AX_NO            (arg->_ax_no)
#define SVAC             (arg->_svac)
