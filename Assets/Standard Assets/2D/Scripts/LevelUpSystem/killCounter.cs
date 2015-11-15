
using UnityEngine;
using System.Collections;

public class killCounter : MonoBehaviour {
	
	
	public int killCount = 0;
	



	void  Update (){

	}
	
	public void  incScore (){
		killCount ++;
		

	}
	
	void  OnGUI (){
		GUI.Box(new Rect(5, 110, 40, 20), "Score");
		
		
		GUI.Box(new Rect(45, 110, 100, 20), killCount + "");
		
		
	}
	
	
}