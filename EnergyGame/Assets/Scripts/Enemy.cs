using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public Rigidbody2D rb2d;
	public SpriteRenderer sr;
	public Transform playerTransform;
	public SimpleAnimation walk;
	public SimpleAnimationPlayer anim;
	public float moveSpeed;
	public float maxHealth;
	private float health;
	protected ObjectPooler lootPool;

	protected virtual void Start() {
		anim.anim = walk;
		anim.Play();
		lootPool = ObjectPooler.GetObjectPooler("EnergyLoot");
	}

	protected virtual void Awake(){
		health = maxHealth;
	}

	protected virtual void Update(){
		float vx = 0;
		float vy = 0;
		Vector3 playerPos = playerTransform.position;
		Vector3 playerPosUnit = (playerPos - transform.position).normalized;
		vx = playerPosUnit.x;
		vy = playerPosUnit.y;
		rb2d.velocity = new Vector2(vx, vy) * moveSpeed;
		sr.flipX = vx < 0;
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

}