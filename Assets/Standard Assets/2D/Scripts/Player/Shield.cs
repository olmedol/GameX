using UnityEngine;
using System.Collections;

//this class is a shell for easier interaction with a player's child shield object
public class Shield : Photon.MonoBehaviour {
	private Transform shield;

	// Use this for initialization
	void Start () {
		foreach (Transform child in transform)
			if (child.tag == "Shield")
				shield = child;
		GetComponent<PhotonView>().RPC ("shieldActive", PhotonTargets.All, false);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public bool shieldActive(){
		return shield.gameObject.activeSelf;
	}

	[PunRPC]
	public void shieldActive(bool b){
		shield.gameObject.SetActive (b);
	}
}
