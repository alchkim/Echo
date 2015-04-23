using UnityEngine;
using System.Collections;

public class Path : MonoBehaviour {
	enum direction {North, East, South, West}
//	public GameObject center;
	public int radius;
	public int path;
	public bool clockwise;

	int speed = 5;
	Vector3 lastPos;
	Vector3 currPos;
	// Use this for initialization
	void Start () {
//		transform.position = center.transform.position + Vector3.forward * radius;
		lastPos = transform.position;
		currPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Patrol ();
	}

	void Patrol () {
		Rigidbody body = gameObject.GetComponent< Rigidbody > ();
		//X-coord is x, Y-coord is z, Z-coord is y
		switch (path) {
		case 0:
			body.velocity = Vector3.forward * speed;
			break;
		case 1:
			body.velocity = Vector3.right * speed;
			break;
		case 2:
			body.velocity = Vector3.back * speed;
			break;
		case 3:
			body.velocity = Vector3.left * speed;
			break;
		default:
			break;
		}
		currPos = transform.position;
		if (2 * radius <= Vector3.Distance(lastPos, currPos)) {
			if (clockwise) {
				path += 1;
				if (path > 3) {
					path = (int)direction.North;
				}
			} else {
				path -= 1;
				if (path < 0) {
					path = (int)direction.West;
				}
			}
			lastPos = transform.position;
		}
	}

	void Respawn () {

	}

	void Shoot () {

	}
}
