using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class UART1 : MonoBehaviour
{
    #region VARIABLES
    //debug
    public string hexaIn;

    //Useful
    public string[] initSequence;
    public string dataSent;
    public bool readyToSend = false;

    SerialPort serial1;
    int bufSize = 16;
    public byte[] buf = new byte[16];
    public int bufCount = 0;
    public int a, b, sum;
    #endregion

    #region EXECUTION
    //Use this for initialization
    void Start()
    {
        serial1 = new SerialPort();
        serial1.PortName = "COM3";
        serial1.Parity = Parity.None;
        serial1.BaudRate = 9600;
        serial1.DataBits = 8;
        serial1.ReadTimeout = 120;
        serial1.WriteTimeout = 120;
        serial1.StopBits = StopBits.One;

        //try { serial1.Close(); } catch { }
        serial1.Open();
        bufCount = 0;
        a = 0;
        b = 0;

        // Serial port data init
        System.Threading.Thread.Sleep(1000);
        hexaIn = "";
        serial1.Write("0ptrw0300000B0");
        try
        {
            ReadSerialData(); hexaIn = ByteToHexBitFiddle(buf);
        }
        catch { }
        System.Threading.Thread.Sleep(1000);
        hexaIn = "";
        serial1.Write("0ptrw0300000B0");
        try
        {
            ReadSerialData(); hexaIn = ByteToHexBitFiddle(buf);
        }
        catch { }
        System.Threading.Thread.Sleep(1000);


        BuildInitSequence();
        System.Threading.Thread.Sleep(200);
        StartCoroutine(sendInitSequence());

        //readyToSend = true;
    }

    //What happens when the application closes
    void OnApplicationQuit()
    {
        serial1.Close();
    }

    //Update is called once per frame
    void FixedUpdate()
    {
        StartCoroutine(ListenToSerial());

        //if (readyToSend)
        //{
        //    if (dataSent != "")
        //    {
        //        t = 0;
        //        hexaIn = "";
        //        serial1.Write(dataSent);
        //        ReadSerialData(); hexaIn = ByteToHexBitFiddle(buf);
        //        System.Threading.Thread.Sleep(1000);
        //        hexaIn = "";
        //        serial1.Write(dataSent);
        //        ReadSerialData(); hexaIn = ByteToHexBitFiddle(buf);
        //        System.Threading.Thread.Sleep(1000);
        //        hexaIn = "";
        //        StartCoroutine(SendReadData(dataSent));
        //    }
        //}
    }

    #endregion

    #region METHODS


    IEnumerator ListenToSerial()
    {
        hexaIn = "000000000000000000000000000000000";
        float t = 0f;
        while (!readyToSend && hexaIn[1] != 2 && t < 0.02f) {
            t += Time.deltaTime;
            try
            {
                ReadSerialData();
            }
            catch { }
        }
        //Debug.Log(hexaIn);
        readyToSend = true;
        yield break;
    }

    /*
     * Reads a sequence of serial data (16 Bytes), builds a byte array with it.
     * buf
     */
    void ReadSerialData()
    {
        buf[15] = 0;
        bufCount = 0;
        sum = 0;
        bufCount = serial1.Read(buf, sum, 16 - sum);

        Debug.Log(buf);

    }

    /*
     * Builds a string from the byte array (hex)
     */
    static string ByteToHexBitFiddle(byte[] bytes)
    {
        char[] c = new char[bytes.Length * 2];
        int b;
        for (int i = 0; i < bytes.Length; i++)
        {
            b = bytes[i] >> 4;
            c[i * 2] = (char)(55 + b + (((b - 10) >> 31) & -7));
            b = bytes[i] & 0xF;
            c[i * 2 + 1] = (char)(55 + b + (((b - 10) >> 31) & -7));
        }
        return new string(c);
    }

    /*
     * If the Serial Port is available, sends a Serial request, then wait for an answer.
     */
    IEnumerator SendReadData(string data) {
        while (!readyToSend) { }
        readyToSend = false;
        hexaIn = "";
        //System.Threading.Thread.Sleep(40);
        while ((hexaIn[30] != '0' && hexaIn[31] != '3') ) {
            try
            {
                System.Threading.Thread.Sleep(40);
                serial1.Write(dataSent);
                ReadSerialData(); hexaIn = ByteToHexBitFiddle(buf);
            }
            catch
            {
                Debug.Log("Not received yet!");
            }
            //System.Threading.Thread.Sleep(20);
        };
        readyToSend = true;
        StartCoroutine(SendReadData(data));
        yield return new WaitForSeconds(.0001f);
    }

    IEnumerator sendInitSequence()
    {
        for(int i = 0; i < 347; i++)
        {
            serial1.Write(initSequence[i]);
            readyToSend = false;
            while (!readyToSend) { Debug.Log("in the loop baby"); yield return new WaitForSeconds(.01f); }
            Debug.Log("Out of the loop baby! i = " + i);
        }
        Debug.Log("fini!");
        yield break;

    }

    public void BuildInitSequence()
    {
        Debug.Log("Construction de la séquence!");
        initSequence = new string[348];
        initSequence[0] = "0ptrw0300000B0";
        initSequence[1] = "0T40000000908F";
        initSequence[2] = "0T40000000908F";
        initSequence[3] = "0W400000014090";
        initSequence[4] = "0W400000014090";
        initSequence[5] = "0T40000000C085";
        initSequence[6] = "0T40000000C085";
        initSequence[7] = "0W400000003092";
        initSequence[8] = "0W400000003092";
        initSequence[9] = "1ptrw0300000AF";
        initSequence[10] = "1ptrw0300000AF";
        initSequence[11] = "1T40000000908E";
        initSequence[12] = "1T40000000908E";
        initSequence[13] = "1W40000001408F";
        initSequence[14] = "1W40000001408F";
        initSequence[15] = "1T40000000C084";
        initSequence[16] = "1W400000003091";
        initSequence[17] = "1W400000003091";
        initSequence[18] = "2ptrw0300000AE";
        initSequence[19] = "2ptrw0300000AE";
        initSequence[20] = "2T40000000908D";
        initSequence[21] = "2T40000000908D";
        initSequence[22] = "2W40000001408E";
        initSequence[23] = "2W40000001408E";
        initSequence[24] = "2T40000000C083";
        initSequence[25] = "2T40000000C083";
        initSequence[26] = "2W400000003090";
        initSequence[27] = "2W400000003090";
        initSequence[28] = "3ptrw0300000AD";
        initSequence[29] = "3ptrw0300000AD";
        initSequence[30] = "4ptrw0300000AC";
        initSequence[31] = "4ptrw0300000AC";
        initSequence[32] = "5ptrw0300000AB";
        initSequence[33] = "5ptrw0300000AB";
        initSequence[34] = "6ptrw0300000AA";
        initSequence[35] = "6ptrw0300000AA";
        initSequence[36] = "7ptrw0300000A9";
        initSequence[37] = "7ptrw0300000A9";
        initSequence[38] = "8ptrw0300000A8";
        initSequence[39] = "8ptrw0300000A8";
        initSequence[40] = "9ptrw0300000A7";
        initSequence[41] = "9ptrw0300000A7";
        initSequence[42] = "Aptrw03000009F";
        initSequence[43] = "Aptrw03000009F";
        initSequence[44] = "Bptrw03000009E";
        initSequence[45] = "Bptrw03000009E";
        initSequence[46] = "Cptrw03000009D";
        initSequence[47] = "Cptrw03000009D";
        initSequence[48] = "Dptrw03000009C";
        initSequence[49] = "Dptrw03000009C";
        initSequence[50] = "Eptrw03000009B";
        initSequence[51] = "Eptrw03000009B";
        initSequence[52] = "Fptrw03000009A";
        initSequence[53] = "Fptrw03000009A";
        initSequence[54] = "";
        initSequence[55] = "";
        initSequence[56] = "";
        initSequence[57] = "0Q10100000009D";
        initSequence[58] = "0R400000400096";
        initSequence[59] = "0R400000401095";
        initSequence[60] = "0R400000402094";
        initSequence[61] = "0R400000403093";
        initSequence[62] = "0R400000404092";
        initSequence[63] = "0R400000405091";
        initSequence[64] = "0R400000406090";
        initSequence[65] = "0R40000040708F";
        initSequence[66] = "0R40000040808E";
        initSequence[67] = "0R40000040908D";
        initSequence[68] = "0R40000040A085";
        initSequence[69] = "0R40000040B084";
        initSequence[70] = "0R40000040C083";
        initSequence[71] = "0R40000040D082";
        initSequence[72] = "0R40000040E081";
        initSequence[73] = "0R40000040F080";
        initSequence[74] = "0R400000410095";
        initSequence[75] = "0R400000411094";
        initSequence[76] = "0R400000412093";
        initSequence[77] = "0R400000413092";
        initSequence[78] = "0R400000414091";
        initSequence[79] = "0R400000415090";
        initSequence[80] = "0R40000041608F";
        initSequence[81] = "0R40000041708E";
        initSequence[82] = "0R40000041808D";
        initSequence[83] = "0R40000041908C";
        initSequence[84] = "0R40000041A084";
        initSequence[85] = "0R40000041B083";
        initSequence[86] = "0R40000041C082";
        initSequence[87] = "0R40000041D081";
        initSequence[88] = "0R40000041E080";
        initSequence[89] = "0R40000041F07F";
        initSequence[90] = "0Q10000000009E";
        initSequence[91] = "0R40000000009A";
        initSequence[92] = "0R400000001099";
        initSequence[93] = "0R400000002098";
        initSequence[94] = "0R400000003097";
        initSequence[95] = "0R400000004096";
        initSequence[96] = "0R400000005095";
        initSequence[97] = "0R400000006094";
        initSequence[98] = "0R400000007093";
        initSequence[99] = "0R400000008092";
        initSequence[100] = "0R400000009091";
        initSequence[101] = "0R40000000A089";
        initSequence[102] = "0R40000000B088";
        initSequence[103] = "0R40000000C087";
        initSequence[104] = "0R40000000D086";
        initSequence[105] = "0R40000000E085";
        initSequence[106] = "0R40000000F084";
        initSequence[107] = "0R400000010099";
        initSequence[108] = "0R400000011098";
        initSequence[109] = "0R400000012097";
        initSequence[110] = "0R400000013096";
        initSequence[111] = "0R400000014095";
        initSequence[112] = "0R400000015094";
        initSequence[113] = "0R400000016093";
        initSequence[114] = "0R400000017092";
        initSequence[115] = "0R400000018091";
        initSequence[116] = "0R400000019090";
        initSequence[117] = "0R40000001A088";
        initSequence[118] = "0R40000001B087";
        initSequence[119] = "0R40000001C086";
        initSequence[120] = "0R40000001D085";
        initSequence[121] = "0R40000001E084";
        initSequence[122] = "0R40000001F083";
        initSequence[123] = "0R40000680008C";
        initSequence[124] = "0R4000FFD4B044";
        initSequence[125] = "0R4000FFD46050";
        initSequence[126] = "0R400007C00080";
        initSequence[127] = "0R400006C0307E";
        initSequence[128] = "0Q10100000009D";
        initSequence[129] = "0R400000400096";
        initSequence[130] = "0R400000401095";
        initSequence[131] = "0R400000402094";
        initSequence[132] = "0R400000403093";
        initSequence[133] = "0R400000404092";
        initSequence[134] = "0R400000405091";
        initSequence[135] = "0R400000406090";
        initSequence[136] = "0R40000040708F";
        initSequence[137] = "0R40000040808E";
        initSequence[138] = "0R40000040908D";
        initSequence[139] = "0R40000040A085";
        initSequence[140] = "0R40000040B084";
        initSequence[141] = "0R40000040C083";
        initSequence[142] = "0R40000040D082";
        initSequence[143] = "0R40000040E081";
        initSequence[144] = "0R40000040F080";
        initSequence[145] = "0R400000410095";
        initSequence[146] = "0R400000411094";
        initSequence[147] = "0R400000412093";
        initSequence[148] = "0R400000413092";
        initSequence[149] = "0R400000414091";
        initSequence[150] = "0R400000415090";
        initSequence[151] = "0R40000041608F";
        initSequence[152] = "0R40000041708E";
        initSequence[153] = "0R40000041808D";
        initSequence[154] = "0R40000041908C";
        initSequence[155] = "0R40000041A084";
        initSequence[156] = "0R40000041B083";
        initSequence[157] = "0R40000041C082";
        initSequence[158] = "0R40000041D081";
        initSequence[159] = "0R40000041E080";
        initSequence[160] = "0R40000041F07F";
        initSequence[161] = "0Q10000000009E";
        initSequence[162] = "0R40000000009A";
        initSequence[163] = "0R400000001099";
        initSequence[164] = "0R400000002098";
        initSequence[165] = "0R400000003097";
        initSequence[166] = "0R400000004096";
        initSequence[167] = "0R400000005095";
        initSequence[168] = "0R400000006094";
        initSequence[169] = "0R400000007093";
        initSequence[170] = "0R400000008092";
        initSequence[171] = "0R400000009091";
        initSequence[172] = "0R40000000A089";
        initSequence[173] = "0R40000000B088";
        initSequence[174] = "0R40000000C087";
        initSequence[175] = "0R40000000D086";
        initSequence[176] = "0R40000000E085";
        initSequence[177] = "0R40000000F084";
        initSequence[178] = "0R400000010099";
        initSequence[179] = "0R400000011098";
        initSequence[180] = "0R400000012097";
        initSequence[181] = "0R400000013096";
        initSequence[182] = "0R400000014095";
        initSequence[183] = "0R400000015094";
        initSequence[184] = "0R400000016093";
        initSequence[185] = "0R400000017092";
        initSequence[186] = "0R400000018091";
        initSequence[187] = "0R400000019090";
        initSequence[188] = "0R40000001A088";
        initSequence[189] = "0R40000001B087";
        initSequence[190] = "0R40000001C086";
        initSequence[191] = "0R40000001D085";
        initSequence[192] = "0R40000001E084";
        initSequence[193] = "0R40000001F083";
        initSequence[194] = "0R40000680008C";
        initSequence[195] = "0R4000FFD4B044";
        initSequence[196] = "0R4000FFD46050";
        initSequence[197] = "0R400007C00080";
        initSequence[198] = "0R400006C0407D";
        initSequence[199] = "0n000000000082";
        initSequence[200] = "0R400006C0407D";
        initSequence[201] = "0n000000000082";
        initSequence[202] = "0R400006C0407D";
        initSequence[203] = "0n000000000082";
        initSequence[204] = "0R400006C0407D";
        initSequence[205] = "0n000000000082";
        initSequence[206] = "0R400006C0407D";
        initSequence[207] = "0n000000000082";
        initSequence[208] = "0R400006C0407D";
        initSequence[209] = "0n000000000082";
        initSequence[210] = "0R400006C0407D";
        initSequence[211] = "0n000000000082";
        initSequence[212] = "0R400006C0407D";
        initSequence[213] = "0n000000000082";
        initSequence[214] = "0R400006C0407D";
        initSequence[215] = "0n000000000082";
        initSequence[216] = "0R400006C0407D";
        initSequence[217] = "0n000000000082";
        initSequence[218] = "0R400006C0407D";
        initSequence[219] = "0n000000000082";
        initSequence[220] = "0R400006C0407D";
        initSequence[221] = "0n000000000082";
        initSequence[222] = "0R400006C0407D";
        initSequence[223] = "0n000000000082";
        initSequence[224] = "0R400006C0407D";
        initSequence[225] = "0n000000000082";
        initSequence[226] = "0R400006C0407D";
        initSequence[227] = "0n000000000082";
        initSequence[228] = "0R400006C0407D";
        initSequence[229] = "0n000000000082";
        initSequence[230] = "0R400006C0407D";
        initSequence[231] = "0n000000000082";
        initSequence[232] = "0R400006C0407D";
        initSequence[233] = "0n000000000082";
        initSequence[234] = "0R400006C0407D";
        initSequence[235] = "0n000000000082";
        initSequence[236] = "0R400006C0407D";
        initSequence[237] = "0n000000000082";
        initSequence[238] = "0R400006C0407D";
        initSequence[239] = "0n000000000082";
        initSequence[240] = "0R400006C0407D";
        initSequence[241] = "0n000000000082";
        initSequence[242] = "0R400006C0407D";
        initSequence[243] = "0n000000000082";
        initSequence[244] = "0R400006C0407D";
        initSequence[245] = "0n000000000082";
        initSequence[246] = "0R400006C0407D";
        initSequence[247] = "0n000000000082";
        initSequence[248] = "0R400006C0407D";
        initSequence[249] = "0n000000000082";
        initSequence[250] = "0R400006C0407D";
        initSequence[251] = "0n000000000082";
        initSequence[252] = "0R400006C0407D";
        initSequence[253] = "0n000000000082";
        initSequence[254] = "0R400006C0407D";
        initSequence[255] = "0n000000000082";
        initSequence[256] = "0R400006C0407D";
        initSequence[257] = "0n000000000082";
        initSequence[258] = "0R400006C0407D";
        initSequence[259] = "0n000000000082";
        initSequence[260] = "0R400006C0407D";
        initSequence[261] = "0n000000000082";
        initSequence[262] = "0R400006C0407D";
        initSequence[263] = "0n000000000082";
        initSequence[264] = "0R400006C0407D";
        initSequence[265] = "0n000000000082";
        initSequence[266] = "0R400006C0407D";
        initSequence[267] = "0n000000000082";
        initSequence[268] = "0R400006C0407D";
        initSequence[269] = "0n000000000082";
        initSequence[270] = "0R400006C0407D";
        initSequence[271] = "0n000000000082";
        initSequence[272] = "0R400006C0407D";
        initSequence[273] = "0n000000000082";
        initSequence[274] = "0R400006C0407D";
        initSequence[275] = "0n000000000082";
        initSequence[276] = "0R400006C0407D";
        initSequence[277] = "0n000000000082";
        initSequence[278] = "0R400006C0407D";
        initSequence[279] = "0n000000000082";
        initSequence[280] = "0R400006C0407D";
        initSequence[281] = "0n000000000082";
        initSequence[282] = "0R400006C0407D";
        initSequence[283] = "0n000000000082";
        initSequence[284] = "0R400006C0407D";
        initSequence[285] = "0n000000000082";
        initSequence[286] = "0R400006C0407D";
        initSequence[287] = "0n000000000082";
        initSequence[288] = "0R400006C0407D";
        initSequence[289] = "0n000000000082";
        initSequence[290] = "0R400006C0407D";
        initSequence[291] = "0n000000000082";
        initSequence[292] = "0R400006C0407D";
        initSequence[293] = "0n000000000082";
        initSequence[294] = "0R400006C0407D";
        initSequence[295] = "0n000000000082";
        initSequence[296] = "0R400006C0407D";
        initSequence[297] = "0n000000000082";
        initSequence[298] = "0R400006C0407D";
        initSequence[299] = "0n000000000082";
        initSequence[300] = "0R400006C0407D";
        initSequence[301] = "0n000000000082";
        initSequence[302] = "0R400006C0407D";
        initSequence[303] = "0n000000000082";
        initSequence[304] = "0R400006C0407D";
        initSequence[305] = "0n000000000082";
        initSequence[306] = "0R400006C0407D";
        initSequence[307] = "0n000000000082";
        initSequence[308] = "0R400006C0407D";
        initSequence[309] = "0n000000000082";
        initSequence[310] = "0R400006C0407D";
        initSequence[311] = "0n000000000082";
        initSequence[312] = "0R400006C0407D";
        initSequence[313] = "0n000000000082";
        initSequence[314] = "0R400006C0407D";
        initSequence[315] = "0n000000000082";
        initSequence[316] = "0R400006C0407D";
        initSequence[317] = "0n000000000082";
        initSequence[318] = "0R400006C0407D";
        initSequence[319] = "0n000000000082";
        initSequence[320] = "0R400006C0407D";
        initSequence[321] = "0n000000000082";
        initSequence[322] = "0R400006C0407D";
        initSequence[323] = "0n000000000082";
        initSequence[324] = "0R400006C0407D";
        initSequence[325] = "0n000000000082";
        initSequence[326] = "0R400006C0407D";
        initSequence[327] = "0n000000000082";
        initSequence[328] = "0R400006C0407D";
        initSequence[329] = "0n000000000082";
        initSequence[330] = "0R400006C0407D";
        initSequence[331] = "0n000000000082";
        initSequence[332] = "0R400006C0407D";
        initSequence[333] = "0n000000000082";
        initSequence[334] = "0R400006C0407D";
        initSequence[335] = "0n000000000082";
        initSequence[336] = "0R400006C0407D";
        initSequence[337] = "0n000000000082";
        initSequence[338] = "0R400006C0407D";
        initSequence[339] = "0n000000000082";
        initSequence[340] = "0R400006C0407D";
        initSequence[341] = "0n000000000082";
        initSequence[342] = "0R400006C0407D";
        initSequence[343] = "0n000000000082";
        initSequence[344] = "0R400006C0407D";
        initSequence[345] = "0n000000000082";
        initSequence[346] = "0R400006C0407D";
        initSequence[347] = "0n000000000082";

    }

    #endregion


    //Update is called once per frame
    //void Update()
    //{
    //    if (sendIt)
    //    {
    //        if (dataSent != "")
    //        {
    //            serial1.Write(dataSent);
    //            ReadSerialData(); hexaIn = ByteToHexBitFiddle(buf);
    //            System.Threading.Thread.Sleep(2000);
    //            serial1.Write(dataSent);
    //            ReadSerialData(); hexaIn = ByteToHexBitFiddle(buf);
    //            System.Threading.Thread.Sleep(2000);
    //            serial1.Write(dataSent);
    //            ReadSerialData(); hexaIn = ByteToHexBitFiddle(buf);
    //            sendIt = false;
    //        }
    //    }
    //}

    //void FixedUpdate()
    //{
    //    t += Time.fixedDeltaTime;
    //    if (sendIt)
    //    {
    //        if (dataSent != "")
    //        {
    //            t = 0;
    //            hexaIn = "";
    //            serial1.Write(dataSent);
    //            ReadSerialData(); hexaIn = ByteToHexBitFiddle(buf);
    //            System.Threading.Thread.Sleep(1000);
    //            hexaIn = "";
    //            serial1.Write(dataSent);
    //            ReadSerialData(); hexaIn = ByteToHexBitFiddle(buf);
    //            System.Threading.Thread.Sleep(1000);
    //            hexaIn = "";
    //            serial1.Write(dataSent);
    //            ReadSerialData(); hexaIn = ByteToHexBitFiddle(buf);
    //            Debug.Log(hexaIn[30]); Debug.Log(hexaIn[31]);
    //            while ((hexaIn[30] != '0' && hexaIn[31] != '3') && t < 0.050f) { Debug.Log("Wainting for 'U'"); };
    //            hexaIn = "";
    //            serial1.Write(dataSent);
    //            ReadSerialData(); hexaIn = ByteToHexBitFiddle(buf);
    //            sendIt = false;
    //        }
    //    }
    //}

    //void FixedUpdate()
    //{
    //    if (readyToSend)
    //    {
    //        if (dataSent != "")
    //        {
    //            t = 0;
    //            hexaIn = "";
    //            serial1.Write(dataSent);
    //            ReadSerialData(); hexaIn = ByteToHexBitFiddle(buf);
    //            System.Threading.Thread.Sleep(1000);
    //            hexaIn = "";
    //            serial1.Write(dataSent);
    //            ReadSerialData(); hexaIn = ByteToHexBitFiddle(buf);
    //            System.Threading.Thread.Sleep(1000);
    //            hexaIn = "";
    //            if (readyToSend)
    //            {
    //                StartCoroutine(SendReadData(dataSent));
    //            }
    //            if (readyToSend)
    //            {
    //                StartCoroutine(SendReadData(dataSent));
    //            }
    //            readyToSend = false;
    //        }
    //    }
    //}
}
