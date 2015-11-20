


using UnityEngine;
using System.Collections;

public class Player : Photon.MonoBehaviour {
	private int health; //Player's health
	private int maxHealth; //Player's health

	public float maxspeed; //Player's top speed
	private Vector2 direction; //Player's current direction
	private float invulnTime; //Amount of time until the player can be hurt again after being damaged
	private float damageCooldown; //Time until player can be damaged
	private Vector2 playerPos; //Position of the player
	private Vector2 mousePos; //Position of the mouse
	private float angle; //Angle of the Player's sprite
	public bool dmg1; //Damage Upgrade active/inactive
	public bool dmg2; //Damage Upgrade active/inactive
	public bool laser1; //Double-laser upgrade active/inactive
	public bool laser2; //Side-laser upgrade active/inactive
	public bool laser3; //More lasers upgrade active/inactive
	public bool shield; //shield upgrade active/inactive
	private Shield shield_component;
	private float shieldCooldown; //Time until shield regenerates
	AudioSource audio;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();

		health = 10;
		maxspeed = 5;
		maxHealth = 10;

		invulnTime = 1;
		damageCooldown = 0;
		dmg1 = false;
		dmg2 = false;
		laser1 = false;
		laser2 = false;
		laser3 = false;
		shield = false;
		shieldCooldown = 0;
		shield_component = GetComponent<Shield> ();
	}
	
	// Update is called once per frame
	void Update () {
		direction = Vector2.ClampMagnitude (new Vector2(Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical")), 1);
		playerPos =  Camera.main.WorldToScreenPoint(transform.localPosition);
		mousePos = new Vector2 (Input.mousePosition.x - playerPos.x, Input.mousePosition.y - playerPos.y);
		angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
		transform.localEulerAngles = new Vector3(0, 0, angle);
		
		if (health < 1) {
			AudioSource.PlayClipAtPoint(audio.clip, new Vector3(0,0,0));
			Camera.main.transform.SetParent(null);
			PhotonNetwork.Destroy (GetComponent<PhotonView> ());
		}
		if (damageCooldown > 0)
			damageCooldown -= Time.deltaTime;

		if (shieldCooldown > 0)
			shieldCooldown -= Time.deltaTime;

		if (shieldCooldown <= 0 && !shield_component.shieldActive() && shield)
			shield_component.shieldActive (true);
		
		if (Input.GetButton ("Fire1") && Time.timeScale == 1)
			GetComponent<ProjectileSpawner>().SpawnProjectile(laser1, laser2, laser3);
	}
	
	void FixedUpdate() {
		Rigidbody2D r = GetComponent<Rigidbody2D> ();
		r.AddForce (direction * 20);
		r.velocity = Vector2.ClampMagnitude (r.velocity, maxspeed);
	}
	
	void OnTriggerStay2D(Collider2D otherCollider){
		if (damageCooldown > 0)
			return;
		Projectile p = otherCollider.gameObject.GetComponent<Projectile> ();
		if (p != null && p.isEnemy () == true && shield_component.shieldActive()) {

			shield_component.shieldActive(false);
			shieldCooldown = 10;
			PhotonNetwork.Destroy (p.GetComponent<PhotonView>());
			damageCooldown = invulnTime;


		}
		else if (p != null && p.isEnemy () == true){
			health -= p.damageInflicted();
			
			Component RemoveHealth = GameObject.Find("Main Camera").GetComponent("GUIManager");
			RemoveHealth.SendMessage("AdjustCurrentHealth",-10);
			
			PhotonNetwork.Destroy (p.GetComponent<PhotonView>());
			damageCooldown = invulnTime;
		}
	}
	
	void OnCollisionStay2D(Collision2D otherCollision){
		if (damageCooldown <= 0 && otherCollision.gameObject.tag != "Player" 
		    && shield_component.shieldActive()) {
			
			shield_component.shieldActive(false);
			shieldCooldown = 10;
			damageCooldown = invulnTime;
		}
		else if (damageCooldown <= 0 && otherCollision.gameObject.tag != "Player"
		         && !shield_component.shieldActive()){

			health -= 1;
			Component RemoveHealth = GameObject.Find ("Main Camera").GetComponent ("GUIManager");
			RemoveHealth.SendMessage ("AdjustCurrentHealth", -10);
			
			damageCooldown = invulnTime;
		}
	}
	
	public void damage(int x){
		if (damageCooldown <= 0 && shield_component.shieldActive()) {
			
			shield_component.shieldActive(false);
			shieldCooldown = 10;
			damageCooldown = invulnTime;
		}
		else if(damageCooldown <= 0 && !shield_component.shieldActive()){
			health -= x;
			Component RemoveHealth = GameObject.Find ("Main Camera").GetComponent ("GUIManager");
			RemoveHealth.SendMessage ("AdjustCurrentHealth", -10 * x);
			
			damageCooldown = invulnTime;
		}
	}
	public void increaseCap(int x){
		maxHealth += x;
		health = maxHealth;
	}
	public void heal(int x){
		if (health + x >= maxHealth) {
			health = maxHealth;
			
		} else {
			health+=x;
		}
		
		Component AddHealth = GameObject.Find ("Main Camera").GetComponent ("GUIManager");
		AddHealth.SendMessage ("AdjustCurrentHealth", 10 * x);
		
	}
	
}



