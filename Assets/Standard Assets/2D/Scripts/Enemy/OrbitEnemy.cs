using UnityEngine;
using System.Collections;

public class OrbitEnemy : MonoBehaviour {
	private float speed; //speed at which enemy moves at, while approaching
	private Vector2 movement; //velocity vector
	private Transform target; //the target, which is a random player
	private bool inRange; //whether or not the orbiter is within range of a player
	private float orbitDistance; //how far the orbiter is currently orbiting from the player
	private Vector3 relativeDistance; //how far the orbiter currently is from the player
	private Vector3 direction; //whether the orbiter orbits to the right or left


	// Use this for initialization
	void Start () {
		speed = 15;
		GameObject[] targets = GameObject.FindGameObjectsWithTag ("Player");
		target = targets[Random.Range (0, targets.Length)].transform;
		inRange = false;
		orbitDistance = 10;
		relativeDistance = transform.position - target.position;
		direction = Random.value > 0.5 ? Vector3.forward : Vector3.back;
	}
	
	// Update is called once per frame
	void Update () {
		if (PhotonNetwork.isMasterClient) {
			if(target == null){ //sets new target if current one is missing (either by destruction or player disconnection)
				inRange = false;
				orbitDistance = 10;
				GameObject[] targets = GameObject.FindGameObjectsWithTag ("Player");
				target = targets[Random.Range (0, targets.Length)].transform;
				return;
			}

			Vector2 relativePos = new Vector2 (transform.position.x - target.position.x, transform.position.y - target.position.y);
			float angle = Mathf.Atan2 (relativePos.y, relativePos.x) * Mathf.Rad2Deg - 90;
			transform.localEulerAngles = new Vector3 (0, 0, angle);

			if (inRange) { //orbit distance shrinks at 1 unit per second
				orbitDistance -= Time.deltaTime;
			} else if (Vector2.Distance (transform.position, target.position) < 10) { //once within 10 units, is in range
				inRange = true;
			} else { //orbiter pursues if not in range
				Vector2 direction = (target.position - transform.position).normalized;
				movement = direction * speed;
			}
		}
	}

	// Update is called once per physics update, after all other Update and FixedUpdate calls have occurred
	void LateUpdate() {
		if (PhotonNetwork.isMasterClient) {
			if(target == null)
				return;
			if (inRange) { //if in range it rotates around the target player at 60 degrees per second
				transform.position = target.position + relativeDistance;
				transform.RotateAround (target.position, direction, 60 * Time.deltaTime);
				relativeDistance = (transform.position - target.position).normalized * orbitDistance;
			}
		}
	}

	// Update is called once per physics update
	void FixedUpdate() {
		if (PhotonNetwork.isMasterClient) {
			if(target == null)
				return;
			if (!inRange) //Orbiter only uses this if it is not in range
				GetComponent<Rigidbody2D> ().velocity = movement;
		}
	}

}
