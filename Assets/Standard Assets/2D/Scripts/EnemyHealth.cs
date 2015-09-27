﻿using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
	public int health;
	private float invulnTime;
	private float ramCooldown;

	// Use this for initialization
	void Start () {
		if (health == null)
			health = 5;
		invulnTime = 1f;
		ramCooldown = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (health < 1)
			Destroy (gameObject);
		if (ramCooldown > 0)
			ramCooldown -= Time.deltaTime;
	}

	void OnTriggerStay2D(Collider2D otherCollider){
		Projectile p = otherCollider.gameObject.GetComponent<Projectile>();
		if (p != null) {
			if(!p.isEnemy()){
				Destroy (p.gameObject);
				health -= p.damageInflicted();
			}
		}
	}

	void OnCollisionStay2D(Collision2D otherCollision){
		if (ramCooldown <= 0 && otherCollision.gameObject.tag == "Player"){
			health -= 1;
			ramCooldown = invulnTime;
		}
	}
}
