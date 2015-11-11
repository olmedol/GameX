using UnityEngine;
using System.Collections;

public class SniperEnemy : Photon.MonoBehaviour {
	private float speed;
	private Vector2 movement;
	private Transform target;
	private float firingTime, firingPeriod, firingCooldownTime, firingCooldownPeriod;
	private Vector2 relativePos;
	private float angle;
	private LineRenderer line;
	public Material greenmaterial;
	public Material redmaterial;
	private Vector3 finalTarget;
	private int damage;


	AudioSource warmup;
	AudioSource shot;
	
	// Use this for initialization
	void Start () {
		AudioSource[] audio = GetComponents<AudioSource> ();
		warmup = audio [0];
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
			Vector2 direction = (target.position - transform.position).normalized;
			float distance = Vector2.Distance (transform.position, target.position);
			movement = direction * speed;
			relativePos = new Vector2 (transform.position.x - target.position.x, transform.position.y - target.position.y);
			angle = Mathf.Atan2 (relativePos.y, relativePos.x) * Mathf.Rad2Deg + 180;
			transform.localEulerAngles = new Vector3 (0, 0, angle);
		
			if (firingCooldownTime > 0) {
				if (firingCooldownTime < .5) {
					line.enabled = false;
				}
				if (distance < 30) {
					speed = -6;
					angle = Mathf.Atan2 (relativePos.y, relativePos.x) * Mathf.Rad2Deg;
					transform.localEulerAngles = new Vector3 (0, 0, angle);
				}
				firingCooldownTime -= Time.deltaTime;
			} else if (firingTime > 0) {
				FireAtPlayer ();
			} else if (distance < 20) {
				speed = -6;
				angle = Mathf.Atan2 (relativePos.y, relativePos.x) * Mathf.Rad2Deg;
				transform.localEulerAngles = new Vector3 (0, 0, angle);
			} else if (distance > 50) {
				speed = 6;
			} else {
				speed = 0.01f;
				firingTime = firingPeriod;
			}
		}
	}
	
	void FixedUpdate() {
		if (PhotonNetwork.isMasterClient)
			GetComponent<Rigidbody2D>().velocity = movement;
	}
	
	void FireAtPlayer(){
		firingTime -= Time.deltaTime;
		line.SetPosition (0, transform.position);
		if (line.enabled == false) {
			warmup.PlayDelayed(1);
			line.SetPosition (1, target.position);
			line.material = greenmaterial;
			line.SetColors(Color.green, Color.green);
			line.enabled = true;
		}
		if (firingTime > .5)
			line.SetPosition (1, target.position);
		else if(firingTime > 0){
			shot.Play ();
			if(finalTarget == Vector3.zero)
				finalTarget = target.position;
			line.material = redmaterial;
			line.SetColors(Color.red, Color.red);
		} else {
			line.SetPosition(1, (finalTarget - transform.position) * 8 + transform.position);
			fireRaycast(finalTarget, transform.position);
			finalTarget = Vector3.zero;
			firingCooldownTime = firingCooldownPeriod;
			GameObject[] targets = GameObject.FindGameObjectsWithTag ("Player");
			target = targets[Random.Range (0, targets.Length)].transform;
		}
	}

	void fireRaycast(Vector3 targetpos, Vector3 pos){
		RaycastHit2D[] hits = Physics2D.RaycastAll (pos, (targetpos - pos));
		foreach(RaycastHit2D hit in hits)
			if (hit.collider != null) {
				GameObject g = hit.collider.gameObject;
				if (g.tag == "Player")
					g.GetComponent<Player>().damage(damage);
				else if(g != gameObject && g.tag != "Asteroid")
					g.GetComponent<PhotonView>().RPC ("damage", PhotonTargets.AllBufferedViaServer, damage);
			}
	}
}
