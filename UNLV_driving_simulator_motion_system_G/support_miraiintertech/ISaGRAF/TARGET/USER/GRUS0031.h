/*
  user procedure interface
  name: get_tber
*/

typedef long  T_BOO;
typedef long  T_ANA;
typedef float T_REAL;
typedef long  T_TMR;
typedef char  *T_MSG;

typedef struct {
   /* CALL   */ T_BOO  _get;
   /* RETURN */ T_ANA  _tber;
} str_arg;

#define GET              (arg->_get)
#define TBER             (arg->_tber)
