/*
 *  @Author Eric Nursey 
 *  Function for the damage up perk
 * 
 *
 */



using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageUp : Photon.MonoBehaviour {
	
	public GameObject[] players;
	private Text purchased;

	
	public void damageup(){
		
		//Ensures the perk is only applied to the controlling player
		players = GameObject.FindGameObjectsWithTag ("Player");
		
		for (int i = 0; i < players.Length; i++) {
			
			if(players[i].GetComponent<PhotonView>().isMine){
				//Checks if enough points are available, then adds the perk
				if(players[i].GetComponent<Perks>().perkPoints >= 2){
					players[i].GetComponent<Player>().dmg1 = true;
					players[i].GetComponent<Perks>().perkPoints -=2;
					GameObject.Find ("DamageUpbut").SetActive(false);
					purchased = GameObject.Find ("DUCost").GetComponent<Text> ();
					purchased.text = "Purchased!";
					break;
					
				}
			}
			
		}
		
		
	}
}