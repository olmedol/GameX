using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FireRateUp1 : Photon.MonoBehaviour {
	
	public GameObject[] players;
	private Text purchased;
	
	
	public void firerateup1(){
		
		
		players = GameObject.FindGameObjectsWithTag ("Player");
		
		for (int i = 0; i < players.Length; i++) {
			
			if(players[i].GetComponent<PhotonView>().isMine){
				
				if(players[i].GetComponent<Perks>().perkPoints >= 2){
					players[i].GetComponent<ProjectileSpawner>().rateOfFire *= 0.5f;
					players[i].GetComponent<Perks>().perkPoints -=2;
					GameObject.Find ("FireRate1but").SetActive(false);
					purchased = GameObject.Find ("FR1Cost").GetComponent<Text> ();
					purchased.text = "Purchased!";
					break;
					
				}
			}
			
		}
		
		
	}
}