using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {
	public int timeLimit = 90;
	private float startTime;
	private float endTime;
	private float guiTime;
	public Text text;
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
		}
	}

	void CountDown () {
		//Countdown sequence to start game
	}
}
