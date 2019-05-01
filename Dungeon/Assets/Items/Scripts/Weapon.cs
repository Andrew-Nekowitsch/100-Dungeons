using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public string itemName;
	public int level = 1;
	public double healthBoost = 2;
	public int strengthBoost = 1;
	public int speedBoost = 100;
	public int armorBoost = 0;
	public float attackSpeedBoost = 1;
	public int detectionBoost = 10;
	public int stealthBoost = 5;
	public float healthRegenBoost = .1f;
	public WeaponType type;
	private static Player p;
	public float distance;
	public Sprite icon;

	private void Awake()
	{
		p = Player.instance();
	}
	public enum WeaponType
	{
		weapon,
		helmet,
		pants,
		shirt,
		boots,
		gloves,
		ring
	};

	private void Update()
	{
		distance = Vector2.Distance(p.gameObject.transform.position, gameObject.transform.position);
		if (distance <= 0.75f)
		{
			if (p.AddItemToInventory(this))
			{
				p.Equip(this);
				gameObject.SetActive(false);
			}
			else
			{
				Debug.Log("couldn't add to inventory");
			}
		}
	}
	void Start()
	{
		Weapon w = gameObject.GetComponent<Weapon>();
	}

	/// <summary> returns true if the current weapon is better or equivalent
	///
	/// and returns false if the other one is better
	/// </summary>
	public bool Compare(Weapon other)
	{
		int counter = 0;
		if (healthRegenBoost > other.healthRegenBoost)
			counter++;
		else if (healthRegenBoost < other.healthRegenBoost)
			counter--;
		if (healthBoost > other.healthBoost)
			counter++;
		else if (healthBoost < other.healthBoost)
			counter--;
		if (strengthBoost > other.strengthBoost)
			counter++;
		else if (strengthBoost < other.strengthBoost)
			counter--;
		if (speedBoost > other.speedBoost)
			counter++;
		else if (speedBoost < other.speedBoost)
			counter--;
		if (armorBoost > other.armorBoost)
			counter++;
		else if (armorBoost < other.armorBoost)
			counter--;
		if (attackSpeedBoost > other.attackSpeedBoost)
			counter++;
		else if (attackSpeedBoost < other.attackSpeedBoost)
			counter--;
		if (detectionBoost > other.detectionBoost)
			counter++;
		else if (detectionBoost < other.detectionBoost)
			counter--;
		if (stealthBoost > other.stealthBoost)
			counter++;
		else if (stealthBoost < other.stealthBoost)
			counter--;
		return counter >= 0;
	}
}
