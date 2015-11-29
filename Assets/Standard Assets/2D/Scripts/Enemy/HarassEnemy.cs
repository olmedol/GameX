using UnityEngine;
using System.Collections;

public class HarassEnemy : MonoBehaviour {
	private float maxspeed; //max speed of enemy
	private Vector2 direction; //direction of acceleration, points directly at target player
	private Transform target; //the target, which is a random player
	private float firingTime; //Time until it reevaluates whether it should move or continue firing
	private float firingPeriod; //How long it spends firing before reevaluating whether it should move or not

	// Use this for initialization
	void Start () {
		maxspeed = 15;
		GameObject[] targets = GameObject.FindGameObjectsWithTag ("Player");
		target = targets[Random.Range (0, targets.Length)].transform;
		firingTime = 0;
		firingPeriod = 3.01f;
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
			Vector2 relativePos = new Vector2 (transform.position.x - target.position.x, transform.position.y - target.position.y);
			float angle = Mathf.Atan2 (relativePos.y, relativePos.x) * Mathf.Rad2Deg + 180;
			transform.localEulerAngles = new Vector3 (0, 0, angle);

			if (firingTime > 0) { //If firingTime > 0, it is in the process of shooting at a player
				GetComponent<ProjectileSpawner> ().SpawnProjectile (true);
				firingTime -= Time.deltaTime;
			} else if (Vector2.Distance (gameObject.transform.position, target.position) < 10) { //Once within 10 units, it stops and startes firing
				maxspeed = 0;
				firingTime = firingPeriod;
			} else //If firingTime <= 0 and distance > 10, it moves towards its target
				maxspeed = 15;
		}
	}

	// Update is called once per physics update
	void FixedUpdate() {
		if (PhotonNetwork.isMasterClient) {
			if(target == null)
				return;
			Rigidbody2D r = GetComponent<Rigidbody2D> ();
			r.AddForce (direction * 20);
			r.velocity = Vector2.ClampMagnitude (r.velocity, maxspeed); //Ensures enemy cannot exceed maximum speed
		}
	}
	
}
