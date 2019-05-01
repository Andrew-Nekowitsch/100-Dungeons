using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
	private static Player p;
	private Companion c;
	private EnemyList el;
	private bool wasPlayer; //Testing if the one who killed was player

	public Weapon[] drops;
	public float pDistance = 10000;
	public float cDistance = 10000;
	public int sightRange;


	private void Awake()
	{
		c = GameObject.Find("Companion").GetComponent<Companion>();
		p = Player.instance();
		el = GameObject.Find("Enemies").GetComponent<EnemyList>();
	}

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	public override void Start()
	{
		entityType = Type.Enemy;
	}

	public override void FindTarget()
	{
		pDistance = Vector2.Distance(transform.position, p.gameObject.transform.position);
		cDistance = Vector2.Distance(transform.position, c.gameObject.transform.position);
		if (cDistance < tDistance) // if companion is closer than the target
		{
			target = c;
			tDistance = cDistance;
		}
		if (pDistance < tDistance) // if the player is closer than companion
		{
			target = p;
			tDistance = pDistance;
		}
		if (target)
		{ //? Have we found the target?
			sightRange = detection - target.stealth;
			tDistance = Vector2.Distance(transform.position, target.gameObject.transform.position);
		}
	}

	public override string ToString() => this.entityName;

	public void RemoveFromList()
	{
		el.RemoveFromList(this);
	}

	public override void Die()
	{
		RemoveFromList();
		gameObject.SetActive(false);
		//if (wasPlayer)
			//p.playerAnim1.SetBool("InCombat", false);
	}

	//private void OnCollisionEnter2D(Collision2D other)
	//{
	//	Debug.Log("collision with player");
	//	if (other.gameObject.CompareTag("Player"))
	//	{
	//		wasPlayer = true;
	//	}
	//	else
	//		wasPlayer = false;
	//}


	public override void Respawn()
	{
		health = maxHealth;
		attackTimer = 0;
		cDistance = 10000;
		tDistance = 10000;
		pDistance = 10000;
		HealthBG.gameObject.SetActive(false);
		healthBar.fillAmount = 1f;
	}
}