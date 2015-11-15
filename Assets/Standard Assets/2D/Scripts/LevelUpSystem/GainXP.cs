
using UnityEngine;
using System.Collections;

public class GainXP : MonoBehaviour {
	
	
	private LevelUpSystem levelUpScript;
	
	void  Start (){	
		levelUpScript = GameObject.Find("Main Camera").GetComponent<LevelUpSystem>();
	}
	
	void  OnCollisionEnter2D (){
		//print("asdf");
		//Destroy(col.gameObject);
	}
	
	void  OnDestroy (){
		levelUpScript.currentXP += 10;
		
		//print("Script was destroyed");
		
	}
}