using UnityEngine;
using System.Collections;

public class Asteroid : Photon.MonoBehaviour {
	private int speed;
	private Vector2 movement;
	private float timeToDestroy;
	private float minX, maxX, minY, maxY;

	// Use this for initialization
	void Start () {
		speed = Random.Range(3,10);
		timeToDestroy = 20f;
		Boundary b = GameObject.FindWithTag ("Respawn").GetComponent<Boundary> ();
		minX = b.minX; maxX = b.maxX; minY = b.minY; maxY = b.maxY;
	}
	
	// Update is called once per frame
	void Update () {
		if (PhotonNetwork.isMasterClient) {
			Vector2 direction = new Vector2(Random.value, Random.value).normalized;
			movement = direction * speed;
			transform.Rotate(Vector3.forward);
			Vector2 p = transform.position;
			if(transform.position.x < minX || transform.position.x > maxX || transform.position.y < minY || transform.position.y > maxY)
				timeToDestroy -= Time.deltaTime;
			else
				timeToDestroy = 20f;
			if(timeToDestroy < 0)
				PhotonNetwork.Destroy (GetComponent<PhotonView>());
		}

	}

	void FixedUpdate() {
		if (PhotonNetwork.isMasterClient)
			GetComponent<Rigidbody2D> ().velocity = movement;
	}

	void OnTriggerStay2D(Collider2D otherCollider){
		PhotonNetwork.Destroy (otherCollider.GetComponent<PhotonView>());;
	}
}
