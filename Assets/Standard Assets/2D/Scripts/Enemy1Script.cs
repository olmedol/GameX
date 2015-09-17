using UnityEngine;
using System.Collections;

public class Enemy1Script : MonoBehaviour {
	private int health;
	private Vector2 speed;
	private Vector2 movement;
	private Transform target;

	// Use this for initialization
	void Start () {
		health = 1;
		speed = new Vector2 (4, 4);
		target = GameObject.Find ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 direction = (target.position - transform.position).normalized;
		movement = new Vector2 (speed.x * direction.x, speed.y * direction.y);
		if (health < 1) {
			Destroy (gameObject);
		}
	}

	void FixedUpdate() {
		GetComponent<Rigidbody2D>().velocity = movement;
	}

	void OnTriggerEnter2D(Collider2D otherCollider){
		Projectile p = otherCollider.gameObject.GetComponent<Projectile>();
		if (p != null) {
			if(!p.isEnemy()){
				Destroy (p.gameObject);
				health -= p.damageInflicted();
			}
		}
	}

	void OnCollisionEnter2D(){
		health -= 1;
	}

	public Vector2 Direction(){
		return movement.normalized;
	}
}
