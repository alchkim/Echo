//using UnityEngine;
//using System.Collections;
//
//public class Player : MonoBehaviour {
//	
//	
//	private Vector3 curLoc;
//	private GameObject prevLoc;
//	public float moveSpeed = 2;
//	public float rotateSpeed;
//	private bool set;
//	public GameObject marker;
//	public GameObject teleportArriveEffect;
//	GameObject HaloEffectObject;
//	public GameObject teleportHaloEffect;
//	
//	// Camera variables
//	private Vector3 mouseOrigin;	// Position of cursor when mouse dragging starts
//	private bool isRotating;	// Is the camera being rotated?
//	private float turnSpeed = 4.0f;
//	private Vector3 defaultCameraPosition;
//
//	private int cameraZoomState = 0;
//	
//	// Camera transition variables
//	private bool isTeleporting;
//	private Vector3 teleportStartPosition;
//	private Vector3 teleportEndPosition;
//	private float warpTime = 0.0f;
//	private float maxWarpTime = 0.8f;
//	
//	private bool inputEnabled = true;
//
//	// Map variables
//	private bool mapIsOpen = false;
//	public GameObject mapCameraPrefab;
//	GameObject mapCamera;
//	private GameObject goalMapMarker;
//	public GameObject goalMapMarkerPrefab;
//
//
//	void Awake(){
//		defaultCameraPosition = Camera.main.transform.localPosition;
//		Screen.showCursor = false;
//		Screen.lockCursor = true;
//	}
//	void Update () 
//	{
//		
//		CameraCheck ();
//		if (inputEnabled) {
//			InputListen ();
//			Poof ();
//		}
//		
//		if (isTeleporting) {
//			renderer.enabled = false;
//			collider.enabled = false;
//			inputEnabled = false;
//			transform.position = Vector3.Lerp (transform.position, prevLoc.transform.position, warpTime / maxWarpTime);
//			warpTime += Time.deltaTime;
//			HaloEffectObject.transform.position = transform.position;
//			if (Vector3.Distance (transform.position, prevLoc.transform.position) < .5f){
//				isTeleporting = false;
//				inputEnabled = true;
//				transform.position = prevLoc.transform.position;
//				//rigidbody.velocity = Vector3.zero;
//				Instantiate (teleportArriveEffect, transform.position, Quaternion.identity);
//				renderer.enabled = true;
//				collider.enabled = true;
//				warpTime = 0;
//				Destroy (prevLoc);
//				prevLoc = null;
//				Destroy (HaloEffectObject);
//				HaloEffectObject = null;
//			}
//			
//		}
//
//
//	}
//	
//	private void mapFunction(bool IsOpen){
//				if (!IsOpen) {
//						mapCamera = Instantiate (mapCameraPrefab) as GameObject;
//			GameObject goal = GameObject.FindWithTag("Goal");
//			Vector3 goalPosition = goal.transform.position;
//			goalMapMarker = Instantiate (goalMapMarkerPrefab) as GameObject;
//			goalMapMarker.transform.position = goal.transform.position;
//						mapIsOpen = true;
//			//inputEnabled = false;
//				}
//				else if (IsOpen) {
//						Destroy (mapCamera);
//						mapCamera = null;
//			Destroy (goalMapMarker);
//			goalMapMarker = null;
//						mapIsOpen = false;
//			//inputEnabled = true;
//				}
//				
//		}
//	private void CameraCheck(){
//		RaycastHit[] hits;
//		int layerMask = 1 << 8;
//		layerMask += 1 << 9;
//		layerMask += 1 << 2;
//		layerMask = ~layerMask;
//		for (int playerPoint = 0; playerPoint < 4; playerPoint++) {
//			Ray cameraToPlayer;
//			Vector3 referencePosition = transform.position;
//			if (playerPoint == 0){
//				referencePosition.x += 0.45f * transform.localScale.x;
//				referencePosition.y += 0.45f * transform.localScale.y;
//			}
//			if (playerPoint == 1){
//				referencePosition.x += 0.45f * transform.localScale.x;
//				referencePosition.y -= 0.45f * transform.localScale.y;
//			}
//			if (playerPoint == 2){
//				referencePosition.x -= 0.45f * transform.localScale.x;
//				referencePosition.y += 0.45f * transform.localScale.y;
//			}
//			if (playerPoint == 3){
//				referencePosition.x -= 0.45f * transform.localScale.x;
//				referencePosition.y -= 0.45f * transform.localScale.y;
//			}
//			
//			cameraToPlayer = new Ray (referencePosition, Camera.main.transform.position - transform.position);
//			
//			hits = Physics.RaycastAll (cameraToPlayer, Vector3.Distance(referencePosition, Camera.main.transform.position), layerMask);
//			
//			for (int i = 0; i < hits.Length; ++i) {
//				RaycastHit hit = hits [i];
//				Renderer renderer = hit.collider.renderer;
//				if (renderer) {
//					AddTransparency AT = renderer.GetComponent<AddTransparency> ();
//					if (!AT) {
//						AT = renderer.gameObject.AddComponent<AddTransparency> ();
//					}
//					AT.makeTransparent ();
//				}
//			}
//		}
//	}
//
//
//	
//	private void InputListen() {
//		curLoc = transform.position;
//		
//		Vector3 velocity = Vector3.zero;
//		velocity.y = rigidbody.velocity.y;
//		if(Input.GetKey(KeyCode.A))
//		{
//			velocity += Vector3.Cross (transform.forward, Vector3.up) * moveSpeed;
//		}
//		if(Input.GetKey(KeyCode.D))
//		{
//			velocity += Vector3.Cross (transform.forward, Vector3.up) * -moveSpeed;
//		}
//		if(Input.GetKey(KeyCode.W))
//		{
//			velocity += transform.forward * moveSpeed;
//		}
//		if(Input.GetKey(KeyCode.S))
//		{
//			velocity += transform.forward * -moveSpeed;
//		}
//		if(Input.GetKey (KeyCode.Escape))
//		{
//			Application.LoadLevel (Application.loadedLevel);
//		}
//		if(Input.GetKey(KeyCode.P))
//		{
//			Screen.lockCursor = !Screen.lockCursor;
//		}
//
//		if (Input.GetKeyDown (KeyCode.Tab)) {
//			mapFunction (mapIsOpen);
//		}
//		if (Input.GetAxis ("Mouse ScrollWheel") < 0 && cameraZoomState >= -3) {
//			Ray cameraAngle = new Ray (transform.position, Camera.main.transform.position - transform.position);
//			Vector3 zoomTick = cameraAngle.direction;
//			Vector3 tempPos = Camera.main.transform.position;
//			tempPos += zoomTick;
//			cameraZoomState -= 1;
//			Camera.main.transform.position = tempPos;
//		}
//		if (Input.GetAxis ("Mouse ScrollWheel") > 0 && cameraZoomState <= 3) {
//			Ray cameraAngle = new Ray (transform.position, Camera.main.transform.position - transform.position);
//			Vector3 zoomTick = cameraAngle.direction;
//			Vector3 tempPos = Camera.main.transform.position;
//			tempPos -= zoomTick;
//			cameraZoomState += 1;
//			Camera.main.transform.position = tempPos;
//		}
//
//		// Rotate player based on mouse
//		transform.Rotate(Vector3.up * rotateSpeed * Input.GetAxis ("Mouse X"));
//		
//		// Camera stuff
//		/*
//		if(Input.GetMouseButtonDown(0))
//		{
//			// Get mouse origin
//			mouseOrigin = Input.mousePosition;
//			isRotating = true;
//		}
//		if (!Input.GetMouseButton(0)) isRotating=false;
//
//		if (isRotating)
//		{
//			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
//			
//			//transform.RotateAround(transform.position, transform.right, -pos.y * turnSpeed);
//			transform.RotateAround(transform.position, Vector3.up, -pos.x * turnSpeed);
//
//		}
//		*/
//		// end camera stuff
//		
//		
//		//if(Input.GetKey(KeyCode.Space))
//		//curLoc.y += 2* moveSpeed * Time.fixedDeltaTime;
//		transform.rigidbody.velocity = velocity;
//		
//	}
//	
//	private void Poof() {
//		if(Input.GetKeyUp(KeyCode.Mouse1)) {
//			if(set) {
//				Destroy(prevLoc);
//			}
//			prevLoc = Instantiate(marker, curLoc, Quaternion.identity) as GameObject;
//			prevLoc.transform.parent = transform.parent;
//			set = true;
//		}
//		if(Input.GetKeyUp(KeyCode.Mouse0)) {
//			if(set) {
//				
//				//transform.position = prevLoc.transform.position;
//				set = false;
//				isTeleporting = true;
//				teleportStartPosition = transform.position;
//				HaloEffectObject = Instantiate(teleportHaloEffect, curLoc, Quaternion.identity) as GameObject;
//				//HaloEffectObject.transform.parent = transform;
//				prevLoc.GetComponent<Marker>().setInvis();
//			}
//		}
//	}
//	public void MarkerDestroyed()
//	{
//		set = false;
//	}
//
//	
//	void OnCollisionEnter (Collision hit) { 
//		if(hit.gameObject.CompareTag ("MovingPlatform"))
//		{
//			transform.parent = hit.gameObject.transform; 
//		}
//		else if(hit.gameObject.CompareTag ("RotatingPlatform"))
//		{
//			transform.parent = hit.gameObject.transform.parent; 
//		}
//	}
//	void OnCollisionExit(Collision hit){
//		if(hit.gameObject.CompareTag ("MovingPlatform"))
//		{
//			transform.parent = null; 
//		}
//		else if(hit.gameObject.CompareTag ("RotatingPlatform"))
//		{
//			transform.parent = null;
//		}
//	}
//	public void reload()
//	{
//		Application.LoadLevel (Application.loadedLevel);
//	}
//}