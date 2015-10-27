using UnityEngine;
using System.Collections;

public class MinerEnemy : MonoBehaviour {
	private float minY, minX, maxY, maxX;
	private float speed;
	private Vector2 direction;
	private Vector2 movement;
	private float wanderCooldown;
	
	// Use this for initialization
	void Start () {
		Boundary b = GameObject.FindWithTag ("Player").GetComponent<Boundary> ();
		minY = b.minY; minX = b.minX; maxY = b.maxY; maxX = b.maxX;
		direction = (Vector3.zero - transform.position).normalized;
		speed = 2;
		wanderCooldown = 2;
	}
	
	// Update is called once per frame
	void Update () {
		if (PhotonNetwork.isMasterClient) {
			Vector2 pos = transform.position;
			if (pos.x < minX || pos.x > maxX || pos.y < minY || pos.y > maxY) {
				direction = (Vector3.zero - transform.position).normalized;
				wanderCooldown = 2;
			} else if (wanderCooldown <= 0) {
				float xchange = 0;
				float ychange = 0;
				if (Mathf.Abs (direction.x) > Mathf.Abs (direction.y))
					ychange = Random.value > 0.5 ? -1 : 1;
				else
					xchange = Random.value > 0.5 ? -1 : 1;
				direction = new Vector2 (direction.x + xchange, direction.y + ychange).normalized;
				wanderCooldown = 2;
			}
			movement = direction * speed;
			wanderCooldown -= Time.deltaTime;
			GetComponent<ProjectileSpawner> ().SpawnProjectile (true);
		}
	}
	
	void FixedUpdate() {
		if (PhotonNetwork.isMasterClient)
			GetComponent<Rigidbody2D> ().velocity = movement;
	}

}
