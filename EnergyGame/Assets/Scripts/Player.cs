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
		AimAtMouse();
		GetAxisInput();
	}

	private void GetAxisInput() {
		float vx = Input.GetAxisRaw("Horizontal");
		float vy = Input.GetAxisRaw("Vertical");
		rb2d.velocity = new Vector2(vx, vy) * moveSpeed;
	}

	private void AimAtMouse(){
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0, 0, angle);
	}
}
