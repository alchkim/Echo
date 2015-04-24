using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Life : MonoBehaviour {
	public int lifeTotal;
	public Text text;
	public Image heart;

	int playerLife = 1;
	int bossLife = 20;
	GameObject lifeBar;
	float counter;
	string lastHit;

	// Use this for initialization
	void Start () {
		counter = 0;
		if (gameObject.name != "Enemy") {
			lifeBar = GameObject.Find("LifeBar" + gameObject.tag);
			SetLife ();
			lifeTotal = playerLife;
		} else {
			lifeTotal = bossLife;
		}
	}
	
	// Update is called once per frame
	void Update () {
		Check ();
		if (gameObject.tag == "3") {
			counter -= Time.deltaTime;
			counter = Mathf.Clamp (counter, 0, 10);
			Revive (counter);
		}
	}

	public void Damage () {
		if (gameObject.name != "Enemy") {
			Image script = lifeBar.transform.GetChild(lifeTotal).GetComponent< Image > ();
			script.enabled = false;
		} if (gameObject.name == "Enemy") {
			gameObject.GetComponents< AudioSource > ()[2].Play ();
		}
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
			value += 3;
			text.text = value.ToString();
		} else {
			string score;
			if (gameObject.tag == "1") {
				score = "Score2";
			} else {
				score = "Score1";
			}
			text = GameObject.Find (score).GetComponent< Text> ();
			int value = int.Parse(text.text);
			value++;
			text.text = value.ToString();
		}
	}

	void Increment () {
		string score;
		if (gameObject.tag == "1") {
			score = "Score2";
		} else if (gameObject.tag == "2") {
			score = "Score1";
		} else {
			score = "Score" + lastHit;
		}
		text = GameObject.Find (score).GetComponent< Text> ();
		int value = int.Parse(text.text);
		if (gameObject.tag == "3") {
			value += 2;
		}
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
			gameObject.transform.position = list.transform.FindChild (player).GetChild (loc).transform.position + new Vector3 (0, 20, 0);
			gameObject.GetComponents< AudioSource > ()[1].Play ();
			break;
		case 2:
			player += caseSwitch;
			gameObject.transform.position = list.transform.FindChild (player).GetChild (loc).transform.position + new Vector3 (0, 20, 0);
			gameObject.GetComponents< AudioSource > ()[1].Play ();
			break;
		case 3:
			gameObject.GetComponent< Collider > ().enabled = false;
			gameObject.GetComponent< MeshRenderer > ().enabled = false;
			gameObject.GetComponent< Route > ().enabled = false;
			gameObject.GetComponent< EnemyFire > ().enabled = false;
			gameObject.GetComponent< Rigidbody > ().velocity = new Vector3 (0, 0, 0);

			gameObject.transform.position += new Vector3 (0, 20, 0);
			counter = 10;
			gameObject.GetComponents< AudioSource > ()[0].Play ();
			break;
		}
	}

	void ResetLife () {
		if (gameObject.name != "Enemy") {
			lifeTotal = playerLife;
			SetLife ();
		} else {
			lifeTotal = bossLife;
		}
	}

	void SetLife () {
		for (int i = 0; i < playerLife + 1; i++) {
			Image temp = Instantiate(heart, lifeBar.transform.position + new Vector3(i * 50, 0, 0), Quaternion.identity) as Image;
			temp.rectTransform.parent = lifeBar.transform;
		}
	}

	void Revive (float counter) {
		if (counter <= 0 && gameObject.GetComponent< MeshRenderer > ().enabled != true) {
			gameObject.GetComponent< Collider > ().enabled = true;
			gameObject.GetComponent< MeshRenderer > ().enabled = true;
			gameObject.GetComponent< EnemyFire > ().enabled = true;
			Vector3 orig = gameObject.transform.position - new Vector3 (0, 20, 0);
			gameObject.transform.position = orig;
			gameObject.GetComponent< Route > ().enabled = true;
			gameObject.GetComponents< AudioSource > ()[1].Play ();
		}
	}	

	void OnCollisionEnter (Collision coll) {
		lastHit = coll.gameObject.tag;
	}
}
