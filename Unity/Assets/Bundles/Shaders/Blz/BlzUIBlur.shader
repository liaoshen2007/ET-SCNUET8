Shader "Blz/UI/Blur"
{
    Properties
    {
        _MainTex("Base (RGB)", 2D) = "white" {}
        _BlurLength ("Blur Length", float) = 1
        _Illuminance ("Illuminance", float) = 0.6
    }
    SubShader
    {
		Tags
		{ 
			"IgnoreProjector"="True" 
			"RenderType"="Opaque" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest [unity_GUIZTestMode]
		//Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            half _BlurLength;
            half _Illuminance;
            sampler2D _MainTex;float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				half G[9] = { 1,  2,  1,
							  2,  4,  2,
							  1,  2,  1};
                fixed4 col = 0;
				for (int it = 0; it < 9; it++) {
					col += tex2D(_MainTex, i.uv + fixed2(floor(it / 3) - 1, it - floor(it / 3) * 3 - 1) * _BlurLength * 0.01) * G[it];
				}
				col /= 16;
                col.a = 1;
                
                
				return col * _Illuminance;
            }
            ENDCG
        }
    }
}
