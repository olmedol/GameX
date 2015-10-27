﻿using UnityEngine;
using System.Collections;

public class ProjectileSpawner : Photon.MonoBehaviour {
	public Projectile projectile;
	public float rateOfFire;
	private float shotCooldown;

	// Use this for initialization
	void Start () {
		shotCooldown = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (shotCooldown > 0)
			shotCooldown -= Time.deltaTime;
	}

	public void SpawnProjectile(bool Enemy){
		if (shotCooldown <= 0f) {
			//Projectile p = (Projectile) Instantiate (projectile, transform.position, transform.rotation);
			GameObject p = PhotonNetwork.Instantiate ("Laser", transform.position, transform.rotation, 0);
			//p.setEnemy(Enemy);
			p.GetComponent<Projectile>().setEnemy(Enemy);
			shotCooldown = rateOfFire;
		}
	}

	public void SpawnProjectile(bool Enemy, float angleOffset, int count){
			if (shotCooldown <= 0f) {
				float startAngle = Random.Range (0, 360);
				for(int i = 0; i < count; i++){
					Quaternion rotation = Quaternion.Euler (0, 0, (startAngle + angleOffset * i) % 360);
					//Projectile p = (Projectile) Instantiate (Projectile, transform.position, rotation);
					GameObject p = PhotonNetwork.Instantiate (projectile.ToString (), transform.position, rotation, 0);
					//p.setEnemy(Enemy);
					p.GetComponent<Projectile>().setEnemy(Enemy);
				}
				shotCooldown = rateOfFire;
			}
	}

}
