using UnityEngine;
using System.Collections;

public class ProjectileSpawner : Photon.MonoBehaviour {
	public string projectile;
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
				GameObject p = PhotonNetwork.Instantiate (projectile, transform.position, transform.rotation, 0);
				p.GetComponent<Projectile> ().setEnemy (Enemy);
				shotCooldown = rateOfFire;
		}
	}
	public void SpawnProjectile(bool Enemy, float angleOffset, int count){
			if (shotCooldown <= 0f) {
				float startAngle = Random.Range (0, 360);
				for(int i = 0; i < count; i++){
					Quaternion rotation = Quaternion.Euler (0, 0, (startAngle + angleOffset * i) % 360);
					GameObject p = PhotonNetwork.Instantiate (projectile, transform.position, rotation, 0);
					p.GetComponent<Projectile>().setEnemy(Enemy);
				}
				shotCooldown = rateOfFire;
			}
	}

	public void SpawnProjectile(bool laser1, bool laser2, bool laser3){
		if (shotCooldown <= 0f) {

				if(!laser1){
				//base projectile
				GameObject p = PhotonNetwork.Instantiate (projectile, transform.position, transform.rotation, 0);
				p.GetComponent<Projectile> ().setEnemy (false);
			}
				if(laser1){

			
				GameObject q = PhotonNetwork.Instantiate (projectile, transform.position, transform.rotation, 0);
				q.GetComponent<Projectile>().setEnemy(false);
				GameObject p = PhotonNetwork.Instantiate (projectile, transform.position, transform.rotation, 0);
				p.GetComponent<Projectile> ().setEnemy (false);

			}
				if (laser2) {
				//side projectile 1
				Quaternion rotation1 = Quaternion.Euler(0, 0, ((10)+ transform.rotation.eulerAngles.z) %360);
				GameObject q = PhotonNetwork.Instantiate (projectile, transform.position, rotation1, 0);
				q.GetComponent<Projectile>().setEnemy(false);
				//side projectile 2
				Quaternion rotation2 = Quaternion.Euler(0, 0, ((-10)+ transform.rotation.eulerAngles.z) %360);
				GameObject r = PhotonNetwork.Instantiate (projectile, transform.position, rotation2, 0);
				r.GetComponent<Projectile>().setEnemy(false);

			}
			if(laser3){

				//side projectile 1
				Quaternion rotation1 = Quaternion.Euler(0, 0, ((15)+ transform.rotation.eulerAngles.z) %360);
				GameObject q = PhotonNetwork.Instantiate (projectile, transform.position, rotation1, 0);
				q.GetComponent<Projectile>().setEnemy(false);
				//side projectile 2
				Quaternion rotation2 = Quaternion.Euler(0, 0, ((-15)+ transform.rotation.eulerAngles.z) %360);
				GameObject r = PhotonNetwork.Instantiate (projectile, transform.position, rotation2, 0);
				r.GetComponent<Projectile>().setEnemy(false);



			}
			shotCooldown = rateOfFire;
		}
	}

}
