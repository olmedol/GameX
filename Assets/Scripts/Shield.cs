/*
 *  @Author Eric Nursey 
 *  Function for the shield perk
 * 
 *
 */
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Shield : Photon.MonoBehaviour {
	
	public GameObject[] players;
	private Text purchased;
	
	
	public void shield(){
		
		//Ensures the perk is only applied to the controlling player
		players = GameObject.FindGameObjectsWithTag ("Player");
		
		for (int i = 0; i < players.Length; i++) {
			
			if(players[i].GetComponent<PhotonView>().isMine){
				//Checks if enough points are available, then adds the perk
				if(players[i].GetComponent<Perks>().perkPoints >= 6){
					players[i].GetComponent<Player>().shield = true;
					players[i].GetComponent<Perks>().perkPoints -=6;
					GameObject.Find ("Shieldbut").SetActive(false);
					purchased = GameObject.Find ("SCost").GetComponent<Text> ();
					purchased.text = "Purchased!";
					break;
					
				}
			}
			
		}
		
		
	}
}