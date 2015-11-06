﻿using UnityEngine;
using System.Collections;

public class HarassEnemy : MonoBehaviour {
	private float speed;
	private Vector2 movement;
	private Transform target;
	private float firingTime;
	private float firingPeriod;
	private Vector2 relativePos;
	private float angle;

	// Use this for initialization
	void Start () {
		speed = 10;
		GameObject[] targets = GameObject.FindGameObjectsWithTag ("Player");
		target = targets[Random.Range (0, targets.Length)].transform;
		firingTime = 0;
		firingPeriod = 3.01f;
	}
	
	// Update is called once per frame
	void Update () {
		if (PhotonNetwork.isMasterClient) {
			Vector2 direction = (target.position - transform.position).normalized;
			movement = direction * speed;
			relativePos = new Vector2 (transform.position.x - target.position.x, transform.position.y - target.position.y);
			angle = Mathf.Atan2 (relativePos.y, relativePos.x) * Mathf.Rad2Deg + 180;
			transform.localEulerAngles = new Vector3 (0, 0, angle);

			if (firingTime > 0) {
				FireAtPlayer ();
				firingTime -= Time.deltaTime;
			} else if (Vector2.Distance (gameObject.transform.position, target.position) < 10) {
				speed = 0.01f;
				firingTime = firingPeriod;
			} else
				speed = 10;
		}
	}

	void FixedUpdate() {
		if (PhotonNetwork.isMasterClient)
			GetComponent<Rigidbody2D> ().velocity = movement;
	}

	void FireAtPlayer(){
		GetComponent<ProjectileSpawner> ().SpawnProjectile (true);
	}
	
}