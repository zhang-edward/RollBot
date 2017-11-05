using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public Rigidbody2D rb2d;
	public SpriteRenderer sr;
	public Transform playerTransform;
	public Transform enemyTransform;
	public SimpleAnimation walk;
	public SimpleAnimationPlayer anim;
	public float moveSpeed;
	public float maxHealth;
	public float energyDropAmount;
	private float health;
	protected ObjectPooler lootPool;
	public float collisionDamage;
	public float knockbackRadius;

	private Coroutine movementRoutine;
	private Coroutine bumpRoutine;

	protected virtual void Start() {
		anim.anim = walk;
		anim.Play();
		lootPool = ObjectPooler.GetObjectPooler("EnergyLoot");
		movementRoutine = StartCoroutine(Movement());
	}

	protected virtual void Awake(){
		health = maxHealth;
	}

	protected virtual IEnumerator Movement() {
		for(;;){
			Vector3 dir = (playerTransform.position - transform.position).normalized;
			rb2d.velocity = dir * moveSpeed;
			sr.flipX = dir.x < 0;
			yield return null;
		}
	}

	private IEnumerator Bumping(Vector3 init, Vector3 dest) {
		float time = 0.35f;
		float t = 0;
		while(t<time){
			t+=Time.deltaTime;
			transform.position = Vector3.Lerp(transform.position, dest, t/time);
			yield return null;
		}
		movementRoutine = StartCoroutine(Movement());
	}

	public void Hit(float damage){
		health -= damage;
		StartCoroutine(FlashRed());
		if(health <= 0){
			this.Die();
		}
	}

	public virtual void Die() {
		Player player = GameManager.instance.player.GetComponent<Player>();
		player.UpdateCombo();
		GameObject l = lootPool.GetPooledObject();
		l.transform.position = transform.position;
		l.GetComponent<EnergyLoot>().SetEnergy(energyDropAmount);
		l.SetActive(true);
		gameObject.SetActive(false);
		GameManager.instance.RemoveEnemy(this);

	}

	protected virtual void OnCollisionEnter2D(Collision2D collision) {
		Player player = GameManager.instance.player.GetComponent<Player>();
		if (collision.collider.CompareTag("Player")) {
			player.TakeEnergy(collisionDamage);
			Vector3 playerPos = playerTransform.position;
			Vector3 backwardPlayerPosUnit = (transform.position - playerPos).normalized;
			StopCoroutine(movementRoutine);
			if (bumpRoutine != null)
				StopCoroutine(bumpRoutine);
			bumpRoutine = StartCoroutine(Bumping(transform.position, (backwardPlayerPosUnit + transform.position)));
		}
	}

	private IEnumerator FlashRed()
	{
		sr.color = Color.red;
		yield return new WaitForSeconds(0.2f);
		sr.color = Color.white;
	}
}