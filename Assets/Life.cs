using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Life : MonoBehaviour {
	public int lifeTotal;
	public Text text;
	int playerLife = 1;
	int bossLife = 20;
	// Use this for initialization
	void Start () {
		if (gameObject.name != "Enemy") {
			lifeTotal = playerLife;
		} else {
			lifeTotal = bossLife;
		}
	}
	
	// Update is called once per frame
	void Update () {
		Check ();
	}

	public void Damage () {
		lifeTotal -= 1;
	}

	public void Check (Collision collider) {
		if (lifeTotal < 0) {
			ResetLife ();
			Increment (collider);
			Respawn ();
		}
	}

	public void Check () {
		if (lifeTotal < 0) {
			ResetLife ();
			Increment ();
			Respawn ();
		}
	}

	void Increment (Collision collider) {
		int tag = int.Parse (gameObject.tag);
		if (tag == 3) {
			string winner = collider.gameObject.tag;
			string score = "Score" + winner;
			text = GameObject.Find (score).GetComponent< Text> ();
			int value = int.Parse(text.text);
			value++;
			text.text = value.ToString();
		} else {
			string score = "Score" + gameObject.tag;
			text = GameObject.Find (score).GetComponent< Text> ();
			int value = int.Parse(text.text);
			value++;
			text.text = value.ToString();
		}
	}

	void Increment () {
		string score = "Score" + gameObject.tag;
		text = GameObject.Find (score).GetComponent< Text> ();
		int value = int.Parse(text.text);
		value++;
		text.text = value.ToString();
	}
	
	void Respawn () {
		int caseSwitch = int.Parse (gameObject.tag);
		GameObject list = GameObject.Find ("SpawnList");
		int loc = Mathf.FloorToInt(Random.Range (0, 4));
		string player = "Player";
		switch (caseSwitch) {
		case 1:
			player += caseSwitch;
			gameObject.transform.position = list.transform.FindChild (player).GetChild (loc).transform.position;
			break;
		case 2:
			player += caseSwitch;
			gameObject.transform.position = list.transform.FindChild (player).GetChild (loc).transform.position;
			break;
		case 3:
			break;
		}
	}

	void ResetLife () {
		if (gameObject.name != "Enemy") {
			lifeTotal = playerLife;
		} else {
			lifeTotal = bossLife;
		}
	}

//	void OnCollision (Collision collider) {
//		Debug.Log ("Dead");
//		Check (collider);
//	}
}
