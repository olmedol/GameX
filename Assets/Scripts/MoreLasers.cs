using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoreLasers : Photon.MonoBehaviour {
	
	public GameObject[] players;
	private Text purchased;
	
	
	public void morelasers(){
		
		
		players = GameObject.FindGameObjectsWithTag ("Player");
		
		for (int i = 0; i < players.Length; i++) {
			
			if(players[i].GetComponent<PhotonView>().isMine){
				
				if(players[i].GetComponent<Perks>().perkPoints >= 6){
					players[i].GetComponent<Player>().laser3 = true;
					players[i].GetComponent<Perks>().perkPoints -=6;
					GameObject.Find ("MoreLasersbut").SetActive(false);
					purchased = GameObject.Find ("MLCost").GetComponent<Text> ();
					purchased.text = "Purchased!";
					break;
					
				}
			}
			
		}
		
		
	}
}