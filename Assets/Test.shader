﻿Shader "Shader" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "black" {}
        _MainColor ("Main Color", Color) = (1.0, 1.0, 1.0, 1.0) // Color of echo
        _Position ("Position", Vector) = (0.0, 0.0, 0.0)
        _Radius ("Radius", float) = 5.0
        _MaxRadius ("MaxRadius", float) = 5.0
        _Fade ("Fade", float) = 0.0
        _MaxFade ("MaxFade", float) = 0.0
	}
	SubShader {
		Tags { "RenderType" = "Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf NoLighting

//Makes it so that lighting from other lights in world are ignored
        half4 LightingNoLighting (SurfaceOutput s, half3 lightDir, half atten) {
            half4 c;
            c.rgb = s.Albedo;
            c.a = s.Alpha;
            return c;
        }
        
//        half4 LightingSimpleLambert (SurfaceOutput s, half3 lightDir, half atten) {
//            half NdotL = dot (s.Normal, lightDir);
//            half4 c;
//            c.rgb = s.Albedo * _LightColor0.rgb * (NdotL * atten * 2);
//            c.a = s.Alpha;
//            return c;
//        }

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		sampler2D _MainTex;
		float4 _MainColor;
		float3 _Position;
		float _Radius;
		float _MaxRadius;
		float _Fade;
		float _MaxFade;
	
		void surf (Input IN, inout SurfaceOutput o) {
			float dist = distance (IN.worldPos, _Position); 
			float fade = (dist - _Radius) * -1 + _Fade;
			float c2 = fade / _MaxRadius;
			float c1 = 1.0 - c2;
			
			half4 c = tex2D (_MainTex, IN.uv_MainTex);

			//If this pixel is inside echo range, change, else maintain main texture			
			if(dist > _Radius && fade > 0.0){
				o.Albedo = _MainColor.rgb * c1 + c.rgb * c2;
            } else {
				o.Albedo = c.rgb;
			}
//			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
  