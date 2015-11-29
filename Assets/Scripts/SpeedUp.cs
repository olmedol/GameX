/*
 *  @Author Eric Nursey 
 *  Function for the speed up perk
 * 
 *
 */
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpeedUp : Photon.MonoBehaviour {

	public GameObject[] players;
	private Text purchased;

	public void speed(){

		//Ensures the perk is only applied to the controlling player
		players = GameObject.FindGameObjectsWithTag ("Player");

		for (int i = 0; i < players.Length; i++) {

			if(players[i].GetComponent<PhotonView>().isMine){
				//Checks if enough points are available, then adds the perk
				if(players[i].GetComponent<Perks>().perkPoints >= 1){
					players[i].GetComponent<Player>().maxspeed += 2.5f;
					players[i].GetComponent<Perks>().perkPoints -=1;
					GameObject.Find ("SpeedUpbut").SetActive(false);
					purchased = GameObject.Find ("SUCost").GetComponent<Text> ();
					purchased.text = "Purchased!";
				break;

				}
			}

		}


	}
}