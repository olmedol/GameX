using UnityEngine;
using System.Collections;

public class Asteroid : Photon.MonoBehaviour {
	private int speed;
	private Vector2 movement;
	private float timeToDestroy;

	// Use this for initialization
	void Start () {
		speed = Random.Range(3,20);
		timeToDestroy = 20f;
	}
	
	// Update is called once per frame
	void Update () {
		if (PhotonNetwork.isMasterClient) {
			Vector2 direction = new Vector2(Random.value, Random.value).normalized;
			movement = direction * speed;
			transform.Rotate(direction);
			Vector2 p = transform.position;
			Boundary b = GetComponent<Boundary>();
			if(p.x < b.minX || p.x > b.maxX || p.y < b.minY || p.y > b.maxY)
				timeToDestroy -= Time.deltaTime;
			else
				timeToDestroy = 20f;
			if(timeToDestroy < 0)
				;//PhotonNetwork.Destroy (GetComponent<PhotonView>());
		}

	}
	void FixedUpdate() {
		if (PhotonNetwork.isMasterClient)
			GetComponent<Rigidbody2D> ().velocity = movement;
	}
}
