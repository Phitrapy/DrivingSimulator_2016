/*
  user procedure interface
  name: rotate_r
*/

typedef long  T_BOO;
typedef long  T_ANA;
typedef float T_REAL;
typedef long  T_TMR;
typedef char  *T_MSG;

typedef struct {
   /* CALL   */ T_ANA  _ax_no;
   /* CALL   */ T_ANA  _velocity;
   /* CALL   */ T_ANA  _accel;
   /* RETURN */ T_BOO  _done;
} str_arg;

#define AX_NO            (arg->_ax_no)
#define VELOCITY         (arg->_velocity)
#define ACCEL            (arg->_accel)
#define DONE             (arg->_done)
