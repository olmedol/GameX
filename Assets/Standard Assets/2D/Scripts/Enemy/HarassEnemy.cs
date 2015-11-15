using UnityEngine;
using System.Collections;

public class HarassEnemy : MonoBehaviour {
	private float maxspeed;
	private Vector2 direction;
	private Transform target;
	private float firingTime;
	private float firingPeriod;
	private Vector2 relativePos;
	private float angle;

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
			direction = (target.position - transform.position).normalized;
			relativePos = new Vector2 (transform.position.x - target.position.x, transform.position.y - target.position.y);
			angle = Mathf.Atan2 (relativePos.y, relativePos.x) * Mathf.Rad2Deg + 180;
			transform.localEulerAngles = new Vector3 (0, 0, angle);

			if (firingTime > 0) {
				GetComponent<ProjectileSpawner> ().SpawnProjectile (true);
				firingTime -= Time.deltaTime;
			} else if (Vector2.Distance (gameObject.transform.position, target.position) < 10) {
				maxspeed = 0;
				firingTime = firingPeriod;
			} else
				maxspeed = 15;
		}
	}

	void FixedUpdate() {
		if (PhotonNetwork.isMasterClient) {
			Rigidbody2D r = GetComponent<Rigidbody2D> ();
			r.AddForce (direction * 20);
			r.velocity = Vector2.ClampMagnitude (r.velocity, maxspeed);
		}
	}
	
}
