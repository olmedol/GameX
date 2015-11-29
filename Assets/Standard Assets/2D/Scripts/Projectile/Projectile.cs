using UnityEngine;
using System.Collections;

//This class is never used directly, instead it defines some shared behaviors for Lasers and Mines
public abstract class Projectile : MonoBehaviour {
	protected int damage; //damage the projectile inflicts
	protected bool? enemy; //whether the projectile is an enemy one or not

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool? isEnemy(){
		return enemy;
	}

	public void setEnemy(bool b){
		enemy = b;
	}

	public int damageInflicted(){
		return damage;
	}
}
