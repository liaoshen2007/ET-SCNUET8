Shader "Blz/Effect/NoiseDissolve"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		[HDR]_Color ("Color", Color) = (1, 1, 1, 1)
		
		_MaskMap ("MaskMap", 2D) = "white" {}
		_NoiseDissolveMap ("NoiseDissolveMap", 2D) = "black" {}
		_NoiseVertexMap ("NoiseVertexMap", 2D) = "black"{}

		[HDR]_DissolveColor("DissolveColor", Color) = (0.5, 0.5, 0.5, 0.5)
		_DissolveRange("DissolveRange", Range(0, 1)) = 0
		_DissolveOffset("DissolveOffeset", Range(0, 1)) = 0
		
		[HideInInspector]_Mode ("__mode", Float) = 0
		[HideInInspector]_SrcBlend ("__src", Float) = 1
		[HideInInspector]_DstBlend ("__dst", Float) = 0

		[HideInInspector]_StencilComp("Stencil Comparison", Float) = 8
		[HideInInspector]_Stencil("Stencil ID", Float) = 0
		[HideInInspector]_StencilOp("Stencil Operation", Float) = 0
		[HideInInspector]_StencilWriteMask("Stencil Write Mask", Float) = 255
		[HideInInspector]_StencilReadMask("Stencil Read Mask", Float) = 255
		[HideInInspector]_ColorMask("Color Mask", Float) = 15
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue"="Transparent+10" }

		Pass
		{
			Stencil
			{
				Ref[_Stencil]
				Comp[_StencilComp]
				Pass[_StencilOp]
				ReadMask[_StencilReadMask]
				WriteMask[_StencilWriteMask]
			}
			ColorMask[_ColorMask]

			Blend [_SrcBlend] [_DstBlend]
			ZWrite Off
			Cull Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma multi_compile_particles

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				//**** xy is uv. z is MainTex x speed. w is MainTex y speed.
				float4 tex0 : TEXCOORD0;
				//**** x is NoiseTex x Speed. y is NoiseTex y Speed. z is cutoff. w is NoiseMap x Speed.
				float4 tex1 : TEXCOORD1;
				//**** x is NoiseMap x Speed.y is 
				float4 tex2 : TEXCOORD2;
				float4 col : COLOR;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f
			{
				fixed4 uv : TEXCOORD0;
				fixed4 uv1 : TEXCOORD1;
				fixed4 args : TEXCOORD2;
				float4 vertex : SV_POSITION;

				#if UNITY_UI_CLIP_RECT
				float2 objPos : TEXCOORD3;
				#endif
				fixed4 args2 : TEXCOORD4;
				float4 col : COLOR;

				UNITY_VERTEX_OUTPUT_STEREO
			};

			sampler2D _MainTex, _NoiseDissolveMap, _NoiseVertexMap, _MaskMap;
			fixed4 _MainTex_ST, _NoiseDissolveMap_ST, _NoiseVertexMap_ST, _MaskMap_ST;
			float4 _Color, _DissolveColor;
			float4 _ClipRect;
			fixed _DissolveRange, _DissolveOffset;
			fixed _NoiseVertexXLength, _NoiseVertexYLength;
			
			v2f vert (appdata v)
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				#if UNITY_UI_CLIP_RECT
				o.objPos = v.vertex;
				#endif

				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv.xy = TRANSFORM_TEX(v.tex0.xy, _MainTex);
				o.uv.xy += v.tex2.y;
				//o.uv.xy += v.tex0.zw * _Time.y;
				o.uv.zw = TRANSFORM_TEX(v.tex0.xy, _NoiseVertexMap) + v.tex2.z; 
				o.uv1.xy = TRANSFORM_TEX(v.tex0.xy, _NoiseDissolveMap)+ v.tex2.z;
				o.uv1.xy += v.tex1.xy * _Time.y;
				o.uv1.zw = TRANSFORM_TEX(v.tex0.xy, _MaskMap) + v.tex0.zw * _Time.y;
				o.args.x = v.tex1.z;
				o.args.yz = fixed2(v.tex1.w, v.tex2.x);
				o.args.w = v.tex2.y;
				o.args2.x = v.tex2.z;
				o.col = v.col;
				return o;
			}

			float4 frag (v2f i) : SV_Target
			{
				float3 rgb = _Color * i.col;
				float alpha = tex2D(_MainTex, i.uv).r * _Color.a * i.col.a;
				float4 col = float4(rgb, alpha);
				float4 offset1 = tex2D(_NoiseVertexMap, i.uv.zw - _Time.yy * i.args.yz / 10);
				float4 offset2 = tex2D(_NoiseVertexMap, i.uv.zw + _Time.yy * i.args.yz / 10);
				fixed2 maskuv = fixed2(i.uv1.z - offset1.r * _NoiseVertexXLength, i.uv1.w - offset2.r * _NoiseVertexYLength);
				float4 mask = tex2D(_MaskMap, maskuv);
				float4 noiseDissolve = tex2D(_NoiseDissolveMap, i.uv1.xy);

				fixed _Blend = i.args.x * 4 - 1;
                fixed blendValue = smoothstep(_Blend - _DissolveRange, _Blend + _DissolveRange + _DissolveOffset, mask.r + noiseDissolve.r * _DissolveRange);
				col.rgb = lerp(col.rgb, _DissolveColor.rgb * _Color.rgb * i.col.rgb, 1 - blendValue);
				col.a = lerp(col.a, min(_DissolveColor.a, col.a), 1 - blendValue);
				clip((mask.r + noiseDissolve.r * _DissolveRange)  -  (_Blend  -  _DissolveRange)) ;
				#if UNITY_UI_CLIP_RECT
				col.a *= UnityGet2DClipping(i.objPos.xy, _ClipRect);
				#endif
				return col;
			}
			ENDCG
		}
	}
	CustomEditor "Blz_EffectNoiseDissolve_GUI"
}
