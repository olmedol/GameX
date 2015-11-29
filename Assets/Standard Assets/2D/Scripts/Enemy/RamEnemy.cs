using UnityEngine;
using System.Collections;

public class RamEnemy : MonoBehaviour {
	private float maxspeed, //max speed of enemy
		aimcooldown; //how long until it zeroes out its speed for more precise pursuit
	private Vector2 direction; //direction of acceleration, points directly at target player
	private Transform target; //the target, which is a random player

	// Use this for initialization
	void Start () {
		maxspeed = 20;
		aimcooldown = 3;
		GameObject[] targets = GameObject.FindGameObjectsWithTag ("Player");
		target = targets[Random.Range (0, targets.Length)].transform;
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
			aimcooldown -= Time.deltaTime;
		}
	}

	// Update is called once per physics update
	void FixedUpdate() {
		if (PhotonNetwork.isMasterClient) {
			if(target == null)
				return;
			Rigidbody2D r = GetComponent<Rigidbody2D> ();
			if(Vector2.Distance (transform.position, target.position) > 20){ //enemy accelerates much faster if outside 20 units
				if(aimcooldown > 0)
					r.AddForce (direction * 40);
				else{ //if aim cooldown is zero, zero out current velocity for more precise pursuit
					aimcooldown = 3;
					r.velocity = Vector2.zero;
				}
			} else //if close to the player acceleration is much more relaxed, allowing the player to evade and escape
				r.AddForce (direction * 10);
			r.velocity = Vector2.ClampMagnitude (r.velocity, maxspeed); //Ensures enemy cannot exceed maximum speed
		}
	}
	
}
