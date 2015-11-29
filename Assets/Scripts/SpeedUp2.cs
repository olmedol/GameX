/*
 *  @Author Eric Nursey 
 *  Function for the speed up 2 perk
 * 
 *
 */
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpeedUp2 : Photon.MonoBehaviour {
	
	public GameObject[] players;
	private Text purchased;
	
	public void speed2(){
		
		//Ensures the perk is only applied to the controlling player
		players = GameObject.FindGameObjectsWithTag ("Player");
		
		for (int i = 0; i < players.Length; i++) {
			
			if(players[i].GetComponent<PhotonView>().isMine){
				//Checks if enough points are available, then adds the perk
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