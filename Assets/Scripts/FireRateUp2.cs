using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FireRateUp2 : Photon.MonoBehaviour {
	
	public GameObject[] players;
	private Text purchased;
	
	
	public void firerateup2(){
		
		
		players = GameObject.FindGameObjectsWithTag ("Player");
		
		for (int i = 0; i < players.Length; i++) {
			
			if(players[i].GetComponent<PhotonView>().isMine){
				
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