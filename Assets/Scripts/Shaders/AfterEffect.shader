Shader "Custom/AfterEffect" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Washi ("washi", 2D) = "gray"{}
		_HanaBlur ("hanabira", 2D) = "black"{}
		_Fractal ("fNoise", 2D) = "black"{}
		_FractalColor ("Fractal Color", Color) = (1, 1, 1, 1)
		_FractalPower ("Fractal Power", Float) = 1
		
		_HO ("hanabira overlay", Float) = 1.5
		_GC ("glow color", Color) = (1,1,1,1)
		_GlowBlend ("Glow Blend", Float) = 0.5
		_WashiBlend ("Washi Blend", Vector) = (1, 1, 0, 1)
	}
	CGINCLUDE
		#include "UnityCG.cginc"
		#include "PhotoshopMath.cginc"
 
		sampler2D _MainTex, _Washi, _HanaBlur, _Fractal;
		float4 _MainTex_ST;
		float4 _MainTex_TexelSize;
		float4 _Washi_ST;
		float4 _Washi_TexelSize;
		float _HO;
		float4 _GC;
		float _GlowBlend;
		float4 _WashiBlend;
		float4 _FractalColor;
		float _FractalPower;

		struct v2f {
			float4 pos : SV_POSITION;
			half2 uv : TEXCOORD0;
			half2 uvWashi : TEXCOORD1;
		};

		v2f vert( appdata_img v ) {
			v2f o;
			o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
			//o.uv = MultiplyUV( UNITY_MATRIX_TEXTURE0, v.texcoord );
			o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
			float aspect = (_ScreenParams.x * _Washi_TexelSize.w) / (_ScreenParams.y * _Washi_TexelSize.z);
			o.uvWashi = TRANSFORM_TEX(v.texcoord, _Washi) * float2(aspect, 1);
			return o;
		}
		fixed4 frag(v2f i) : COLOR{

			#if UNITY_UV_STARTS_AT_TOP
			if (_MainTex_TexelSize.y < 0) 
				i.uv.y = 1-i.uv.y;
			#endif	

			float4 c = tex2D(_MainTex, i.uv);			
			float n = tex2D(_Fractal, i.uv);			
			float4 hb = tex2D(_HanaBlur, i.uv);
			float b = saturate(2 * c.a);
			hb.rgb *= _HO; //hb.rgb*_HO;
			c.rgb += _GC.rgb * _GC.a * hb.rgb * (1-c.a)*(1-c.a);
			c.rgb += b * (n * n) * _FractalPower * _FractalColor.a * _FractalColor.rgb;
			c.rgb = lerp(c.rgb, hb.rgb, saturate(hb.a*_GlowBlend));
			
			float4 w = tex2D(_Washi, i.uvWashi);
			c.rgb *= lerp(1, (_WashiBlend.x * pow(w.rgb, _WashiBlend.y) + _WashiBlend.z), b * _WashiBlend.w);
			
			return c;
		}
	ENDCG
	
	SubShader {
		ZTest Always Cull Off ZWrite Off
		Fog { Mode off } 
 
		pass{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			ENDCG
		}
	} 
}
