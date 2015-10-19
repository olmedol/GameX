using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	protected int damage;
	protected bool? enemy;

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
