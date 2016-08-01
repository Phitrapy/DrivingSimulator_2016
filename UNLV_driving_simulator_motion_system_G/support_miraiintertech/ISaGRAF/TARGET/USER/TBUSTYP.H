//////////////////////////////////////////////////////////////////////////
//
//   Type of Common Variables for Termi-BUS SIO ISaGRAF Function Libraries
//
//////////////////////////////////////////////////////////////////////////

struct TermiBUS_var{
   int comm_en;     // Termi-BUS SIO Function Enable
   int comm_ok;     // Termi-BUS SIO Command Complete
   int move;        // In "move" Function Execution Cycle
   int pfin;        // "pfin" Status for "all_pfin" Function
   int zfin;        // "zfin" Status for "all_zfin" Function
   int last_son;    // Last SON Input of "son_soff" Function
   int cur_pos;     // Current Position Data
   short cur_spd;   // Current Speed Data
};

struct TermiBUS_sio{
   int reset;       // Request For init_sio() Function Call
   int exe_cnt;     // TermiBUS SIO TurnArround Counter
   int retry ;      // TermiBUS SIO maximam communication retry
   int axes[16] ;   // TermiBUS SIO available axes descripter
   char port[32] ;  // TermiBUS SIO COM PORT name
};
