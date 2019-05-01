using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : Entity
{
	private Entity enemy;

	private static Player p;
	private Companion c;
	public static EnemyList el;
	public float pDistance = 10000f; /// distance to the player
	public float eDistance = 10000f;
	public int sightRange = 10;

	private void Awake()
	{
		c = GetComponent<Companion>();
		p = Player.instance();
		el = GameObject.Find("Enemies").GetComponent<EnemyList>();
	}

	public override void Start() {
		DontDestroyOnLoad(gameObject);
		weapon = new Weapon[7];
		sound = GetComponent<AudioSource>();
		InvokeRepeating("Regen", 0, 3);
	}

	public override void FindTarget()
	{
		eDistance = 100;
		pDistance = Vector2.Distance(transform.position, p.gameObject.transform.position);
		target = p;
		if (pDistance > 15) // get the the choppa! or to the player, w/e
		{
			gameObject.transform.position = p.gameObject.transform.position;
			tDistance = pDistance;
			return;
		}
		if (pDistance > 10) // get the the choppa! or to the player, w/e
		{
			tDistance = pDistance;
			return;
		}
		foreach (Enemy temp in el.GetEnemyList())
		{
			if (temp.pDistance < p.detection - temp.stealth) // if the enemy is inside the aggro range
			{
				if (temp.cDistance < eDistance) // if they are the closest to the companion
				{
					target = temp;
					eDistance = temp.cDistance;
				}
			}
		}
		if (target == p)
			tDistance = pDistance;
		else
			tDistance = eDistance;

		sightRange = detection - target.stealth;
	}

	public override string ToString() => this.entityName;

}