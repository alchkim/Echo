using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	private float moveSpeed = 5.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		var directionVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
//		if (directionVector != Vector3.zero) {
//			// Get the length of the directon vector and then normalize it
//			// Dividing by the length is cheaper than normalizing when we already have the length anyway
//			var directionLength = directionVector.magnitude;
//			directionVector = directionVector / directionLength;
//			
//			// Make sure the length is no bigger than 1
//			directionLength = Mathf.Min(1, directionLength);
//			
//			// Make the input vector more sensitive towards the extremes and less sensitive in the middle
//			// This makes it easier to control slow speeds when using analog sticks
//			directionLength = directionLength * directionLength;
//			
//			// Multiply the normalized direction vector by the modified length
//			directionVector = directionVector * directionLength;
//		}
//
//		directionVector.x = directionVector.x * transform.forward.x;
//		directionVector.y = directionVector.y * transform.forward.y;
//		directionVector.z = directionVector.z * transform.forward.z;
//
//		rigidbody.velocity = directionVector;
		InputListen ();
//		float h = Input.GetAxis ("Horizontal");
//		float v = Input.GetAxis ("Vertical");
//
//		Vector3 targetDirection = new Vector3(h, 0f, v);
//		targetDirection = Camera.main.transform.TransformDirection(targetDirection);
//		targetDirection.y = 0.0f;
	}

	private void InputListen() {

		Vector3 velocity = Vector3.zero;
		velocity.y = GetComponent<Rigidbody>().velocity.y;
		if(Input.GetKey(KeyCode.A))
		{
			velocity += Vector3.Cross (transform.forward, Vector3.up) * moveSpeed;
//			velocity += transform.right * moveSpeed;
		}
		if(Input.GetKey(KeyCode.D))
		{
			velocity += Vector3.Cross (transform.forward, Vector3.up) * -moveSpeed;
//			velocity += transform.right * -moveSpeed;
		}
		if(Input.GetKey(KeyCode.W))
		{
			velocity += transform.forward * moveSpeed;
		}
		if(Input.GetKey(KeyCode.S))
		{
			velocity += transform.forward * -moveSpeed;
		}
		
		// Rotate player based on mouse
//		transform.Rotate(Vector3.up * rotateSpeed * Input.GetAxis ("Mouse X"));
		Debug.Log (velocity);
		transform.GetComponent<Rigidbody>().velocity = velocity;
		
	}

	void OnCollisionEnter (Collision coll) {
		if (coll.gameObject.tag == "Floor") {
			GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
		}
	}
}
