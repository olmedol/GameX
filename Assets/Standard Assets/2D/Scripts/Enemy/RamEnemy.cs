using UnityEngine;
using System.Collections;

public class RamEnemy : MonoBehaviour {
	private float maxspeed, aimcooldown;
	private Vector2 direction;
	private Transform target;

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
			direction = (target.position - transform.position).normalized;
			aimcooldown -= Time.deltaTime;
		}
	}

	void FixedUpdate() {
		if (PhotonNetwork.isMasterClient) {
			Rigidbody2D r = GetComponent<Rigidbody2D> ();
			if(Vector2.Distance (transform.position, target.position) > 20){
				if(aimcooldown > 0)
					r.AddForce (direction * 40);
				else{
					aimcooldown = 3;
					r.velocity = Vector2.zero;
				}
			} else
				r.AddForce (direction * 10);
			r.velocity = Vector2.ClampMagnitude (r.velocity, maxspeed);
		}
	}
	
}
