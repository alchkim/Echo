Shader "Custom/Echo/Skin" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "black" {}
		_EchoTex ("Echo (RGBA)", 2D) = "white" {}
		_MainColor ("Main Color",Color) = (1.0,1.0,1.0,1.0)
		_SecondColor ("Second Color",Color) = (1.0,1.0,1.0,1.0)
		_ThirdColor ("Third Color",Color) = (1.0,1.0,1.0,1.0)
		_DistanceFade("Distance Fade",float) = 1.0	
		_MaxRadius("MaxRadius",float) = 1.0
		_MaxFade("MaxFade",float) = 1.0
		
		_OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_Outline ("Outline width", Range (.002, 0.03)) = .005
//		_ToonShade ("ToonShader Cubemap(RGB)", CUBE) = "" { Texgen CubeNormal }


		_Position0("Position0",Vector) = (0.0,0.0,0.0)
		_Position1("Position1",Vector) = (0.0,0.0,0.0)
		_Position2("Position2",Vector) = (0.0,0.0,0.0)
		_Position3("Position3",Vector) = (0.0,0.0,0.0)
		_Position4("Position4",Vector) = (0.0,0.0,0.0)
		_Position5("Position5",Vector) = (0.0,0.0,0.0)
		_Position6("Position6",Vector) = (0.0,0.0,0.0)
		_Position7("Position7",Vector) = (0.0,0.0,0.0)
		_Position8("Position8",Vector) = (0.0,0.0,0.0)
		_Position9("Position9",Vector) = (0.0,0.0,0.0)
		_Position10("Position10",Vector) = (0.0,0.0,0.0)
						
		_Radius0("Radius0",float) = 0.0	
		_Radius1("Radius1",float) = 0.0	
		_Radius2("Radius2",float) = 0.0	
		_Radius3("Radius3",float) = 0.0	
		_Radius4("Radius4",float) = 0.0	
		_Radius5("Radius5",float) = 0.0	
		_Radius6("Radius6",float) = 0.0	
		_Radius7("Radius7",float) = 0.0	
		_Radius8("Radius8",float) = 0.0	
		_Radius9("Radius9",float) = 0.0
		_Radius10("Radius10",float) = 0.0
						
		_Fade0("Fade0",float) = 0.0
		_Fade1("Fade1",float) = 0.0
		_Fade2("Fade2",float) = 0.0
		_Fade3("Fade3",float) = 0.0
		_Fade4("Fade4",float) = 0.0
		_Fade5("Fade5",float) = 0.0
		_Fade6("Fade6",float) = 0.0
		_Fade7("Fade7",float) = 0.0
		_Fade8("Fade8",float) = 0.0
		_Fade9("Fade9",float) = 0.0
		_Fade10("Fade10",float) = 0.0
	} 
	
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf NoLighting
		#include "UnityCG.cginc"
		
		struct Input {
			float2 uv_MainTex;
			float3 worldPos;	
		};
		
		sampler2D _MainTex;
		sampler2D _EchoTex;
		
		float4 _MainColor;
		float4 _SecondColor;
		float4 _ThirdColor;
		float _DistanceFade;
		float _MaxRadius;
		float _MaxFade;
		
		float3 _Position0;
		float3 _Position1;
		float3 _Position2;
		float3 _Position3;
		float3 _Position4;
		float3 _Position5;
		float3 _Position6;
		float3 _Position7;
		float3 _Position8;
		float3 _Position9;
		float3 _Position10;
						
		float _Radius0;
		float _Radius1;
		float _Radius2;
		float _Radius3;
		float _Radius4;
		float _Radius5;
		float _Radius6;
		float _Radius7;
		float _Radius8;
		float _Radius9;
		float _Radius10;
						
		float _Fade0;
		float _Fade1;
		float _Fade2;
		float _Fade3;
		float _Fade4;
		float _Fade5;
		float _Fade6;
		float _Fade7;
		float _Fade8;
		float _Fade9;
		float _Fade10;
		
		// Custom light model that ignores actual lighting. 
		half4 LightingNoLighting (SurfaceOutput s, half3 lightDir, half atten) {
			half4 c;
			c.rgb = s.Albedo * 10.0f;
//			c.rgb = s.Albedo*5.0f;
			c.a = s.Alpha;
			return c;
		}

		float ApplyFade(Input IN,float3 position, float radius, float infade){
			
			float dist = distance(IN.worldPos, position);	// Distance from current pixel (from its world coord) to center of echo sphere
			
			if(radius >= 3*_MaxRadius || dist >= radius){
				return 0.0;
			} else {
				// If _DistanceFade = true, fading is related to vertex distance from echo origin.
				// If false, fading is even across entire echo.
				
//				float c1 = (_DistanceFade>=1.0) ? pow(dist/radius, 100) : 1.0;
//				float c1 = (_DistanceFade>=1.0) ? (.75 * pow(dist/radius, 50) + .25 * sqrt(dist/radius)) : 1.0;
				float c1 = (_DistanceFade>=1.0) ? sqrt(dist/radius) : 1.0;
				// Apply fading effect.
				c1 *= (infade > _MaxFade) ? 0 : 1;
//				c1 *= (infade<=_MaxFade) ? 1.0-infade/_MaxFade : 0.0f;	//adjust by fade distance.
				// Ignore Fade values <= 0 (meaning no fade.)
				c1 = (infade<=0)?1.0:c1;
				
				return c1;
			}
		}
		// Custom surfacer that mimics an echo effect
		void surf (Input IN, inout SurfaceOutput o) {
			float c1 = 0.0;
			float c3 = 0.0;
			float c5 = 0.0;
			
			// manually add more echos here.
			c1 += ApplyFade(IN,_Position0,_Radius0,_Fade0);
			c1 += ApplyFade(IN,_Position1,_Radius1,_Fade1);
			c1 += ApplyFade(IN,_Position2,_Radius2,_Fade2);
			c1 += ApplyFade(IN,_Position3,_Radius3,_Fade3);
			c1 += ApplyFade(IN,_Position4,_Radius4,_Fade4);
			
			c1 /= 5.0;
			
			c3 += ApplyFade(IN,_Position5,_Radius5,_Fade5);
			c3 += ApplyFade(IN,_Position6,_Radius6,_Fade6);
			c3 += ApplyFade(IN,_Position7,_Radius7,_Fade7);
			c3 += ApplyFade(IN,_Position8,_Radius8,_Fade8);
			c3 += ApplyFade(IN,_Position9,_Radius9,_Fade9);
			
			c3 /= 5.0;  
		
			c5 += ApplyFade(IN,_Position10,_Radius10,_Fade10);
						
			float c2 = .4 - c1;
			o.Albedo = _MainColor.rgb * c2 + tex2D (_MainTex, IN.uv_MainTex).rgb * c1 ;		

			float c4 = .4 - c3;
			o.Albedo += _SecondColor.rgb * c4 + tex2D (_MainTex, IN.uv_MainTex).rgb * c3 ;	
			
			float c6 = .2 - c5;
			o.Albedo += _ThirdColor.rgb * c6 + tex2D (_MainTex, IN.uv_MainTex).rgb * c5 ;	
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
