using UnityEngine;
using System.Collections;

public class RamEnemy : MonoBehaviour {
	private Vector2 speed;
	private Vector2 movement;
	private Transform target;

	// Use this for initialization
	void Start () {
		speed = new Vector2 (6, 6);
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

	public Vector2 Direction(){
		return movement.normalized;
	}
}
