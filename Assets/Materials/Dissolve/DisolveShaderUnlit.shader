﻿Shader "Custom/DisolveShaderUnlit" {
    Properties {
         _MainTex ("Texture (RGB)", 2D) = "white" {}
          _SliceGuide ("Slice Guide (RGB)", 2D) = "white" {}
          _SliceAmount ("Slice Amount", Range(0.0, 1.0)) = 0.5     
         _BurnSize ("Burn Size", Range(0.0, 1.0)) = 0.15
         _BurnRamp ("Burn Ramp (RGB)", 2D) = "white" {}
     }
	SubShader {
		Tags { "IgnoreProjector"="True" "RenderType"="TransparentCutout" }
		LOD 300
		
		CGPROGRAM
		#pragma surface surf NoLighting alphatest:Zero

        fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten)
        {
            fixed4 c;
            c.rgb = s.Albedo; 
            c.a = s.Alpha;
            return c;
        }

		  sampler2D _MainTex;
          sampler2D _SliceGuide;
          float _SliceAmount;
          sampler2D _BurnRamp;
          float _BurnSize;

		struct Input {
		    float2 uv_MainTex;
            float2 uv_SliceGuide;
            float _SliceAmount;
		};

		 void surf (Input IN, inout SurfaceOutput o) {
              clip(tex2D (_SliceGuide, IN.uv_SliceGuide).rgb - _SliceAmount);
              o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
     
             half test = tex2D (_SliceGuide, IN.uv_MainTex).rgb - _SliceAmount;
             
             if(test < _BurnSize && _SliceAmount > 0 && _SliceAmount < 1) {
                o.Emission = tex2D(_BurnRamp, float2(test *(1/_BurnSize), 0));
                o.Albedo *= o.Emission;
             }
         }
		ENDCG
	} 
	FallBack "Transparent/Cutout/VertexLit"
}