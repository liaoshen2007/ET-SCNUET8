Shader "Blz/Scene/Dissolve"
{
     Properties
    {
		_Color("Color", Color) = (1,1,1,1)
        _MainTex ("Main Tex", 2D) = "white" {}

        _NoiseMap ("NoiseMap", 2D) = "white" {}
        [HDR]_DissolveFirstColor ("DissolveFirstColor", Color) = (1, 1, 1, 1)
		[HDR]_DissolveSecondColor ("DissolveSecondColor", Color) = (1, 1, 1, 1)
        _Alpha("Alpha", float) = 1        

		_EmissMap ("Emiss Map", 2D) = "white" {}
		[HDR]_EmissColor ("Emiss Color", Color) = (1, 1, 1, 1)
		_EmissMaskSpeedX ("Emiss Mask Speed X", float) = 0
		_EmissMaskSpeedY ("Emiss Mask Speed Y", float) = 0
		_EmissNoiseFactor ("Emiss Noise Factor", float) = 0
		_EmissNoiseStrength ("Emiss Noise Strength", float) = 0
		_EmissBreathSpeed ("Emiss Breath Speed", float) = 0
        _EmissBreathMap("Emiss Breath Map", 2D) = "white" {}

    }
    
    SubShader
    {
        LOD 100 

        Pass
        {
            Tags { "LightMode"="ForwardBase" "Queue"="AlphaTest" "RenderType"="TransparentCutout" "IgnoreProjector"="True"}

			Cull Back

            CGPROGRAM
            #pragma fragmentoption ARB_precision_hint_fastest
            
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
			#pragma multi_compile _ BumpMap_ON
			#pragma multi_compile _ EmissMap_ON
            #pragma multi_compile _ EmissBreathMap_ON

            #include "UnityCG.cginc"
			#include "AutoLight.cginc"
			#include "Lighting.cginc"

			half4 _Color;
            sampler2D _MainTex; half4 _MainTex_ST;
			sampler2D _EmissMap; half4 _EmissMap_ST;
            sampler2D _EmissBreathMap;half4 _EmissBreathMap_ST;
			half4 _EmissColor;half _EmissMaskSpeedX; half _EmissMaskSpeedY;
			half _EmissNoiseFactor; half _EmissNoiseStrength;float _EmissBreathSpeed;

			sampler2D _NoiseMap; float4 _NoiseMap_ST;
			float4 _DissolveFirstColor;float4 _DissolveSecondColor;
            float _Alpha;float _DissolveWidth;

            struct vertexIN_base
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float4 tangent: TANGENT;
            };
            
            struct v2f_base
            {
                float4 pos : SV_POSITION;
                float3 vertexLight : COLOR;
                float4  uv : TEXCOORD0;
                float3 normal : TEXCOORD1;
                float3 lightDir : TEXCOORD2;
                UNITY_FOG_COORDS(3)
				half4 emissInfo : TEXCOORD4;
				SHADOW_COORDS(5)
				float4 TtoW0 : TEXCOORD6; 
				float4 TtoW1 : TEXCOORD7;
				float4 TtoW2 : TEXCOORD8;
                float4 emissBreath : TEXCOORD9;
				#ifdef LIGHTMAP_ON
				half2 texcoordLM : TEXCOORD10;
                #endif
            };

            v2f_base vert(vertexIN_base v)
            {
                v2f_base o;
                o.pos = UnityObjectToClipPos(v.vertex);
				float3 worldPos = mul(unity_ObjectToWorld, v.vertex);
                float3 worldNormal = UnityObjectToWorldNormal(v.normal);
                float3 worldTangent = UnityObjectToWorldDir(v.tangent);
				float3 worldBinormal = cross(worldNormal,worldTangent) * v.tangent.w;
                o.TtoW0 = float4(worldTangent.x,worldBinormal.x,worldNormal.x,worldPos.x);
				o.TtoW1 = float4(worldTangent.y,worldBinormal.y,worldNormal.y,worldPos.y);
				o.TtoW2 = float4(worldTangent.z,worldBinormal.z,worldNormal.z,worldPos.z);

                o.uv.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.uv.zw = TRANSFORM_TEX(v.texcoord, _NoiseMap);
				o.emissInfo.xy = TRANSFORM_TEX(v.texcoord, _EmissMap).xy;
				o.emissInfo.zw = o.emissInfo.xy + _Time.z * float2(_EmissMaskSpeedX / 100, _EmissMaskSpeedY / 100);
                o.emissBreath.xy = TRANSFORM_TEX(v.texcoord, _EmissBreathMap);
                o.normal = v.normal;
                o.lightDir = ObjSpaceLightDir(v.vertex);

                float3 shlight = ShadeSH9(float4(worldNormal, 1.0));
                o.vertexLight = shlight;
                #ifdef VERTEXLIGHT_ON
                    o.vertexLight += Shade4PointLights (
                        unity_4LightPosX0, unity_4LightPosY0, unity_4LightPosZ0,
                        unity_LightColor[0].rgb, unity_LightColor[1].rgb, unity_LightColor[2].rgb, unity_LightColor[3].rgb,
                        unity_4LightAtten0, worldPos, worldNormal
                        );
                #endif
				#ifdef LIGHTMAP_ON
				o.texcoordLM = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
				#endif
				TRANSFER_SHADOW(o);
                UNITY_TRANSFER_FOG(o,o.pos);
                return o; 
            }
            
            float4 frag(v2f_base i) : COLOR
            {
                i.lightDir = normalize(i.lightDir);
                i.normal = normalize(i.normal);

                float diffuse = max(0, dot(i.normal, i.lightDir));

                float4 mainColor = tex2D(_MainTex, i.uv.xy);

				fixed dissolve = tex2D(_NoiseMap, i.uv.zw).r;
				fixed clipValue = _Alpha - dissolve;
				clip(clipValue);

				float4 c = mainColor * _Color;

				float shadow = SHADOW_ATTENUATION(i);

				float4 clr = mainColor * _LightColor0 * diffuse * shadow;
			
				#if EmissMap_ON
				float4 offset1 = tex2D(_EmissMap, i.uv - _Time.yy * _EmissNoiseFactor / 10);
				float4 offset2 = tex2D(_EmissMap, i.uv + _Time.yy * _EmissNoiseFactor / 10);
				float2 uvOffset = float2(i.emissInfo.x - offset1.z * _EmissNoiseStrength / 100, i.emissInfo.y - offset2.z * _EmissNoiseStrength / 100);
				float emissR = tex2D(_EmissMap, uvOffset).r;
				float emissG = tex2D(_EmissMap, i.emissInfo.zw).g;
                float breath = 0;

                #if EmissBreathMap_ON
				fixed4 ebt = tex2D(_EmissBreathMap, _Time.xy * _EmissBreathSpeed);
                breath = ebt.x;
                #else
                breath = saturate(sin(_Time.y * _EmissBreathSpeed * 0.01) * 0.5 + 0.5);
				if (_EmissBreathSpeed == 0) breath = 1;
				#endif

				clr.rgb += _EmissColor * emissR * emissG * breath;                    
                #endif

				#ifdef LIGHTMAP_ON
				float3 lm = DecodeLightmap(UNITY_SAMPLE_TEX2D(unity_Lightmap, i.texcoordLM.xy));
				clr.rgb = clr.rgb + mainColor.rgb * lm;
				#else
                clr.rgb += mainColor.rgb * i.vertexLight;
				#endif

				fixed t = 1 - smoothstep(0.0, 0.1, clipValue);
				fixed3 burnColor = lerp(_DissolveFirstColor, _DissolveSecondColor, t);
				burnColor = pow(burnColor, 5);
				clr.rgb = lerp(clr.rgb, burnColor, t * step(0.0001, clipValue));

                UNITY_APPLY_FOG(i.fogCoord,clr);

                return clr;
            }
            ENDCG
        }
    }
	CustomEditor "Blz_SceneDissolve_GUI"
}
