using UnityEngine;
using System.Collections;

public class AsteroidSpawn : Photon.MonoBehaviour {
	private int asteroidCount, asteroidCap;
	private float randomTime;
	private float minY, minX, maxY, maxX;


	// Use this for initialization
	void Start () {
		Boundary b = GetComponent<Boundary> ();
		minY = b.minY; minX = b.minX; maxY = b.maxY; maxX = b.maxX;
		asteroidCount = 0;
		asteroidCap = 15;
		randomTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (PhotonNetwork.isMasterClient) {
			asteroidCount = GameObject.FindGameObjectsWithTag ("Asteroid").Length;
			if (randomTime > 0)
				randomTime -= Time.deltaTime;
			else if (asteroidCount < asteroidCap)
				spawnAsteroid ();
		}
	}

	void spawnAsteroid() {
		randomTime = Random.Range (5, 11);
		string asteroid = null;
		switch (Random.Range (1, 5)) {
			case 1:
				asteroid = "Asteroid1";
				break;
			case 2:
				asteroid = "Asteroid2";
				break;
			case 3:
				asteroid = "Asteroid3";
				break;
			case 4:
				asteroid = "Asteroid4";
				break;
			default:
				print ("Spawn is out of range!");
				break;
		}
		bool isx = Random.value > 0.5;
		int val;
		if(isx)
			val = Random.value > 0.5 ? (int) minX : (int) maxX;
		else
			val = Random.value > 0.5 ? (int) minY : (int) maxY;
		int posORneg = val > 0 ? 1 : -1;
		Vector2 spawnPos = new Vector2((isx ? 1 : 0) * (val + Random.Range (5, 11) * posORneg), (isx ? 0 : 1) * (val + Random.Range (5, 11) * posORneg));
		PhotonNetwork.InstantiateSceneObject (asteroid, spawnPos, transform.rotation, 0, null);
	}
}
