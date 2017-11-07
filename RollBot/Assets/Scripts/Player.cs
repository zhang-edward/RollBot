using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public const float COST_SPRINT = 2f;				// Energy cost per second
	private const float COST_SHOOT = 5f;				// Energy cost per shot
	private const float SPRINT_SPEED_MULTIPLIER = 1.5f;

	public ObjectPooler bulletPool;
	public Rigidbody2D rb2d;
	public Transform firePoint;
	[Header("Sprite and Animation")]
	public SpriteRenderer sr;
	public SimpleAnimationPlayer anim;
	public SimpleAnimation walkU, walkSide, walkD;
	public SimpleAnimation deathAnim;
	[Header("Audio")]
	public AudioClip shootSound;
	public AudioClip dieSound;
	public AudioClip hurtSound;
	public AudioClip energyPickupSound;
	[Header("Properties")]
	public float defaultMoveSpeed;
	public float moveSpeedMultiplier = 1f;
	public float energy;
	public float maxEnergy = 100f;
	private bool invincible = false;
	public float fireRate = 3f;
	private float originalFireRate;

	public float maxCombo = 100f;
	public float combo;
	public float comboStatus;

	private Vector2 dir;
	private bool sprinting = false;
	private float moveSpeed {
		get {
			return defaultMoveSpeed * moveSpeedMultiplier;
		}
	}

	public delegate void OnPlayerStateChanged();
	public event OnPlayerStateChanged OnPlayerDied;

	void Awake() {
		energy = maxEnergy;
		combo = 0;
		originalFireRate = fireRate;
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
		if (!GameManager.instance.gameStarted)
			return;
		if(combo <= 0f){
			fireRate = originalFireRate;
			comboStatus = 0;
		}
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
		if(combo > 0){
			combo -= 1;
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
			while (!GameManager.instance.gameStarted)
				yield return null;
			while (!Input.GetMouseButton(0))
				yield return null;
			Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position;
			Shoot(dir);
			yield return new WaitForSeconds(1f / fireRate);
		}
	}

	private void Shoot(Vector2 dir) {
		// Debug.DrawLine(transform.position, transform.position + dir, Color.red, 1);
		// Physics2D.RaycastAll(transform.position, dir, 5);
//		print("combo: " + combo + " comboStatus: " + comboStatus + " fireRate: " + fireRate);
		GameObject o = bulletPool.GetPooledObject();
		o.transform.position = firePoint.position;
		o.SetActive(true);
		o.GetComponent<Bullet>().Init(dir);
		SoundManager.RandomizeSFX(shootSound);
		energy -= COST_SHOOT/(fireRate/(1.5f)*originalFireRate);
		if(energy <= 0f){
			Die();
		}
	}

	public void AddEnergy(float amt){
		energy += amt;
		SoundManager.RandomizeSFX(energyPickupSound);
		//make sure player isn't going over max energy
		if(energy > maxEnergy){
			energy = maxEnergy;
		}
	}

	public void TakeEnergy(float amt){
		if (invincible)
			return;
		SoundManager.RandomizeSFX(hurtSound);
		energy -= amt;
		combo = 0;
		CameraControl.instance.StartShake(0.1f, 0.05f, true, true);
		StartCoroutine(FlashRed());
		energy -= amt;
		if(energy <= 0f){
			Die();
		}
	}

	public void UpdateCombo(){
		combo = maxCombo;
		if(comboStatus < 20){
			comboStatus++;
			fireRate += (originalFireRate/12f);
		}
	}

	private void Die(){
		gameObject.SetActive(false);
		SoundManager.RandomizeSFX(dieSound);
		if (OnPlayerDied != null)
			OnPlayerDied();
		//EffectPooler.PlayEffect(deathAnim, transform.position);
	}

	private IEnumerator FlashRed() {
		sr.color = Color.red;
		invincible = true;
		yield return new WaitForSeconds(0.5f);
		sr.color = Color.white;
		invincible = false;
	}

}
