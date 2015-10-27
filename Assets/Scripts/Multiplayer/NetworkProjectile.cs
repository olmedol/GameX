using UnityEngine;
using System.Collections;

public class NetworkProjectile : Photon.MonoBehaviour {
	private bool isAlive= true;
	Vector3 position;
	Quaternion rotation;
	float lerpSmoothing = 10f;
	// Use this for initialization
	
	
	void Start () {
		if (photonView.isMine) {
			GetComponent<Projectile> ().enabled = true;
			
		} else {
			StartCoroutine("Alive");
			
			//for network players
			//folks you're playing with
			
		}
	}
	
	////	// Update is called once per frame
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
		} else {
			position = (Vector3)stream.ReceiveNext();
			rotation =(Quaternion)stream.ReceiveNext();
		}
		
	}
	
	//
	IEnumerator Alive(){
		while(isAlive){
			transform.position = Vector3.Lerp (transform.position, position, Time.deltaTime * lerpSmoothing);
			transform.rotation = Quaternion.Lerp (transform.rotation, rotation, Time.deltaTime * lerpSmoothing);
			yield return null;
		}
		yield return null;
	}
}
