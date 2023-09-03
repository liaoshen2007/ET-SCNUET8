Shader "Blz/Effect/CustomRimColor"
{
	Properties
	{
		_Color ("Color", Color) = (1, 1, 1, 1)
		[HDR]_OutLineColor("OutLineColor", Color) = (0, 0, 0, 0)
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
		Tags { "RenderType"="Opaque" "Queue"="Transparent+10" }

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
			ZWrite On

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			//#pragma target 3.0
			#pragma fragmentoption ARB_precision_hint_fastest			
			#pragma multi_compile_particles


			#include "UnityCG.cginc"

			inline float Pow_Schlick(float buttom, float num)
			{
				return buttom / (num - ((num - 1) * buttom));
			}

			struct appdata
			{
				float4 vertex : POSITION;
				float4 uv : TEXCOORD0;
				float3 normal : NORMAL;
				float4 color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float2 uv2 : TEXCOORD1;
				float4 rimcol : COLOR;
				float4 vertex : SV_POSITION;
				float4 args : TEXCOORD2;

				#if UI_On
				float2 worldPos : TEXCOORD3;
				#endif

				UNITY_VERTEX_OUTPUT_STEREO
			};

			float4 _Color, _OutLineColor;

			v2f vert (appdata v)
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				o.vertex = UnityObjectToClipPos(v.vertex);

				float3 viewdir = ObjSpaceViewDir(v.vertex);
				float3 normdir = v.normal;
				float rim = 1 - saturate(dot(normalize(viewdir), normalize(normdir)));
				rim = Pow_Schlick(rim, v.uv.z);

				rim = smoothstep(0, v.uv.w, rim);
				o.rimcol = rim * _OutLineColor;
				o.args.w = v.color.a;

				return o;
			}


			float4 frag (v2f i) : SV_Target
			{
				float4 col = _Color;
				col = col + i.rimcol;
				col.a = i.args.w;
				return col;
			}
			ENDCG
		}
	}

	FallBack "Unlit/Texture"
	CustomEditor "Blz_CustomEffectRimColor_GUI"
}
