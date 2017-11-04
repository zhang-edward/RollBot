using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private Rigidbody2D rb2d;

	void Awake() {
		rb2d = GetComponent<Rigidbody2D>();
	}

	public void Init(Vector2 dir) {
		rb2d.velocity = dir.normalized;
		StartCoroutine(DestroySelfRoutine());
	}

	private IEnumerator DestroySelfRoutine() {
		yield return new WaitForSeconds(2.0f);
		gameObject.SetActive(false);
	}
}
