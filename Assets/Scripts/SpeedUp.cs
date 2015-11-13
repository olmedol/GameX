using UnityEngine;
using System.Collections;

public class SpeedUp : Photon.MonoBehaviour {

	public GameObject[] players;

	public void speed(){

		players = GameObject.FindGameObjectsWithTag ("Player");

		for (int i = 0; i < players.Length; i++) {

			if(players[i].GetComponent<PhotonView>().isMine){

				players[i].GetComponent<Player>().maxspeed += 2.5f;
				break;
			}

		}
		//GameObject.FindWithTag ("Player").GetComponent<Player> ().maxspeed += 2.5f;
			//GameObject.Find ("Perks").GetComponent<Perks> ().perkPoints -= 1;

	}
}