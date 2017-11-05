using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour {
	public Rigidbody2D rb2d;

	void Awake() {
		rb2d = GetComponent<Rigidbody2D>();
	}


	public void Init(Vector3 dropLocation){

	}

	void OnTriggerEnter2D(Collider2D collision) {
		Player playerCollision = collision.gameObject.GetComponent<Player>();
		if (collision.CompareTag("Player")) {
			gameObject.SetActive(false);
			Player player = collision.gameObject.GetComponent<Player>();
		}
	}



}