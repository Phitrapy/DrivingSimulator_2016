/*
  user procedure interface
  name: get_pfin
*/

typedef long  T_BOO;
typedef long  T_ANA;
typedef float T_REAL;
typedef long  T_TMR;
typedef char  *T_MSG;

typedef struct {
   /* CALL   */ T_ANA  _ax_no;
   /* RETURN */ T_BOO  _pfin;
} str_arg;

#define AX_NO            (arg->_ax_no)
#define PFIN             (arg->_pfin)
