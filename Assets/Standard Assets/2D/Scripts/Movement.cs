using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	private GameObject obj;
	private Vector2 speed;
	private Vector2 direction;

	// Use this for initialization
	void Start () {

	}

	public void Init (GameObject obj, Vector2 speed) {
		this.obj = obj;
		this.speed = speed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		if(obj != null && speed != null && direction != null)
			obj.GetComponent<Rigidbody2D>().velocity = new Vector2(speed.x * direction.x, speed.y * direction.y);
	}

	public void setDirection(Vector2 direction){
		this.direction = direction;
	}

	public Vector2 getDirection(){
		return direction;
	}
}
