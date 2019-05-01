using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	private Player p;
	private Enemy e;
	private Rigidbody2D rb2d;
	private bool facingRight = true;
	public ContactFilter2D contactFilter;
	public RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
	public Collider2D c2d;
	public Collider2D[] asdf;

	private void Start()
	{
		//for collisions with other entities
		contactFilter.useTriggers = false;
		contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
		contactFilter.useLayerMask = true;
	}

	void Awake()
	{
		//find the gameobjects
		rb2d = GetComponent<Rigidbody2D>();
		e = GetComponent<Enemy>();
		p = Player.instance();
		c2d = GetComponent<Collider2D>();
	}

	void Update()
	{
		//e.FindTarget();
		Movement(e.target, e.tDistance);
	}

	void Movement(Entity target, float distance)
	{
		asdf = new Collider2D[10];
		int i = c2d.OverlapCollider(contactFilter, asdf);
		if (i > 0)
		{
			foreach (Collider2D col in asdf)
			{
				if (col != null)
					if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Companion")) // if there is a collision AND it is not an enemy
					{
						rb2d.velocity = new Vector2(0, 0); // stop walking, too close
						e.Combat(col.gameObject.GetComponent<Entity>());
						return;
					}
			}
		}
		Move(target, distance);
	}

	// @param currentDistanceAway the distance between the target and the entity
	private void Move(Entity tar, float currentDistanceAway)
	{
		if (currentDistanceAway < e.sightRange) //walk towards the player
		{
			float step = e.speed * Time.deltaTime / 10;
			transform.position = Vector2.MoveTowards(transform.position, tar.gameObject.transform.position, step);
			FaceTarget(tar);
		}
	}

	void FaceTarget(Entity target)
	{
		if (target.gameObject.transform.position.x < e.gameObject.transform.position.x && facingRight)
		{
			Flip();
		}
		else if (target.gameObject.transform.position.x > e.gameObject.transform.position.x && !facingRight)
		{
			Flip();
		}
	}

	void Flip()
	{ // Switch the way the player is labeled as facing.    
		facingRight = !facingRight; // Multiply the player's x local scale by -1.    
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		Vector3 healthScale = e.HealthBG.rectTransform.localScale;
		healthScale.x *= -1;
		e.HealthBG.rectTransform.localScale = healthScale;
	}
}