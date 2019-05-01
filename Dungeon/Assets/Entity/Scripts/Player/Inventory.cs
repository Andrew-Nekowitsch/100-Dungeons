using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
	public Weapon[] inv;
	public GameObject[] slots;
	private void Start () {
		inv = new Weapon[36];
	}

}