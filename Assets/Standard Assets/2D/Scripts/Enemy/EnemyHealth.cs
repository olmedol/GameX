using UnityEngine;
using System.Collections;

public class EnemyHealth : Photon.MonoBehaviour {
	public int health; //health of the enemy
	private float invulnTime; //period of time enemy is invulnerable to further collision damage after colliding
	private float ramCooldown; //how much invulnTime the enemy has left
	public AudioClip dead;
	public ParticleSystem deathParticle; 
	public Object instanceParticleSystem;
	AudioSource audio;
	ParticleSystem expl;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		expl = GetComponent<ParticleSystem> ();
		invulnTime = 1f;
		ramCooldown = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (health < 1) { //Enemy is destroyed once health < 1
			AudioSource.PlayClipAtPoint(audio.clip, new Vector3(0,0,0));
			expl.Play();
			if(deathParticle!=null)
				instanceParticleSystem= Instantiate ( deathParticle, gameObject.transform.position, gameObject.transform.rotation);
			Destroy(instanceParticleSystem,2.5f);
			PhotonNetwork.Destroy (GetComponent<PhotonView> ());
		}
		if (ramCooldown > 0)
			ramCooldown -= Time.deltaTime;
	}

	//Function handling projectile interaction
	void OnTriggerStay2D(Collider2D otherCollider){
		Projectile p = otherCollider.gameObject.GetComponent<Projectile> ();
		PhotonView v = otherCollider.gameObject.GetComponent<PhotonView> ();
		if (p != null && v != null && p.isEnemy () == false && v.isMine) { //Only player projectiles are evaluated, and only if owned by you, to prevent duplicate damage events
			GetComponent<PhotonView>().RPC ("damage", PhotonTargets.AllBufferedViaServer, p.damageInflicted ());
			PhotonNetwork.Destroy (p.GetComponent<PhotonView>());
		}
	}

	//Function handling collision interaction
	void OnCollisionStay2D(Collision2D otherCollision){
		GameObject g = otherCollision.gameObject;
		if(g.GetComponent<RamEnemy>() != null) //The Ramming Enemy is immune to ramming damage
			return;
		PhotonView v = g.GetComponent<PhotonView> ();
		if (g != null && v != null && ramCooldown <= 0 && v.isMine && (g.tag == "Player" || g.tag == "Asteroid")){
			GetComponent<PhotonView>().RPC ("damage", PhotonTargets.AllBufferedViaServer, 1);
			ramCooldown = invulnTime;
		}
	}

	[PunRPC]
	void damage(int x){
		health -= x;
	}
}
