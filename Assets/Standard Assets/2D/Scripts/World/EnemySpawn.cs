using UnityEngine;
using System.Collections;

public class EnemySpawn : Photon.MonoBehaviour {
	private int enemyCount, //number of enemies on the map
		enemyCap; //number of enemies allowed on the map
	private float randomTime; //time until another spawn event is scheduled
	private float minY, minX, maxY, maxX; //boundary of play
	private float difficultyTimer; //time increment that dictates increasing difficulty
	private int bossCount; //number of bosses spawned

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
			else if (enemyCount < enemyCap) //once time remaining < 0 and enemy count is below cap, a spawn event occurs
				spawnEnemy ();
			if (Mathf.Floor (difficultyTimer / 60) > bossCount){ //boss is spawned every 60 seconds
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

	//process for spawning an enemy
	void spawnEnemy() {
		randomTime = Random.Range (5, 11);

		string enemy = null;
		int count = 1;
		foreach (GameObject _ in GameObject.FindGameObjectsWithTag ("Player")) { //an enemy is spawned for each living player
			switch (Random.Range (1, 8)) { //a random enemy is chosen to spawn in
				case 1:
					enemy = "RamEnemy";
					break;
				case 2:
					enemy = "HarassEnemy";
					break;
				case 3:
					enemy = "OrbitEnemy";
					count = 3; //this particular enemy counts as 1/3 of an enemy, so 3 are spawned
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
			for (int i = 0; i < count; i++) { //an enemy equal to the number of count is spawned at the corners of the map
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
