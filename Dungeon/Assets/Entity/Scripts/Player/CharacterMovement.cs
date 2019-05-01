using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
	private Rigidbody2D playerRigidBody2D;
	private bool facingRight = true;
	private Player p;
	public Animator playerAnim;
	public Rigidbody2D rb2d;
	public ContactFilter2D contactFilter;
	//	public RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
	public Collider2D c2d;
	public Collider2D[] asdf;

	void Awake()
	{
		playerRigidBody2D = (Rigidbody2D)GetComponent(typeof(Rigidbody2D));
		p = Player.instance();
		playerAnim = gameObject.GetComponent<Animator>();
	}

	void Update()
	{
		Movement();
	}

	void Movement()
	{
		asdf = new Collider2D[10];
		int i = c2d.OverlapCollider(contactFilter, asdf);
		if (i > 0)
		{
			foreach (Collider2D col in asdf)
			{
				if (col != null)
					if (col.gameObject.CompareTag("Enemy")) // if there is a collision AND it is not an enemy
					{
						rb2d.velocity = new Vector2(0, 0); // stop walking, too close
						p.Combat(col.gameObject.GetComponent<Entity>());
						break;
					}
			}
		}
		Move();
	}

	void Move()
	{
		float movePlayerVectorH = Input.GetAxis("Horizontal");
		float movePlayerVectorV = Input.GetAxis("Vertical");
		float step = p.speed * Time.deltaTime / 10;
		playerRigidBody2D.velocity = new Vector2(movePlayerVectorH * step, movePlayerVectorV * step) * 90;
		if (movePlayerVectorH > 0 && !facingRight)
		{
			Flip();
		}
		else if (movePlayerVectorH < 0 && facingRight)
		{
			Flip();
		}
		if (movePlayerVectorH < 0)
			movePlayerVectorH *= -1;
		if (movePlayerVectorV < 0)
			movePlayerVectorV *= -1;
		float moveAvg = ((movePlayerVectorH + movePlayerVectorV));
		playerAnim.SetFloat("Walk", Mathf.Abs(moveAvg));

	}

	void Flip()
	{ // Switch the way the player is labeled as facing.    
		facingRight = !facingRight; // Multiply the player's x local scale by -1.    
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		Vector3 healthScale = p.HealthBG.rectTransform.localScale;
		healthScale.x *= -1;
		p.HealthBG.rectTransform.localScale = healthScale;
	}

}