Shader "Blz/Charactor/Player"
{
   Properties
    {
        _MainTex ("Main Tex", 2D) = "white" {}
		_Color ("Color", Color) = (1, 1, 1, 1)

		//**** Outline content.
		_OutlineColor("OutlineColor", Color) = (1, 1, 1, 1)
		_OutlineLength("OutlineLength", float) = 0.5
		_OutlineLightness ("OutlineLightness", Range(0, 1)) = 1
		_OutlineCameraVector("OutlineCameraVector", float) = 0
		_OutlineType("OutlineType", float) = 0
		//**** Main Content.
		_BumpMap ("Normal Map", 2D) = "bump"{}
		_BumpScale ("Bump Scale", float) = 1

		_ShadowThreshold("ShadowThreshold", Range(0, 1)) = 0.5
		_ShadowColor("ShadowColor", Color) = (0.5, 0.5, 0.5, 1)

		//_RampMap ("Ramp Map", 2D) = "white" {}
		//_TintLayer1 ("Tint Layer1", Color) = (1, 1, 1, 1)
		//_TintLayer2 ("Tint Layer2", Color) = (1, 1, 1, 1)
		//_TintLayer3 ("Tint Layer3", Color) = (1, 1, 1, 1)

		_SSSLUT ("SSSLUT", 2D) = "white" {}		
		_CurveFactor ("CurveFactor", Range(0, 1)) = 1		
		_FrontSurfaceDistortion("FrontSurfaceDistortion",float) = 1
		_BackSurfaceDistortion("BackSurfaceDistortion",float) = 1
		[HDR]_InteriorColor("InteriorColor",Color) = (1,1,1,1)
		[HDR]_InteriorColor2("InteriorColor2", Color) = (1, 1, 1, 1)
		_InteriorColorPower("InteriorColorPower",float) = 1
		_FrontSSSIntensity("FrontSSSIntensity",float) = 1
		_BackSSSIntensity("BackSSSIntensity",float)=1

		//**** Light Map: R channel draw metallic area, G channel draw strand area.
		_LightMap ("Light Map", 2D) = "black"{}

		[MaterialToggle(LightMap_R_ON)] _LightMapRChannel("Enable LightMap R Channel", Float) = 0 
		_Shininess ("Shininess", float) = 1
		_SpecularColor ("Specular Color", Color) = (1, 1, 1, 1)
		_SpecularMulti ("Spec Multi", float) = 1
		_DirectionLightOffset ("Direction Light Offset", vector) = (0, 0, 0)
		//_FresnelVector ("Fresnel Vector", Range(0, 1)) = 0.55

		[MaterialToggle(LightMap_G_ON)] _LightMapGChannel("Enable LightMap G Channel", Float) = 0 
		//_StrandNoiseMap ("Strand Noise Map", 2D) = "white"{}
		//_StrandFirstOff ("Strand Firs Off", float) = 0
		//_StrandFirstSmooth ("Strand First Smooth", float) = 0
		//_StrandSpecularColor ("Strand Specular Color", Color) = (1, 1, 1, 1)
		//_StrandSpecularStrength ("Strand Specular Strength", Range(0, 1)) = 0.5
		
		[MaterialToggle(LightMap_B_ON)] _LightMapBChannel("Enable LightMap B Channel", Float) = 0 
		_SpecularColor2 ("Specular Color2", Color) = (0, 0, 0, 1)
		_SpecularMulti2 ("Spec Multi2", float) = 1
		
		//**** Emiss content
		//**** Emiss map：R channel draw illumine area，G channel draw noise in the illnumine area，B channel draw noise to disturbance all the illumine area.
		_EmissMap ("Emiss Map", 2D) = "white" {}
		[HDR]_EmissColor ("Emiss Color", Color) = (1, 1, 1, 1)
		_EmissMaskSpeedX ("Emiss Mask Speed X", float) = 0
		_EmissMaskSpeedY ("Emiss Mask Speed Y", float) = 0	
		_EmissNoiseFactor ("Emiss Noise Factor", float) = 0
		_EmissNoiseStrength ("Emiss Noise Strength", float) = 0
		_EmissBreathSpeed ("Emiss Breath Speed", float) = 0	

		_DissolveMap("Dissolve Map", 2D) = "white" {}
		_DissolveLineWidth("Dissolve LineWidth", Range(0.0, 0.2)) = 0.1 
		_DissolveFirstColor("Dissolve First Color", Color) = (1, 0, 0, 1)
		_DissolveSecondColor("Dissolve Second Color", Color) = (1, 0, 0, 1)
		_DissolveCutoff("Dissolve Cutoff", Range(0, 1.5)) = 0

		_FlashColor("Flash Color", Color) = (1, 1, 1, 1)
		_FlashIntensity("FlashIntensity", Range(0, 1)) = 0

		_IgnoreFOW("Ignore FOW", float) = 0
    }
    SubShader
    {

		Pass
		{
			Name "FORWARD"
			Tags { "LightMode" = "ForwardBase" "RenderType"="Transparent" "Queue"="Transparent - 10" }
			Cull Back
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma target 3.0 
			#pragma multi_compile_instancing
			#pragma multi_compile_fwdbase
			#pragma multi_compile _ UNITY_UI_CLIP_RECT
			#pragma multi_compile_fog

			#pragma multi_compile _ EmissMap_ON
			#pragma multi_compile _ LightMap_R_ON
			#pragma multi_compile _ LightMap_G_ON
            #pragma multi_compile _ LightMap_B_ON
            #pragma multi_compile _ DissolveMap_ON

			//#pragma only_renderers d3d9 d3d11 glcore gles gles3 metal xboxone ps4 switch
			#include "UnityCG.cginc"
			#include "AutoLight.cginc"
			#include "Lighting.cginc"
			
			struct a2v
            {
                float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 tangent :TANGENT;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
				float3 vertexLight : COLOR;
				//**** Set TangentToWorld matrix in xyz. Set worldPos in w.
				float4 TtoW0 : TEXCOORD1; 
				float4 TtoW1 : TEXCOORD2;
				float4 TtoW2 : TEXCOORD3;
				UNITY_FOG_COORDS(4)
				SHADOW_COORDS(5)
				float4 emissInfo : TEXCOORD6;
            };

			//**** Is in UI? Greater 0 is in UI.
			fixed _IsUI;
			sampler2D _MainTex; fixed4 _MainTex_ST; fixed4 _Color;
			sampler2D _BumpMap; fixed4 _BumpMap_ST;
			fixed _BumpScale;
			
			fixed4 _ShadowColor;fixed _ShadowThreshold;

			//**** Diffuse mult channel color.
			//sampler2D _RampMap; fixed4 _RampMap_ST;
			//fixed4 _TintLayer1; fixed4 _TintLayer2; fixed4 _TintLayer3;

			//**** Emission content.
			sampler2D _EmissMap; fixed4 _EmissMap_ST;
			fixed4 _EmissColor;fixed _EmissMaskSpeedX; fixed _EmissMaskSpeedY;fixed _EmissIntensity;
			fixed _EmissNoiseFactor; fixed _EmissNoiseStrength;	float _EmissBreathSpeed;

			//**** Specular content
			sampler2D _LightMap; fixed4 _LightMap_ST;
			fixed4 _SpecularColor;fixed _SpecularMulti;fixed _Shininess;fixed3 _DirectionLightOffset;
			//fixed _FresnelVector;

			sampler2D _StrandNoiseMap; fixed4 _StrandNoiseMap_ST;
			fixed4 _StrandSpecularColor;
			fixed _StrandFirstOff; fixed _StrandFirstSmooth; fixed _StrandSpecularStrength;

			fixed4 _FlashColor;fixed _FlashIntensity;

			half _IgnoreFOW;

			fixed _SpecularMulti2;fixed4 _SpecularColor2;
			
			uniform sampler2D _FOWMaskTex;
			uniform vector _FOWInfo;
			uniform half _FOWVisible;

			sampler2D _DissolveMap;
			fixed4 _DissolveFirstColor;
			fixed4 _DissolveSecondColor;
			float _DissolveLineWidth;
			half _DissolveCutoff;

			sampler2D _SSSLUT; float4 _SSSLUT_ST; float _CurveFactor;
			float4 _InteriorColor, _InteriorColor2;
			float _InteriorColorPower;
			float _BackSurfaceDistortion;
			float _BackSSSIntensity;

			float SubSurfaceScattering(float3 viewDir,float3 lightDir,float3 normalDir,float backSubSurfaceDistortion,float backSSSIntensity) {
				float3 frontLitDir = normalDir  - lightDir;
				float3 backLitDir = normalDir  + lightDir;
				float backSSS = saturate(dot(viewDir, -backLitDir));
				float result = saturate(backSSS * backSSSIntensity);
				return result;
			}		

			//**** Screen blend
			inline fixed3 ComputeScreenBlend(fixed3 sour, fixed3 dest)
			{				
				return 1 - ((1 - sour) * (1 - dest));
			}

			//**** Multiply a color to another color alpha channel. 
			inline fixed3 ComputeColorAlpha(fixed3 col, fixed alpha)
			{
				return col * alpha + fixed3(1, 1, 1) * (1 - alpha);
			}

			//**** Compute diffuse color by ramp color(Contain alpha channel).
			inline fixed3 ComputeTintColor(fixed4 sour, fixed dest)
			{
				fixed3 finalColor = ComputeScreenBlend(sour.rgb, fixed3(dest, dest, dest));
				return ComputeColorAlpha(finalColor.rgb, sour.a);
			}

			inline fixed ComputeFresnel(float3 viewDir, float3 halfVector, float fresnelValue)
			{
				fixed fresnel = pow(1.0 - dot(viewDir, halfVector), 5.0);
				fresnel += fresnelValue * (1.0 - fresnel);
				return fresnel;
			}
			
			//**** Anisotropy color.(Acting on the strand)
			inline fixed StrandSpecular(float3 T, float3 V, float3 L, float exponent, float strength)
			{
				//float check = dot(V, L);
				//if (check < 0) return 0;
				float3 H = normalize(L+V);
				float dotTH = dot(T, H);
				float sinTH = sqrt(1 - dotTH * dotTH);
				float dirAtten = smoothstep(-1.0, 0.0, dotTH);
				return dirAtten * pow(sinTH, exponent) * strength;
			}

			inline float3x3 RotaionMatrix(float alpha, float beta, float gamma)
			{
				return float3x3(cos(alpha)*cos(gamma) - cos(beta)*sin(gamma), -cos(beta)*cos(gamma)*sin(alpha) - cos(alpha)*sin(gamma), sin(alpha)*sin(beta),
								cos(gamma)*sin(alpha) + cos(alpha)*cos(beta)*sin(gamma), cos(alpha)*cos(beta)*cos(gamma) - sin(alpha)*sin(gamma), -cos(alpha)*sin(beta),
								sin(beta)*sin(gamma), cos(gamma)*sin(beta), cos(beta));				
			}
			
			v2f vert(a2v v)
			{
				v2f o;
				UNITY_INITIALIZE_OUTPUT(v2f,o);

				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				o.emissInfo.xy = TRANSFORM_TEX(v.uv, _EmissMap).xy;
				o.emissInfo.zw = o.emissInfo.xy + _Time.z * float2(_EmissMaskSpeedX / 100, _EmissMaskSpeedY / 100);

				float3 worldPos = mul(unity_ObjectToWorld,v.vertex);
				fixed3 worldNormal = UnityObjectToWorldNormal(v.normal);
				fixed3 worldTangent = UnityObjectToWorldDir(v.tangent);
				fixed3 worldBinormal = cross(worldNormal,worldTangent) * v.tangent.w;
				o.TtoW0 = float4(worldTangent.x,worldBinormal.x,worldNormal.x,worldPos.x);
				o.TtoW1 = float4(worldTangent.y,worldBinormal.y,worldNormal.y,worldPos.y);
				o.TtoW2 = float4(worldTangent.z,worldBinormal.z,worldNormal.z,worldPos.z);
				
                float3 shlight = ShadeSH9(float4(worldNormal, 1.0));
                o.vertexLight = shlight;				

				TRANSFER_SHADOW(o);
				if (_IsUI <= 0)
					UNITY_TRANSFER_FOG(o,o.pos);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{	
				
				fixed3 finalColor = fixed3(1, 1, 1);
				fixed3 c = tex2D(_MainTex, i.uv).rgb;
				fixed3 albedo = tex2D(_MainTex, i.uv).rgb * _LightColor0.a * _Color.rgb;
				fixed3 lightMapColor = tex2D(_LightMap, i.uv).rgb;
				float3 worldPos = float3(i.TtoW0.w, i.TtoW1.w, i.TtoW2.w);
				fixed3 v_normal = normalize(fixed3(i.TtoW0.z, i.TtoW1.z, i.TtoW2.z));
				fixed3 tangentDir = normalize(fixed3(i.TtoW0.x, i.TtoW1.x, i.TtoW2.x));
				fixed3 b_tangentDir = normalize(fixed3(i.TtoW0.y, i.TtoW1.y, i.TtoW2.y));
				fixed3 lightDir = normalize(UnityWorldSpaceLightDir(worldPos));
				float3x3 lrm = RotaionMatrix(radians(_DirectionLightOffset.x), radians(_DirectionLightOffset.y), radians(_DirectionLightOffset.z));
				lightDir = mul(lrm, lightDir);
				fixed3 viewDir = normalize(UnityWorldSpaceViewDir(worldPos));
				fixed3 halfDir = normalize(lightDir + viewDir);
				
				float2 maskUV = float2(worldPos.x / _FOWInfo.x, worldPos.z / _FOWInfo.y);
				float4 mask = tex2D(_FOWMaskTex, maskUV);
				half fowVisible = _FOWVisible * (1 - UNITY_MATRIX_P[3][3]) * (1 - _IsUI);
				float visible = lerp(mask.g, mask.r, _FOWInfo.z) * fowVisible + (1 - fowVisible);

				fixed3 b_tangentNormal = UnpackScaleNormal(tex2D(_BumpMap, TRANSFORM_TEX(i.uv, _BumpMap)), _BumpScale);
				fixed3 b_worldNormal = normalize(fixed3(dot(i.TtoW0.xyz, b_tangentNormal), dot(i.TtoW1.xyz, b_tangentNormal), dot(i.TtoW2.xyz, b_tangentNormal)));

				fixed NdotL = dot(b_worldNormal, -lightDir);
				float diff = NdotL * 0.5 + 0.5;
				finalColor = diff > _ShadowThreshold ? albedo * _ShadowColor * _LightColor0.a : albedo;

				//fixed3 ramp = tex2D(_RampMap, float2(diff, diff)).rgb;
			 //   fixed3 finalTintColor1 = ComputeTintColor(_TintLayer1, ramp.r);
				//fixed3 finalTintColor2 = ComputeTintColor(_TintLayer2, ramp.g);
				//fixed3 finalTintColor3 = ComputeTintColor(_TintLayer3, ramp.b);
				//finalColor = albedo *finalTintColor1 * finalTintColor2 * finalTintColor3;
				
				#ifdef LightMap_R_ON			
				float shinPow = pow(max(dot(v_normal, halfDir), 0), _Shininess);
				//float oneMinusSpec = 1 - lightMapColor.z;
				//oneMinusSpec = oneMinusSpec - shinPow;
				//int specMaslk = step(0,oneMinusSpec);
				fixed3 specColorR = _SpecularMulti * _SpecularColor.xyz * shinPow * lightMapColor.r;
				//specColor = lightMapColor.x * specColor;
				//if(specMaslk != 0)
				//	specColor = 0;
				//else
				//	specColor = specColor;
				//fixed fresnelVector = ComputeFresnel(viewDir, halfDir, _FresnelVector);
				
				finalColor += specColorR * step(1, _LightColor0.a) ;
				#endif

				#ifdef LightMap_G_ON
					fixed cuv = saturate(_CurveFactor * 0.01 * (length(fwidth(v_normal)) / length(fwidth(worldPos))));
					fixed nl = dot(v_normal, lightDir);		
					fixed b_nl = dot(v_normal, -lightDir);		
					fixed3 diffuse = tex2D(_SSSLUT, float2(nl * 0.5 + 0.5, _CurveFactor));
					finalColor = lerp(_InteriorColor * albedo, albedo, diffuse) * lightMapColor.g + finalColor * (1 - lightMapColor.g);

					float SSS = SubSurfaceScattering(viewDir, lightDir, v_normal, _BackSurfaceDistortion, _BackSSSIntensity);
					float3 SSSCol = lerp(_InteriorColor, _InteriorColor2.rgb, saturate(pow(SSS, _InteriorColorPower))).rgb * SSS;				
					//float thickness=tex2D(_ThicknessTex,i.uv).r;
					//SSSCol*=(1-thickness);
					//SSSCol *= 1;
					//float FdotL=saturate(dot(lightDir,i.frontDir));
					//float CFdotF=abs(dot(float3(0,0,-1),i.clipFrontDir));
					//SSSCol*=(1-FdotL)*pow(CFdotF,5);
					finalColor += SSSCol * lightMapColor.g * finalColor;

					//float rim = 1.0 - max(0, dot(normal, viewDir));
					//float rimValue = lerp(rim, 0, SSS);
					//float3 rimCol = lerp(_InteriorColor, _LightColor0.rgb, rimValue)*pow(rimValue, _RimPower)*_RimIntensity;

				#endif

				#ifdef LightMap_B_ON		
				fixed3 specColorB = _SpecularMulti2 * _SpecularColor2.xyz * lightMapColor.b ;	
				finalColor += specColorB * step(1, _LightColor0.a) * (1 - step(_ShadowThreshold, diff));
				#endif

				#if EmissMap_ON
				fixed4 offset1 = tex2D(_EmissMap, i.uv - _Time.yy * _EmissNoiseFactor / 10);
				fixed4 offset2 = tex2D(_EmissMap, i.uv + _Time.yy * _EmissNoiseFactor / 10);
				fixed2 uvOffset = fixed2(i.emissInfo.x - offset1.z * _EmissNoiseStrength / 100, i.emissInfo.y - offset2.z * _EmissNoiseStrength / 100);
				fixed emissR = tex2D(_EmissMap, uvOffset).r;
				fixed emissG = tex2D(_EmissMap, i.emissInfo.zw).g;
				fixed breath = saturate(sin(_Time.y * _EmissBreathSpeed * 0.01) * 0.5 + 0.5);
				if (_EmissBreathSpeed == 0) breath = 1;
				finalColor += _EmissColor * emissR * emissG * breath;
				#endif

				half inUI = step(0, _IsUI - 0.5); 				

				finalColor.rgb += c * i.vertexLight * (1 - inUI) ;				

				float rim = 1 - dot(v_normal, viewDir);
				fixed3 clr = _FlashColor * rim;
				finalColor.rgb += _FlashIntensity * _FlashColor * clr;

				#if DissolveMap_ON
				fixed dissolve = tex2D(_DissolveMap, i.uv).r;
				fixed clipValue = dissolve - _DissolveCutoff;
				clip(clipValue);

				fixed t = 1 - smoothstep(0.0, _DissolveLineWidth, clipValue);
				fixed3 burnColor = lerp(_DissolveFirstColor, _DissolveSecondColor, t);
				burnColor = pow(burnColor, 5);
				finalColor.rgb = lerp(finalColor.rgb, burnColor, t * step(0.0001, _DissolveCutoff));
				#endif

				return fixed4(lerp(half3(0, 0, 0), finalColor, clamp(0, 1, visible + 0.7)) * _IgnoreFOW + lerp(half3(0, 0, 0), finalColor, visible)* (1 - _IgnoreFOW), 1);
			}
            ENDCG
		}

		Pass
		{
			Name "ShadowCaster"
			Tags { "LightMode" = "ShadowCaster" }
			
			ZWrite On ZTest LEqual
			ColorMask 0
			CGPROGRAM
			#pragma target 3.0

			#pragma multi_compile_shadowcaster
            #pragma multi_compile _ DissolveMap_ON

			#pragma vertex vertShadowCaster
			#pragma fragment fragShadowCaster

			#include "UnityCG.cginc"

			sampler2D _DissolveMap;
			fixed4 _DissolveFirstColor;
			fixed4 _DissolveSecondColor;
			float _DissolveLineWidth;
			half _DissolveCutoff;

			uniform sampler2D _FOWMaskTex;
			uniform vector _FOWInfo;
			uniform half _FOWVisible;

            struct appdata
            {
                float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 worldPos : TEXCOORD1;
            };

			v2f vertShadowCaster (appdata v)
			{
				v2f o;
				o.uv = v.uv;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.worldPos = mul(unity_ObjectToWorld,v.vertex); 
				return o;
			}

			half4 fragShadowCaster(v2f i) : SV_Target{
				#if DissolveMap_ON
				fixed dissolve = tex2D(_DissolveMap, i.uv).r;
				fixed clipValue = dissolve - _DissolveCutoff;
				clip(clipValue);
				#endif
				return 0;
			}

			ENDCG
		}
    }
	CustomEditor "Blz_CharactorPlayer_GUI"
}
