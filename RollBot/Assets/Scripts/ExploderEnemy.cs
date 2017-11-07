using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploderEnemy : Enemy {

	public GameObject prefab;

	protected override IEnumerator ExplodeRoutine() {
		if (bumpRoutine != null)
			StopCoroutine(bumpRoutine);
		if (movementRoutine != null)
			StopCoroutine(movementRoutine);
		EffectPooler.PlayEffect(explosionAnim, transform.position, false, 0);
		playerTransform = transform;
		yield return new WaitForSeconds(explosionAnim.GetSecondsUntilFrame(2));
		SoundManager.RandomizeSFX(deathSound);
		CameraControl.instance.StartShake(0.1f, 0.1f, true, true);
		for (int i = 0; i < 4; i ++) {
			float randXOffset = Random.Range(-1, 1);
			float randYOffset = Random.Range(-1, 1);
			Vector3 spawnPosition = transform.position + new Vector3(randXOffset, randYOffset, 0);
			GameManager.instance.SpawnEnemy(spawnPosition, prefab);
		}
		gameObject.SetActive(false);
	}
}
