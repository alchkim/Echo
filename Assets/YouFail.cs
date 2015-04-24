using UnityEngine;
using System.Collections;

public class YouFail : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision coll) {
		if (coll.gameObject.layer.ToString () == "8") {
			Destroy (gameObject);
		}
		if (coll.gameObject.layer.ToString () == "9") {
			for (int i = 0; i < 3; i++) {
				coll.gameObject.GetComponent< Life > ().Damage ();
			}
		}
	}
}
