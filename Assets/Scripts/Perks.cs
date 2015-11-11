using UnityEngine;
using System.Collections;

public class Perks : MonoBehaviour {

	public int perkPoints = 0;
	public float speedMult = 1;
	public float damageMult = 1;
	public float attackSpeedMult = 1;
	public float healthMult = 1;

	
	void addPerkPoints(int x){

		perkPoints += x;

	}
	/*
	void addSpeedMult(){

		speedMult += 0.5;

	}

	void addDamageMult(){

		damageMult += 1;

	}

	void addAttackSpeedMult(){

		attackSpeedMult += 1;

	}

	void addHealthMult(){

		healthMult += 1;

	}

	int getPerkPoints(){

		return perkPoints;

	}

	float getSpeedMult(){

		return speedMult;

	}

	float getDamageMult(){

		return damageMult;

	}

	float getAttackSpeedMult(){

		return attackSpeedMult;

	}

	float getHealthMult(){

		return healthMult;

	}

*/
}



