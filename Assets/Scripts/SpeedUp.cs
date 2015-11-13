using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpeedUp : Photon.MonoBehaviour {

	public GameObject[] players;
	private Text purchased;

	public void speed(){


		players = GameObject.FindGameObjectsWithTag ("Player");

		for (int i = 0; i < players.Length; i++) {

			if(players[i].GetComponent<PhotonView>().isMine){
			
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