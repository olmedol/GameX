using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DamageUp : Photon.MonoBehaviour {
	
	public GameObject[] players;
	private Text purchased;

	
	public void damageup(){
		
		
		players = GameObject.FindGameObjectsWithTag ("Player");
		
		for (int i = 0; i < players.Length; i++) {
			
			if(players[i].GetComponent<PhotonView>().isMine){
				
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