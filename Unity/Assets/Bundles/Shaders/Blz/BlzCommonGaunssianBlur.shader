Shader "Blz/Common/GaussianBlur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurLength ("BlurLength", float) = 5
    }
    SubShader
    {
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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed _BlurLength;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				half G[9] = { 1,  2,  1,
							  2,  4,  2,
							  1,  2,  1};
                fixed4 col = 0;
				for (int it = 0; it < 9; it++) {
					col += tex2D(_MainTex, i.uv + fixed2(floor(it / 3) - 1, it - floor(it / 3) * 3 - 1) / 1000 * 4) * G[it];
				}
				col /= 16;

				return col;
            }
            ENDCG
        }
    }
}
