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
		if (p != null && v != null) {
			if(p.isEnemy() == false && v.isMine){
				int damage = p.damageInflicted ();
				GetComponent<PhotonView>().RPC ("damage", PhotonTargets.AllBufferedViaServer, damage);
				//Destroy (p.gameObject);
				PhotonNetwork.Destroy (p.GetComponent<PhotonView>());
			}
		}
	}

	void OnCollisionStay2D(Collision2D otherCollision){
		PhotonView v = otherCollision.gameObject.GetComponent<PhotonView> ();
		if (ramCooldown <= 0 && otherCollision.gameObject.tag == "Player" && v.isMine){
			GetComponent<PhotonView>().RPC ("damage", PhotonTargets.AllBufferedViaServer, 1);
			ramCooldown = invulnTime;
		}
	}

	[PunRPC]
	void damage(int x){
		health -= x;
	}
}
