using UnityEngine;
using System.Collections;

public class ProjectileSpawner : MonoBehaviour {
	public Projectile Projectile;
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
			shotCooldown = rateOfFire;
			Projectile p = (Projectile) Instantiate (Projectile, transform.position, transform.rotation);
			p.setEnemy(Enemy);
		}
	}

}
