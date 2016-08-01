/*
  user procedure interface
  name: tini_cfg
*/

typedef long  T_BOO;
typedef long  T_ANA;
typedef float T_REAL;
typedef long  T_TMR;
typedef char  *T_MSG;

typedef struct {
   /* CALL   */ T_BOO  _tbus_en;
   /* CALL   */ T_ANA  _com_pno;
   /* CALL   */ T_ANA  _retry;
   /* CALL   */ T_BOO  _en0;
   /* CALL   */ T_BOO  _en1;
   /* CALL   */ T_BOO  _en2;
   /* CALL   */ T_BOO  _en3;
   /* CALL   */ T_BOO  _en4;
   /* CALL   */ T_BOO  _en5;
   /* CALL   */ T_BOO  _en6;
   /* CALL   */ T_BOO  _en7;
   /* CALL   */ T_BOO  _en8;
   /* CALL   */ T_BOO  _en9;
   /* CALL   */ T_BOO  _ena;
   /* CALL   */ T_BOO  _enb;
   /* CALL   */ T_BOO  _enc;
   /* CALL   */ T_BOO  _end;
   /* CALL   */ T_BOO  _ene;
   /* CALL   */ T_BOO  _enf;
   /* RETURN */ T_BOO  _init_ok;
} str_arg;

#define TBUS_EN          (arg->_tbus_en)
#define COM_PNO          (arg->_com_pno)
#define RETRY            (arg->_retry)
#define EN0              (arg->_en0)
#define EN1              (arg->_en1)
#define EN2              (arg->_en2)
#define EN3              (arg->_en3)
#define EN4              (arg->_en4)
#define EN5              (arg->_en5)
#define EN6              (arg->_en6)
#define EN7              (arg->_en7)
#define EN8              (arg->_en8)
#define EN9              (arg->_en9)
#define ENA              (arg->_ena)
#define ENB              (arg->_enb)
#define ENC              (arg->_enc)
#define END              (arg->_end)
#define ENE              (arg->_ene)
#define ENF              (arg->_enf)
#define INIT_OK          (arg->_init_ok)
