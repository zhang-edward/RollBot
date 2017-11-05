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
	private float health;
	protected ObjectPooler lootPool;
	public float collisionDamage;
	public float knockbackRadius;
	private bool isBumped;

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

	public IEnumerator Movement(){
		for(;;){
			float vx = 0;
			float vy = 0;
			Vector3 playerPos = playerTransform.position;
			Vector3 playerPosUnit = (playerPos - transform.position).normalized;
			vx = playerPosUnit.x;
			vy = playerPosUnit.y;
			rb2d.velocity = new Vector2(vx, vy) * moveSpeed;
			sr.flipX = vx < 0;
			yield return null;
		}
	}


	public IEnumerator Bumping(Vector3 init, Vector3 dest){
		float time = 0.5f;
		float t = 0;
		print("Destination: " + dest);
		while(t<time){
			t+=Time.deltaTime;
			transform.position = Vector3.Lerp(transform.position, dest, t/time);
			print(transform.position);

			yield return null;
		}
		movementRoutine = StartCoroutine(Movement());
	}

	public void Hit(float damage){
		health -= damage;
		if(health <= 0){
			this.Die();
		}
	}

	public virtual void Die() {
		GameObject l = lootPool.GetPooledObject();
		l.transform.position = transform.position;
		l.SetActive(true);
		gameObject.SetActive(false);
	}

	public void OnCollisionEnter2D(Collision2D collision) {
		Player player = GameManager.instance.player.GetComponent<Player>();
		if (collision.collider.CompareTag("Player")) {
			player.takeEnergy(collisionDamage);
			Vector3 playerPos = playerTransform.position;
			Vector3 backwardPlayerPosUnit = (transform.position - playerPos).normalized;
			print("Destination (calculation): " + backwardPlayerPosUnit + transform.position);
			StopCoroutine(movementRoutine);
			if (bumpRoutine != null)
				StopCoroutine(bumpRoutine);
			bumpRoutine = StartCoroutine(Bumping(transform.position, (backwardPlayerPosUnit + transform.position)));
		}

		/* player.bump() feature DEPRECATED
		Vector3 playerPos = playerTransform.position;
		Vector3 playerPosUnit = playerTransform.position.normalized;
		Vector3 bumpPosUnit = (playerTransform).position.normalized;
		player.Bump(playerPos,playerPos+playerPosUnit*knockbackRadius);
		*/
	}
}