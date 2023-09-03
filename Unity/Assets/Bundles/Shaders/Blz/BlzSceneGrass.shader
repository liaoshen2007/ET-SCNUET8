Shader "Blz/Scene/Grass" {
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
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			sampler2D _MainTex; 
			uniform vector _SelfPos;
			fixed _BenderStrength;
			fixed _BenderYStrength;

			UNITY_INSTANCING_BUFFER_START(Props)
                UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color)
				UNITY_DEFINE_INSTANCED_PROP(fixed, _Index)
            UNITY_INSTANCING_BUFFER_END(Props)

			v2f vert(appdata_t IN)
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(IN);
                UNITY_TRANSFER_INSTANCE_ID(IN, o);

				float4 localPos = IN.vertex;
				localPos.x += pow(1.0 + localPos.y, 3.0) * 0.1 * cos(_Time.y + UNITY_ACCESS_INSTANCED_PROP(Props, _Index)) * 0.5;
                float4 worldPos = mul(unity_ObjectToWorld, localPos);

				float bendRadius = _SelfPos.w;

				float3 benderWorldPos = _SelfPos.xyz;

				float distToBender = distance(float3(worldPos.x, 0, worldPos.z), float3(benderWorldPos.x, 0, benderWorldPos.z));

				float bendPower = (bendRadius - min(bendRadius, distToBender)) / (bendRadius + 0.001);

				float3 bendDir = normalize(worldPos - benderWorldPos);

				float2 vertexOffset = bendDir.xz * bendPower * _BenderStrength;

				worldPos.xz += lerp(float2(0, 0), vertexOffset, IN.texcoord.y);

				o.vertex = mul(UNITY_MATRIX_VP,worldPos);
				o.texcoord = IN.texcoord;
				return o;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(IN);
				half4 color = tex2D(_MainTex, IN.texcoord) * UNITY_ACCESS_INSTANCED_PROP(Props, _Color);
				return color;
			}
		ENDCG
		}
	}
}
