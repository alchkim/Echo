using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {
	public GameObject projectile;
	private float proj_speed = 750;
	private bool oldTrigger = false;
	private string player;
	private int currentSphere = 0;

	float reload;
	GameObject list;

	void Start () {
		player = gameObject.tag;
		list = GameObject.Find ("Empty" + player);
		if (player == "1") {
			currentSphere = 0;
		} else if (player == "2") {
			currentSphere = 0;
		} else {
			currentSphere = 10;
		}
		reload = 0.0f;
	}

	// Update is called once per frame
	void Update () {
		bool newTrigger = (Input.GetAxis("Fire" + player) > 0.0);
		if (oldTrigger != newTrigger && reload <= 0.0) {
			Fire();
		}
		oldTrigger = newTrigger;
		reload -= Time.deltaTime;
		reload = Mathf.Clamp (reload, 0, 0.25f);
	}

	void Fire () {
		gameObject.GetComponent< AudioSource > ().Play ();
		var foward = transform.Find ("Main Camera" + player).transform.forward;
		GameObject bullet = Instantiate(projectile, foward + transform.position + Vector3.up / 2 + transform.right, Quaternion.identity) as GameObject;

		bullet.GetComponent< EchoProp > ().CurrentSphere = currentSphere;
		bullet.transform.parent = list.transform;

		if (int.Parse (gameObject.tag) == 1) {
			if (currentSphere > 4) currentSphere = 0;
			bullet.GetComponent< EchoProp > ().CurrentSphere = currentSphere;
		} else if (int.Parse (gameObject.tag) == 2) {
			if (currentSphere > 4) currentSphere = 0;
			bullet.GetComponent< EchoProp > ().CurrentSphere2 = currentSphere;
		}

		bullet.GetComponent< Rigidbody > ().AddForce (foward * proj_speed);
		reload += 0.25f;
		currentSphere += 1;
	}
}
