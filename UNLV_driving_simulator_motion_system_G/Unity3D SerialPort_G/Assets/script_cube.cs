using UnityEngine;
using System.Collections;

public class script_cube : MonoBehaviour {

	public GameObject cube;

    public const float ROTATION_ANGLE = 4f;
        
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
	void FixedUpdate () {
        readAllAngles();
        adaptAllAngles();
        updateSentPulses();

			

			//mooving cube with arrow
			//axe 0
			if (Input.GetKey(KeyCode.LeftArrow)){  //to the left
					transform.Rotate(ROTATION_ANGLE * Time.fixedDeltaTime,0,0);
			}
			if (Input.GetKey(KeyCode.RightArrow)){  //to the right
					transform.Rotate(-ROTATION_ANGLE * Time.fixedDeltaTime,0,0);
			}

			//axe 1
			if (Input.GetKey(KeyCode.UpArrow)){  //to the left
					transform.Rotate(0,0,ROTATION_ANGLE * Time.fixedDeltaTime);
			}
			if (Input.GetKey(KeyCode.DownArrow)){  //to the right
					transform.Rotate(0,0,-ROTATION_ANGLE * Time.fixedDeltaTime);
			}

			//axe 2
			if (Input.GetKey(KeyCode.B)){  //to the left
					transform.Rotate(0, ROTATION_ANGLE * Time.fixedDeltaTime, 0);
			}
			if (Input.GetKey(KeyCode.N)){  //to the right
					transform.Rotate(0,-ROTATION_ANGLE * Time.fixedDeltaTime, 0);
			}
	}

    #region CUBE ANGLE AND TRANSFORMATION

    /// <summary>
    /// Reads the angle of the axis 0 of the game object
    /// </summary>
    void adaptAngle_0() {
        if (angle_cube_x >= 0 && angle_cube_x <= 17.5)
        {
            angle_cube_x = 17.5f - angle_cube_x;
        }
        else if (angle_cube_x >= 342.5 && angle_cube_x <= 360)
        {
            angle_cube_x = 360f - angle_cube_x + 17.5f;
        }
        else if (angle_cube_x == 0)
        {
            angle_cube_x = 17.5f;
        }
        else
        {
            angle_cube_x = 0f;
        }
    }

    /// <summary>
    /// Adapts the angle of the axis 1 of the game object
    /// </summary>
    void adaptAngle_1()
    {
        if (angle_cube_z >= 0 && angle_cube_z <= 17.5)
        {
            angle_cube_z = 17.5f - angle_cube_z;
        }
        else if (angle_cube_z >= 342.5 && angle_cube_z <= 360)
        {
            angle_cube_z = 360f - angle_cube_z + 17.5f;
        }
        else if (angle_cube_z == 0)
        {
            angle_cube_z = 17.5f;
        }
        else
        {
            angle_cube_z = 0f;
        }
    }

    /// <summary>
    /// Adapts the angle of the axis 2 of the game object
    /// </summary>
    void adaptAngle_2()
    {
        if (angle_cube_y >= 0 && angle_cube_y <= 17.5)
        {
            angle_cube_y = 17.5f - angle_cube_y;
        }
        else if (angle_cube_y >= 342.5 && angle_cube_y <= 360)
        {
            angle_cube_y = 360f - angle_cube_y + 17.5f;
        }
        else if (angle_cube_y == 0)
        {
            angle_cube_y = 17.5f;
        }
        else
        {
            angle_cube_y = 0f;
        }
    }

    /// <summary>
    /// Adapts the angles of all the axis of the game object
    /// </summary>
    void adaptAllAngles()
    {
        adaptAngle_0();
        adaptAngle_1();
        adaptAngle_2();
    }

    /// <summary>
    ///  Adapts the angles of all the axis of the game object
    /// </summary>
    void readAllAngles()
    {
        angle_cube_x = cube.transform.eulerAngles.x;
        angle_cube_y = cube.transform.eulerAngles.y;
        angle_cube_z = cube.transform.eulerAngles.z; // axis 1
    }

    /// <summary>
    /// Converts the angleS of the object into pulses and sends it to ActuatorData.cs
    /// </summary>
    void updateSentPulses() {
        //Conversion in pulses (ratio = 35° / 20 000 pulses)
        //This is if you encoche the "Use pulses" square
        angle_in_pulse_x = angle_cube_x / 0.0017f;
        majActuatorData.PulsesRev0 = angle_in_pulse_x;  //sending to ActuatorData.cs

        angle_in_pulse_y = angle_cube_y / 0.0017f;
        majActuatorData.PulsesRev2 = angle_in_pulse_y;  //sending to ActuatorData.cs

        angle_in_pulse_z = angle_cube_z / 0.0017f;
        majActuatorData.PulsesRev1 = angle_in_pulse_z;  //sending to ActuatorData.cs
    }

    #endregion

}
