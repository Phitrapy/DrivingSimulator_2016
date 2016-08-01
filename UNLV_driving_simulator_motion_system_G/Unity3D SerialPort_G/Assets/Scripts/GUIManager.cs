using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour 
{
    public static GUIManager Instance;

    private UnitySerialPort unitySerialPort;

    public GUISkin GUISkin;

    public string OutputString = "";

    private string PortOpenStatus;

	const char STX = '\u0002';
	const char ETX = '\u0003';

    void Awake ()
    {
        Instance = this;
    }

	void Start () 
    {
        // Register reference to the UnitySerialPort. This
        // was defined in the scripts Awake function so we
        // know it is instantiated before this call.

        unitySerialPort = UnitySerialPort.Instance;
	}
	
	void Update () 
    {
        // Check to see if we have a serial port defined
        // and if not then return.

        if (unitySerialPort.SerialPort == null)
        {
            PortOpenStatus = "Open Port"; return;
        }

        // Check to see if the serial port is open or not
        // and then set the button text "PortOpenStatus" 
        // to reflect.

        switch (unitySerialPort.SerialPort.IsOpen)
        {
            case true: PortOpenStatus = "Close Port"; break;
            case false: PortOpenStatus = "Open Port"; break;
        }

	    // Here we have some sample usage scenarios that
        // demo the operation of the UnitySerialPort. In
        // order to use these you must first ensure that
        // the custom inputs are defined via:

        // Edit > Project Settings > Input

        if (Input.GetButtonDown("SendData"))
        { unitySerialPort.SendSerialDataAsLine(OutputString); }
	}

    void OnGUI ()
    {
        // If we have defined a GUI Skin via the unity 
        // editor then apply it.

        if (GUISkin != null) { GUI.skin = GUISkin; }

        // Draw an area to hold the GUI content.

        GUILayout.BeginArea(new Rect(10, 10, 200, 200), "", GUI.skin.box);

        // Draw a button that can be used to open or
        // close the serial port.

        if (GUILayout.Button(PortOpenStatus, GUILayout.Height(30)))
        {
            if (unitySerialPort.SerialPort == null)
            { unitySerialPort.OpenSerialPort(); return; }

            switch (unitySerialPort.SerialPort.IsOpen)
            {
                case true: unitySerialPort.CloseSerialPort(); break;
                case false: unitySerialPort.OpenSerialPort(); break;
            }
        }

        // Draw a title for the input textfield

        GUILayout.Label("Input string");

        // Draw a textfield that can be used to show 
        // data sent via the serial port to unity.

        GUILayout.TextField(unitySerialPort.RawData,GUILayout.Height(20));

        // Provide some padding to seperate

        GUILayout.Space(20);

        // Draw a title for the output textfield

        GUILayout.Label("Output string");

        // Draw a textfield that can be used to define 
        // data to be sent via the serial port

        OutputString = GUILayout.TextField(OutputString, GUILayout.Height(20));

        // Draw a button that can be used to send serial
        // data from the unity environment.

        if (GUILayout.Button("Send Data", GUILayout.Height(30)))
        {
			sendData ();
        }

        // Thats it we are finished so lets close the area

        GUILayout.EndArea();
    }

		public void sendData () {
				if (unitySerialPort.SerialPort.IsOpen) {
						unitySerialPort.SendSerialDataAsLine (OutputString);
			}
				else {
						Debug.LogError ("le port n'ai pas ouvert boloss");
				}
		}
}
