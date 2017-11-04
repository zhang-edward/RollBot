using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public Rigidbody2D rb2d;
	public Transform playerTransform;
	public float zombieMoveSpeed;
	public Loot lootDrop;
	public ObjectPooler lootPool;



	void Update(){
		float vx = 0;
		float vy = 0;
		Vector3 playerPos = playerTransform.position;
		Vector3 playerPosUnit = (playerPos - transform.position).normalized;
		vx = playerPosUnit.x;
		vy = playerPosUnit.y;
		rb2d.velocity = new Vector2(vx, vy) * zombieMoveSpeed;
	}

	public void Die() {
		gameObject.SetActive(false);
		GameObject l = lootPool.GetPooledObject();
		l.transform.position = transform.position;
		l.SetActive(true);
	}

}