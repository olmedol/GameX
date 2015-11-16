using UnityEngine;
using System.Collections;

public class BossEnemy : MonoBehaviour {
	private float maxspeed, chargeCooldown;
	private bool charging;
	private Vector2 direction;
	private Transform target;

	// Use this for initialization
	void Start () {
		maxspeed = 20;
		chargeCooldown = 10;
		charging = false;
		GameObject[] targets = GameObject.FindGameObjectsWithTag ("Player");
		target = targets[Random.Range (0, targets.Length)].transform;
		GetComponent<EnemyHealth> ().health = 60 + (targets.Length - 1) * 45;
	}
	
	// Update is called once per frame
	void Update () {
		if (PhotonNetwork.isMasterClient) {
			if(target == null){
				GameObject[] targets = GameObject.FindGameObjectsWithTag ("Player");
				target = targets[Random.Range (0, targets.Length)].transform;
				return;
			}
			direction = (target.position - transform.position).normalized;
			chargeCooldown -= Time.deltaTime;
			if(!charging && chargeCooldown < 9)
				GetComponent<ProjectileSpawner> ().SpawnProjectile (true, 45, 8);
		}
	}

	void FixedUpdate() {
		if (PhotonNetwork.isMasterClient) {
			if(target == null)
				return;
			Rigidbody2D r = GetComponent<Rigidbody2D> ();
			if(chargeCooldown > 0)
				r.AddForce (direction * 50);
			else if(charging){
				r.AddForce (direction * 300);
				if (Vector2.Dot (direction, r.velocity) < 0){
					charging = false;
					chargeCooldown = 10;
				}
			} else{
				charging = true;
				r.velocity = Vector2.zero;
			}
			r.velocity = Vector2.ClampMagnitude (r.velocity, maxspeed);
		}
	}
}
