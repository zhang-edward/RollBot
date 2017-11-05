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
	[Header("Sprite and Animation")]
	public SpriteRenderer sr;
	public SimpleAnimationPlayer anim;
	public SimpleAnimation walkU, walkSide, walkD;
	public SimpleAnimation deathAnim;
	[Header("Properties")]
	public float defaultMoveSpeed;
	public float moveSpeedMultiplier = 1f;
	public float energy;
	public float maxEnergy = 100f;
	public float fireRate = 0.1f;

	private Vector2 dir;
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
		anim.anim = walkD;
		anim.Play();
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
		dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		if (dir.y > 0 && dir.y > dir.x) {
			
		}
		LookAtDir();
		GetAxisInput();
	}

	private void GetAxisInput() {
		float vx = Input.GetAxisRaw("Horizontal");
		float vy = Input.GetAxisRaw("Vertical");
		Vector2 velocity = new Vector2(vx, vy);
		rb2d.velocity = velocity.normalized * moveSpeed;
		if (velocity.magnitude <= 0)
			anim.paused = true;
		else
			anim.paused = false;
	}

	private void LookAtDir(){
		if (Mathf.Abs(dir.y) > Mathf.Abs(dir.x)) {      // If the player is mostly facing up
			if (dir.y > 0)
				anim.anim = walkU;
			else
				anim.anim = walkD;
		}
		else {
			anim.anim = walkSide;
			sr.flipX = dir.x < 0;
		}
	}

	private IEnumerator ShootRoutine()
	{
		for (;;) {
			while (!Input.GetMouseButton(0))
				yield return null;
			Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position;
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
		if(energy <= 0){
			Die();
		}
	}

	public void addEnergy(float amt){
		energy += amt;
		//make sure player isn't going over max energy
		if(energy > maxEnergy){
			energy = maxEnergy;
		}
	}

	public void takeEnergy(float amt){
		energy -= amt;
		if(energy <= 0){
			Die();
		}
	}

	private void Die(){
		gameObject.SetActive(false);
		EffectPooler.PlayEffect(deathAnim, transform.position);
	}

	/* bump "feature", has confict w/ velocity updates in Update()
	public void Bump(Vector3 initial, Vector3 target){
		firePoint.position = Vector3.Lerp(initial, target, 1);
	}
	*/

}
