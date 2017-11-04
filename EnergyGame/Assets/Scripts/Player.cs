using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public ObjectPooler bulletPool;
	public Rigidbody2D rb2d;
	public float moveSpeed;
	public float energy = 100f;
	public float energyDecreaseSpeed = 0.5f;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update()
	{
		energy -= Time.deltaTime * energyDecreaseSpeed;

		Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		LookAtDir(dir);
		GetAxisInput();
		if (Input.GetMouseButtonDown(0))
		{
			Physics2D.RaycastAll(transform.position, dir, 5);
			Debug.DrawLine(transform.position, transform.position + dir, Color.red, 1);
			GameObject o = bulletPool.GetPooledObject();
			o.transform.position = transform.position;
			o.SetActive(true);
			o.GetComponent<Bullet>().Init(dir);
		}
	}

	private void GetAxisInput() {
		float vx = Input.GetAxisRaw("Horizontal");
		float vy = Input.GetAxisRaw("Vertical");
		rb2d.velocity = new Vector2(vx, vy) * moveSpeed;
	}

	private void LookAtDir(Vector2 dir){
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0, 0, angle);
	}
}
