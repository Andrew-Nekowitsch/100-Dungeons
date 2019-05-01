using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
	public ushort inventorySize;
	public ushort inventoryCount = 0;
	public int Money;
	public Animator playerAnim1;
	public GameObject Helmet;
	public GameObject Chest;
	public GameObject Legs;
	public GameObject Boots;
	public GameObject MainHand;
	public GameObject OffHand;
	private static Player player;
	public static Inventory inventory;
	public static EnemyList el;

	private double tempHealth = 100;

	public static Player instance()
	{
		if (player == null)
		{
			player = GameObject.Find("Player").GetComponent<Player>();
			inventory = player.gameObject.GetComponent<Inventory>();
		}
		return player;
	}

	private void Awake()
	{
		for (int i = 0; i < inventorySize; i++)
		{
			inventory.inv[i] = null;
		}
		playerAnim1 = gameObject.GetComponent<Animator>();
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("NextScene"))
		{
			SceneMngr.NextScene();
		}
	}

	private void OnCollisionStay2D(Collision2D other)
	{
		Debug.Log("collision");
		if (other.gameObject.CompareTag("Enemy"))
		{
			if (weapon[(int)Weapon.WeaponType.weapon])
			{
				playerAnim1.SetBool("IsHurt", false);
				Combat(other.gameObject.GetComponent<Entity>());
			}
			else
				playerAnim1.SetBool("IsHurt", true);
		}
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		playerAnim1.SetBool("InCombat", false);
		playerAnim1.SetBool("IsHurt", false);
	}
	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	public override void Start()
	{
		entityType = Type.Player;
		sound = GetComponent<AudioSource>();
		el = GameObject.Find("Enemies").GetComponent<EnemyList>();
		DontDestroyOnLoad(gameObject);

	}

	private void OnDestroy()
	{
		PlayerPrefs.SetInt("Money", Money);
		PlayerPrefs.SetInt("Level", level);
		PlayerPrefs.SetInt("strength", strength);
		PlayerPrefs.SetInt("speed", speed);
		PlayerPrefs.SetInt("armor", armor);
		PlayerPrefs.SetFloat("attackSpeed", attackSpeed);
		PlayerPrefs.SetFloat("healthRegen", healthRegen);
		PlayerPrefs.SetInt("detection", detection);
		PlayerPrefs.SetInt("stealth", stealth);

	}

	public void NotHurt()
	{
		animator.SetBool("IsHurt", false);
	}

	public override void Die()
	{
		playerAnim1.SetBool("IsDead", true);
	}
	///returns true if the item was added to the inventory
	///returns false if the inventory is full
	public bool AddItemToInventory(Weapon i)
	{
		if (inventoryCount < inventorySize)
		{
			//find the first element in the array that is empty, then add the item to it
			for (int index = 0; index < inventorySize; index++)
			{
				if (inventory.inv[index] == null)
				{
					inventory.inv[index] = i;
					inventoryCount++;
					Transform[] tra = inventory.slots[index].GetComponentsInChildren<Transform>(true);
					foreach (Transform child in tra)
					{
						if (child.CompareTag("Item"))
						{
							Image image = child.GetComponent<Image>();
							image.sprite = i.icon;
							child.gameObject.SetActive(true);
						}
					}
					Equip(i);
					return true;
				}
			}
		}
		return false;
	}

	public void Equip(Weapon w)
	{
		if (weapon[(int)w.type] != null)
		{
			if (weapon[(int)w.type].Compare(w))
				return;
			UnEquip((int)w.type);
		}
		Image image;
		switch ((int)w.type)
		{
			case (int)Weapon.WeaponType.weapon:
				image = MainHand.GetComponent<Image>();
				image.sprite = w.icon;
				MainHand.SetActive(true);
				break;
			case (int)Weapon.WeaponType.helmet:
				image = Helmet.GetComponent<Image>();
				image.sprite = w.icon;
				Helmet.SetActive(true);
				break;
			case (int)Weapon.WeaponType.pants:
				image = Legs.GetComponent<Image>();
				image.sprite = w.icon;
				Legs.SetActive(true);
				break;
			case (int)Weapon.WeaponType.shirt:
				image = Chest.GetComponent<Image>();
				image.sprite = w.icon;
				Chest.SetActive(true);
				break;
			case (int)Weapon.WeaponType.boots:
				image = Boots.GetComponent<Image>();
				image.sprite = w.icon;
				Boots.SetActive(true);
				break;
			case (int)Weapon.WeaponType.ring:
				image = OffHand.GetComponent<Image>();
				image.sprite = w.icon;
				OffHand.SetActive(true);
				break;
			default:
				break;
		}
		weapon[(int)w.type] = w;

		maxHealth += w.healthBoost;
		strength += w.strengthBoost;
		speed += w.speedBoost;
		armor += w.armorBoost;
		attackSpeed += w.attackSpeedBoost;
		healthRegen += w.healthRegenBoost;
		detection += w.detectionBoost;
		stealth += w.stealthBoost;
		//TODO: update ui
		return;
	}

	public bool UnEquip(int w)
	{
		weapon[w] = null;
		maxHealth -= weapon[w].healthBoost;
		strength -= weapon[w].strengthBoost;
		speed -= weapon[w].speedBoost;
		armor -= weapon[w].armorBoost;
		attackSpeed -= weapon[w].attackSpeedBoost;
		healthRegen -= weapon[w].healthRegenBoost;
		detection -= weapon[w].detectionBoost;
		stealth -= weapon[w].stealthBoost;
		//TODO: update ui
		return false;
	}

	public override void FindTarget()
	{
		target = null;
		tDistance = Vector2.Distance(transform.position, gameObject.transform.position);
		foreach (Enemy temp in el.GetEnemyList())
		{
			if (temp.pDistance < detection) // if the enemy is inside the aggro range
			{
				if (temp.cDistance < tDistance) // if they are the closest to the companion
				{
					target = temp;
					tDistance = temp.cDistance;
				}
			}
		}
		if (target)
		{
			tDistance = Vector2.Distance(transform.position, target.gameObject.transform.position);
		}
	}
}