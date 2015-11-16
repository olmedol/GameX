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
			if(target == null){
				inRange = false;
				orbitDistance = 10;
				GameObject[] targets = GameObject.FindGameObjectsWithTag ("Player");
				target = targets[Random.Range (0, targets.Length)].transform;
				return;
			}

			Vector2 relativePos = new Vector2 (transform.position.x - target.position.x, transform.position.y - target.position.y);
			float angle = Mathf.Atan2 (relativePos.y, relativePos.x) * Mathf.Rad2Deg - 90;
			transform.localEulerAngles = new Vector3 (0, 0, angle);

			if (inRange) {
				orbitDistance -= Time.deltaTime;
			} else if (Vector2.Distance (transform.position, target.position) < 10) {
				inRange = true;
			} else {
				Vector2 direction = (target.position - transform.position).normalized;
				movement = direction * speed;
			}
		}
	}

	void LateUpdate() {
		if (PhotonNetwork.isMasterClient) {
			if(target == null)
				return;
			if (inRange) {
				transform.position = target.position + relativeDistance;
				transform.RotateAround (target.position, direction, 60 * Time.deltaTime);
				relativeDistance = (transform.position - target.position).normalized * orbitDistance;
			}
		}
	}

	void FixedUpdate() {
		if (PhotonNetwork.isMasterClient) {
			if(target == null)
				return;
			if (!inRange)
				GetComponent<Rigidbody2D> ().velocity = movement;
		}
	}

}
