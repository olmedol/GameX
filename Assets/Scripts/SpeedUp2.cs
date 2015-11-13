using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpeedUp2 : Photon.MonoBehaviour {
	
	public GameObject[] players;
	private Text purchased;
	
	public void speed2(){
		
		
		players = GameObject.FindGameObjectsWithTag ("Player");
		
		for (int i = 0; i < players.Length; i++) {
			
			if(players[i].GetComponent<PhotonView>().isMine){
				
				if(players[i].GetComponent<Perks>().perkPoints >= 4){
					players[i].GetComponent<Player>().maxspeed += 2.5f;
					players[i].GetComponent<Perks>().perkPoints -=4;
					GameObject.Find ("SpeedUp2but").SetActive(false);
					purchased = GameObject.Find ("SU2Cost").GetComponent<Text> ();
					purchased.text = "Purchased!";
					break;
					
				}
			}
			
		}
		
		
	}
}