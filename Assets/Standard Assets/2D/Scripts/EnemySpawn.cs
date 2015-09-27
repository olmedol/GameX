using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour {
	public Transform enemy;
	private int enemyCount;
	private int enemyCap;
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
		Vector2 spawnPos = new Vector2(playerPos.x + Random.Range (10, 20), playerPos.y + Random.Range (10, 20));
		randomTime = Random.Range (5, 10);
		Instantiate (enemy, spawnPos, GameObject.Find ("Player").transform.rotation);
	}
}
