using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public const float COST_SPRINT = 2f;				// Energy cost per second
	private const float COST_SHOOT = 1f;				// Energy cost per shot
	private const float SPRINT_SPEED_MULTIPLIER = 1.5f;

	public ObjectPooler bulletPool;
	public Rigidbody2D rb2d;
	public Transform firePoint;
	[Header("Properties")]
	public float defaultMoveSpeed;
	public float moveSpeedMultiplier = 1f;
	public float energy;
	public float maxEnergy = 100f;
	public float fireRate = 0.1f;

	private bool sprinting = false;
	private float moveSpeed {
		get {
			return defaultMoveSpeed * moveSpeedMultiplier;
		}
	}

	void Awake() {
		energy = maxEnergy;
	}

	// Use this for initialization
	void Start () {
		StartCoroutine(ShootRoutine());
	}

	// Update is called once per frame
	void Update()
	{

		//energy -= Time.deltaTime * ENERGY_DECREASE_SPEED;
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			moveSpeedMultiplier *= SPRINT_SPEED_MULTIPLIER;
			sprinting = true;

		}
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			moveSpeedMultiplier /= SPRINT_SPEED_MULTIPLIER;
			sprinting = false;
		}

		if (sprinting)
		{
			energy -= Time.deltaTime * COST_SPRINT;
		}
		Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		LookAtDir(dir);
		GetAxisInput();
	}

	private void GetAxisInput() {
		float vx = Input.GetAxisRaw("Horizontal");
		float vy = Input.GetAxisRaw("Vertical");
		rb2d.velocity = new Vector2(vx, vy).normalized * moveSpeed;
	}

	private void LookAtDir(Vector2 dir){
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0, 0, angle);
	}

	private IEnumerator ShootRoutine()
	{
		for (;;) {
			while (!Input.GetMouseButton(0))
				yield return null;
			Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
			Shoot(dir);
			yield return new WaitForSeconds(fireRate);
		}
	}

	private void Shoot(Vector2 dir) {
		// Debug.DrawLine(transform.position, transform.position + dir, Color.red, 1);
		// Physics2D.RaycastAll(transform.position, dir, 5);
		GameObject o = bulletPool.GetPooledObject();
		o.transform.position = firePoint.position;
		o.SetActive(true);
		o.GetComponent<Bullet>().Init(dir);
		energy -= COST_SHOOT;
	}

	public void addEnergy(float amt){
		energy += amt;
		//make sure player isn't going over max energy
		if(energy > maxEnergy){
			energy = maxEnergy;
		}
	}

}
