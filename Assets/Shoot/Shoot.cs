using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {
	public GameObject projectile;
	private float proj_speed = 2000;
	private bool oldTrigger = false;
	private string player;
	GameObject list;

	void Start () {
		player = gameObject.tag;
		list = GameObject.Find ("Empty" + player);
	}

	// Update is called once per frame
	void Update () {
		bool newTrigger = (Input.GetAxis("Fire" + player) > 0.0);
		if (oldTrigger != newTrigger) {
			Fire();
		}
		oldTrigger = newTrigger;
	}

	void Fire () {
		if (list.transform.childCount > 2) {
			GameObject temp = list.transform.GetChild(0).gameObject;
			Destroy (temp);
		}
		var bob = transform.Find ("Main Camera" + player).transform.forward;
		GameObject bullet = Instantiate(projectile, bob + transform.position + new Vector3 (0, 1, 0), Quaternion.identity) as GameObject;
		bullet.transform.parent = list.transform;

		bullet.GetComponent< Rigidbody > ().AddForce (bob * proj_speed);
	}
}
