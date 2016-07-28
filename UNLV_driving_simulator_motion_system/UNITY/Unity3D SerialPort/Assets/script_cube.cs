using UnityEngine;
using System.Collections;

public class script_cube : MonoBehaviour {

		public GameObject cube;
		//public ActuatorData test_angle;
		public float angle_cube_x = 0;
		public float angle_in_pulse_x = 0;
		public ActuatorData majActuatorData;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
			angle_cube_x = cube.transform.eulerAngles.x;
			//Debug.Log ("EnPulses = " +test_angle.calculPulsesFromAngle (17f));
			//Debug.Log ("from /script_cube.cs : angle_cube_x = " + cube.transform.eulerAngles.x);
			//Debug.Log ("from /script_cube.cs : TargetPosition = " + test_angle.calculPulsesFromAngle(angle_cube_x));


				//convert angle
				if (angle_cube_x >= 0 && angle_cube_x <= 17.5) {
						angle_cube_x = 17.5f - angle_cube_x;
				} else if (angle_cube_x >= 342.5 && angle_cube_x <= 360) {
						angle_cube_x = 360f - angle_cube_x + 17.5f;
				} else if (angle_cube_x == 0) {
						angle_cube_x = 17.5f;
				} else {
						angle_cube_x = 0f;
				}


				//Conversion in pulses (ratio = 35° / 20 000 pulses)
				//This is if you encoche the "Use pulses" square
				angle_in_pulse_x = angle_cube_x/0.0017f;
				majActuatorData.PulsesRev = angle_in_pulse_x;  //sending to ActuatorData.cs



				//mooving cube with arrow
				if (Input.GetKey(KeyCode.LeftArrow)){  //to the left
						transform.Rotate(0.3f,0,0);
				}
				if (Input.GetKey(KeyCode.RightArrow)){  //to the right
						transform.Rotate(-0.3f,0,0);
				}



	
	}
}
