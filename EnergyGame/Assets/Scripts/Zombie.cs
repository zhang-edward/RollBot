using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour {

	public Rigidbody2D rb2d;
	public Transform playerTransform;
	public float zombieMoveSpeed;


	void Start(){

	}

	void Update(){
		float vx = 0;
		float vy = 0;
		Vector3 playerPos = playerTransform.position;
		Vector3 playerPosUnit = (playerPos-transform.position).normalized;
		vx = playerPosUnit.x;
		vy = playerPosUnit.y;
		rb2d.velocity = new Vector2(vx, vy) * zombieMoveSpeed;

	}
}