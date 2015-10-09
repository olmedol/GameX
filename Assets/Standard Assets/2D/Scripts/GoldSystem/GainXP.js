#pragma strict

private var levelUpScript : LevelUpSystem;

function Start () 
{	
	levelUpScript = GameObject.Find("Main Camera").GetComponent(LevelUpSystem);


}

function OnCollisionEnter2D()
{
    		print("asdf");
        //Destroy(col.gameObject);
}

function OnDestroy () {
        levelUpScript.currentXP += 10;

        print("Script was destroyed");

    }