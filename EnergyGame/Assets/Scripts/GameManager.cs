using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject enemyPrefab;
	public Transform player;
	public static GameManager instance;

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
			float randXOffset = Random.Range(-5, 5);
			float randYOffset = Random.Range(-5, 5);
			Vector3 spawnPosition = player.position + new Vector3(randXOffset, randYOffset, 0);
			GameObject o = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
			Enemy e = o.GetComponent<Enemy>();
			e.playerTransform = player;
			yield return new WaitForSeconds(2.0f);
		}
	}
}
