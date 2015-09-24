using UnityEngine;
using System.Collections;

public class Boundary : MonoBehaviour {
	public float minY = 0.0f, minX = 0.0f, maxY = 0.0f, maxX = 0.0f;
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
