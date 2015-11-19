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
			return;
		}
	

		Points.text = player.GetComponent<Perks> ().perkPoints.ToString ();

			//Points.text (player.GetComponent<Perks>().perkPoints.ToString ());
		if (Time.timeScale == 1) {

			if (Input.GetKeyDown ("escape")) {

				Time.timeScale = 0;
				PauseGUI.SetActive (true);

			}

			if (Input.GetKeyDown ("p")) {
			
				Time.timeScale = 0;
				PerksMenu.SetActive (true);
			
			}
		}
		if(PerksMenu.GetActive()==true){


				if (Time.timeScale == 0) {
					
					if(Input.GetKeyDown("escape")){
						
						PauseGUI.SetActive (false);
						PerksMenu.SetActive (false);
						Time.timeScale = 1;
						
					}
					
				}
				
				
				
				
			}

	
	}


}
