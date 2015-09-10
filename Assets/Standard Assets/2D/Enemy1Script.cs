using UnityEngine;
using System.Collections;

public class Enemy1Script : MonoBehaviour {
	public Transform target;
	public Vector2 speed = new Vector2(5, 5);
	private Vector2 movement;

	// Use this for initialization
	void Start () {
		target = GameObject.Find ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 direction = (target.position - transform.position).normalized;
		movement = new Vector2 (speed.x * direction.x, speed.y * direction.y);
	}

	void FixedUpdate() {
		GetComponent<Rigidbody2D>().velocity = movement;
	}
}
