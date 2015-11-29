using UnityEngine;
using System.Collections;

public class BossEnemy : MonoBehaviour {
	private float maxspeed, //max speed of enemy
		chargeCooldown; // delay until enemy can charge again
	private bool charging; //whether or not enemy is currently charging
	private Vector2 direction; //direction of acceleration, points directly at target player
	private Transform target; //the target, which is a random player

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
			if(target == null){ //sets new target if current one is missing (either by destruction or player disconnection)
				GameObject[] targets = GameObject.FindGameObjectsWithTag ("Player");
				target = targets[Random.Range (0, targets.Length)].transform;
				return;
			}
			direction = (target.position - transform.position).normalized;
			chargeCooldown -= Time.deltaTime;
			if(!charging && chargeCooldown < 9) //Enemy only shoots while not charging
				GetComponent<ProjectileSpawner> ().SpawnProjectile (true, 45, 8);
		}
	}

	// Update is called once per physics update
	void FixedUpdate() {
		if (PhotonNetwork.isMasterClient) {
			if(target == null)
				return;
			Rigidbody2D r = GetComponent<Rigidbody2D> ();
			if(chargeCooldown > 0) //Enemy changes direction lethargically while not charging
				r.AddForce (direction * 50);
			else if(charging){ //Enemy accelerates much faster while charging
				r.AddForce (direction * 300);
				if (Vector2.Dot (direction, r.velocity) < 0){ //If the current velocity is more than 90 degrees away from the player, stop charging
					charging = false;
					chargeCooldown = 10;
				}
			} else{ //Only executes when cooldown is 0 and not already charging, prepares enemy to charge
				charging = true;
				r.velocity = Vector2.zero;
			}
			r.velocity = Vector2.ClampMagnitude (r.velocity, maxspeed); //Ensures enemy cannot exceed maximum speed
		}
	}
}
