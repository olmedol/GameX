using UnityEngine;
using System.Collections;

public class RamEnemy : MonoBehaviour {
	private float speed;
	private Vector2 movement;
	private Transform target;

	// Use this for initialization
	void Start () {
		speed = 4.5f;
		target = GameObject.FindWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (PhotonNetwork.isMasterClient) {
			Vector2 direction = (target.position - transform.position).normalized;
			movement = direction * speed;
		}
	}

	void FixedUpdate() {
		if (PhotonNetwork.isMasterClient)
			GetComponent<Rigidbody2D>().velocity = movement;
	}
	
}
