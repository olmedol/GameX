﻿using UnityEngine;
using System.Collections;

public class Mine : Projectile {

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 120);
		damage = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
