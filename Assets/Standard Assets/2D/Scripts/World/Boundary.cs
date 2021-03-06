﻿using UnityEngine;
using System.Collections;

public class Boundary : MonoBehaviour {
	public readonly float minY = -30, minX = -49, maxY = 30, maxX = 49;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 currentPosition = transform.position;
		currentPosition.y = Mathf.Clamp (currentPosition.y, minY, maxY);
		currentPosition.x = Mathf.Clamp (currentPosition.x, minX, maxX);
		transform.position = currentPosition;
	}
}
