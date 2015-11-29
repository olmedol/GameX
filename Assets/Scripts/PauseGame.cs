using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseGame : Photon.MonoBehaviour {

	public GameObject PauseGUI;
	public GameObject PerksMenu;
	private GameObject player;
	private Text Points;

	void Start(){
		Points = PerksMenu.transform.FindChild ("Points").GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {
		if (player == null) {
			foreach(GameObject p in GameObject.FindGameObjectsWithTag("Player"))
				if(p.GetComponent<PhotonView>().isMine){
					player = p;
					break;
				}
			if (Input.GetKeyDown ("escape")) {
				
				if(!LoadOnClick.onlineStatus)
					Time.timeScale = 0;
				PauseGUI.SetActive (true);
				
			} return;
		}
	

		Points.text = player.GetComponent<Perks> ().perkPoints.ToString ();

		//Check to see if the game is paused
		if (Time.timeScale == 1) {
			//Enable pause menu
			if (Input.GetKeyDown ("escape")) {

				if(!LoadOnClick.onlineStatus)
					Time.timeScale = 0;
				PauseGUI.SetActive (true);

			}
			//Enable perks menu
			if (Input.GetKeyDown ("p")) {
			
				if(!LoadOnClick.onlineStatus)
					Time.timeScale = 0;
				PerksMenu.SetActive (true);
			
			}
		}
		if(PerksMenu.GetActive()==true){

				//Exit from perks menu
				if (Time.timeScale == 0 || LoadOnClick.onlineStatus) {
					
					if(Input.GetKeyDown("escape")){
						
						PauseGUI.SetActive (false);
						PerksMenu.SetActive (false);
						Time.timeScale = 1;
						
					}
					
				}
				
				
				
				
			}

	
	}


}
