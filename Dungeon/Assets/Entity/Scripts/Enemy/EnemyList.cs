using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
	public static LinkedList<Enemy> enemyList;
	public int numEnemies;

	private void Awake()
	{
		enemyList = new LinkedList<Enemy>();
		numEnemies = enemyList.Count;
	}

	///returns a LinkedList<Enemy> of the current list
	public LinkedList<Enemy> GetEnemyList() => enemyList;

	///returns true if successful
	///returns false if unsuccessful
	///adds an enemy to the back of the linked list
	public bool AddToList(Enemy e)
	{
		if (enemyList.AddLast(e) != null) {
			numEnemies++;
			return true;
		}
		return false;
	}

	/// returns true if succesfully removes else
	/// returns false if unsuccesful
	public bool RemoveFromList(Enemy e)
	{
		if (enemyList.Remove(e))
		{
			numEnemies--;
			e.gameObject.SetActive(false);
			return true;
		}
		return false;
	}
}
