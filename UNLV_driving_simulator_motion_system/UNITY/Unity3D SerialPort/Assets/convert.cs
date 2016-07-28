using UnityEngine;
using System.Collections;

public class convert : MonoBehaviour {

		public int decValue = 800;
	// Use this for initialization
	void Start () {
				string hexValue = decValue.ToString("X");
				//Convert.ToInt64(hexValue, 16);
				//string.format("{0:x}", decValue);
				Debug.Log("decValue -> "+ hexValue);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
