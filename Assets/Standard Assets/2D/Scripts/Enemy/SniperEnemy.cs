using UnityEngine;
using System.Collections;

public class SniperEnemy : Photon.MonoBehaviour {
	private float speed; //speed at which enemy moves at
	private Vector2 movement; //velocity vector
	private Transform target; //the target, which is a random player
	private float firingTime, //seconds remaining in firing sequence
		firingPeriod, //how long the firing sequence is
		firingCooldownTime, //seconds remaining in shot cooldown
		firingCooldownPeriod; //how long the shot cooldown is
	private Vector2 relativePos; //position of target relative to self in absolute terms
	private float angle; //angle that the sniper aims at and its raycast is produced at
	private LineRenderer line; //visible line that represents where the sniper is currently aiming at
	public Material greenmaterial;
	public Material redmaterial;
	private Vector3 finalTarget; //direction of aim during the last .5 seconds of its firing sequence
	private int damage; //how much damage the shot does


	AudioSource warmup;
	AudioSource shot;
	
	// Use this for initialization
	void Start () {
		AudioSource[] audio = GetComponents<AudioSource> ();
		warmup = audio [2];
		shot = audio [1];

		speed = 6;
		GameObject[] targets = GameObject.FindGameObjectsWithTag ("Player");
		target = targets[Random.Range (0, targets.Length)].transform;
		firingTime = 0;
		firingPeriod = 5.51f;
		firingCooldownTime = 3;
		firingCooldownPeriod = 0.6f;
		line = gameObject.AddComponent<LineRenderer>();
		line.enabled = false;
		line.material = greenmaterial;
		line.SetWidth(0.1f, 0.1f);
		line.SetVertexCount (2);
		damage = 2;
	}
	
	// Update is called once per frame
	void Update () {
		if (PhotonNetwork.isMasterClient) {
			if(target == null){
				GameObject[] targets = GameObject.FindGameObjectsWithTag ("Player");
				target = targets[Random.Range (0, targets.Length)].transform;
				firingCooldownTime = firingTime;
				firingTime = 0;
				finalTarget = Vector3.zero;
				line.enabled = false;
				return;
			}

			Vector2 direction = (target.position - transform.position).normalized;
			float distance = Vector2.Distance (transform.position, target.position);
			movement = direction * speed;
			relativePos = new Vector2 (transform.position.x - target.position.x, transform.position.y - target.position.y);
			angle = Mathf.Atan2 (relativePos.y, relativePos.x) * Mathf.Rad2Deg + 180;
			transform.localEulerAngles = new Vector3 (0, 0, angle);
		
			if (firingCooldownTime > 0) { //what the sniper does when its shots are on cooldown
				if (firingCooldownTime < .5) { //line is disable when cooldown is < .5, disabling lingering effect
					line.enabled = false;
				}
				if (distance < 30) { //Sniper runs away from player if it is closer than 30 units
					speed = -6;
					angle = Mathf.Atan2 (relativePos.y, relativePos.x) * Mathf.Rad2Deg;
					transform.localEulerAngles = new Vector3 (0, 0, angle);
				}
				firingCooldownTime -= Time.deltaTime;
			} else if (firingTime > 0) { //sniper is currently firing
				FireAtPlayer ();
			} else if (distance < 20) { //sniper runs away if it is less than 20 units away and not actively firing
				speed = -6;
				angle = Mathf.Atan2 (relativePos.y, relativePos.x) * Mathf.Rad2Deg;
				transform.localEulerAngles = new Vector3 (0, 0, angle);
			} else if (distance > 50) { //sniper closes distances if more than 50 units away and not actively firing
				speed = 6;
			} else { //If sniper's distance is >20 and <50, its not currently firing, and its shot is not on cooldown, begin firing
				speed = 0.01f;
				firingTime = firingPeriod;
			}
		}
	}

	// Update is called once per physics update
	void FixedUpdate() {
		if (PhotonNetwork.isMasterClient) {
			if(target == null)
				return;
			GetComponent<Rigidbody2D> ().velocity = movement;
		}
	}

	//function that determines what the sniper does while firing
	void FireAtPlayer(){
		firingTime -= Time.deltaTime;
		line.SetPosition (0, transform.position);
		if (line.enabled == false) { //if line is disabled, it is enabled and set to be green
			warmup.PlayDelayed(1.2F);
			line.SetPosition (1, target.position);
			line.material = greenmaterial;
			line.SetColors(Color.green, Color.green);
			line.enabled = true;
		}
		if (firingTime > .5) //if firing time remaining is > .5 seconds, line endpoint tracks its target player
			line.SetPosition (1, target.position);
		else if(firingTime > 0){ //if firing time is less than .5 but not 0, it is frozen in position and turns red
			if(finalTarget == Vector3.zero)
				finalTarget = target.position;
			line.material = redmaterial;
			line.SetColors(Color.red, Color.red);
		} else { //once firing time hits 0, line extends to 8 times its currently length and damaging raycast is fired
			shot.Play ();
			line.SetPosition(1, (finalTarget - transform.position) * 8 + transform.position);
			fireRaycast(finalTarget, transform.position);
			finalTarget = Vector3.zero;
			firingCooldownTime = firingCooldownPeriod;
			GameObject[] targets = GameObject.FindGameObjectsWithTag ("Player");
			target = targets[Random.Range (0, targets.Length)].transform;
		}
	}

	//function that determines the effect of the raycast fired when the sniper takes its shot
	void fireRaycast(Vector3 targetpos, Vector3 pos){
		RaycastHit2D[] hits = Physics2D.RaycastAll (pos, (targetpos - pos));
		foreach(RaycastHit2D hit in hits)
			if (hit.collider != null) {
				GameObject g = hit.collider.gameObject;
				PhotonView v = g.GetComponent<PhotonView>();
				if (g.tag == "Player" && v.isMine) //handling for interaction with player objects
					g.GetComponent<Player>().damage(damage);
				else if(g != gameObject && (g.tag == "Enemy" || g.tag == "EnemySwarm") && v.isMine) //handling for interaction with enemies that are not self
					v.RPC ("damage", PhotonTargets.AllBufferedViaServer, damage);
			}
	}
}
