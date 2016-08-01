using UnityEngine;
using System.Collections;
using System.Text;

public class ActuatorData : MonoBehaviour {
	
	public float timer = 0f;
	public bool simulatorGOO = false;
	public bool usePulses = true;
	public bool initAxes = true;

	public enum Axe{X = 0, Y = 1, Z = 2};
	public Axe[] axe_raw = new Axe[] {Axe.X, Axe.Y, Axe.Z};
	
	public string[] initialPositionsAxes = new string[] {""+STX+"0o07000000007A"+ETX,""+STX+"1o070000000079"+ETX,""+STX+"2o070000000078"+ETX};
	public Axe _Axe = Axe.X;
	public byte _axe = 0;
	public enum Command{a,o};
	public Command _Command = Command.a;

	const char STX = '\u0002';
	const char ETX = '\u0003';


	///////////////// METTRE A JOUR LES VARIABLES ///

	public string _command = "";
	public float DegreePosition0 = 15;
	public float DegreePosition1 = 15;
	public float DegreePosition2 = 15;

	public float PulsesRev = 800; //Actualised variable by script_cube.cs, if "use pulse" is on.
	public float PulsesRev0 = 800;
	public float PulsesRev1 = 800;
	public float PulsesRev2 = 800;

	///////////////// FIN METTRE A JOUR LES VARIABLES ///

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

		public byte i;
	

	void Start () {
				string aa = CalculateChecksum("0T4000004000");
				//Debug.Log(aa+"/   "+calculPulsesFromAngle(14));
				i = 0;


				classSerialPort.OpenSerialPort (); //vrai


				//init ();
	}
	
	
	// Update is called once per frame
	void FixedUpdate () {

		//if(Input.GetKeyDown(KeyCode.Space)){
				//init ();

		//}

		/*if (Input.GetKey (KeyCode.RightArrow)){
				//classSerialPort.OpenSerialPort (); //vrai
				//classSerialPort.SerialPort.Open (); //ajout
				ouvrirPort();
		}*/
		
		if (Input.GetKey (KeyCode.Space)) {
				init ();	
		}

		
		////// RECUPERATION DES ANGLES DE LA VOITURE ///
		
		DegreePosition0 = classcube.angle_cube_x; //axe 0
		DegreePosition1 = classcube.angle_cube_z; //axe 1
		DegreePosition2 = classcube.angle_cube_y; //axe 2

		///// FIN RECUPERATION ///

		//Debug.Log ("from ActuatorsData.cs : DegreePosition " + DegreePosition);

		//if(Input.GetKeyUp(KeyCode.UpArrow)){
				//simulatorGOO = true;

		//}

				// ANCIEN TIMER TEST SUR UN SEUL AXE

				/*if (simulatorGOO) {
						timer += Time.fixedDeltaTime;

						createRaw0 ();
						if (timer > 0.8f) {
								

								S_GUIManager.OutputString = raw;
								S_GUIManager.sendData ();
								timer = 0f;
						} 



				} */
						
		
		///////////////////////////////////////////////// à ajouter
		// gestion du nouveau timer

		if (simulatorGOO) {
			timer += Time.deltaTime;
				

						if (timer <= 0.4f) {
								createRaw0 ();
								S_GUIManager.OutputString = raw;
								S_GUIManager.sendData ();
								
						} else if (timer > 0.4f && timer <= 0.8f) {
								createRaw1 ();
								S_GUIManager.OutputString = raw;
								S_GUIManager.sendData ();

						} else if (timer > 0.8f && timer <= 1.2f) {
								createRaw2 ();
								S_GUIManager.OutputString = raw;
								S_GUIManager.sendData ();

						}		
						else {
								timer = 0f;
						}

				}  

		/*Debug.Log ("DegreePosition0  " + DegreePosition0);
		Debug.Log ("DegreePosition1  " + DegreePosition1);
		Debug.Log ("DegreePosition2  " + DegreePosition2);

		Debug.Log ("PulsesRev0  " + PulsesRev0);
		Debug.Log ("PulsesRev1  " + PulsesRev1);
		Debug.Log ("PulsesRev2  " + PulsesRev2);
		*/

		//////////////////////////////////////////////////////


		

	}
	
		//IEnumerator waitMaGueule () {
				//yield return new WaitForSeconds(5f);
				//simulatorGOO = true;
		//}

	public void createRaw0 () { // this function create the string data

				_axe = 0;
				PulsesRev = PulsesRev0;  // changement ici  <------------------
				_command = convertCommand(_Command);
				calculTargetPosition(DegreePosition0);
				//Debug.Log("targetPos = "+ TargetPosition);
				raw = ""+_axe+""+_command+""+TargetPosition+""+getString2Numbers(PlaceHolder);
				string bcc = CalculateChecksum(raw);
				//Debug.Log("Bcc = "+ bcc);
				raw = STX+""+raw+""+bcc+""+ETX;

			
	}

	

//////////////// DUPLIQUER POUR LES DEUX AUTRES AXES

	public void createRaw1 () { // this function create the string data

				_axe = 1;
				PulsesRev = PulsesRev1;
				_command = convertCommand(_Command);
				calculTargetPosition(DegreePosition1);
				//Debug.Log("targetPos = "+ TargetPosition);
				raw = ""+_axe+""+_command+""+TargetPosition+""+getString2Numbers(PlaceHolder);
				string bcc = CalculateChecksum(raw);
				//Debug.Log("Bcc = "+ bcc);
				raw = STX+""+raw+""+bcc+""+ETX;


	}

	public void createRaw2 () { // this function create the string data

				_axe = 2;
				PulsesRev = PulsesRev2;
				_command = convertCommand(_Command);
				calculTargetPosition(DegreePosition2);
				//Debug.Log("targetPos = "+ TargetPosition);
				raw = ""+_axe+""+_command+""+TargetPosition+""+getString2Numbers(PlaceHolder);
				string bcc = CalculateChecksum(raw);
				//Debug.Log("Bcc = "+ bcc);
				raw = STX+""+raw+""+bcc+""+ETX;


	}

//////////////////////// FIN DUPLIQUER ////////////////////////////////////////////

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
				//classSerialPort.OpenSerialPort ();




				//setAxe(_Axe);
				//S_GUIManager.OutputString = initialPositionsAxes[_axe];

				S_GUIManager.OutputString = STX+"0o07000000007A"+ETX;
				S_GUIManager.sendData ();
				//yield return new WaitForSeconds(1);
				S_GUIManager.OutputString = STX+"1o070000000079"+ETX;
				S_GUIManager.sendData ();
				//yield return new WaitForSeconds(1);
				S_GUIManager.OutputString = STX+"2o070000000078"+ETX;
				S_GUIManager.sendData ();

				initAxes = false;
				//simulatorGOO = true;
				//StartCoroutine(waitMaGueule());

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
				//Debug.Log("hexvalue --------> "+ hexValue);
				return hexValue;
		}
	
}
