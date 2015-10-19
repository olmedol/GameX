﻿using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {
	public Transform ram, harass, orbit, miner;
	private int enemyCount, enemyCap;
	private float randomTime;

	// Use this for initialization
	void Start () {
		enemyCount = 0;
		enemyCap = 2;
		randomTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		enemyCount = GameObject.FindGameObjectsWithTag ("Enemy").Length + GameObject.FindGameObjectsWithTag ("EnemySwarm").Length / 3;
		if (randomTime > 0)
			randomTime -= Time.deltaTime;
		else if (enemyCount < enemyCap)
			spawnEnemy();
	}

	void spawnEnemy() {
		Vector2 playerPos = GameObject.Find ("Player").transform.position;
		randomTime = Random.Range (5, 11);

		Transform enemy = null;
		int count = 1;
		switch (Random.Range (1, 5)) {
			case 1:
				enemy = ram;
				break;
			case 2:
				enemy = harass;
				break;
			case 3:
				enemy = orbit;
				count = 3;
				break;
			case 4:
				enemy = miner;
				break;
			default:
				print ("Spawn is out of range!");
				break;
		}

		for (int i = 0; i < count; i++) {
			int randx = Random.value > 0.5 ? -1 : 1;
			int randy = Random.value > 0.5 ? -1 : 1;
			Vector2 spawnPos = new Vector2(playerPos.x + Random.Range (10, 21) * randx, playerPos.y + Random.Range (10, 21) * randy);
			Instantiate (enemy, spawnPos, GameObject.Find ("Player").transform.rotation);
		}
	}
}
