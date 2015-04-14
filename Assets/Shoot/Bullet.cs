using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Bullet : MonoBehaviour {
	int MCLayer = 9;
	public bool trigger;
	public Vector3 coll;
	public Text text;
	private int tag;

	void Start () {
		tag = int.Parse(gameObject.tag);
		if (tag != 1) {
			tag = 1;
		} else {
			tag = 2;
		}
		trigger = false;
	}

	void OnCollisionEnter (Collision collision) {
		trigger = true;
		coll = collision.contacts[0].point;
		if (collision.gameObject.layer == MCLayer && collision.gameObject != transform.parent.gameObject) {
			Debug.Log ("YES");
			Increment ();
			Respawn (collision);
		}
		Destroy (GetComponent< Collider > ());
		Destroy (GetComponent< Rigidbody > ());
		Destroy (GetComponent< MeshRenderer > ());
//		Destroy (gameObject);
	}

	void Increment () {
		if (int.Parse (gameObject.tag)  == 1) {
			text = FindObjectsOfType< Text > ()[tag];
		} else if (int.Parse (gameObject.tag) == 2) {
			text = FindObjectsOfType< Text > ()[tag - 1];
		}
		int value = int.Parse(text.text);
		value++;
		text.text = value.ToString();
	}

	void Respawn (Collision collision) {
		int caseSwitch = int.Parse (collision.gameObject.tag);
		GameObject list = GameObject.Find ("SpawnList");
//		int loc = Random.Range (0, list.transform.childCount);
		int loc = Random.Range (0, 3);
		string player = "Player";
		switch (caseSwitch) {
		case 1:
			player += caseSwitch;
			collision.gameObject.transform.position = list.transform.FindChild (player).GetChild (loc).transform.position;
			break;
		case 2:
			player += caseSwitch;
			collision.gameObject.transform.position = list.transform.FindChild (player).GetChild (loc).transform.position;
			break;
		}
	}
}
