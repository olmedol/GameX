using UnityEngine;
using System.Collections;

public class Player : Photon.MonoBehaviour {
	private int health; //Player's health
	private float maxspeed; //Player's top speed
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
		if (r.velocity.magnitude > maxspeed)
			r.velocity = r.velocity.normalized * maxspeed;
	}

	void OnTriggerStay2D(Collider2D otherCollider){
		if (damageCooldown > 0)
			return;
		Projectile p = otherCollider.gameObject.GetComponent<Laser>();
		if (p == null)
			p = otherCollider.gameObject.GetComponent<Mine> ();
		if (p != null) {
			if(p.isEnemy() == true){
				health -= p.damageInflicted();
				PhotonNetwork.Destroy (p.GetComponent<PhotonView>());;
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

	public void damage(int x){
		if (damageCooldown > 0)
			return;
		health -= x;
		damageCooldown = invulnTime;
		Component RemoveHealth = GameObject.Find("Main Camera").GetComponent("GUIManager");
		RemoveHealth.SendMessage("AdjustCurrentHealth",-20 * x);
	}
}