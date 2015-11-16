﻿using UnityEngine;
using System.Collections;

public class EnemySpawn : Photon.MonoBehaviour {
	private int enemyCount, enemyCap;
	private float randomTime;
	private float minY, minX, maxY, maxX;
	private float difficultyTimer;
	private int bossCount;

	// Use this for initialization
	void Start () {
		Boundary b = GetComponent<Boundary> ();
		minY = b.minY; minX = b.minX; maxY = b.maxY; maxX = b.maxX;
		enemyCount = 0;
		enemyCap = 2;
		randomTime = 0;
		difficultyTimer = 0;
	}

	// Update is called once per frame
	void Update () {
		if (PhotonNetwork.isMasterClient) {
			difficultyTimer += Time.deltaTime;
			enemyCap = (int) Mathf.Floor(GameObject.FindGameObjectsWithTag ("Player").Length * (2 + difficultyTimer / 30));
			enemyCount = GameObject.FindGameObjectsWithTag ("Enemy").Length + GameObject.FindGameObjectsWithTag ("EnemySwarm").Length / 3;
			if (randomTime > 0)
				randomTime -= Time.deltaTime;
			else if (enemyCount < enemyCap)
				spawnEnemy ();

			if (Mathf.Floor (difficultyTimer / 60) > bossCount){
				int x = Random.value > 0.5 ? (int)minX : (int)maxX;
				int y = Random.value > 0.5 ? (int)minY : (int)maxY;
				int randx = x > 0 ? 1 : -1;
				int randy = y > 0 ? 1 : -1;
				Vector2 spawnPos = new Vector2 (x + Random.Range (5, 11) * randx, y + Random.Range (5, 11) * randy);
				PhotonNetwork.InstantiateSceneObject ("BossEnemy", spawnPos, transform.rotation, 0, null);
				bossCount++;
			}
		}
	}

	void spawnEnemy() {
		randomTime = Random.Range (5, 11);

		string enemy = null;
		int count = 1;
		foreach (GameObject g in GameObject.FindGameObjectsWithTag ("Player")) {
			switch (Random.Range (1, 8)) {
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
			case 7:
				enemy = "EliteEnemy";
				break;
			default:
				print ("Spawn is out of range!");
				break;
			}
			for (int i = 0; i < count; i++) {
				int x = Random.value > 0.5 ? (int)minX : (int)maxX;
				int y = Random.value > 0.5 ? (int)minY : (int)maxY;
				int randx = x > 0 ? 1 : -1;
				int randy = y > 0 ? 1 : -1;
				Vector2 spawnPos = new Vector2 (x + Random.Range (5, 11) * randx, y + Random.Range (5, 11) * randy);
				PhotonNetwork.InstantiateSceneObject (enemy, spawnPos, transform.rotation, 0, null);
			}
		}
	}
}
