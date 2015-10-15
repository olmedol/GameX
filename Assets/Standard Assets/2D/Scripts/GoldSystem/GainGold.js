#pragma strict

private var goldScript : GoldSystem;

function Start () 
{	
	goldScript = GameObject.Find("Main Camera").GetComponent(GoldSystem);


}

function OnCollisionEnter2D()
{
    		print("asdf");
        //Destroy(col.gameObject);
}

function OnDestroy () {
        goldScript.currentGold += 10;

        print("Script was destroyed");

    }