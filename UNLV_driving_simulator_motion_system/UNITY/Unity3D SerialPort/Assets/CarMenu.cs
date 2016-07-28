using UnityEngine;
using System.Collections;

public class CarMenu : MonoBehaviour {
	
	//public AutoCam S_autoCam;
	public GameObject carCamera, carCollection, carMenu;
	
	public Transform[] NodesCamera;
	
	public AudioSource AmbianceSound;

	void Start() {

		racingCamera (false);
		if(AmbianceSound != null) {
			AmbianceSound.Play();
		}
		LaunchAnimation ();

	}

	IEnumerator WaitAndPrint(float waitTime) {
				
		yield return new WaitForSeconds(2f);
		//S_autoCam.target = NodesCamera[0];
		yield return new WaitForSeconds(waitTime);
		//S_autoCam.target = NodesCamera[1];
		yield return new WaitForSeconds(waitTime);
		//S_autoCam.target = NodesCamera[2];
		yield return new WaitForSeconds(waitTime);
		//S_autoCam.target = NodesCamera[3];
		yield return new WaitForSeconds(waitTime);
		//S_autoCam.target = NodesCamera[0];
		yield return new WaitForSeconds(waitTime);
		//S_autoCam.target = NodesCamera[1];
		yield return new WaitForSeconds(waitTime);
		//S_autoCam.target = NodesCamera[2];
		yield return new WaitForSeconds(waitTime);
		//S_autoCam.target = NodesCamera[3];
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("q")) {
			racingCamera (true);
		}
		if(Input.GetKeyDown("m")) {
			racingCamera (false);
		}
	}

	public void racingCamera (bool pBool) {

		if(pBool) {
			carCollection.SetActive(true);
			carCamera.SetActive(true);
			carMenu.SetActive(false);
		}
		else {
			carCollection.SetActive(false);
			carCamera.SetActive(false);
			carMenu.SetActive(true);
		}
	}
	
		public void LaunchAnimation () {

				StartCoroutine(WaitAndPrint(10F));
		}
}
