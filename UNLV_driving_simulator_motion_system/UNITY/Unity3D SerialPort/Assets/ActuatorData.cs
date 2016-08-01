using UnityEngine;
using System.Collections;
using System.Text;
using System;


public class ActuatorData : MonoBehaviour {


    //debug
    public int requetes_index = 0;
    public bool test1 = false;
    public bool testInit = false;

    //Useful for the initialisation
    public bool initDone = true;
    public int requests_index = 0;

    public static string raw_standby = '\u0002' + "" + '\u0003';


    public float timer = 0f;
	public bool simulatorGOO = false;
	public bool usePulses = true;
	public bool initAxes = true;
    public string sentData_raw;

	public enum Axe{X = 0, Y = 1, Z = 2};
	public Axe[] axe_raw = new Axe[] {Axe.X, Axe.Y, Axe.Z};
	
	public string[] initialPositionsAxes = new string[] {""+STX+"0o07000000007A"+ETX,""+STX+"1o070000000079"+ETX,""+STX+"2o070000000078"+ETX};
	public Axe _Axe = Axe.X;
	public byte _axe = 0;
	public enum Command{a,o};
	public Command _Command = Command.a;

	const char STX = '\u0002';
	const char ETX = '\u0003';

	public string _command = "";
	public float DegreePosition = 15;
	public float PulsesRev = 800; //Actualised variable by script_cube.cs, if "use pulse" is on.
	public float DegreeByPulse = 0.0017482517482517f; //35° / 20 000 pulses
	//public float DegreeByPulse = 0.45f;
	public string TargetPosition = "FFFFFFFF";
	public byte PlaceHolder = 0;
	public byte BCC = 0;
	
	public script_cube classcube; //this is to get the "angle_cube_x" variable from script_cube.cs
	public UnitySerialPort classSerialPort;

	public string raw = "";
	public string dataToSend = "";

	public GUIManager S_GUIManager;
    public UnitySerialPort unitySerialPort;

    public byte i;
	

	void Start () {
				string aa = CalculateChecksum("0T4000004000");
				Debug.Log(aa+"/   "+calculPulsesFromAngle(14));
				i = 0;


				classSerialPort.OpenSerialPort (); //vrai


				//init ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (Input.GetKeyUp (KeyCode.L)) {
            init ();
		}

        if (Input.GetKey(KeyCode.S))
        {
            simulatorGOO = !simulatorGOO;
        }

        DegreePosition = classcube.angle_cube_x;
		//Debug.Log ("from ActuatorsData.cs : DegreePosition " + DegreePosition);

        if (simulatorGOO) {
            timer += Time.fixedDeltaTime;
			//timer += Time.deltaTime;
            createRaw();
			if (timer > 0.8f) {
                S_GUIManager.OutputString = raw;
                S_GUIManager.sendData ();
                sentData_raw = raw;
				timer = 0f;
			}
        }
	}

	public void createRaw () { // this function create the string data

				_command = convertCommand(_Command);
				calculTargetPosition(DegreePosition);
				//Debug.Log("targetPos = "+ TargetPosition);
				raw = ""+_axe+""+_command+""+TargetPosition+""+getString2Numbers(PlaceHolder);
				string bcc = CalculateChecksum(raw);
				//Debug.Log("Bcc = "+ bcc);
				raw = STX+""+raw+""+bcc+""+ETX;
	}

		/*public void ouvrirPort (){
				if (classSerialPort.SerialPort == null)
				{ classSerialPort.OpenSerialPort(); return; }

				switch (classSerialPort.SerialPort.IsOpen)
				{
				case true: classSerialPort.CloseSerialPort(); break;
				case false: classSerialPort.OpenSerialPort(); break;
				}
		
		}*/


	public void init() {

        // Register reference to the UnitySerialPort. This
        // was defined in the scripts Awake function so we
        // know it is instantiated before this call.
        unitySerialPort = UnitySerialPort.Instance;

        //unitySerialPort.BuildInitSequence();
        //StartCoroutine(unitySerialPort.SendInitSequence(0, 320));//unitySerialPort.getInitSequenceLength()));
        setAxe(_Axe);
        S_GUIManager.OutputString = initialPositionsAxes[_axe];
        S_GUIManager.sendData();

        initAxes = false;

    }



    public void setAxe (Axe pAxe) {

				switch(pAxe) {
				case Axe.X:
						_axe = 0;
						break;
				case Axe.Y:
						_axe = 1;
						break;
				case Axe.Z:
						_axe = 2;
						break;
				default :
						Debug.LogError("Error convert Axe ! - ActuatorData");
						break;
				}
		}
	
	public void calculTargetPosition (float pAngle) {
				int pulses = calculPulsesFromAngle(pAngle);
				if(usePulses) {
					TargetPosition = convertPulsesInHEX((int)PulsesRev);
				}
				else {
					TargetPosition = convertPulsesInHEX(pulses);
				}
	}

	public string getString2Numbers (byte pByte) {
		string res = "";

		if(pByte > 9) {
			res = ""+pByte;
		}
		else {
			res = "0"+pByte;
		}
		return res;
	}

	public string convertCommand (Command pCommand) {
				string command = "";
				switch(pCommand) {
				case Command.a:
						command = "a";
						break;
				case Command.o:
						command = "o";
						break;
				default :
						Debug.LogError("Error convert Command ! - ActuatorData");
						break;
				}
				return command;
		}

	private string CalculateChecksum(string dataToCalculate)
		{
				byte[] byteToCalculate = Encoding.ASCII.GetBytes(dataToCalculate);

				int checksum = 0;

				foreach (byte chData in byteToCalculate)
				{
						checksum += chData;
				}

				//checksum &= 0xff;
				checksum = (0x100 - checksum) & 0xff;

				return checksum.ToString("X2");

		} 

	public int calculPulsesFromAngle (float pAngle) {

				return (int)(pAngle/DegreeByPulse);
		}
	
	public string convertPulsesInHEX (int pPulses) {

				int decValue = (int)pPulses;
				decValue = decValue*(-1);
				string hexValue = decValue.ToString("X");
				Debug.Log("hexvalue --------> "+ hexValue);
				return hexValue;
		}

}
