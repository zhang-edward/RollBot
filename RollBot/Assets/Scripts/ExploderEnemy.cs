using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploderEnemy : Enemy {

	public SimpleAnimation explosionAnim;
	public GameObject prefab;

	protected override void Start()
	{
		base.Start();
	}

	public override void Die()
	{
		GameObject l = lootPool.GetPooledObject();
		l.transform.position = transform.position;
		l.SetActive(true);
		StartCoroutine(ExplodeRoutine());
	}

	private IEnumerator ExplodeRoutine() {
		EffectPooler.PlayEffect(explosionAnim, transform.position, false, 0);
		playerTransform = transform;
		yield return new WaitForSeconds(explosionAnim.GetSecondsUntilFrame(4));
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
