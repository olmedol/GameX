using UnityEngine;
using System.Collections;

public class Laser : Projectile {
	private float speed;
	
	// Use this for initialization
	void Start () {
		damage = 1;
		Destroy (gameObject, 10);
		speed = 20;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void FixedUpdate() {
		GetComponent<Rigidbody2D> ().velocity = transform.right * speed;
	}
	
}
