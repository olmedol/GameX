
using UnityEngine;
using System.Collections;

public class LevelUpSystem : MonoBehaviour {
	

	public int currentXP = 0;
	private int maxXP = 50;
	private int level = 1;
	
	private Texture2D levelUpTexture;

	private bool  leveledUp = true;
	
	private float timeToShowLevelUp = 3f;
	private float timeTillNotShowLevelUp = 0f;
	
	void  Update (){
		if(currentXP >= maxXP)
		{
			LevelUp();
		}
		
		if(leveledUp)
		{
			if(Time.time > timeTillNotShowLevelUp)
			{
				leveledUp = false;
			}
		}
	}
	
	void  LevelUp (){
		currentXP = 0;
		maxXP = maxXP + 20;
		level++;
		

		leveledUp = true;
		timeTillNotShowLevelUp = Time.time + timeToShowLevelUp;
		
		GUIManager GUIManager = GameObject.Find("Main Camera").GetComponent<GUIManager>();
		GUIManager.maxHealth = GUIManager.maxHealth + 20;
		transform.parent.gameObject.SendMessage ("increaseCap", 2);
		GUIManager.currentHealth = GUIManager.maxHealth;
		//Perks perks = player.GetComponent<Perks>();
		transform.parent.gameObject.SendMessage("addPerkPoints", 1);
	}
	
	void  OnGUI (){
		GUI.Box(new Rect(5, 70, 40, 20), "XP");
		GUI.Box(new Rect(5, 90, 40, 20), "Level");
		
		GUI.Box(new Rect(45, 70, 100, 20), currentXP + "/" + maxXP);
		GUI.Box(new Rect(45, 90, 100, 20), level + "");
		
		
		
	}
	
	
}