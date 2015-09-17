using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	private int damage;
	private bool enemy;
	private Vector2 direction;
	private Vector2 speed;
	private Vector2 movement;

	// Use this for initialization
	void Start () {
		damage = 1;
		enemy = false;
		Destroy (gameObject, 10);
		speed = new Vector2 (10, 10);
	}
	
	// Update is called once per frame
	void Update () {
		if (direction != null)
			movement = new Vector2 (speed.x * direction.x, speed.y * direction.y);
	}

	void FixedUpdate() {
		GetComponent<Rigidbody2D>().velocity = movement;
	}

	public void setDirection(Vector2 d){
		direction = d;
	}

	public bool isEnemy(){
		return enemy;
	}

	public void setEnemy(bool b){
		enemy = b;
	}

	public int damageInflicted(){
		return damage;
	}
}
