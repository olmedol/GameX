#pragma strict

var currentXP : int = 0;
var maxXP : int = 50;
var level : int = 1;

var levelUpTexture : Texture2D;

private var leveledUp : boolean = true;

var timeToShowLevelUp : float = 3f;
var timeTillNotShowLevelUp : float = 0f;

function Update()
{
	if(currentXP >= maxXP)
	{
		LevelUpSystem();
	}
	
	if(leveledUp)
	{
		if(Time.time > timeTillNotShowLevelUp)
		{
			leveledUp = false;
		}
	}
}

function LevelUpSystem()
{
	currentXP = 0;
	maxXP = maxXP + 50;
	level++;
	
	leveledUp = true;
	timeTillNotShowLevelUp = Time.time + timeToShowLevelUp;
	
	var GUIManager : GUIManager = GameObject.Find("Main Camera").GetComponent(GUIManager);
	GUIManager.maxHealth = GUIManager.maxHealth + 20;
	GUIManager.currentHealth = GUIManager.maxHealth;
	GUIManager.maxMana = GUIManager.maxMana + 20;
	GUIManager.maxStamina = GUIManager.maxStamina + 20;
}

function OnGUI()
{
	GUI.Box(new Rect(5, 100, 40, 20), "XP");
	GUI.Box(new Rect(5, 120, 40, 20), "Level");
	
	GUI.Box(new Rect(45, 100, 100, 20), currentXP + "/" + maxXP);
	GUI.Box(new Rect(45, 120, 100, 20), level + "");
	
	
	
}

