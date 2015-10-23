using UnityEngine;
using System.Collections;

public class Player_NetworkSetup : NetworkBehavior {

	// Use this for initialization
	void Start () {
		if (isLocalPlayer) {
			GameObject.Find("Main Camera").SetActive(false);
		
		}
	}
	

}
