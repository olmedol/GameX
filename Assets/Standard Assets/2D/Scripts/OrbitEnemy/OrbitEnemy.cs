using UnityEngine;
using System.Collections;

public class OrbitEnemy : MonoBehaviour {
	private float speed;
	private Vector2 movement;
	private Transform target;
	private bool inRange;
	private float orbitDistance;
	private Vector3 relativeDistance;
	private Vector3 direction;
	
	// Use this for initialization
	void Start () {
		speed = 10;
		target = GameObject.Find ("Player").transform;
		inRange = false;
		orbitDistance = 10;
		relativeDistance = transform.position - target.position;
		direction = Random.value > 0.5 ? Vector3.forward : Vector3.back;
	}
	
	// Update is called once per frame
	void Update () {
		if (inRange) {
			orbitDistance -= Time.deltaTime;
		} else if (Vector2.Distance (gameObject.transform.position, target.position) < 10) {
			inRange = true;
		} else {
			Vector2 direction = (target.position - transform.position).normalized;
			movement = direction * speed;
		}
	}

	void LateUpdate() {
		if (inRange) {
			transform.position = target.position + relativeDistance;
			transform.RotateAround (target.position, direction, 60 * Time.deltaTime);
			relativeDistance = (transform.position - target.position).normalized * orbitDistance;
		}
	}

	void FixedUpdate() {
		if(!inRange)
			GetComponent<Rigidbody2D>().velocity = movement;
	}
	
	public Vector2 Direction(){
		return movement.normalized;
	}
}
