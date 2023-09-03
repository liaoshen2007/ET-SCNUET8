Shader "Blz/Effect/Dissolve"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [HDR]_Color("Color", Color) = (1, 1, 1, 1)
        _NoiseMap ("NoiseMap", 2D) = "white" {}
        [HDR]_DissolveFirstColor ("DissolveFirstColor", Color) = (1, 1, 1, 1)
		[HDR]_DissolveSecondColor ("DissolveSecondColor", Color) = (1, 1, 1, 1)

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
            #pragma multi_compile_fog
			
			#pragma multi_compile _ MaskMap_On
			//**** Acting on UGUI clip.
			#pragma multi_compile __ UNITY_UI_CLIP_RECT
			//**** To use ParticleSystem custom vertex streams.
			#pragma multi_compile_particles

            #include "UnityCG.cginc"
			#include "UnityUI.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
				float4 col : COLOR;
				//**** xy is uv. z is dissolve width. w is cutoff.
                float4 tex0 : TEXCOORD0;

				UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 uvs : TEXCOORD0;
                float4 vertex : SV_POSITION;
				
				#if UNITY_UI_CLIP_RECT
				float2 objPos : TEXCOORD2;
				#endif

				float4 col : COLOR;
                UNITY_FOG_COORDS(3)			

				float4 info : TEXCOORD4;
				UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex; float4 _MainTex_ST; 
			sampler2D _NoiseMap; float4 _NoiseMap_ST;
			float4 _Color;float4 _DissolveFirstColor;float4 _DissolveSecondColor;
			float4 _ClipRect;
			float _DissolveCutoff;float _DissolveWidth;

            v2f vert (appdata v)
            {
                v2f o;

				UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				#if UNITY_UI_CLIP_RECT
				o.objPos = v.vertex;
				#endif

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uvs.xy = TRANSFORM_TEX(v.tex0.xy, _MainTex);
				o.uvs.zw = TRANSFORM_TEX(v.tex0.xy, _NoiseMap);
				o.info.xy = v.tex0.zw;
				o.col = v.col;
				UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uvs.xy) * i.col * _Color;				
				fixed dissolve = tex2D(_NoiseMap, i.uvs.zw).r;
				fixed clipValue = dissolve - i.info.y;
				clip(clipValue);
				fixed t = 1 - smoothstep(0.0, i.info.x, clipValue);
				fixed3 burnColor = lerp(_DissolveFirstColor, _DissolveSecondColor, t);
				burnColor = pow(burnColor, 5);
				col.rgb = lerp(col.rgb, burnColor, t * step(0.0001, i.info.y));

                UNITY_APPLY_FOG(i.fogCoord, col);

				#if UNITY_UI_CLIP_RECT
				col.a *= UnityGet2DClipping(i.objPos.xy, _ClipRect);
				#endif
                
				return col;
            }
            ENDCG
        }
	}
	CustomEditor "Blz_EffectDissolve_GUI"
}
