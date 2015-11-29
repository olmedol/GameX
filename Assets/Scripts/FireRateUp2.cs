/*
 *  @Author Eric Nursey 
 *  Function for the fire rate up 2 perk
 * 
 *
 */
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FireRateUp2 : Photon.MonoBehaviour {
	
	public GameObject[] players;
	private Text purchased;
	
	
	public void firerateup2(){
		
		//Ensures the perk is only applied to the controlling player
		players = GameObject.FindGameObjectsWithTag ("Player");
		
		for (int i = 0; i < players.Length; i++) {
			
			if(players[i].GetComponent<PhotonView>().isMine){
				//Checks if enough points are available, then adds the perk
				if(players[i].GetComponent<Perks>().perkPoints >= 5){
					players[i].GetComponent<ProjectileSpawner>().rateOfFire *= 0.5f;
					players[i].GetComponent<Perks>().perkPoints -=5;
					GameObject.Find ("FireRate2but").SetActive(false);
					purchased = GameObject.Find ("FR2Cost").GetComponent<Text> ();
					purchased.text = "Purchased!";
					break;
					
				}
			}
			
		}
		
		
	}
}