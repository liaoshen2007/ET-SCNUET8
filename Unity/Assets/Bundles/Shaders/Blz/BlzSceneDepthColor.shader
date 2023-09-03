Shader "Blz/Scene/DepthColor"
{
    Properties
    {
        _MainTex("MainTex", 2D) = "white"{}
        _Color("Color", Color) = (1, 1, 1, 1)
        _Depth("Depth", float) = 1
        _Lin1Color("Lin1Color", Color) = (1, 1, 1, 1)
        _Lin2Color("Lin2Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags{  "IgnoreProjector" = "True" "RenderType" = "Transparent" }

        Pass
        {            
            ZWrite Off
            Blend SrcAlpha  OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            sampler2D _MainTex;float4 _MainTex_ST;
            uniform sampler2D _CameraDepthTexture;
            float4 _Color;
            float _Depth;
            float4 _Lin1Color, _Lin2Color;
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 projPos : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv =  TRANSFORM_TEX(v.uv, _MainTex);
                o.projPos = ComputeScreenPos(o.vertex);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }

            //todo 
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 color = tex2D(_MainTex, i.uv);
                float sceneZ = max(0, LinearEyeDepth(UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
				float partZ = max(0, i.projPos.z - _ProjectionParams.g);
                float depth = saturate((sceneZ - partZ) / _Depth);
                
                fixed4 col = lerp(_Lin1Color, _Lin2Color, depth) * color;
                col.a *= depth;
                return col;
            }
            ENDCG
        }
    }
}
