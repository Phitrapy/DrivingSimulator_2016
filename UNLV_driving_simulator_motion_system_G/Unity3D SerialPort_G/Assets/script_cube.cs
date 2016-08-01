using UnityEngine;
using System.Collections;

public class script_cube : MonoBehaviour {

		public GameObject cube;
		//public ActuatorData test_angle;

		public float angle_cube_x = 0;  //0
		public float angle_cube_y = 0;  //2
		public float angle_cube_z = 0;  //1

		public float angle_in_pulse_x = 0;
		public float angle_in_pulse_y = 0;
		public float angle_in_pulse_z = 0;

		public ActuatorData majActuatorData;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
			angle_cube_x = cube.transform.eulerAngles.x;
			angle_cube_y = cube.transform.eulerAngles.y;
			angle_cube_z = cube.transform.eulerAngles.z; //axe 1

			//Debug.Log ("angle_cube_y = " + angle_cube_y);
			//Debug.Log ("angle_cube_z = " + angle_cube_z);
			
			//Debug.Log ("EnPulses = " +test_angle.calculPulsesFromAngle (17f));
			//Debug.Log ("from /script_cube.cs : angle_cube_x = " + cube.transform.eulerAngles.x);
			//Debug.Log ("from /script_cube.cs : TargetPosition = " + test_angle.calculPulsesFromAngle(angle_cube_x));


				//convert angle axe 0
				if (angle_cube_x >= 0 && angle_cube_x <= 17.5) {
						angle_cube_x = 17.5f - angle_cube_x;
				} else if (angle_cube_x >= 342.5 && angle_cube_x <= 360) {
						angle_cube_x = 360f - angle_cube_x + 17.5f;
				} else if (angle_cube_x == 0) {
						angle_cube_x = 17.5f;
				} else {
						angle_cube_x = 0f;
				}

				//convert angle axe 2
				if (angle_cube_y >= 0 && angle_cube_y <= 17.5) {
						angle_cube_y = 17.5f - angle_cube_y;
				} else if (angle_cube_y >= 342.5 && angle_cube_y <= 360) {
						angle_cube_y = 360f - angle_cube_y + 17.5f;
				} else if (angle_cube_y == 0) {
						angle_cube_y = 17.5f;
				} else {
						angle_cube_y = 0f;
				}

				//convert angle axe 1
				if (angle_cube_z >= 0 && angle_cube_z <= 17.5) {
						angle_cube_z = 17.5f - angle_cube_z;
				} else if (angle_cube_z >= 342.5 && angle_cube_z <= 360) {
						angle_cube_z = 360f - angle_cube_z + 17.5f;
				} else if (angle_cube_z == 0) {
						angle_cube_z = 17.5f;
				} else {
						angle_cube_z = 0f;
				}


				//Conversion in pulses (ratio = 35° / 20 000 pulses)
				//This is if you encoche the "Use pulses" square
				angle_in_pulse_x = angle_cube_x/0.0017f;
				majActuatorData.PulsesRev0 = angle_in_pulse_x;  //sending to ActuatorData.cs

				angle_in_pulse_y = angle_cube_y/0.0017f;
				majActuatorData.PulsesRev2 = angle_in_pulse_y;  //sending to ActuatorData.cs

				angle_in_pulse_z = angle_cube_z/0.0017f;
				majActuatorData.PulsesRev1 = angle_in_pulse_z;  //sending to ActuatorData.cs


				//Debug.Log ("angle_in_pulse_z = " + angle_in_pulse_z);

				//mooving cube with arrow
				//axe 0
				if (Input.GetKey(KeyCode.LeftArrow)){  //to the left
						transform.Rotate(0.3f,0,0);
				}
				if (Input.GetKey(KeyCode.RightArrow)){  //to the right
						transform.Rotate(-0.3f,0,0);
				}

				//axe 1
				if (Input.GetKey(KeyCode.UpArrow)){  //to the left
						transform.Rotate(0,0,0.3f);
				}
				if (Input.GetKey(KeyCode.DownArrow)){  //to the right
						transform.Rotate(0,0,-0.3f);
				}

				//axe 2
				if (Input.GetKey(KeyCode.B)){  //to the left
						transform.Rotate(0,0.3f,0);
				}
				if (Input.GetKey(KeyCode.N)){  //to the right
						transform.Rotate(0,-0.3f,0);
				}








	
	}
}
