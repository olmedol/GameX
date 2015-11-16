
using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {
	public float currentHealth = 100;
	public float maxHealth = 100;
	
	private float currentMana = 100.0f;
	public int maxMana = 100;
	
	private float barLength= 0.0f;
	
	//private Camera cam;
	
	void  Start (){
		barLength = Screen.width / 8;
		//cam = GetComponent<Camera>();
	}
	
	void  Update (){
		AdjustCurrentMana (0);
		

	}	
	void  OnGUI (){
		//Icons for GUI
		GUI.Box(new Rect(5, 30, 40, 20), "HP");
		//GUI.Box(new Rect(5, 50, 40, 20), "Mana");
		//GUI.Box(new Rect(5, 70, 40, 20), "Stam");
		
		//Health / Mana / Stamina main bars
		GUI.Box(new Rect(45, 30, barLength, 20), currentHealth.ToString("0") + "/" + maxHealth);
		//GUI.Box(new Rect(45, 50, barLength, 20), currentMana.ToString("0") + "/" + maxMana);
		//GUI.Box(new Rect(45, 70, barLength, 20), currentStamina.ToString("0") + "/" + maxStamina);
	}
	void AdjustMaxHealth (float adj){
		maxHealth += adj;


	}

	 void AdjustCurrentHealth (float adj){
		//print("Script was called");
		
		currentHealth += adj;
		
		if(currentHealth >= maxHealth)
		{
			currentHealth = maxHealth;
		}
		
		if(currentHealth <= 0)
		{
			currentHealth = 0;
		}
	}
	
	void  AdjustCurrentMana (float adj){
		currentMana += adj;
	}
}