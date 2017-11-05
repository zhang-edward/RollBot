using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public GameObject[] enemyPrefabs;
	public GameObject bossPrefab;
	public Transform player;
	public SimpleAnimation spawnAnim;

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
		int enemyCount = 0;
		for (;;) {
			float randXOffset = Random.Range(-5, 5);
			float randYOffset = Random.Range(-5, 5);
			Vector3 spawnPosition = player.position + new Vector3(randXOffset, randYOffset, 0);
			EffectPooler.PlayEffect(spawnAnim, spawnPosition, false, 2.0f);
			yield return new WaitForSeconds(2.0f);
			SpawnEnemy(spawnPosition, enemyPrefabs[Random.Range(0, enemyPrefabs.Length)]);
			enemyCount++;
			if (enemyCount % 10 == 0)
				SpawnEnemy(player.position - new Vector3(randXOffset, randYOffset), bossPrefab);
			yield return new WaitForSeconds(0.5f);
		}
	}

	public void SpawnEnemy(Vector3 position, GameObject prefab) {
		GameObject o = Instantiate(prefab, position, Quaternion.identity);
		Enemy e = o.GetComponent<Enemy>();
		e.playerTransform = player;
	}
}
