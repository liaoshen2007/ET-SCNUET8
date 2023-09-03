Shader "Blz/Scene/Water"
{
	Properties
	{
		_Color("Main Color", Color) = (1,1,1,1)
		_RefractTex ("Refract Texture", 2D) = "white" {}
		_BumpTex ("Bump Texture", 2D) = "white"{}
		_BumpStrength ("Bump strength", Range(0.0, 10.0)) = 1.0
		_BumpDirection ("Bump direction(2 wave)", Vector)=(1,1,1,-1)
		_BumpTiling ("Bump tiling", Vector)=(0.0625,0.0625,0.0625,0.0625)
		_FresnelTex("Fresnel Texture", 2D) = "white" {}
		_Skybox("skybox", Cube)="white"{}
		_Specular("Specular Color", Color)=(1,1,1,0.5)
		_Params("shiness,Refract Perturb,Reflect Perturb", Vector)=(128, 0.025, 0.05, 0)
		_SunDir("sun direction", Vector)=(0,0,0,0)
		[HDR]_FresnelColor("Fresnel Color", Color) = (1, 1, 1, 1) 
		_Depth("Depth", Range(0, 30)) = 30
		_Depthdarkness("Depth darkness", Range(0, 1)) = 0.5

			
		_RimColor("Rim Color", Color) = (1,1,1,1)
		_RimSize("Rim Size", Range(0, 4)) = 2
		_Rimfalloff("Rim falloff", Range(0, 5)) = 0.25

		_Rim2Color("Rim2 Color", Color) = (1, 1, 1, 1)
		_Rim2Depth("Rim2 Depth", Range(0, 1)) = 0.5

		_Tiling("Tiling", Range(0.1, 1)) = 0.9
		_Rimtiling("Rim tiling", Float) = 2
		_Wavesspeed("Waves speed", Range(0, 10)) = 0.75
		//
		_WaveSpeed1("Speed", float) = 0.0
		_WaveStrength("Strength", Range(0.0, 1.0)) = 0.0
		_WaveDist("_WaveDist", float) = 10.0
		[NoScaleOffset]_Shadermap("Shadermap", 2D) = "black" {}
		[MaterialToggle] _Worldspacetiling("Worldspace tiling", Float) = 0

		[HDR]_FoamColor ("FoamColor", Color) = (1, 1, 1, 1)
        _FoamScale ("FoamScale", Float ) = 40
		_FoamTexture ("FoamTexture", 2D) = "white" {}
		_FoamSpeed ("FoamSpeed", Float ) = 0.1
		_FoamDepthSpeed ("FoamDepthSpeed", Float) = 1
		_FoamOff ("FoamOff", Float) = 1
		_FoamAlpha ("FomaAlpha", Range(0, 1)) = 1
	}
	SubShader
	{
		Tags{"Queue"="Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }

		Pass
		{
			offset 1,1
			Blend SrcAlpha  OneMinusSrcAlpha
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			#pragma multi_compile_fog
			#pragma fragmentoption ARB_precision_hint_fastest

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv:TEXCOORD0;
				float4 color:COLOR;
				float3 normal:NORMAL;
				//float3 tangent:TANGENT;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				float4 bumpCoords:TEXCOORD1;//浮点数精度不够在手机真机上水面可能会产生轻微抖动，特别注意！
				float3 viewVector:TEXCOORD2;
				float4 posWorld : TEXCOORD3;
				
			//	float4 color : COLOR;
				//
				float4 projPos : TEXCOORD4;
				fixed3 worldNormal : TEXCOORD5;
				UNITY_FOG_COORDS(6)
			};
			float4 _Color;
			sampler2D _RefractTex;
			sampler2D _BumpTex;
			float _BumpStrength;
			float4 _BumpDirection;
			float4 _BumpTiling;
			sampler2D _FresnelTex;
			samplerCUBE _Skybox;
			float4 _Specular;
			float4 _Params;
			float4 _SunDir;
			float4 _FresnelColor;
			//
			uniform fixed _Depth;
			uniform fixed _Depthdarkness;
			uniform sampler2D _CameraDepthTexture;
			uniform fixed _RimSize;
			uniform fixed4 _RimColor;
			uniform fixed _Rimfalloff;
			uniform fixed4 _Rim2Color;
			uniform fixed _Rim2Depth;
			//uniform sampler2D _CameraDepthTexture;
			uniform fixed _Rimtiling;
			uniform fixed _Tiling;
			uniform sampler2D _Shadermap;
			float4 _Shadermap_ST;
			//uniform fixed _Tiling;
			uniform fixed _Worldspacetiling;
			uniform fixed _Wavesspeed;
			///////////////////////////////////////////////
			sampler2D _GTex;

			//sampler2D _WaterTex;
			sampler2D _NoiseTex;
			sampler2D _WaveTex;
			sampler2D _GrabTexture;

			sampler2D _FoamTexture;float4 _FoamTexture_ST;
			float4 _FoamColor;
			float _FoamSpeed;
			float _FoamDepthSpeed;
			float _FoamScale;
			float _FoamOff;
			float _FoamAlpha;

			float4 _Range;
			float _WaterSpeed;
			float _WaveSpeed;
			fixed _WaveDelta;
			float _WaveRange;
			float _NoiseRange;
			float4 _WaterTex_TexelSize;
			float rim;
			////////////////////////////////////////////////////
			float _XSpeed;
			float _YSpeed;
			float _WaveStrength;
			float _WaveSpeed1;
			float _WaveDist;
			float4 movement(float4 pos, float2 uv) {
				//pos.y += (sin((uv.x + uv.y) * _WaveDist + _Time.y * _WaveSpeed1))*(uv.x * 2)*_WaveStrength;
				pos.y += (sin((uv.x + uv.y) * _WaveDist + _Time.y * _WaveSpeed1)) * _WaveStrength;
				return pos;
			}

			float3 PerPixelNormal(sampler2D bumpMap, float4 coords, float bumpStrength) 
			{
				float2 bump = (UnpackNormal(tex2D(bumpMap, coords.xy)) + UnpackNormal(tex2D(bumpMap, coords.zw))) * 0.5;
				//bump += (UnpackNormal(tex2D(bumpMap, coords.xy*2))*0.5 + UnpackNormal(tex2D(bumpMap, coords.zw*2))*0.5) * 0.5;
				//bump += (UnpackNormal(tex2D(bumpMap, coords.xy*8))*0.5 + UnpackNormal(tex2D(bumpMap, coords.zw*8))*0.5) * 0.5;
				float3 worldNormal = float3(0,0,0);
				worldNormal.xz = bump.xy * bumpStrength;
				worldNormal.y = 1;
				return worldNormal;
			}
			
			inline float FastFresnel(float3 I, float3 N, float R0)
			{
				float icosIN = saturate(1-dot(I, N));
				float i2 = icosIN*icosIN;
				float i4 = i2*i2;
				return R0 + (1-R0)*(i4*icosIN);
			}

			uniform float _FixedTime;
			uniform float _FixedCircleTime;

			v2f vert (appdata v)
			{
				v2f o;

				o.vertex = UnityObjectToClipPos(movement(v.vertex, v.uv));
				//o.posWorld = mul(unity_ObjectToWorld, v.vertex);

				//
				///////////////////////////////////////////////////////////////原版无顶点扰动
				//o.vertex = UnityObjectToClipPos(v.vertex);
				float3 worldPos = mul(unity_ObjectToWorld, v.vertex);
				float4 screenPos = ComputeScreenPos(o.vertex);
				o.uv.xy = v.uv;
				o.posWorld = mul(unity_ObjectToWorld, v.vertex);
				////////////////////////////////////////////////////////////////////
				//
//				float time_1, time_2;
//				//_FixedTime = 1.0;
//				//_FixedCircleTime=1.0;
//#if defined(SHADER_API_MOBILE)
//				time_1 = _FixedTime.x * 0.05;
//				time_2 = _FixedCircleTime * 0.05;
//#else
//				time_1 = _Time.y;
//				time_2 = _Time.y;
//#endif
				//
				//o.bumpCoords.xyzw = (worldPos.xzxz + _Time.yyyy * _BumpDirection.xyzw) * _BumpTiling.xyzw;
				o.bumpCoords.xyzw = (worldPos.xzxz + _Time.zzzz * _BumpDirection.xyzw) * _BumpTiling.xyzw;
				o.viewVector = (worldPos - _WorldSpaceCameraPos.xyz);
				//计算深度图，需要投影坐标
				o.projPos = ComputeScreenPos(o.vertex);
				COMPUTE_EYEDEPTH(o.projPos.z);
				//边缘顶点色
				//o.color = v.color;
				//边缘光
				//float3 viewDir = normalize(ObjSpaceViewDir(v.vertex));
				//float rim = 1 - saturate(dot(viewDir, v.normal));
				//
				//rim = 1.0 - saturate(dot(normalize(viewDir), v.normal));
				//o.color.xyz = _RimColor.rgb * pow(rim, _RimPower) * 0.5;
				//
				//o.color = pow(rim, _RimPower);
				o.worldNormal = mul(v.normal, (float3x3)unity_WorldToObject);
				UNITY_TRANSFER_FOG(o, o.vertex); // pass fog coordinates to pixel shader
				
				return o;
			}
			
			//海水是否需要支持接收阴影,由于自发光没有diffuse，所以加接收阴影需要添加diffuse
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 result = fixed4(0,0,0,1);
				float3 worldNormal = normalize(PerPixelNormal(_BumpTex, i.bumpCoords, _BumpStrength));
				float3 viewVector = normalize(i.viewVector);
				float3 halfVector = normalize((normalize(_SunDir.xyz)-viewVector));

				float2 offsets = worldNormal.xz*viewVector.y;
				float4 refractColor = tex2D(_RefractTex, i.uv.xy+offsets*_Params.y)*_Color;
				//
				float3 reflUV = reflect( viewVector, worldNormal);
				float3 reflectColor = texCUBE(_Skybox, reflUV);
				//
				float2 fresnelUV = float2( saturate(dot(-viewVector, worldNormal)), 0.5);
				float fresnel = tex2D(_FresnelTex, fresnelUV).r;
				//计算深度，在边缘透明和边缘光都会用到
				float sceneZ = max(0, LinearEyeDepth(UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
				float partZ = max(0, i.projPos.z - _ProjectionParams.g);
				float depth = saturate((sceneZ - partZ) / _Depth);
				/////////////////////////////////////////////////////////////////////////////////////////////////
				//加入边缘颜色渐变和边缘浪
				fixed WaveSpeed = (_Time.g*(_Wavesspeed*0.1));
				fixed RimAllphaMultiply = (_RimColor.a*(1.0 - pow(saturate((sceneZ - partZ) / _RimSize), _Rimfalloff)));
				fixed Rim2Depth = RimAllphaMultiply - 0.5;
				fixed2 Tiling = (lerp(((-20.0)*i.uv), i.posWorld.rgb.rb, _Worldspacetiling)*(1.0 - _Tiling));
				fixed2 rimTiling = (Tiling*_Rimtiling);
				fixed2 rimPanningU = (rimTiling + WaveSpeed * float2(1.1, 0));
				float4 rimTexR = tex2D(_Shadermap, rimPanningU);
				fixed2 rimPanningV = (rimTiling + WaveSpeed * float2(0, 0.9));
				float4 rimTexB = tex2D(_Shadermap, rimPanningV);
				float AddRimTextureToMask = (RimAllphaMultiply + (RimAllphaMultiply*(1.0 - (rimTexR.b*rimTexB.b))*_RimColor.a));

				//////////////////////////////////////////////////////////////////////////////////////////////////
					
				//浅滩泡沫							
				//float2x2 foamMatrix = float2x2(1 / 25.0, 1, -cos(1 / 25.0), sin(25.0));
    //            float2 node_9794 = (mul(i.uv - float2(0.5, 0.5), foamMatrix) + float2(0.5, 0.5));
				float foamDepthFactor = sin(_Time.g * 0.2 * _FoamDepthSpeed - tex2D(_FresnelTex, i.uv).b) * 0.1;
                float2 foamUV = float2(_FoamSpeed * 0.1, _FoamSpeed * 0.1) * _Time.g + (i.uv * _FoamScale);
                float4 foamNoise = tex2D(_FoamTexture, foamUV);	
				float foamFactor = (foamDepthFactor + 0.2) * (foamNoise.r * _FoamOff) ;
                float foamDepth = (1.0 - saturate(pow(saturate((sceneZ-partZ) / foamFactor),15.0) * 10));

				//
				if(IsGammaSpace())
				{
					fresnel = pow(fresnel, 2.2);
				}
				//fresnel = FastFresnel(-viewVector, worldNormal, 0.02);

				refractColor.xyz= lerp(refractColor.xyz, _RimColor.rgb, saturate(AddRimTextureToMask));
				
				result.xyz = lerp(refractColor.xyz, reflectColor.xyz, fresnel);
				float3 specularColor = _Specular.w*pow(max(0, dot(worldNormal, halfVector)), _Params.x);
				result.xyz += _Specular.xyz*specularColor * _FresnelColor;
				/////////////////////////////////////////////////////////////////////////////////////////////////
				
				result.xyz += _FoamColor * foamDepth;

				//rim
				//fixed RimAllphaMultiply = (_RimColor.a*(1.0 - pow(saturate((sceneZ - partZ) / _RimSize), _Rimfalloff)));
				
				//alpha
				//result.a = lerp(refractColor.a, (refractColor.a*(1.0 - _Depthdarkness)), depth)*depth;//相当于用折射的地图当作漫反射颜色
				result.a = result.a* depth;//相当于上面一行的简化算法（在这个shader中），节约运算量
				if (foamDepth > 0) result.a = _FoamAlpha;

				//result.a *= min(_Range.x, depth) / _Range.x;
				UNITY_APPLY_FOG(i.fogCoord, result); // apply fog

				return result;
			}
			ENDCG
		}
	}
//	FallBack "Diffuse"
}
