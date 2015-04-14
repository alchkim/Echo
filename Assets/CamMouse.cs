using UnityEngine;
using System.Collections;

public class CamMouse : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Input.mousePosition;
	}
}
