Shader "Custom/Fractal" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_TimeScale ("Time scale", Float) = 1
		_Scale("fractal scale", Float) = 1
		_I ("intencity", Float) = 1
		_MaskTex ("Mask", 2D) = "black" {}
		_MaskTone ("Mask Tone", Vector) = (1, 0.5, 1, 0)
	}
	CGINCLUDE
		#include "UnityCG.cginc"
		#include "./Noise.cginc"
		#include "./PhotoshopMath.cginc"
 
		sampler2D _MainTex;
		float4 _MainTex_ST;
		half4 _MainTex_TexelSize;
		float _DeltaTime,_Scale,_I;
		float4 _DateTime;
		float _TimeScale;
		sampler2D _MaskTex;
		float4 _MaskTex_ST;
		float4 _MaskTone;

		struct v2f {
			float4 pos : SV_POSITION;
			half2 uvMain : TEXCOORD0;
			half2 uv : TEXCOORD1;
		};
		v2f vert( appdata_img v ) {
			v2f o;
			o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
			o.uvMain = v.texcoord;
			o.uv = v.texcoord;
			#if UNITY_UV_STARTS_AT_TOP
			if (_MainTex_TexelSize.y < 0)
		        o.uv.y = 1-o.uv.y;
			#endif
			o.uv = TRANSFORM_TEX(o.uv, _MaskTex);
			return o;
		}

		half4 frag(v2f i) : COLOR{
			float2 uv = i.uv;
			uv.x *= _ScreenParams.x / _ScreenParams.y;
			float t = _DateTime.z * _TimeScale;
			float3 f3 = (float3(uv, 0) + t * float3(0.2, 0.2, 0.4)) * _Scale;
			half n = snoise(f3)+0.5*snoise(f3*2)+0.25*snoise(f3*4)+0.125*snoise(f3*8);

			float cmask = tex2D(_MaskTex, i.uv).r;
			return lerp(n, lerp(_MaskTone.y, _MaskTone.z, n) * _MaskTone.x * cmask, saturate(_MaskTone.w)) *_I;
		}
	ENDCG
	
	SubShader {
		ZTest Always Cull Off ZWrite Off
		Fog { Mode off }  
		ColorMask RGB
 
		pass{
			CGPROGRAM
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma glsl
			ENDCG
		}
	} 
}