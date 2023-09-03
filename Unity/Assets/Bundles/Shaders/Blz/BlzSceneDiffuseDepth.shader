Shader "Blz/Scene/DiffuseDepth"
{
    Properties
    {
		_Color ("Color", Color) = (1, 1, 1, 1)
        _MainTex ("Main Tex", 2D) = "white" {}
        //_Depth("Depth", float) = 1
    }
    SubShader
    {
        Tags{  "IgnoreProjector" = "True" "RenderType" = "Transparent" "Queue"="Transparent"}

        Pass
        {
            Tags { "LightMode"="ForwardBase" }

            ZWrite On
            Blend SrcAlpha  OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
			#include "AutoLight.cginc"
			#include "Lighting.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 projPos : TEXCOORD1;
                float3 normal : TEXCOORD2;
                float3 lightDir : TEXCOORD3;
                UNITY_FOG_COORDS(4)
                //SHADOW_COORDS(5)
                #ifdef LIGHTMAP_ON
				float2 texcoordLM : TEXCOORD6;
                #endif
                float4 color : COLOR;
                float4 pos : SV_POSITION;
            };

            sampler2D _MainTex;float4 _MainTex_ST;
            uniform sampler2D _CameraDepthTexture;
            float4 _Color;float _Depth;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.pos);
                o.normal = v.normal;
                o.lightDir = ObjSpaceLightDir(v.vertex);
                float3 worldNormal = UnityObjectToWorldNormal(v.normal);
                //float3 shlight = ShadeSH9(float4(worldNormal, 1.0));
                //o.vertexLight = shlight;
                #ifdef LIGHTMAP_ON
				o.texcoordLM = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
				#endif
    //            o.projPos = ComputeScreenPos(o.pos);
    //            COMPUTE_EYEDEPTH(o.projPos.z);
				//TRANSFER_SHADOW(o);
                o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                i.lightDir = normalize(i.lightDir);
                i.normal = normalize(i.normal);

                float diffuse = max(0, dot(i.normal, i.lightDir));
                float shadow = SHADOW_ATTENUATION(i);

                // sample the texture
                fixed4 mainColor = tex2D(_MainTex, i.uv);
                fixed4 col = mainColor * _Color * i.color;
                col.rgb = col.rgb * _LightColor0 * diffuse;

				#ifdef LIGHTMAP_ON
				float3 lm = DecodeLightmap(UNITY_SAMPLE_TEX2D(unity_Lightmap, i.texcoordLM.xy));
				col.rgb = col.rgb + mainColor.rgb * lm;
				#endif

                UNITY_APPLY_FOG(i.fogCoord, col);
    //            float sceneZ = max(0, LinearEyeDepth(UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
				//float partZ = max(0, i.projPos.z - _ProjectionParams.g);
    //            float depth = saturate((sceneZ - partZ) / _Depth);
    //            col.a = depth;\

                return col;
            }
            ENDCG
        }
    }
}
