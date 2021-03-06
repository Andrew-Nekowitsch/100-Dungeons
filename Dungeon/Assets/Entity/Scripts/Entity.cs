﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
	public string entityName = "Entity Object";
	public int level = 1;
	public double health = 1;
	public double maxHealth = 1;
	public int strength = 1;
	public int magic = 0;
	public int speed = 10;
	public int armor = 0;
	public float attackSpeed = 1;
	public Weapon[] weapon;
	public float healthRegen = .1f;
	public int detection = 10;
	public int stealth = 5;
	public Entity target;

	public Type entityType;

	public Image healthBar;
	public Image HealthBG;

	public float attackTimer;
	public Animator animator;
	public AudioSource sound;
	public float tDistance;

	//public bool InCombat = false;

	public enum Type
	{
		Enemy,
		Player,
		Companion,
		NPC
	};

	public virtual void Start()
	{
		//tempHealth = health;
		weapon = new Weapon[7];
		sound = GetComponent<AudioSource>();
		InvokeRepeating("Regen", 0, 3);

	}
	private void Regen()
	{
		if (health <= maxHealth)
			health += healthRegen;
		if (health > maxHealth)
			health = maxHealth;

	}
	public void Update()
	{
		FindTarget();
		if (health <= 0)
			Die();
		if (HealthBG.gameObject.activeInHierarchy && health == maxHealth)
			HealthBG.gameObject.SetActive(false);
		//if (gameObject.CompareTag("Player"))
		//{
		//    if (tempHealth > health)
		//    {
		//        animator.SetBool("IsHurt", true);
		//        tempHealth = health;
		//    }
		//}

	}

	public virtual void Die()
	{
		Debug.Log("Entity Virtual");
		gameObject.SetActive(false);
	}

	public virtual void Combat(Entity other)
	{
		//Debug.Log("IN COMBAT");
		//InCombat = true;
		if (Time.time > attackTimer)
		{
			sound.Play();
			animator.SetBool("InCombat", true);
			//    Debug.Log(gameObject.GetComponent<Entity>().ToString() + " attacks");

			attackTimer = Time.time + attackSpeed;
			other.TakeDamage(strength - other.armor);
			if (this.GetComponent<Rigidbody2D>().IsSleeping())
			{
				this.GetComponent<Rigidbody2D>().WakeUp();
			}
			return;
		}
		// Debug.Log(gameObject.GetComponent<Entity>().ToString() + " recharges their attack");
	}

	public void TakeDamage(double amount)
	{
		health -= amount;
		deathCheck();
		healthBar.fillAmount = (float)(health / maxHealth);
		if (!HealthBG.gameObject.activeInHierarchy)
			HealthBG.gameObject.SetActive(true);
	}

	public virtual void deathCheck()
	{
		if (health <= 0)
			Die();
	}

	public virtual void Respawn()
	{
		health = maxHealth;
		attackTimer = 0;
		HealthBG.gameObject.SetActive(false);
		healthBar.fillAmount = 1f;
	}


	public virtual void FindTarget() { return; }

}