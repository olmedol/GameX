/*
 *  @Author Eric Nursey 
 *  Function for the damage up 2 perk
 * 
 *
 */
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageUp2 : Photon.MonoBehaviour {
	
	public GameObject[] players;
	private Text purchased;
	
	
	public void damageup2(){
		
		//Ensures the perk is only applied to the controlling player
		players = GameObject.FindGameObjectsWithTag ("Player");
		
		for (int i = 0; i < players.Length; i++) {
			
			if(players[i].GetComponent<PhotonView>().isMine){
				//Checks if enough points are available, then adds the perk
				if(players[i].GetComponent<Perks>().perkPoints >= 5){
					players[i].GetComponent<Player>().dmg2 = true;
					players[i].GetComponent<Perks>().perkPoints -=5;
					GameObject.Find ("DamageUp2but").SetActive(false);
					purchased = GameObject.Find ("DU2Cost").GetComponent<Text> ();
					purchased.text = "Purchased!";
					break;
					
				}
			}
			
		}
		
		
	}
}