/*
  user procedure interface
  name: tbus_ini
*/

typedef long  T_BOO;
typedef long  T_ANA;
typedef float T_REAL;
typedef long  T_TMR;
typedef char  *T_MSG;

typedef struct {
   /* CALL   */ T_BOO  _tbus_en;
   /* RETURN */ T_BOO  _init_ok;
} str_arg;

#define TBUS_EN          (arg->_tbus_en)
#define INIT_OK          (arg->_init_ok)
