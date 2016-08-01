using UnityEngine;
using System.Collections;

using System.IO;
using System.IO.Ports;
using System;

using System.Threading;

public class UnitySerialPort : MonoBehaviour 
{
    //debug
    public string sentData;
    private float timeOut = 0.150f;

    //Usefull
    private string previouslySentData = "";
    private string[] initSequence = new string[350];
    int bufSize = 16;
    public byte[] buf = new byte[16];
    public int bufCount = 0;
    public int sum;
    private bool readyToSend = false;
    private string hexaIn = "";

    public int getInitSequenceLength() {
        return initSequence.Length;
    }




    // Init a static reference if script is to be accessed by others when used in a 
    // none static nature eg. its dropped onto a gameObject. The use of "Instance"
    // allows access to public vars as such as those available to the unity editor.
    public static UnitySerialPort Instance;

    #region Properties

		const char STX = '\u0002';
		const char ETX = '\u0003';

	public string dataToSend = "";

    // The serial port
    public SerialPort SerialPort;

    // The script update can run as either a seperate thread
    // or as a standard coroutine. This can be selected via 
    // the unity editor.

    public enum LoopUpdateMethod
    { Threading, Coroutine }

    // This is the public property made visible in the editor.
    public LoopUpdateMethod UpdateMethod = 
        LoopUpdateMethod.Threading;

    // Thread used to recieve and send serial data
    private Thread serialThread;

    // List of all baudrates available to the arduino platform
    private ArrayList baudRates =
        new ArrayList() { 300, 600, 1200, 2400, 4800, 9600, 14400, 19200, 28800, 38400, 57600, 115200 };

    // List of all com ports available on the system
    private ArrayList comPorts =
        new ArrayList();

    // If set to true then open the port when the start
    // event is called.
    public bool OpenPortOnStart = true;

    // Holder for status report information
    private string portStatus = "";
    public string PortStatus
    {
        get { return portStatus; }
        set { portStatus = value; }
    }

    // Current com port and set of default
    public string ComPort = "COM3";

    // Current baud rate and set of default
    public int BaudRate = 38400;

    public int ReadTimeout = 10;

    public int WriteTimeout = 10;

    // Property used to run/keep alive the serial thread loop
    private bool isRunning = false;
    public bool IsRunning
    {
        get { return isRunning; }
        set { isRunning = value; }
    }

    // Set the gui to show ready
    public string rawData = "Ready";
    public string RawData
    {
        get { return rawData; }
        set { rawData = value; }
    }
    
    // Storage for parsed incoming data
    private string[] chunkData;
    public string[] ChunkData
    {
        get { return chunkData; }
        set { chunkData = value; }
    }

    // Refs populated by the editor inspector for default gui
    // functionality if script is to be used in a non-static
    // context.
    public GameObject ComStatusText;

    public GameObject RawDataText;

    #endregion Properties

    #region Unity Frame Events

    /// <summary>
    /// The awake call is used to populate refs to the gui elements used in this 
    /// example. These can be removed or replaced if needed with bespoke elements.
    /// This will not affect the functionality of the system. If we are using awake
    /// then the script is being run non staticaly ie. its initiated and run by 
    /// being dropped onto a gameObject, thus enabling the game loop events to be 
    /// called e.g. start, update etc.
    /// </summary>
    void Awake()
    {
        // Define the script Instance
        Instance = this;

        // If we have used the editor inspector to populate any included gui
        // elements then lets initiate them and set some default values.

        // Details if the port is open or closed
        if (ComStatusText != null)
        { ComStatusText.GetComponent<GUIText>().text = "ComStatus: Closed"; }
    }

    void GameObjectSerialPort_DataRecievedEvent(string[] Data, string RawData)
    {
        print("Data Recieved: " + RawData);
    }

    /// <summary>
    /// The start call is used to populate a list of available com ports on the
    /// system. The correct port can then be selected via the respective guitext
    /// or a call to UpdateComPort();
    /// </summary>
    void Start()
    {
        // Build the init Sequence
        BuildInitSequence();
        //StartCoroutine(SendInitSequence(0,initSequence.Length));

        // Population of comport list via system.io.ports
        PopulateComPorts();

        // If set to true then open the port. You must 
        // ensure that the port is valid etc. for this! 
        if (OpenPortOnStart) { OpenSerialPort(); }
			
    }

    /// <summary>
    /// The update frame call is used to provide caps for sending data to the arduino
    /// triggered via keypress. This can be replaced via use of the static functions
    /// SendSerialData() & SendSerialDataAsLine(). Additionaly this update uses the
    /// RawData property to update the gui. Again this can be removed etc.
    /// </summary>
    void Update()
    {
        // Check if the serial port exists and is open
        if (SerialPort == null || SerialPort.IsOpen == false) { return; }

        // Example calls from system to the arduino. For more detail on the
        try
        {
            // Example of sending space press event to arduino
            if (Input.GetKeyDown("space"))
			//if (Input.GetKey(KeyCode.UpArrow))
			{ SerialPort.WriteLine(dataToSend);/*SerialPort.WriteLine("");*/ }

            // Example of sending key 1 press event to arduino.
            // The "A,1" string will call functionA and pass a
            // char value of 1
            if (Input.GetKeyDown(KeyCode.Alpha1))
            { SerialPort.WriteLine("A,1"); }

            // Example of sending key 1 press event to arduino.
            // The "A,2" string will call functionA and pass a
            // char value of 2
            if (Input.GetKeyDown(KeyCode.Alpha2))
            { SerialPort.WriteLine("A,2"); }
        }
        catch (Exception ex)
        {
            // Failed to send serial data
            Debug.Log("Error 6: " + ex.Message.ToString());
        }

        try
        {
            // If we have set a GUI Text object then update it. This can only be
            // run on the thread that initialised the object thus cnnot be run
            // in the ParseSerialData() call below... Unless run as a coroutine!

            // I have also included a raw data example which is called from a
            // seperate script... see RawDataExample.cs

            if (RawDataText != null)
                RawDataText.GetComponent<GUIText>().text = RawData;
        }
        catch (Exception ex)
        {
            // Failed to update serial data
            Debug.Log("Error 7: " + ex.Message.ToString());
        }
    }

    /// <summary>
    /// Clean up the thread and close the port on application close event.
    /// </summary>
    void OnApplicationQuit()
    {
        // Call to cloase the serial port
        CloseSerialPort();

        Thread.Sleep(500);

        if (UpdateMethod == LoopUpdateMethod.Threading)
        {
            // Call to end and cleanup thread
            StopSerialThread();
        }

        if (UpdateMethod == LoopUpdateMethod.Coroutine)
        {
            // Call to end and cleanup coroutine
            StopSerialCoroutine();
        }

        Thread.Sleep(500);
    }

    #endregion Unity Frame Events

    #region Object Serial Port

    /// <summary>
    /// Opens the defined serial port and starts the serial thread used
    /// to catch and deal with serial events.
    /// </summary>
    public void OpenSerialPort()
    {
        try
        {
            // Initialise the serial port
            SerialPort = new SerialPort(ComPort, BaudRate);

            SerialPort.ReadTimeout = ReadTimeout;

            SerialPort.WriteTimeout = WriteTimeout;

            // Open the serial port
            SerialPort.Open();

            // Update the gui if applicable
            if (Instance != null && Instance.ComStatusText != null)
            { Instance.ComStatusText.GetComponent<GUIText>().text = "ComStatus: Open"; }

            if (UpdateMethod == LoopUpdateMethod.Threading)
            {
                // If the thread does not exist then start it
                if (serialThread == null)
                { StartSerialThread(); }
            }

            if (UpdateMethod == LoopUpdateMethod.Coroutine)
            {
                if (isRunning == false)
                {
                    StartSerialCoroutine();
                }
                else
                {
                    isRunning = false;

                    // Give it chance to timeout
                    Thread.Sleep(100);

                    try
                    {
                        // Kill it just in case
                        StopCoroutine("SerialCoroutineLoop");
                    }
                    catch(Exception ex)
                    {
                        print("Error N: " + ex.Message.ToString());
                    }

                    // Restart it once more
                    StartSerialCoroutine();
                }
            }

            print("SerialPort successfully opened!");
            

        }
        catch (Exception ex)
        {
            // Failed to open com port or start serial thread
            Debug.Log("Error 1: " + ex.Message.ToString());
        }
    }

    /// <summary>
    /// Cloases the serial port so that changes can be made or communication
    /// ended.
    /// </summary>
    public void CloseSerialPort()
    {
        try
        {
            // Close the serial port
            SerialPort.Close();

            // Update the gui if applicable
            if (Instance.ComStatusText != null)
            { Instance.ComStatusText.GetComponent<GUIText>().text = "ComStatus: Closed"; }
        }
        catch (Exception ex)
        {
            if (SerialPort == null || SerialPort.IsOpen == false)
            {
                // Failed to close the serial port. Uncomment if
                // you wish but this is triggered as the port is
                // already closed and or null.

                // Debug.Log("Error 2A: " + "Port already closed!");
            }
            else
            {
                // Failed to close the serial port
                Debug.Log("Error 2B: " + ex.Message.ToString());
            }
        }

        print("Serial port closed!");
    }

    #endregion Object Serial Port

    #region Serial Coroutine

    /// <summary>
    /// Function used to start coroutine for reading serial 
    /// data.
    /// </summary>
    public void StartSerialCoroutine()
    {
        isRunning = true;

        StartCoroutine("SerialCoroutineLoop");
    }

    /// <summary>
    /// A Coroutine used to recieve serial data thus not 
    /// affecting generic unity playback etc.
    /// </summary>
    public IEnumerator SerialCoroutineLoop()
    {
        while (isRunning)
        {
            GenericSerialLoop();
            yield return null;
        }

        print("Ending Coroutine!");
    }

    /// <summary>
    /// Function used to stop the coroutine and kill
    /// off any instance
    /// </summary>
    public void StopSerialCoroutine()
    {
        isRunning = false;

        Thread.Sleep(100);

        try
        {
            StopCoroutine("SerialCoroutineLoop");
        }
        catch (Exception ex)
        {
            print("Error 2A: " + ex.Message.ToString());
        }

        // Reset the serial port to null
        if (SerialPort != null)
        { SerialPort = null; }

        // Update the port status... just in case :)
        portStatus = "Ended Serial Loop Coroutine!";

        print("Ended Serial Loop Coroutine!");
    }

    #endregion Serial Coroutine

    #region Serial Thread

    /// <summary>
    /// Function used to start seperate thread for reading serial 
    /// data.
    /// </summary>
    public void StartSerialThread()
    {
        try
        {
            // define the thread and assign function for thread loop
            serialThread = new Thread(new ThreadStart(SerialThreadLoop));
            // Boolean used to determine the thread is running
            isRunning = true;
            // Start the thread
            serialThread.Start();

            print("Serial thread started!");
        }
        catch (Exception ex)
        {
            // Failed to start thread
            Debug.Log("Error 3: " + ex.Message.ToString());
        }
    }

    /// <summary>
    /// The serial thread loop. A Seperate thread used to recieve
    /// serial data thus not affecting generic unity playback etc.
    /// </summary>
    private void SerialThreadLoop()
    {
        while (isRunning)
        { GenericSerialLoop(); }

        print("Ending Thread!");
    }

    /// <summary>
    /// Function used to stop the serial thread and kill
    /// off any instance
    /// </summary>
    public void StopSerialThread()
    {
        // Set isRunning to false to let the while loop
        // complete and drop out on next pass
        isRunning = false;

        // Pause a little to let this happen
        Thread.Sleep(100);

        // If the thread still exists kill it
        // A bit of a hack using Abort :p
        if (serialThread != null)
        {
            serialThread.Abort();
            // serialThread.Join();
            Thread.Sleep(100);
            serialThread = null;
        }

        // Reset the serial port to null
        if (SerialPort != null)
        { SerialPort = null; }

        // Update the port status... just in case :)
        portStatus = "Ended Serial Loop Thread";

        print("Ended Serial Loop Thread!");
    }

    #endregion Serial Thread 

    #region Static Functions

    /// <summary>
    /// Function used to send string data over serial with
    /// an included line return
    /// </summary>
    /// <param name="data">string</param>
    public void SendSerialDataAsLine(string data)
    {
        if (SerialPort != null)
        //{ SerialPort.WriteLine(data); }
        { SerialPort.Write(data); }

        //print("Sent data: " + data);
        sentData = data;
        Debug.Log("Sent data: " + data);
    }

    /// <summary>
    /// Function used to send string data over serial without
    /// a line return included.
    /// </summary>
    /// <param name="data"></param>
    public void SendSerialData(string data)
    {
        if (SerialPort != null) // && data != previouslySentData)
        { SerialPort.Write(data); }
        print("Sent data: " + data);
        previouslySentData = data;
        ReadSerialData();
    }

    public void ReadSerialData()
    {
        bufCount = 0;
        sum = 0;
        bufCount += SerialPort.Read(buf, sum, 16 - sum);
        System.Threading.Thread.Sleep(1000);
    }


    public void BuildInitSequence() {
        initSequence[0] = "0ptrw0300000B0";
        initSequence[1] = "0T40000000908F";
        initSequence[2] = "0W400000014090";
        initSequence[3] = "0T40000000C085";
        initSequence[4] = "0W400000003092";
        initSequence[5] = "1ptrw0300000AF";
        initSequence[6] = "1T40000000908E";
        initSequence[7] = "1W40000001408F";
        initSequence[8] = "1T40000000C084";
        initSequence[9] = "1W400000003091";
        initSequence[10] = "2ptrw0300000AE";
        initSequence[11] = "2T40000000908D";
        initSequence[12] = "2W40000001408E";
        initSequence[13] = "2T40000000C083";
        initSequence[14] = "2W400000003090";
        initSequence[15] = "3ptrw0300000AD";
        initSequence[16] = "4ptrw0300000AC";
        initSequence[17] = "5ptrw0300000AB";
        initSequence[18] = "6ptrw0300000AA";
        initSequence[19] = "7ptrw0300000A9";
        initSequence[20] = "8ptrw0300000A8";
        initSequence[21] = "9ptrw0300000A7";
        initSequence[22] = "Aptrw03000009F";
        initSequence[23] = "Bptrw03000009E";
        initSequence[24] = "Cptrw03000009D";
        initSequence[25] = "Dptrw03000009C";
        initSequence[26] = "Eptrw03000009B";
        initSequence[27] = "Fptrw03000009A";
        initSequence[28] = "";
        initSequence[29] = "0Q10100000009D";
        initSequence[30] = "0R400000400096";
        initSequence[31] = "0R400000401095";
        initSequence[32] = "0R400000402094";
        initSequence[33] = "0R400000403093";
        initSequence[34] = "0R400000404092";
        initSequence[35] = "0R400000405091";
        initSequence[36] = "0R400000406090";
        initSequence[37] = "0R40000040708F";
        initSequence[38] = "0R40000040808E";
        initSequence[39] = "0R40000040908D";
        initSequence[40] = "0R40000040A085";
        initSequence[41] = "0R40000040B084";
        initSequence[42] = "0R40000040C083";
        initSequence[43] = "0R40000040D082";
        initSequence[44] = "0R40000040E081";
        initSequence[45] = "0R40000040F080";
        initSequence[46] = "0R400000410095";
        initSequence[47] = "0R400000411094";
        initSequence[48] = "0R400000412093";
        initSequence[49] = "0R400000413092";
        initSequence[50] = "0R400000414091";
        initSequence[51] = "0R400000415090";
        initSequence[52] = "0R40000041608F";
        initSequence[53] = "0R40000041708E";
        initSequence[54] = "0R40000041808D";
        initSequence[55] = "0R40000041908C";
        initSequence[56] = "0R40000041A084";
        initSequence[57] = "0R40000041B083";
        initSequence[58] = "0R40000041C082";
        initSequence[59] = "0R40000041D081";
        initSequence[60] = "0R40000041E080";
        initSequence[61] = "0R40000041F07F";
        initSequence[62] = "0Q10000000009E";
        initSequence[63] = "0R40000000009A";
        initSequence[64] = "0R400000001099";
        initSequence[65] = "0R400000002098";
        initSequence[66] = "0R400000003097";
        initSequence[67] = "0R400000004096";
        initSequence[68] = "0R400000005095";
        initSequence[69] = "0R400000006094";
        initSequence[70] = "0R400000007093";
        initSequence[71] = "0R400000008092";
        initSequence[72] = "0R400000009091";
        initSequence[73] = "0R40000000A089";
        initSequence[74] = "0R40000000B088";
        initSequence[75] = "0R40000000C087";
        initSequence[76] = "0R40000000D086";
        initSequence[77] = "0R40000000E085";
        initSequence[78] = "0R40000000F084";
        initSequence[79] = "0R400000010099";
        initSequence[80] = "0R400000011098";
        initSequence[81] = "0R400000012097";
        initSequence[82] = "0R400000013096";
        initSequence[83] = "0R400000014095";
        initSequence[84] = "0R400000015094";
        initSequence[85] = "0R400000016093";
        initSequence[86] = "0R400000017092";
        initSequence[87] = "0R400000018091";
        initSequence[88] = "0R400000019090";
        initSequence[89] = "0R40000001A088";
        initSequence[90] = "0R40000001B087";
        initSequence[91] = "0R40000001C086";
        initSequence[92] = "0R40000001D085";
        initSequence[93] = "0R40000001E084";
        initSequence[94] = "0R40000001F083";
        initSequence[95] = "0R40000680008C";
        initSequence[96] = "0R4000FFD4B044";
        initSequence[97] = "0R4000FFD46050";
        initSequence[98] = "0R400007C00080";
        initSequence[99] = "0R400006C0307E";
        initSequence[100] = "0Q10100000009D";
        initSequence[101] = "0R400000400096";
        initSequence[102] = "0R400000401095";
        initSequence[103] = "0R400000402094";
        initSequence[104] = "0R400000403093";
        initSequence[105] = "0R400000404092";
        initSequence[106] = "0R400000405091";
        initSequence[107] = "0R400000406090";
        initSequence[108] = "0R40000040708F";
        initSequence[109] = "0R40000040808E";
        initSequence[110] = "0R40000040908D";
        initSequence[111] = "0R40000040A085";
        initSequence[112] = "0R40000040B084";
        initSequence[113] = "0R40000040C083";
        initSequence[114] = "0R40000040D082";
        initSequence[115] = "0R40000040E081";
        initSequence[116] = "0R40000040F080";
        initSequence[117] = "0R400000410095";
        initSequence[118] = "0R400000411094";
        initSequence[119] = "0R400000412093";
        initSequence[120] = "0R400000413092";
        initSequence[121] = "0R400000414091";
        initSequence[122] = "0R400000415090";
        initSequence[123] = "0R40000041608F";
        initSequence[124] = "0R40000041708E";
        initSequence[125] = "0R40000041808D";
        initSequence[126] = "0R40000041908C";
        initSequence[127] = "0R40000041A084";
        initSequence[128] = "0R40000041B083";
        initSequence[129] = "0R40000041C082";
        initSequence[130] = "0R40000041D081";
        initSequence[131] = "0R40000041E080";
        initSequence[132] = "0R40000041F07F";
        initSequence[133] = "0Q10000000009E";
        initSequence[134] = "0R40000000009A";
        initSequence[135] = "0R400000001099";
        initSequence[136] = "0R400000002098";
        initSequence[137] = "0R400000003097";
        initSequence[138] = "0R400000004096";
        initSequence[139] = "0R400000005095";
        initSequence[140] = "0R400000006094";
        initSequence[141] = "0R400000007093";
        initSequence[142] = "0R400000008092";
        initSequence[143] = "0R400000009091";
        initSequence[144] = "0R40000000A089";
        initSequence[145] = "0R40000000B088";
        initSequence[146] = "0R40000000C087";
        initSequence[147] = "0R40000000D086";
        initSequence[148] = "0R40000000E085";
        initSequence[149] = "0R40000000F084";
        initSequence[150] = "0R400000010099";
        initSequence[151] = "0R400000011098";
        initSequence[152] = "0R400000012097";
        initSequence[153] = "0R400000013096";
        initSequence[154] = "0R400000014095";
        initSequence[155] = "0R400000015094";
        initSequence[156] = "0R400000016093";
        initSequence[157] = "0R400000017092";
        initSequence[158] = "0R400000018091";
        initSequence[159] = "0R400000019090";
        initSequence[160] = "0R40000001A088";
        initSequence[161] = "0R40000001B087";
        initSequence[162] = "0R40000001C086";
        initSequence[163] = "0R40000001D085";
        initSequence[164] = "0R40000001E084";
        initSequence[165] = "0R40000001F083";
        initSequence[166] = "0R40000680008C";
        initSequence[167] = "0R4000FFD4B044";
        initSequence[168] = "0R4000FFD46050";
        initSequence[169] = "0R400007C00080";
        initSequence[170] = "0R400006C0407D";
        initSequence[171] = "0n000000000082";
        initSequence[172] = "0R400006C0407D";
        initSequence[173] = "0n000000000082";
        initSequence[174] = "0R400006C0407D";
        initSequence[175] = "0n000000000082";
        initSequence[176] = "0R400006C0407D";
        initSequence[177] = "0n000000000082";
        initSequence[178] = "0R400006C0407D";
        initSequence[179] = "0n000000000082";
        initSequence[180] = "0R400006C0407D";
        initSequence[181] = "0n000000000082";
        initSequence[182] = "0R400006C0407D";
        initSequence[183] = "0n000000000082";
        initSequence[184] = "0R400006C0407D";
        initSequence[185] = "0n000000000082";
        initSequence[186] = "0R400006C0407D";
        initSequence[187] = "0n000000000082";
        initSequence[188] = "0R400006C0407D";
        initSequence[189] = "0n000000000082";
        initSequence[190] = "0R400006C0407D";
        initSequence[191] = "0n000000000082";
        initSequence[192] = "0R400006C0407D";
        initSequence[193] = "0n000000000082";
        initSequence[194] = "0R400006C0407D";
        initSequence[195] = "0n000000000082";
        initSequence[196] = "0R400006C0407D";
        initSequence[197] = "0n000000000082";
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
        initSequence[320] = "0o07000000007A";
    }

    public IEnumerator SendInitSequence(int requestIndex, int size)
    {
        if (requestIndex == 0) {
            readyToSend = false;
            SerialPort.Write(initSequence[requestIndex]);
            ReadSerialData(); hexaIn = ByteToHexBitFiddle(buf);
            System.Threading.Thread.Sleep(100);
            SerialPort.Write(initSequence[requestIndex]);
            ReadSerialData(); hexaIn = ByteToHexBitFiddle(buf);
            System.Threading.Thread.Sleep(100);
            readyToSend = true;
            StartCoroutine(SendInitSequence(1, size));
            yield return new WaitForSeconds(.1f);
        }
        else if (requestIndex < size)
        {
            float t = 0;
            int iteration = 0;
            readyToSend = false;
            hexaIn = "";
            System.Threading.Thread.Sleep(40);
            while ((hexaIn[30] != '0' && hexaIn[31] != '3') || iteration < 2) {
                try
                {
                    t = 0;
                    SerialPort.Write(initSequence[requestIndex]);
                    while((hexaIn[30] != '0' && hexaIn[31] != '3') || t < 0.75f)
                    {
                        try
                        {
                            ReadSerialData(); hexaIn = ByteToHexBitFiddle(buf);
                        }
                        catch
                        {
                        }
                        t += Time.deltaTime;
                    }
                    iteration ++;
                }
                catch {Debug.Log("Timed out!!"); }
            }
            readyToSend = true;
            StartCoroutine(SendInitSequence(requestIndex + 1, size));
            yield return new WaitForSeconds(.001f);
        }  
    }

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

    #endregion Static Functions

    /// <summary>
    /// The serial thread loop & the coroutine loop both utilise
    /// the same code with the exception of the null return on 
    /// the coroutine, so we share it here.
    /// </summary>
    private void GenericSerialLoop()
    {
        try
        {
            // Check that the port is open. If not skip and do nothing
            if (SerialPort.IsOpen)
            {
                // Read serial data until a '\n' character is recieved
                //string rData = SerialPort.ReadLine();

                // Read serial data until a '\u03' (ETX) character is recieved
                //string rData = SerialPort.ReadTo("");

                // Another attempt
                SerialPort.Read(buf, sum, 16 - sum); //
                string rData = ByteToHexBitFiddle(buf);


                // If the data is valid then do something with it
                if (rData != null && rData != "")
                {
                    // Store the raw data
                    RawData = rData;
                    // split the raw data into chunks via ',' and store it
                    // into a string array
                    ChunkData = RawData.Split(',');

                    // Or you could call a function to do something with
                    // data e.g.
                    ParseSerialData(ChunkData, RawData);
                }
            }
        }
        catch (TimeoutException timeout)
        {
            // This will be triggered lots with the coroutine method
        }
        catch (Exception ex)
        {
            // This could be thrown if we close the port whilst the thread 
            // is reading data. So check if this is the case!
            if (SerialPort.IsOpen)
            {
                // Something has gone wrong!
                Debug.Log("Error 4: " + ex.Message.ToString());
            }
            else
            {
                // Error caused by closing the port whilst in use! This is 
                // not really an error but uncomment if you wish.

                // Debug.Log("Error 5: Port Closed Exception!");
            }
        }
    }

    /// <summary>
    /// Function used to filter and act upon the data recieved. You can add
    /// bespoke functionality here.
    /// </summary>
    /// <param name="data">string[] of raw data seperated into chunks via ','</param>
    /// <param name="rawData">string of raw data</param>
    private void ParseSerialData(string[] data, string rawData)
    {
        // Examples of reading a value from the recieved data
        // for use if required - remove or replase with bespoke
        // functionality etc

        if (data.Length == 2)
        { int ReceviedValue = int.Parse(data[1]); }
        else { print(rawData); }

        if (data == null || data.Length != 2)
        { print(rawData); }

        // The following can be run if the code is run via the coroutine method.

        //if (RawDataText != null)
        //    RawDataText.guiText.text = RawData;
    }

    /// <summary>
    /// Function that utilises system.io.ports.getportnames() to populate
    /// a list of com ports available on the system.
    /// </summary>
    public void PopulateComPorts()
    {
        // Loop through all available ports and add them to the list
        foreach (string cPort in System.IO.Ports.SerialPort.GetPortNames())
        {
            comPorts.Add(cPort); // Debug.Log(cPort.ToString());
        }

        // Update the port status just in case :)
        portStatus = "ComPort list population complete";
    }

    /// <summary>
    /// Function used to update the current selected com port
    /// </summary>
    public string UpdateComPort()
    {
        // If open close the existing port
        if (SerialPort != null && SerialPort.IsOpen)
        { CloseSerialPort(); }

        // Find the current id of the existing port within the
        // list of available ports
        int currentComPort = comPorts.IndexOf(ComPort);

        // check against the list of ports and get the next one.
        // If we have reached the end of the list then reset to zero.
        if (currentComPort + 1 <= comPorts.Count - 1)
        {
            // Inc the port by 1 to get the next port
            ComPort = (string)comPorts[currentComPort + 1];
        }
        else
        {
            // We have reached the end of the list reset to the
            // first available port.
            ComPort = (string)comPorts[0];
        }

        // Update the port status just in case :)
        portStatus = "ComPort set to: " + ComPort.ToString();

        // Return the new ComPort just in case
        return ComPort;
    }

    /// <summary>
    /// Function used to update the current baudrate
    /// </summary>
    public int UpdateBaudRate()
    {
        // If open close the existing port
        if (SerialPort != null && SerialPort.IsOpen)
        { CloseSerialPort(); }

        // Find the current id of the existing rate within the
        // list of defined baudrates
        int currentBaudRate = baudRates.IndexOf(BaudRate);

        // check against the list of rates and get the next one.
        // If we have reached the end of the list then reset to zero.
        if (currentBaudRate + 1 <= baudRates.Count - 1)
        {
            // Inc the rate by 1 to get the next rate
            BaudRate = (int)baudRates[currentBaudRate + 1];
        }
        else
        {
            // We have reached the end of the list reset to the
            // first available rate.
            BaudRate = (int)baudRates[0];
        }

        // Update the port status just in case :)
        portStatus = "BaudRate set to: " + BaudRate.ToString();

        // Return the new BaudRate just in case
        return BaudRate;
    }

}
