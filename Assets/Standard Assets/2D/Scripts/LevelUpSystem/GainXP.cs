
using UnityEngine;
using System.Collections;

public class GainXP : MonoBehaviour {
	
	
	private LevelUpSystem levelUpScript;
	private killCounter kills;

	
	void  Start (){	
		levelUpScript = GameObject.Find("Main Camera").GetComponent<LevelUpSystem>();
		kills = GameObject.Find("Main Camera").GetComponent<killCounter>();
	}
	
	void  OnCollisionEnter2D (){
		//print("asdf");
		//Destroy(col.gameObject);
	}
	
	void  OnDestroy (){
		levelUpScript.currentXP += 10;

		kills.incScore ();
		//print("Script was destroyed");
		
	}
}