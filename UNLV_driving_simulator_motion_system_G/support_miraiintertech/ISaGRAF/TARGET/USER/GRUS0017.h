/*
  user procedure interface
  name: exe_jog
*/

typedef long  T_BOO;
typedef long  T_ANA;
typedef float T_REAL;
typedef long  T_TMR;
typedef char  *T_MSG;

typedef struct {
   /* CALL   */ T_BOO  _en;
   /* CALL   */ T_TMR  _exec_time;
   /* CALL   */ T_BOO  _dir;
   /* CALL   */ T_ANA  _ax_no;
   /* CALL   */ T_ANA  _velocity;
   /* RETURN */ T_BOO  _done;
} str_arg;

#define EN               (arg->_en)
#define EXEC_TIME        (arg->_exec_time)
#define DIR              (arg->_dir)
#define AX_NO            (arg->_ax_no)
#define VELOCITY         (arg->_velocity)
#define DONE             (arg->_done)
