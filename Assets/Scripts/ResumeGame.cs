using UnityEngine;
using System.Collections;

public class ResumeGame : MonoBehaviour {

	public GameObject PauseGUI;

	public void resumeGame(){

		Time.timeScale = 1;
		PauseGUI.SetActive (false);


	}




}
