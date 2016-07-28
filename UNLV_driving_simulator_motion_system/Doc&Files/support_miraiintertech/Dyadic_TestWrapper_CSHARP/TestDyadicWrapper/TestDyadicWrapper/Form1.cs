using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace TestDyadicWrapper
{
    public partial class Form1 : Form
    {
        [DllImport("TMBSCOM.dll", EntryPoint = "_init_tmbs_config@24")]
        public static unsafe extern int init_tmbs_config(
                        string port,      // Port Name ( "COM1","COM2"etc. )
                        int baud,          // Baudrate( 4,5,6,7,11H,12H,13H,14H )
                        int nrt,           // Retry
                        bool reset,        // 
                        bool automatic,    // 
                        ref int axes_info     // 
                        );
        [DllImport("TMBSCOM.dll", CallingConvention = CallingConvention.Winapi)]
        public static unsafe extern int move_point(int axis, int point);

        [DllImport("TMBSCOM.dll", EntryPoint = "_get_tmbs_state@0")]
        public static unsafe extern int get_tmbs_state();

        [DllImport("TMBSCOM.dll")]
        public static unsafe extern int read_param(int axis, ref COMPACK dst);

        [DllImport("TMBSCOM.dll")]
        public static unsafe extern int read_svmem(int axis, int address, ref int dst);

        [DllImport("TMBSCOM.dll")]
        public static unsafe extern int move_abs(int axis, int position);

        [DllImport("TMBSCOM.dll")]
        public static unsafe extern int move_jog(int axis, int distance);

        [DllImport("TMBSCOM.dll", EntryPoint = "_write_point@12")]
        public static unsafe extern int write_point(int axis, int point, ref COMPACK src);

        [DllImport("TMBSCOM.dll", EntryPoint = "_write_velocity@12")]
        public static unsafe extern int write_velocity(int axis, int vcmd, int acmd);

        [DllImport("TMBSCOM.dll", EntryPoint = "_read_point@12")]
        public extern unsafe static int read_point(int axis, int point, ref COMPACK dst);

        [DllImport("TMBSCOM.dll")]
        public static unsafe extern int load_param(int axis);

        [DllImport("TMBSCOM.dll")]
        public static unsafe extern int write_param(int axis, ref COMPACK src);

        public struct COMPACK
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 31)]
            public long[] address;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 31)]
            public long[] data;

            public void initialize()
            {
                address = new long[31];
                data = new long[31];

                for (int i = 0; i < address.Length; i++)
                    address[i] = i;
            }
        }
        
        public const int TMBS_NO_EXIST = 0;
        public const int TMBS_INITIAL = 1;
        public const int TMBS_INIT_ERROR = 2;
        public const int TMBS_OPENING = 3;
        public const int TMBS_RUNNING = 4;
        public const int SIO_DONE = 1;
        public const int SIO_ERROR = 0;


        public static COMPACK newPacket = new COMPACK();

        public Form1()
        {
            InitializeComponent();

            int i = 0;
            int[] axes_info = new int[16];

            for (i = 0; i < 16; i++)
                axes_info[i] = -1;

            axes_info[0] = 0; // Axis#0 is used
            while (init_tmbs_config("COM1", Convert.ToInt16("14", 16), 3, false, false, ref axes_info[0]) != 1)
            {
                if (get_tmbs_state() == TMBS_INIT_ERROR)
                    Console.WriteLine("INIT FAILED");
            }

            if (get_tmbs_state() == TMBS_RUNNING)
                MessageBox.Show("Termi-BUS is now running");
            else
                MessageBox.Show("Termi-BUS isn't running!!!");
            newPacket.initialize();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Start();
            move_point(0, Convert.ToInt16(this.cbPositionNo.Text, 10));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int bank1 = Convert.ToInt16("29", 16);
            int bank2 = Convert.ToInt16("29", 16);
            int dataLoc = Convert.ToInt16("7400", 16);
            read_svmem(0, dataLoc, ref bank1);
            int retVal = bank1;
            this.label2.Text = retVal.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            move_jog(0, 1000);
        }
        private long getLongValue(string positionStr)
        {
            bool isNegative;
            string hexRep;
            string binaryRep;
            long longRep;
            string onesComp;
            long onesCompInt;
            long position = Convert.ToInt64(positionStr, 10);

            if (position < 0)
                isNegative = true;
            else
                isNegative = false;

            if (isNegative)
            {
                //Convert to hex to view in word format
                hexRep = Convert.ToString(position, 16);
                //Get least significant double word 
                hexRep = hexRep.Substring(hexRep.Length - 8, 8);
                //swap double words
                hexRep = hexRep.PadRight(16,'0');
                //convert to number
                longRep = Convert.ToInt64(hexRep, 16);
                return longRep; 
            }
            else
            {
                //converts integer to a hex value
                hexRep = Convert.ToString(position, 16);
                //pad the value to ensure correct offset
                //since the positive numbers have leading 0's we do not need to fill the full 64 bits
                hexRep = hexRep + "00000000";
                longRep = Convert.ToInt64(hexRep, 16);
                return longRep;
            }
        }
        private int getIntValue(long position)
        {
            string binaryRep;
            string hexRep;
            int intRep;
            bool isNegative;
            string onesComp;
            long onesCompInt;

            binaryRep = Convert.ToString(position, 2);

            //Check to see that the length is 64 bits, and check the sign bit(msb) to see if its negative
            //In the event that the number is positive, the msb=0 and will normally be left out when
            //converted to an integer
            if (binaryRep.Length == 64 && binaryRep.Substring(0, 1) == "1")
                isNegative = true;
            else
            {
                //Check to see if the position is 0
                if (binaryRep.Length == 1)
                    return 0;
                isNegative = false;
            }

            if (isNegative)
            {
                //To get actual decimal value, we must convert from the one's compliment representation
                //To ones compliment switch 0's to 1's and 1's to zeros, then add 1
                onesComp = binaryRep.Replace("0", "2");
                onesComp = onesComp.Replace("1", "0");
                onesComp = onesComp.Replace("2", "1");
                onesCompInt = Convert.ToInt64(onesComp, 2);
                onesCompInt = onesCompInt + 1;
                //This is now the unsigned value
                hexRep = Convert.ToString(onesCompInt, 16);
                //There is an extra word (8 bits) that we must remove
                hexRep = hexRep.Remove(hexRep.Length - 8, 8);
                intRep = Convert.ToInt16(hexRep, 16);
                //Add the sign back on
                intRep = (intRep * -1);
                return intRep;
            }
            else
            {
                //Convert the binary representation to HEX
                hexRep = Convert.ToString(Convert.ToInt64(binaryRep, 2), 16);
                //Remove the extra word (8 Bits)
                hexRep = hexRep.Remove(hexRep.Length - 8, 8);
                //Convert back to a decimal
                intRep = Convert.ToInt32(hexRep, 16);
                return intRep;
            }
        }
        private void btnSetPosition_Click(object sender, EventArgs e)
        {
            newPacket.data[0] = this.getLongValue(this.txtPosition.Text);
            newPacket.data[1] = this.getLongValue(this.txtPosition.Text);
            write_point(0, this.cbPositionNo.SelectedIndex, ref newPacket);
            write_velocity(0, 0, 0);
        }

        private void btnReadPoint_Click(object sender, EventArgs e)
        {
            int Result = read_point(0, this.cbPositionNo.SelectedIndex, ref newPacket); // Read into PointData
            Console.WriteLine(getIntValue(newPacket.data[0]).ToString() + " at Velocity of " + getIntValue(newPacket.data[4]).ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            newPacket.initialize();
            newPacket.address[2] = 0x0000000E;
            int result = read_param(0, ref newPacket);
            Console.WriteLine(getIntValue(newPacket.data[1]).ToString());
        }
    }
}