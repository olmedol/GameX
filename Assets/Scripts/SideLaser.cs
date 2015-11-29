/*
 *  @Author Eric Nursey 
 *  Function for the side laser perk
 * 
 *
 */
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SideLaser : Photon.MonoBehaviour {
	
	public GameObject[] players;
	private Text purchased;
	
	
	public void sidelaser(){
		
		//Ensures the perk is only applied to the controlling player
		players = GameObject.FindGameObjectsWithTag ("Player");
		
		for (int i = 0; i < players.Length; i++) {
			
			if(players[i].GetComponent<PhotonView>().isMine){
				//Checks if enough points are available, then adds the perk
				if(players[i].GetComponent<Perks>().perkPoints >= 4){
					players[i].GetComponent<Player>().laser2 = true;
					players[i].GetComponent<Perks>().perkPoints -=4;
					GameObject.Find ("SideLaserbut").SetActive(false);
					purchased = GameObject.Find ("SLCost").GetComponent<Text> ();
					purchased.text = "Purchased!";
					break;
					
				}
			}
			
		}
		
		
	}
}