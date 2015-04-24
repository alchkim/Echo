using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Bullet : MonoBehaviour {
	public Vector3 coll;
	public bool hasCollided;
	public bool trigger;

	int MCLayer = 9;
	public Material mat;
	string material = "EchoMat";
	
	void Start () {
		trigger = false;
		hasCollided = false;
	}

	void OnCollisionEnter (Collision collision) {
		trigger = true;
		coll = collision.contacts[0].point;
		if (collision.gameObject.layer == MCLayer && collision.gameObject.tag != gameObject.tag) {
			Life life = collision.gameObject.GetComponent< Life > ();
			life.Damage ();
			return;
		}
		GameObject[] echoList = GameObject.FindGameObjectsWithTag (gameObject.tag);
		foreach (GameObject obj in echoList) {
			Bullet bullet = obj.GetComponent< Bullet > ();
			if (bullet != null) {
				if (bullet.hasCollided) {
					Destroy (obj);
				}
			}
		}
		hasCollided = true;
		Destroy (GetComponent< Collider > ());
		Destroy (GetComponent< Rigidbody > ());
		Destroy (GetComponent< MeshRenderer > ());
	}
}
