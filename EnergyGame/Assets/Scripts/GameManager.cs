using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject enemyPrefab;
	public Transform player;

	void Awake() {
	}

	void Start() {
		StartCoroutine(SpawnEnemyRoutine());
	}

	private IEnumerator SpawnEnemyRoutine() {
		for (;;) {
			float randXOffset = Random.Range(-5, 5);
			float randYOffset = Random.Range(-5, 5);
			Vector3 spawnPosition = player.position + new Vector3(randXOffset, randYOffset, 0);
			GameObject o = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
			o.GetComponent<Enemy>().playerTransform = player;
			yield return new WaitForSeconds(2.0f);
		}
	}
}
