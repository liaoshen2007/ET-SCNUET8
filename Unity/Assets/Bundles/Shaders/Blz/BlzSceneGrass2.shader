Shader "Blz/Scene/Grass2" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("MainTex", 2D) = "white" {}
		_BenderStrength("BenderStrength", float) = 1
		_BenderYStrength("BenderYStrength", float) = 1
	}

	SubShader
	{
		Tags
		{
			"RenderType"="Opaque"
		}
		LOD 100
		Cull Off
		ZWrite ON

		Pass
		{
		CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#pragma instancing_options procedural:setup
			#include "UnityCG.cginc"

			struct appdata_t
			{
				float4 vertex   : POSITION;
				float2 texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 texcoord  : TEXCOORD0;
				float3 color : TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			sampler2D _MainTex; 
			uniform vector _SelfPos;
			fixed _BenderStrength;
			fixed _BenderYStrength;

			struct insInfo
			{
				float4x4 matrix0;     
				float4 color0;
				float index;
			};
			StructuredBuffer<insInfo> _InsBuffer;

			v2f vert(appdata_t IN, uint instanceID :SV_INSTANCEID)
			{
				v2f o;
                //UNITY_TRANSFER_INSTANCE_ID(IN, o);

				insInfo info = _InsBuffer[instanceID];

				float4 localPos = IN.vertex;
				localPos.x += pow(1.0 + localPos.y, 3.0) * 0.1 * cos(_Time.y + info.index) * 0.5;
                float4 worldPos = mul(unity_ObjectToWorld, localPos);
				worldPos = mul(info.matrix0, worldPos);

				float bendRadius = _SelfPos.w;

				float3 benderWorldPos = _SelfPos.xyz;

				float distToBender = distance(float3(worldPos.x, 0, worldPos.z), float3(benderWorldPos.x, 0, benderWorldPos.z));

				float bendPower = (bendRadius - min(bendRadius, distToBender)) / (bendRadius + 0.001);

				float3 bendDir = normalize(worldPos - benderWorldPos);

				float2 vertexOffset = bendDir.xz * bendPower * _BenderStrength;

				worldPos.xz += lerp(float2(0, 0), vertexOffset, IN.texcoord.y);

				o.vertex = mul(UNITY_MATRIX_VP,worldPos);
				o.texcoord = IN.texcoord;
				o.color = info.color0;
				return o;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				half4 color = tex2D(_MainTex, IN.texcoord);
				color.rgb = color.rgb * IN.color;
				return color;
			}
		ENDCG
		}
	}
}
