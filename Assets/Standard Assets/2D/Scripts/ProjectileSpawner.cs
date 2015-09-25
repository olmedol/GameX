﻿using UnityEngine;
using System.Collections;

public class ProjectileSpawner : MonoBehaviour {
	public Transform Projectile;
	private float rateOfFire;
	private float shotCooldown;

	// Use this for initialization
	void Start () {
		rateOfFire = 0.25f;
		shotCooldown = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown ("Fire1"))
			SpawnProjectile(false);
		if (shotCooldown > 0)
			shotCooldown -= Time.deltaTime;
	}

	void SpawnProjectile(bool Enemy){
		if (CanAttack()) {
			shotCooldown = rateOfFire;
			GameObject p = Instantiate (Projectile, transform.position, transform.rotation) as GameObject;
			p.GetComponent<Projectile>().setEnemy(Enemy);
		}
	}

	bool CanAttack(){
		return shotCooldown <= 0f;
	}

}
