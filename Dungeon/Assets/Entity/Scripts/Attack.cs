using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
	private Vector2 direction = Vector2.zero;
	public float bulletVelocity = 5f;
	public float timer;

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.CompareTag("Player"))
			return;
		if (col.gameObject.CompareTag("Companion") || col.gameObject.CompareTag("bullet"))
		{
			gameObject.SetActive(false);
			return;
		}
		if (col.gameObject.activeInHierarchy && col.gameObject.CompareTag("Enemy"))
		{
			Debug.Log("Hit Enemy with shuriken");
			col.gameObject.GetComponent<Entity>().TakeDamage(1);
			gameObject.SetActive(false);
		}
	}

	private void OnEnable()
	{
		timer = Time.time;
	}

	private void Update()
	{
		//rotate the bullet
		gameObject.transform.Rotate(0, 0, 360 * Time.deltaTime);

		// Adds velocity to the bullet
		gameObject.GetComponent<Rigidbody2D>().velocity = direction * bulletVelocity;

		if (Time.time >= timer + 3f)
			gameObject.SetActive(false);
	}

	public void setDirection(Vector2 dir)
	{
		direction = dir;
	}
}