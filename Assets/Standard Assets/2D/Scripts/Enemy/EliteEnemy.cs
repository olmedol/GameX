using UnityEngine;
using System.Collections;

public class EliteEnemy : MonoBehaviour {
	private float maxspeed; //max speed of enemy
	private Vector2 direction; //direction of acceleration, points directly at target player
	private Transform target; //the target, which is a random player
	private Vector2 relativePos; //position of target relative to self in absolute terms
	private float angle; //angle that projectiles are fired at

	// Use this for initialization
	void Start () {
		maxspeed = 10;
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
			relativePos = new Vector2 (transform.position.x - target.position.x, transform.position.y - target.position.y);
			angle = Mathf.Atan2 (relativePos.y, relativePos.x) * Mathf.Rad2Deg + 180;
			transform.localEulerAngles = new Vector3 (0, 0, angle);
		}
	}

	// Update is called once per physics update
	void FixedUpdate() {
		if (PhotonNetwork.isMasterClient) {
			if(target == null)
				return;
			Rigidbody2D r = GetComponent<Rigidbody2D> ();
			if(Vector2.Distance (transform.position, target.position) < 20) //Limits enemy to only firing within 20 units of the player
				GetComponent<ProjectileSpawner> ().SpawnProjectile (true);
			r.AddForce (direction * 10);
			r.velocity = Vector2.ClampMagnitude (r.velocity, maxspeed); //Ensures enemy cannot exceed maxspeed
		}
	}
}
