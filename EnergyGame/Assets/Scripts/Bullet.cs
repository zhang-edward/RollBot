using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private Rigidbody2D rb2d;
	public SimpleAnimation anim;
	public float bulletSpeed;

	void Awake() {
		rb2d = GetComponent<Rigidbody2D>();
	}

	void OnDisable() {
		EffectPooler.PlayEffect(anim, transform.position);
	}

	public void Init(Vector2 dir) {
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0, 0, angle);
		rb2d.velocity = dir.normalized * bulletSpeed;
		StartCoroutine(DestroySelfRoutine());
	}

	private IEnumerator DestroySelfRoutine() {
		yield return new WaitForSeconds(2.0f);
		gameObject.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Enemy")) {
			Enemy e = collision.gameObject.GetComponent<Enemy>();
			gameObject.SetActive(false);
			e.Die();
		}
	}
}
