using UnityEngine;
using System.Collections;

public class Player : Photon.MonoBehaviour {
	private int health; //Player's health
	public float maxspeed; //Player's top speed
	private Vector2 movement; //Player's current velocity
	private float invulnTime; //Amount of time until the player can be hurt again after being damaged
	private float damageCooldown; //Time until player can be damaged
	private Vector2 playerPos; //Position of the player
	private Vector2 mousePos; //Position of the mouse
	private float angle; //Angle of the Player's sprite
	
	// Use this for initialization
	void Start () {
		health = 10;
		maxspeed = 5;
		invulnTime = 1;
		damageCooldown = 0;
	}
	
	// Update is called once per frame
	void Update () {
		movement = new Vector2(Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		if (movement.magnitude > 1)
			movement = movement.normalized;
		playerPos =  Camera.main.WorldToScreenPoint(transform.localPosition);
		mousePos = new Vector2 (Input.mousePosition.x - playerPos.x, Input.mousePosition.y - playerPos.y);
		angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
		transform.localEulerAngles = new Vector3(0, 0, angle);

		if (health < 1)
			PhotonNetwork.Destroy (GetComponent<PhotonView>());

		if (damageCooldown > 0)
			damageCooldown -= Time.deltaTime;

		if (Input.GetButton ("Fire1"))
			GetComponent<ProjectileSpawner> ().SpawnProjectile (false);
	}
	
	void FixedUpdate() {
		Rigidbody2D r = GetComponent<Rigidbody2D> ();
		r.AddForce (movement * 20);
		r.velocity = Vector2.ClampMagnitude (r.velocity, maxspeed);
	}

	void OnTriggerStay2D(Collider2D otherCollider){
		if (damageCooldown > 0)
			return;
		Projectile p = otherCollider.gameObject.GetComponent<Projectile> ();
		if(p != null && p.isEnemy() == true){
			health -= p.damageInflicted();

			Component RemoveHealth = GameObject.Find("Main Camera").GetComponent("GUIManager");
			RemoveHealth.SendMessage("AdjustCurrentHealth",-10);

			PhotonNetwork.Destroy (p.GetComponent<PhotonView>());
			damageCooldown = invulnTime;
		}
	}

	void OnCollisionStay2D(Collision2D otherCollision){
		if (damageCooldown <= 0 && otherCollision.gameObject.tag != "Player") {
			health -= 1;

			Component RemoveHealth = GameObject.Find ("Main Camera").GetComponent ("GUIManager");
			RemoveHealth.SendMessage ("AdjustCurrentHealth", -10);

			damageCooldown = invulnTime;
		}
	}

	public void damage(int x){
		if (damageCooldown <= 0) {
			health -= x;

			Component RemoveHealth = GameObject.Find ("Main Camera").GetComponent ("GUIManager");
			RemoveHealth.SendMessage ("AdjustCurrentHealth", -10 * x);

			damageCooldown = invulnTime;
		}
	}

	public void speedUp(float amt){
		
		maxspeed += amt;
		
	}
}