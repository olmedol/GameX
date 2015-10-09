using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {
	public Transform harassenemy, ramenemy;
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
		enemyCount = GameObject.FindGameObjectsWithTag ("Enemy").Length;
		if (randomTime > 0)
			randomTime -= Time.deltaTime;
		else if (enemyCount < enemyCap)
			spawnEnemy();
	}

	void spawnEnemy() {
		Vector2 playerPos = GameObject.Find ("Player").transform.position;
		int randx = Random.value > 0.5 ? -1 : 1;
		int randy = Random.value > 0.5 ? -1 : 1;
		Vector2 spawnPos = new Vector2(playerPos.x + Random.Range (10, 20) * randx, playerPos.y + Random.Range (10, 20) * randy);
		randomTime = Random.Range (5, 10);
		Transform enemy = Random.value > 0.5 ? harassenemy : ramenemy;
		Instantiate (enemy, spawnPos, GameObject.Find ("Player").transform.rotation);
	}
}
