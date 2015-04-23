using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {
	public int timeLimit = 90;
	private float startTime;
	private float endTime;
	private float guiTime;
	public Text text;
	public Text p1;
	public Text p2;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
		endTime = startTime + timeLimit;
		guiTime = endTime - Time.time;
		text.text = guiTime.ToString ();

	}
	
	// Update is called once per frame
	void Update () {
		Tick ();
	}

	void Tick () {
		guiTime = Mathf.Floor(endTime - Time.time);
		if (guiTime > 0) {
			text.text = guiTime.ToString ();
		}
		if (guiTime <= 0) {
			text.text = "TIME";
			if (int.Parse(p1.text) > int.Parse(p2.text)) {
				Application.LoadLevel ("_Player1Win");
			} else if (int.Parse(p1.text) < int.Parse(p2.text)) {
				Application.LoadLevel ("_Player2Win");
			} else {
				Application.LoadLevel ("_Tie");
			}
		}
	}

	void CountDown () {
		//Countdown sequence to start game
	}
}
