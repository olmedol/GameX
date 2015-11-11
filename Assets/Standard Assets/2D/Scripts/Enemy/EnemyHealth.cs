using UnityEngine;
using System.Collections;

public class EnemyHealth : Photon.MonoBehaviour {
	public int health;
	private float invulnTime;
	private float ramCooldown;

	// Use this for initialization
	void Start () {
		invulnTime = 1f;
		ramCooldown = 0f;
		gameObject.AddComponent<GainXP>();
	}
	
	// Update is called once per frame
	void Update () {
		if (health < 1)
			//Destroy (gameObject);
			PhotonNetwork.Destroy (GetComponent<PhotonView>());
		if (ramCooldown > 0)
			ramCooldown -= Time.deltaTime;
	}

	void OnTriggerStay2D(Collider2D otherCollider){
		Projectile p = otherCollider.gameObject.GetComponent<Projectile> ();
		PhotonView v = otherCollider.gameObject.GetComponent<PhotonView> ();
		if (p != null && v != null && p.isEnemy () == false && v.isMine) {
			GetComponent<PhotonView>().RPC ("damage", PhotonTargets.AllBufferedViaServer, p.damageInflicted ());
			PhotonNetwork.Destroy (p.GetComponent<PhotonView>());
		}
	}

	void OnCollisionStay2D(Collision2D otherCollision){
		GameObject g = otherCollision.gameObject;
		PhotonView v = g.GetComponent<PhotonView> ();
		if (ramCooldown <= 0 && v.isMine && (g.tag == "Player" || g.tag == "Asteroid")){
			GetComponent<PhotonView>().RPC ("damage", PhotonTargets.AllBufferedViaServer, 1);
			ramCooldown = invulnTime;
		}
	}

	[PunRPC]
	void damage(int x){
		health -= x;
	}
}
