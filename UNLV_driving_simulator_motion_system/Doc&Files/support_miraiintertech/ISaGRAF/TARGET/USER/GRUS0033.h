/*
  user procedure interface
  name: tbus_cnt
*/

typedef long  T_BOO;
typedef long  T_ANA;
typedef float T_REAL;
typedef long  T_TMR;
typedef char  *T_MSG;

typedef struct {
   /* CALL   */ T_BOO  _clr;
   /* RETURN */ T_ANA  _cnt;
} str_arg;

#define CLR              (arg->_clr)
#define CNT              (arg->_cnt)
