using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public const float MAP_SPAWN_BUFFER = 2f;
	public const float DIFFICULT_VELOCTIY = 4f;
	[Header("Properties")]
	public float spawnRate = 1.0f;
	public int maxEnemiesCap = 10;
	[Header("Prefabs")]
	public GameObject[] enemyPrefabs;
	public GameObject bossPrefab;
	public Transform player;
	public Transform enemiesFolder;
	public SimpleAnimation spawnAnim;

	private List<Enemy> enemies = new List<Enemy>();

	void Awake() {
		//makes a singleton
		if(instance == null){
			instance = this;
		} else if(instance != this){
			Destroy(this.gameObject);
		}
	}

	void Start() {
		StartCoroutine(SpawnEnemyRoutine());
	}

	private IEnumerator SpawnEnemyRoutine() {
		for (;;) {
			while (enemies.Count >= maxEnemiesCap)
				yield return null;
			GameObject toSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
			StartCoroutine(SpawnEnemyRoutine(toSpawn));
			//StartCoroutine(SpawnEnemyRoutine(bossPrefab));
			yield return new WaitForSeconds(spawnRate);
		}
	}

	private IEnumerator SpawnEnemyRoutine(GameObject prefab) {
		float halfMapSize = MapGenerator.MAP_SIZE / 2f;
		float randX = Random.Range(-halfMapSize + MAP_SPAWN_BUFFER, -halfMapSize + MapGenerator.MAP_SIZE - MAP_SPAWN_BUFFER);
		float randY = Random.Range(-halfMapSize + MAP_SPAWN_BUFFER, -halfMapSize + MapGenerator.MAP_SIZE - MAP_SPAWN_BUFFER);
		Vector3 spawnPosition = new Vector3(randX, randY);
		EffectPooler.PlayEffect(spawnAnim, spawnPosition, false, 2.0f);
		yield return new WaitForSeconds(2.0f);
		SpawnEnemy(spawnPosition, prefab);
	}

	public void SpawnEnemy(Vector3 position, GameObject prefab) {
		GameObject o = Instantiate(prefab, position, Quaternion.identity);
		Enemy e = o.GetComponent<Enemy>();
		e.playerTransform = player;
		e.transform.SetParent(enemiesFolder);
		enemies.Add(e);
	}

	public void RemoveEnemy(Enemy e) {
		enemies.Remove(e);
	}
}
