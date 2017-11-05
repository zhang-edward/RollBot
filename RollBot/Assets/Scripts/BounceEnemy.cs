using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceEnemy : Enemy {

	private Vector2 dir;

	protected override void Start()
	{
		base.Start();
		Bounce();
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
	}

	private void Bounce() {
		dir = (playerTransform.position - transform.position).normalized;
	}
}
