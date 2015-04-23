using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class EchoSphere3 {
	public enum ShaderPackingMode { Property, Texture };
	public ShaderPackingMode CurrentPackingMode = ShaderPackingMode.Texture;
	
	public Texture2D EchoTexture;
	public Material EchoMaterial = null;
	public Vector3 Position;
	public int SphereIndex = 0;
	
	// Echo sphere Properties
	public float SphereMaxRadius = 10.0f;		//Final size of the echo sphere.
	private float sphereCurrentRadius = 0.0f;	//Current size of the echo sphere
	
	public float FadeDelay = 0.0f;			//Time to delay before triggering fade.
	public float FadeRate = 1.0f;			//Speed of the fade away
	public float echoSpeed = 1.0f;			//Speed of the sphere growth.
	public bool is_manual = false;			//Is pulse manual.  if true, pulse triggered by left-mouse click
	
	private bool is_animated = false;		//If true, pulse is currently running.
	
	public float pulse_frequency = 5.0f;
	private float deltaTime = 0.0f;
	private float fade = 0.0f;
	
	public EchoSphere3(){}
	
	// Update is called once per frame
	public void Update () {
		if(EchoMaterial == null)return;

		deltaTime += Time.deltaTime;
		UpdateEcho();
		
		if(CurrentPackingMode == ShaderPackingMode.Texture)UpdateTexture();
		if(CurrentPackingMode == ShaderPackingMode.Property)UpdateProperties();
	}
	
	// Called to trigger an echo pulse
	public void TriggerPulse(){
		deltaTime = 0.0f;
		sphereCurrentRadius = 0.0f;
		fade = 0.0f;
		is_animated = true;
	}
	
	// Called to halt an echo pulse.
	void HaltPulse(){
		Debug.Log("HaltPulse reached");
		is_animated = false;	
	}
	
	void ClearPulse(){
		fade = 0.0f;
		sphereCurrentRadius = 0.0f;
		is_animated = false;
	}
	
	void UpdateProperties(){
		if(!is_animated)return;
		float maxRadius = SphereMaxRadius;
		float maxFade = SphereMaxRadius / echoSpeed;
		
		EchoMaterial.SetVector("_Position"+SphereIndex.ToString(),Position);
		EchoMaterial.SetFloat("_Radius"+SphereIndex.ToString(),sphereCurrentRadius);
		EchoMaterial.SetFloat("_Fade"+SphereIndex.ToString(),fade);
		
		EchoMaterial.SetFloat("_MaxRadius",maxRadius);
		EchoMaterial.SetFloat("_MaxFade",maxFade);
	}
	
	void UpdateTexture(){	
		if(!is_animated)return;
		float maxRadius = SphereMaxRadius;
		float maxFade = SphereMaxRadius / echoSpeed;
		
		EchoTexture.SetPixel(SphereIndex,0,FloatPacking.ToColor(Position.x));
		EchoTexture.SetPixel(SphereIndex,1,FloatPacking.ToColor(Position.y));
		EchoTexture.SetPixel(SphereIndex,2,FloatPacking.ToColor(Position.z));
		EchoTexture.SetPixel(SphereIndex,3,FloatPacking.ToColor(sphereCurrentRadius));
		EchoTexture.SetPixel(SphereIndex,4,FloatPacking.ToColor(fade));
		EchoTexture.Apply();	
		
		EchoMaterial.SetFloat("_MaxRadius",maxRadius);
		EchoMaterial.SetFloat("_MaxFade",maxFade);
	}
	// Called to update the echo front edge
	void UpdateEcho(){
		if(!is_animated)return;
		if(sphereCurrentRadius >= SphereMaxRadius){
			HaltPulse();
		} else {
			sphereCurrentRadius += Time.deltaTime * echoSpeed;  
		}
		
		float radius = sphereCurrentRadius;
		float maxRadius = SphereMaxRadius;
		float maxFade = SphereMaxRadius / echoSpeed;
		if(fade > maxFade){
			return;
		}
		
		if(deltaTime > FadeDelay)
			fade += Time.deltaTime * FadeRate;
	}
}
public class EchoProp : MonoBehaviour {
	public EchoSphere3.ShaderPackingMode CurrentPackingMode = EchoSphere3.ShaderPackingMode.Texture;
	public Texture2D EchoTexture;
	public Material EchoMaterial = null;
	
	public int SphereCount = 1;
	public int CurrentSphere = 0;
	public int CurrentSphere2 = 0;
	public int CurrentSphere3 = 0;
	public int CurrentStep = 0;
	// Echo sphere Properties
	public float SphereMaxRadius = 10.0f;		//Final size of the echo sphere.
	
	public float FadeDelay = 0.0f;			//Time to delay before triggering fade.
	public float FadeRate = 1.0f;			//Speed of the fade away
	public float echoSpeed = 1.0f;			//Speed of the sphere growth.
	
	private List<EchoSphere3> Spheres = new List<EchoSphere3>();
	private List<EchoSphere3> Spheres2 = new List<EchoSphere3>();
	private List<EchoSphere3> Spheres3 = new List<EchoSphere3>();

	public CharacterController control;
	
	// Use this for initialization
	void Start () {
		CreateEchoTexture();
		InitializeSpheres();
		control = gameObject.GetComponent < CharacterController > ();
	}
	
	void InitializeSpheres(){
		for(int i = 0; i < SphereCount; i++){
			EchoSphere3 es = new  EchoSphere3{
				EchoMaterial = EchoMaterial,
				EchoTexture = EchoTexture,
				echoSpeed = echoSpeed,
				SphereMaxRadius = SphereMaxRadius,
				FadeDelay = FadeDelay,
				FadeRate = FadeRate,
				SphereIndex = i,
				CurrentPackingMode = CurrentPackingMode
			};
			Spheres.Add(es);
		}
		for(int i = 5; i < SphereCount + 5; i++){
			EchoSphere3 es = new  EchoSphere3{
				EchoMaterial = EchoMaterial,
				EchoTexture = EchoTexture,
				echoSpeed = echoSpeed,
				SphereMaxRadius = SphereMaxRadius,
				FadeDelay = FadeDelay,
				FadeRate = FadeRate,
				SphereIndex = i,
				CurrentPackingMode = CurrentPackingMode
			};
			Spheres2.Add(es);
		}
		for (int i = 10; i < SphereCount + 10; i++) {
			EchoSphere3 es = new  EchoSphere3{
				EchoMaterial = EchoMaterial,
				EchoTexture = EchoTexture,
				echoSpeed = echoSpeed,
				SphereMaxRadius = SphereMaxRadius,
				FadeDelay = FadeDelay,
				FadeRate = FadeRate,
				SphereIndex = i,
				CurrentPackingMode = CurrentPackingMode
			};
			Spheres3.Add(es);
		}
	}
	/// <summary>
	/// Create an echo texture used to hold multiple echo sources and fades.
	/// </summary>
	void CreateEchoTexture(){
		EchoTexture = new Texture2D(128,128,TextureFormat.RGBA32,false);
		EchoTexture.filterMode = FilterMode.Point;
		EchoTexture.Apply();
		
		EchoMaterial.SetTexture("_EchoTex",EchoTexture);
	}
	// Update is called once per frame
	void Update () {
		if(EchoMaterial == null)return;
		//		if (control.velocity != Vector3.zero) {
		//			UpdateSteps ();
		//		}
		foreach (EchoSphere3 es in Spheres){
			es.Update();
		}
		foreach (EchoSphere3 es in Spheres2) {
			es.Update();
		}
		if (gameObject.tag != "Echo") {
			UpdateRayCast();
		} else {
			UpdateRoom();
		}
	}
	
	// Called to manually place echo pulse
	void UpdateRayCast() {
		Bullet bullet = GetComponent< Bullet > ();
		if (bullet.trigger) {
			Ray ray = Camera.main.ScreenPointToRay(bullet.coll);
			if (Physics.Raycast(transform.position , transform.forward)) {
				if (int.Parse(bullet.tag) == 1) {
					Spheres[CurrentSphere].TriggerPulse();
					Spheres[CurrentSphere].Position = transform.position;
					
					CurrentSphere += 1;
					if(CurrentSphere >= Spheres.Count)CurrentSphere = 0;
				} else if (int.Parse(bullet.tag) == 2) {
					Spheres2[CurrentSphere2].TriggerPulse();
					Spheres2[CurrentSphere2].Position = transform.position;
					
					CurrentSphere2 += 1;
					if(CurrentSphere2 >= Spheres.Count)CurrentSphere2 = 0;
				}
			}
		}
	}

	void UpdateRoom() {
		StartCoroutine("Wait");
		Ray ray = Camera.main.ScreenPointToRay(gameObject.transform.position);
		if (Physics.Raycast(transform.position , transform.forward)) {
			Spheres[CurrentSphere3].TriggerPulse();
			Spheres[CurrentSphere3].Position = transform.position;
		}
		double time = Time.time;
		double endTime = time + 5.0;
	}

	IEnumerator Wait() {
		yield return new WaitForSeconds(5);
	}
}
