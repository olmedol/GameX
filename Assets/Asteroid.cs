using UnityEngine;
using System.Collections;

public class Asteroid : Photon.MonoBehaviour {
	private int speed;
	private Vector2 movement;

	// Use this for initialization
	void Start () {
		speed = Random.Range(3,20);
	}
	
	// Update is called once per frame
	void Update () {
		if (PhotonNetwork.isMasterClient) {
			Vector2 direction = new Vector2(Random.value, Random.value);
			movement = direction * speed;
			transform.Rotate(direction);
			
		}

	}
	void FixedUpdate() {
		if (PhotonNetwork.isMasterClient)
			GetComponent<Rigidbody2D> ().velocity = movement;
	}
}
