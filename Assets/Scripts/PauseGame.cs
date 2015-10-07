using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {

	public GameObject PauseGUI;
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown ("escape")) {

			Time.timeScale = 0;
			PauseGUI.SetActive(true);





		}
	
	}
}
