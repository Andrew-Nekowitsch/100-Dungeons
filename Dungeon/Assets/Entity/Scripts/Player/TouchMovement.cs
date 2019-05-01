using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMovement : MonoBehaviour
{
	private Rigidbody2D playerRigidBody2D;
	private bool facingRight = false;
	private Vector3 touchPosition;
	private Player p;

	void Awake()
	{
		playerRigidBody2D = (Rigidbody2D)GetComponent(typeof(Rigidbody2D));
		p = Player.instance();
	}

	void Update()
	{
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
			touchPosition.z = 0;

			float step = p.speed * Time.deltaTime / 10;
			//	playerRigidBody2D.velocity = new Vector2((touchPosition.x - transform.position.x) * step, (touchPosition.y - transform.position.y) * step);
			transform.position = Vector2.MoveTowards(transform.position, touchPosition, step);

			if (touchPosition.x > transform.position.x && !facingRight)
			{
				Flip();
			}
			else if (touchPosition.x < transform.position.x && facingRight)
			{
				Flip();
			}
		}
		else
		{
			playerRigidBody2D.velocity = new Vector2(0, 0);
		}
	}

	void Flip()
	{    // Switch the way the player is labeled as facing.    
		facingRight = !facingRight;    // Multiply the player's x local scale by -1.    
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		Vector3 healthScale = p.HealthBG.rectTransform.localScale;
		healthScale.x *= -1;
		p.HealthBG.rectTransform.localScale = healthScale;
	}
}