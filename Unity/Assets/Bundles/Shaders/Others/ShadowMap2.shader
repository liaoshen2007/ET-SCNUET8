﻿// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_Projector' with 'unity_Projector'
// Upgrade NOTE: replaced '_ProjectorClip' with 'unity_ProjectorClip'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "ShadowMap2" { 
	Properties {
		_ShadowTex ("_ShadowTex", 2D) = "gray" {}
		_Bias("_Bias", Range(0, 2)) = 1.5
		_Bias2("_Bias2", Range(1, 20)) = 1.6
		_Strength("_Strength", Range(0, 0.2)) = 0.1
		_ShadowOpacity("ShadowOpacity", Range(0, 1))=0.5
		_Color ("Color", Color) = (0.5, 0.5, 0.5, 1)
		_SunDir("sun direction", Vector)=(0,0,0,0)//过滤掉大于90度的平面
		
	}
	Subshader {
		Tags {"Queue"="Transparent+1"}
		Pass {
			ZWrite Off
			Fog { Color (1, 1, 1) }
			
			AlphaTest Greater 0
			//ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha
			//BlendOp Max
			//Blend DstColor Zero
			Offset -1, -1
 
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			struct v2f {
				float4 uvShadow : TEXCOORD0;
				float4 uvFalloff : TEXCOORD1;
				float4 pos : SV_POSITION;
				half2 offs : TEXCOORD2;
				fixed3 worldNormal : TEXCOORD3;
			};
			
			uniform float4x4 ShadowMatrix;
			float4x4 unity_Projector;
			float4x4 unity_ProjectorClip;

			sampler2D _ShadowTex;
			fixed4 _ShadowTex_ST;
			uniform fixed4 _ShadowTex_TexelSize;
			sampler2D _FalloffTex;
			float _Bias;
			float _Bias2;
			float _Strength;
			float _ShadowOpacity;
			uniform fixed4 _Color;
			uniform half4 _SunDir;
			//
			/*static const half4 curve4[7] = { half4(0.0205,0.0205,0.0205,0), half4(0.0855,0.0855,0.0855,0), half4(0.232,0.232,0.232,0),
				half4(0.324,0.324,0.324,1), half4(0.232,0.232,0.232,0), half4(0.0855,0.0855,0.0855,0), half4(0.0205,0.0205,0.0205,0) };*/
			//
			
			v2f vert (appdata_base v)
			{
				v2f o = (v2f)0;
				o.pos = UnityObjectToClipPos (v.vertex);
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
				float4x4 matWVP = mul (ShadowMatrix, unity_ObjectToWorld);
				o.uvShadow = mul(matWVP, v.vertex);       
				//
				
				o.offs = o.uvShadow * _Bias;
				//
				return o;
			}
			
			
			

			fixed4 frag (v2f i) : SV_Target
			{
				half2 uv = i.uvShadow.xy / i.uvShadow.w * 0.5 + 0.5;
				#if UNITY_UV_STARTS_AT_TOP
                	uv.y = 1 - uv.y;
                #endif
 				fixed4 res = fixed4(0, 0, 0, 0);
				//添加法线判断
				
				fixed diff = normalize(dot(i.worldNormal, _SunDir.xyz));//法线夹角大于90度，直接clip掉阴影
				clip(0 - diff);
				//
 				//float shadowz = i.uvShadow.z / i.uvShadow.w;
				//
				
				//
				float pad = 1000 * _Bias2;
 				//float pad = 1600* _Bias2;
 				fixed4 texS = tex2D(_ShadowTex, uv);
				//
				res.rgb += _Color.rgb;
				//
 				if(texS.r > 0)
 				{
 					res.a += _Strength;
 				}
 				//float3 kDecodeDot = float3(1.0, 1/255.0, 1/65025.0);
				//float z = dot(texS.gba, kDecodeDot);
				//float flag = 1;
 				//if(texS.r == 1)
 				//{
 				//	flag = -1;
 				//}
 				//if(shadowz - _Bias> z * flag)
 				//{
 					//res.a += _Strength;
 				//}
 				texS = tex2D(_ShadowTex, uv + half2(-0.94201624/pad, -0.39906216/pad)*_Bias);//-0.9，-0.4
 				if(texS.r > 0)
 				{
 					res.a += _Strength;
 				}
 				
 				texS = tex2D(_ShadowTex, uv + half2(0.94558609/pad, -0.76890725/pad)*_Bias);//0.9，-0.8
 				if(texS.r > 0)
 				{
 					res.a += _Strength;
 				}
 				
 				texS = tex2D(_ShadowTex, uv + half2(-0.094184101/pad, -0.92938870/pad)*_Bias);//-0.1，-0.9
 				if(texS.r > 0)
 				{
 					res.a += _Strength;
 				}
 				texS = tex2D(_ShadowTex, uv + half2(0.34495938/pad, 0.29387760/pad)*_Bias);//0.3，0.3
 				if(texS.r > 0)
 				{
 					res.a += _Strength;
 				}
				//增加两次采样
				texS = tex2D(_ShadowTex, uv + half2(0.74495938 / pad, 0.69387760 / pad)*_Bias);//0.7，0.7
				if (texS.r > 0)
				{
					res.a += _Strength;
				}
				texS = tex2D(_ShadowTex, uv + half2(-0.74495938 / pad, 0.79387760 / pad)*_Bias);//-0.7，0.8
				if (texS.r > 0)
				{
					res.a += _Strength;
				}
				/////////////////////////////////////////////////////////9次采样
				texS = tex2D(_ShadowTex, uv + half2(-0.34495938 / pad, -0.29387760 / pad)*_Bias);//0.3，0.3
				if (texS.r > 0)
				{
					res.a += _Strength;
				}
				texS = tex2D(_ShadowTex, uv + half2(0.34495938 / pad, -0.39387760 / pad)*_Bias);//-0.7，0.8
				if (texS.r > 0)
				{
					res.a += _Strength;
				}
				texS = tex2D(_ShadowTex, uv + half2(-0.34495938 / pad, 0.39387760 / pad)*_Bias);//-0.7，0.8
				if (texS.r > 0)
				{
					res.a += _Strength;
				}
				//////////////////////////////////////////////////////////////////////
				////////7次采样
				//texS = tex2D(_ShadowTex, uv + half2(-0.54495938 / pad, -0.01387760 / pad)*_Bias);//0.3，0.3
				//if (texS.r > 0)
				//{
				//	res.a += _Strength;
				//}
				//texS = tex2D(_ShadowTex, uv + half2(0.60495938 / pad, -0.15387760 / pad)*_Bias);//0.3，0.3
				//if (texS.r > 0)
				//{
				//	res.a += _Strength;
				//}
				//res.rgb = lerp(res.rgb,color.rgb,0.2);
				res.a *= _ShadowOpacity;
				return res;
			}
			ENDCG
		}
	}
}
