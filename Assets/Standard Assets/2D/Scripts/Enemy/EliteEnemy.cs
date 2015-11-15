using UnityEngine;
using System.Collections;

public class EliteEnemy : MonoBehaviour {
	private float maxspeed;
	private Vector2 direction;
	private Transform target;
	private Vector2 relativePos;
	private float angle;

	// Use this for initialization
	void Start () {
		maxspeed = 10;
		GameObject[] targets = GameObject.FindGameObjectsWithTag ("Player");
		target = targets[Random.Range (0, targets.Length)].transform;
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
			relativePos = new Vector2 (transform.position.x - target.position.x, transform.position.y - target.position.y);
			angle = Mathf.Atan2 (relativePos.y, relativePos.x) * Mathf.Rad2Deg + 180;
			transform.localEulerAngles = new Vector3 (0, 0, angle);
		}
	}

	void FixedUpdate() {
		if (PhotonNetwork.isMasterClient) {
			if(target == null)
				return;
			Rigidbody2D r = GetComponent<Rigidbody2D> ();
			if(Vector2.Distance (transform.position, target.position) < 20)
				GetComponent<ProjectileSpawner> ().SpawnProjectile (true);
			r.AddForce (direction * 10);
			r.velocity = Vector2.ClampMagnitude (r.velocity, maxspeed);
		}
	}
}
