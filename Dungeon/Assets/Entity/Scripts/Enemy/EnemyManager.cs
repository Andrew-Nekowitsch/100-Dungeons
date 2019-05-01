using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	private static Player player; // don't keep spawning while user is dead
	private static Companion comp;
	public float spawnTimer = 0.1f;
	public Transform[] spawnPoints;
	public Enemy[] lastSpawn;
	private int index = 0;
	public ObjectPooler op;
	EnemyList el;

	private void Awake()
	{
		player = Player.instance();
		comp = GameObject.Find("Companion").GetComponent<Companion>();
		lastSpawn = new Enemy[spawnPoints.Length];
		el = GameObject.Find("Enemies").GetComponent<EnemyList>();
		op = GameObject.Find("EnemyPool").GetComponent<ObjectPooler>();
	}

	void Start()
	{
		InvokeRepeating("Spawn", 0.1f, spawnTimer);
	}

	void Spawn()
	{
		if (player.health <= 0)
			return;

		FindLocation();

		//don't spawn one if there is another one nearby
		if (lastSpawn[index] != null) //? if there has not yet been a spawn here
		{
			float lastSpawnDist = Vector2.Distance(spawnPoints[index].position, lastSpawn[index].transform.position);
			if (lastSpawn[index].gameObject.activeInHierarchy && lastSpawnDist < 2) //? if the mob that spawned here is still within 1m of the last spawned enemy
				return;
			float playerDist = Vector2.Distance(spawnPoints[index].position, player.transform.position);
			if (lastSpawn[index].gameObject.activeInHierarchy && playerDist < 2) //? if the mob that spawned here is still within 1m of the player
				return;
			float companionDist = Vector2.Distance(spawnPoints[index].position, comp.transform.position);
			if (lastSpawn[index].gameObject.activeInHierarchy && companionDist < 2) //? if the mob that spawned here is still within 1m of the companion
				return;
		}
		CreateMob(spawnPoints[index].position);
	}

	private void CreateMob(Vector2 loc)
	{
		GameObject go = op.GetPooledObject();
		if (go != null)
		{
			if (!go.activeInHierarchy)
			{
				go.transform.position = loc;
				go.GetComponent<Entity>().Respawn();
				go.SetActive(true);
				el.AddToList(go.GetComponent<Enemy>());
			}
			else
				Debug.Log("active in hierarchy, cannot add to list");
			go.SetActive(true);

			lastSpawn[index] = go.GetComponent<Enemy>();
		}
	}

	///choose a random spawn point
	private void FindLocation()
	{
		index += Random.Range(0, spawnPoints.Length * 21);
		index = index % spawnPoints.Length;
	}
}
