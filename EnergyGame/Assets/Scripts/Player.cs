using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public Rigidbody2D rb2d;
	public float moveSpeed;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float vx = 0;
		float vy = 0;
		vx = Input.GetAxisRaw("Horizontal");
		vy = Input.GetAxisRaw("Vertical");
		rb2d.velocity = new Vector2(vx, vy) * moveSpeed;
	}
}
