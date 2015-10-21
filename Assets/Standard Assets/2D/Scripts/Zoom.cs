using UnityEngine;
using System.Collections;

public class Zoom : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Camera.main.orthographicSize = 7;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Mouse ScrollWheel") > 0 && Camera.main.orthographicSize > 5)
			Camera.main.orthographicSize -= 1;
		else if (Input.GetAxis("Mouse ScrollWheel") < 0 && Camera.main.orthographicSize < 12)
			Camera.main.orthographicSize += 1;
	}
}
