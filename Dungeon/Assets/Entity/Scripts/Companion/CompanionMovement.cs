using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionMovement : MonoBehaviour
{
	private Player p;
	private Companion c;
	private Rigidbody2D rb2d;
	private bool facingRight = true;
	private Collider2D[] asdf;
	public ContactFilter2D contactFilter;
	public RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
	public Collider2D c2d;
	public Animator animator;

	private void Start()
	{
		//for collisions with other entities
		contactFilter.useTriggers = false;
		contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
		contactFilter.useLayerMask = true;
		animator = gameObject.GetComponent<Animator>();
	}

	void Awake()
	{
		//find the gameobjects
		rb2d = GetComponent<Rigidbody2D>();
		c = GetComponent<Companion>();
		p = Player.instance();
		c2d = GetComponent<Collider2D>();
	}

	void Update()
	{
		//	c.FindTarget();
		Movement(c.target, c.tDistance);
	}

	void Movement(Entity target, float distance)
	{
		animator.SetFloat("Speed", Mathf.Abs(distance));
		//collision detection
		asdf = new Collider2D[10];
		int i = c2d.OverlapCollider(contactFilter, asdf);
		if (i > 0)
		{
			rb2d.velocity = new Vector2(0, 0); // stop walking, too close
			foreach (Collider2D col in asdf)
			{
				if (col != null) // if there is a collision
				{
					//if (col.gameObject.tag.Equals("Player"))
					//Debug.Log("companion is touching player");
					//if (col.gameObject.tag.Equals("Enemy"))
					//Debug.Log("companion is touching enemy");

					if (col.gameObject.GetComponent<Entity>() == target && target != p)
					{
						c.Combat(col.gameObject.GetComponent<Entity>());
						return;
					}
				}
			}
		}

		Move(target, distance);


	}

	// @param currentDistanceAway the distance between the target and the entity
	private void Move(Entity tar, float currentDistanceAway)
	{
		if (tar.gameObject.CompareTag("Player") && currentDistanceAway <= 1.5)
		{
			return;
		}
		if (currentDistanceAway < c.sightRange) //walk towards the target
		{
			animator.SetFloat("Speed", Mathf.Abs(currentDistanceAway));
			float step = c.speed * Time.deltaTime / 10;
			transform.position = Vector2.MoveTowards(transform.position, tar.gameObject.transform.position, step);
			FaceTarget(tar);
			this.animator.SetBool("InCombat", false);
		}
	}

	void FaceTarget(Entity target)
	{
		if (target.gameObject.transform.position.x < c.gameObject.transform.position.x && facingRight)
		{
			Flip();
		}
		else if (target.gameObject.transform.position.x > c.gameObject.transform.position.x && !facingRight)
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
		Vector3 healthScale = c.HealthBG.rectTransform.localScale;
		healthScale.x *= -1;
		c.HealthBG.rectTransform.localScale = healthScale;
	}
}