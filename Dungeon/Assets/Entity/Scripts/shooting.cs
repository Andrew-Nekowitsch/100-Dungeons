using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour {
	public readonly float bulletVelocity = 5f;
	private GameObject bullet;
	public GameObject bullet1;
	private ObjectPooler op;

	private void Start () {
		op = gameObject.GetComponent<ObjectPooler> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			Vector3 worldMousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			float step = bulletVelocity * Time.deltaTime / 10;
			Vector2 direction = (Vector2)(worldMousePos - gameObject.transform.position);
			// Creates the bullet locally
			GameObject bullet = op.GetPooledObject ();
			
			bullet.transform.position = (Vector2)(worldMousePos);
			
			direction.Normalize ();
			bullet.GetComponent<Attack> ().setDirection (direction);
			bullet.SetActive (true);

		}
	}
}