var currentHealth : float = 100;
var maxHealth : int = 100;

var currentMana : float = 100.0;
var maxMana : int = 100;

var currentStamina : float = 100.0;
var maxStamina : int = 100;

var barLength = 0.0;

private var cam : Camera;

function Start()
{
	barLength = Screen.width / 8;
	cam = GetComponent(Camera);
}

function Update ()
{
	AdjustCurrentHealth (0);
	AdjustCurrentMana (0);
	
	/* MANA CONTROL SECTION*/
	
	//Normal Mana Regeneration
	if(currentMana >= 0)
	{
		currentMana += Time.deltaTime * 2;
	}
	
	//Don't let mana go above 100
	if(currentMana >= maxMana)
	{
		currentMana = maxMana;
	}
	
	//if mana reaches 0, never go below!
	if(currentMana <= 0)
	{
		currentMana = 0;
	}
	
	if(Input.GetKeyDown("f"))
	{
		AdjustCurrentMana(-20);
	}
}	
function OnGUI()
{
	//Icons for GUI
	GUI.Box(new Rect(5, 30, 40, 20), "HP");
	GUI.Box(new Rect(5, 50, 40, 20), "Mana");
	GUI.Box(new Rect(5, 70, 40, 20), "Stam");
	
	//Health / Mana / Stamina main bars
	GUI.Box(new Rect(45, 30, barLength, 20), currentHealth.ToString("0") + "/" + maxHealth);
	GUI.Box(new Rect(45, 50, barLength, 20), currentMana.ToString("0") + "/" + maxMana);
	GUI.Box(new Rect(45, 70, barLength, 20), currentStamina.ToString("0") + "/" + maxStamina);
}

function AdjustCurrentHealth (adj)
{
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

function AdjustCurrentMana (adj)
{
	currentMana += adj;
}