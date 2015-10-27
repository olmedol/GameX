using UnityEngine;
using System.Collections;

public class EnemySpawn : Photon.MonoBehaviour {
	private int enemyCount, enemyCap;
	private float randomTime;
	private float minY, minX, maxY, maxX;

	// Use this for initialization
	void Start () {
		Boundary b = GetComponent<Boundary> ();
		minY = b.minY; minX = b.minX; maxY = b.maxY; maxX = b.maxX;
		enemyCount = 0;
		enemyCap = 2;
		randomTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (PhotonNetwork.isMasterClient) {
			enemyCount = GameObject.FindGameObjectsWithTag ("Enemy").Length + GameObject.FindGameObjectsWithTag ("EnemySwarm").Length / 3;
			if (randomTime > 0)
				randomTime -= Time.deltaTime;
			else if (enemyCount < enemyCap)
				spawnEnemy ();
		}
	}

	void spawnEnemy() {
		randomTime = Random.Range (5, 11);

		string enemy = null;
		int count = 1;
		switch (Random.Range (1, 7)) {
			case 1:
				enemy = "RamEnemy";
				break;
			case 2:
				enemy = "HarassEnemy";
				break;
			case 3:
				enemy = "OrbitEnemy";
				count = 3;
				break;
			case 4:
				enemy = "MinerEnemy";
				break;
			case 5:
				enemy = "SniperEnemy";
				break;
			case 6:
				enemy = "TriangleEnemy";
				break;
			default:
				print ("Spawn is out of range!");
				break;
		}

		for (int i = 0; i < count; i++) {
			int x = Random.value > 0.5 ? (int) minX : (int) maxX;
			int y = Random.value > 0.5 ? (int) minY : (int) maxY;
			int randx = x > 0 ? 1 : -1;
			int randy = y > 0 ? 1 : -1;
			Vector2 spawnPos = new Vector2(x + Random.Range (5, 11) * randx, y + Random.Range (5, 11) * randy);
			//Instantiate (enemy, spawnPos, transform.rotation);
			PhotonNetwork.Instantiate (enemy, spawnPos, transform.rotation, 0);
		}
	}
}
