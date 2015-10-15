using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	private int health; //Player's health
	private Vector2 speed; //Player's top speed
	private Vector2 movement; //Player's current velocity
	private float invulnTime; //Amount of time until the player can be hurt again after being damaged
	private float damageCooldown; //Time until player can be damaged
	private Vector2 playerPos; //Position of the player
	private Vector2 mousePos; //Position of the mouse
	private float angle; //Angle of the Player's sprite
	
	// Use this for initialization
	void Start () {
		health = 5;
		speed = new Vector2 (5, 5);
		invulnTime = 1f;
		damageCooldown = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		movement = new Vector2 (speed.x * Input.GetAxis ("Horizontal"), speed.y * Input.GetAxis ("Vertical"));;
		playerPos =  Camera.main.WorldToScreenPoint(transform.localPosition);
		mousePos = new Vector2 (Input.mousePosition.x - playerPos.x, Input.mousePosition.y - playerPos.y);
		angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
		transform.localEulerAngles = new Vector3(0, 0, angle);

		if (health < 1)
			Destroy (gameObject);

		if (damageCooldown > 0)
			damageCooldown -= Time.deltaTime;

	}
	
	void FixedUpdate() {
		GetComponent<Rigidbody2D>().velocity = movement;
	}

	void OnTriggerStay2D(Collider2D otherCollider){
		if (damageCooldown > 0)
			return;
		Projectile p = otherCollider.gameObject.GetComponent<Projectile>();
		if (p != null) {
			if(p.isEnemy() == true){
				health -= p.damageInflicted();
				Destroy (p.gameObject);
				Component RemoveHealth = GameObject.Find("Main Camera").GetComponent("GUIManager");
				RemoveHealth.SendMessage("AdjustCurrentHealth",-20);
				damageCooldown = invulnTime;
			}
		}
	}

	void OnCollisionStay2D(){

		if (damageCooldown > 0)
			return;
		health -= 1;

		Component RemoveHealth = GameObject.Find("Main Camera").GetComponent("GUIManager");
		RemoveHealth.SendMessage("AdjustCurrentHealth",-20);

		damageCooldown = invulnTime;
	}
}