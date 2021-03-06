﻿using UnityEngine;
using System.Collections;

public class Laser : Projectile {
	private float speed; //constant speed of projectile

	// Use this for initialization
	void Start () {

		damage = 1;
		
		foreach(GameObject p in GameObject.FindGameObjectsWithTag ("Player")){ //this damage upgrades to the projectile if you own it
			if(p.GetComponent<PhotonView>().isMine){
				if(p.GetComponent<Player>().dmg1){
					damage +=1;
					break;
				}
				if(p.GetComponent<Player>().dmg2){
					damage +=1;
				}
			}
		}

		AudioSource audio = GetComponent<AudioSource> ();
		AudioSource.PlayClipAtPoint(audio.clip, new Vector3(0,0,0));
		
		Destroy (gameObject, 10);
		speed = 20;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	// Update is called once per physics update
	void FixedUpdate() {
		GetComponent<Rigidbody2D> ().velocity = transform.right * speed; //projectile simply moves "right" upon spawn
	}
	
}
