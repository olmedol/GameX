/*
 *  @Author Eric Nursey 
 *  Function for the dual laser perk
 * 
 *
 */
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DualLaser : Photon.MonoBehaviour {
	
	public GameObject[] players;
	private Text purchased;
	
	
	public void duallaser(){
		
		//Ensures the perk is only applied to the controlling player
		players = GameObject.FindGameObjectsWithTag ("Player");
		
		for (int i = 0; i < players.Length; i++) {
			
			if(players[i].GetComponent<PhotonView>().isMine){
				//Checks if enough points are available, then adds the perk
				if(players[i].GetComponent<Perks>().perkPoints >= 1){
					players[i].GetComponent<Player>().laser1 = true;
					players[i].GetComponent<Perks>().perkPoints -=1;
					GameObject.Find ("DualLaserbut").SetActive(false);
					purchased = GameObject.Find ("DLCost").GetComponent<Text> ();
					purchased.text = "Purchased!";
					break;
					
				}
			}
			
		}
		
		
	}
}