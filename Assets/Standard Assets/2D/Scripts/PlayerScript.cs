using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	private int health;
	private Vector2 speed;
	private Vector2 movement;
	
	// Use this for initialization
	void Start () {
		health = 1;
		speed = new Vector2 (15, 15);
	}
	
	// Update is called once per frame
	void Update () {
		movement = new Vector2 (speed.x * Input.GetAxis ("Horizontal"), speed.y * Input.GetAxis ("Vertical"));
		if (health < 1)
			Destroy (gameObject);
	}
	
	void FixedUpdate() {
		GetComponent<Rigidbody2D>().velocity = movement;
	}

	void OnTriggerEnter2D(Collider2D otherCollider){
		Projectile p = otherCollider.gameObject.GetComponent<Projectile>();
		if (p != null) {
			if(p.isEnemy()){
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