using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceEnemy : Enemy {

	private Vector2 dir;

	protected override void Start()
	{
		base.Start();
		do{
		float initX = Random.Range(-100, 100);
		float initY = Random.Range(-100, 100);
		dir = new Vector2(initX,initY).normalized;
		} while(dir.magnitude == 0); //just in case it doesn't have a starting velocity
//		print("new spawn for smallEnemy: " + dir);
	}

	protected override IEnumerator Movement()
	{
		for (;;)
		{
			rb2d.velocity = dir * moveSpeed;
			yield return null;
		}
	}

	protected override void OnCollisionEnter2D(Collision2D collision)
	{
		base.OnCollisionEnter2D(collision);
		if (collision.collider.CompareTag("Collider"))
		{
			Bounce();
		}
	
		if(collision.collider.CompareTag("Player")){
			Bounce();
		}
	}

	private void Bounce() {
		dir = (playerTransform.position - transform.position).normalized;
	}
}
