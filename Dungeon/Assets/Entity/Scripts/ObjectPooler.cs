using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {
	public ObjectPooler instance;

	public List<GameObject> pooledObjects;
	public GameObject objectToPool;
	public int amountToPool;

	void Start () {
		instance = this;
		instance.pooledObjects = new List<GameObject> ();
		for (int i = 0; i < amountToPool; i++) {
			GameObject obj = (GameObject) Instantiate (objectToPool);
			obj.SetActive (false);
			instance.pooledObjects.Add (obj);
		}
	}

	/// returns an inactive gameobject
	/// or null if there is nothing left to be returned.
	public GameObject GetPooledObject () {
		for (int i = 0; i < instance.pooledObjects.Count; i++) {
			if (!instance.pooledObjects[i].activeInHierarchy) {
				return instance.pooledObjects[i];
			}
		}
		return null;
	}
}