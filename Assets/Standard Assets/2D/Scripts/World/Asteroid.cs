using UnityEngine;
using System.Collections;

public class Asteroid : Photon.MonoBehaviour {
	private int speed; //speed of the asteroid
	private Vector2 movement; //velocity vector
	private float timeToDestroy; //time outside bounds until asteroid is despawned
	private float minX, maxX, minY, maxY; //boundary that starts despawning process

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
			if(transform.position.x < minX || transform.position.x > maxX || transform.position.y < minY || transform.position.y > maxY)
				timeToDestroy -= Time.deltaTime;
			else
				timeToDestroy = 20f;
			if(timeToDestroy <= 0) //Asteroid is destroyed after 20 seconds out of bounds
				PhotonNetwork.Destroy (GetComponent<PhotonView>());
		}

	}

	// Update is called once per physics update
	void FixedUpdate() {
		if (PhotonNetwork.isMasterClient)
			GetComponent<Rigidbody2D> ().velocity = movement;
	}

	//Asteroid simply destroys any projectiles which collide with it
	void OnTriggerStay2D(Collider2D otherCollider){
		PhotonNetwork.Destroy (otherCollider.GetComponent<PhotonView>());;
	}
}
