using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour {
	string loadLevel;

	void Start () {
		loadLevel = "_Shooter";
	}
	public void OnStart () {
		Application.LoadLevel (loadLevel);
	}
}
