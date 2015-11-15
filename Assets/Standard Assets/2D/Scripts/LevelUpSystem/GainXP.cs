
using UnityEngine;
using System.Collections;

public class GainXP : MonoBehaviour {
	
	
	private LevelUpSystem levelUpScript;
	private killCounter kills;

	
	void  Start (){	
		levelUpScript = GameObject.FindWithTag("MainCamera").GetComponent<LevelUpSystem>();
		kills = GameObject.FindWithTag("MainCamera").GetComponent<killCounter>();
	}

	void Update(){
		if (kills == null)
			kills = GameObject.FindWithTag("MainCamera").GetComponent<killCounter>();
		if (levelUpScript == null)
			levelUpScript = GameObject.FindWithTag("MainCamera").GetComponent<LevelUpSystem>();
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