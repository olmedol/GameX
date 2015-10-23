using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


public class Player : NetworkBehaviour {

	[SerializeField] Camera FPSCharacterCam;
	// Use this for initialization
	void Start() 
	{
		if (isLocalPlayer) {
			GameObject.Find("Main Camera").SetActive(false);
			GetComponent<CharacterController>().enabled = true;
	
			FPSCharacterCam.enabled = true;
		}
	}


}