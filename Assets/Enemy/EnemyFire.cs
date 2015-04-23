using UnityEngine;
using System.Collections;

public class EnemyFire : MonoBehaviour {
	enum direction {North, East, South, West}

	public GameObject projectile;

	int fireRate = 1;
	int proj_speed = 15;
	float currTime;
	float pastTime;
	GameObject list;
	private string player;

	// Use this for initialization
	void Start () {
		player = gameObject.tag;
		list = GameObject.Find ("Empty" + player);
		pastTime = Time.time;
		currTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (currTime - pastTime > fireRate) {
			Fire ();
			pastTime = Time.time;
		}
		currTime = Time.time;
	}

	void Fire () {
		int dir = Mathf.FloorToInt(Random.Range (0, 4));
		GameObject bullet;
		switch (dir) {
		case 0:
			bullet = Instantiate(projectile, transform.position + Vector3.forward * transform.localScale.x, Quaternion.identity) as GameObject;
			bullet.GetComponent< Rigidbody > ().AddForce (Vector3.forward * proj_speed);
			break;
		case 1:
			bullet = Instantiate(projectile, transform.position + Vector3.right * transform.localScale.x, Quaternion.identity) as GameObject;
			bullet.GetComponent< Rigidbody > ().AddForce (Vector3.right * proj_speed);
			break;
		case 2:
			bullet = Instantiate(projectile, transform.position + Vector3.back * transform.localScale.x, Quaternion.identity) as GameObject;
			bullet.GetComponent< Rigidbody > ().AddForce (Vector3.back * proj_speed);
			break;
		case 3:
			bullet = Instantiate(projectile, transform.position + Vector3.left * transform.localScale.x, Quaternion.identity) as GameObject;
			bullet.GetComponent< Rigidbody > ().AddForce (Vector3.left * proj_speed);
			break;
		default:
			bullet = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
			break;
		}
		if (bullet != null) {
			bullet.transform.parent = list.transform;
		}

	}
}
