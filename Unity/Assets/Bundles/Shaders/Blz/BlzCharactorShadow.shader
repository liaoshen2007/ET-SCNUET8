Shader "Blz/Charactor/Shadow"
{
    Properties
    {
        _BaseColor("Color", Color) = (1, 1, 1, 1)
        _MainTex("MainTex",2D) = "white"{}
        _ShadowMap("ShadowMap",2D) ="white"{}
        _ShadowColor("ShadowColor",Color)=(0,0,0,0)
        _ShadowThreshold("ShadowThreshold", Range(0, 1)) = 0
        _lightProIndensity("LightProIndensity", Range(0, 1)) = 0
    }

    SubShader{
        Pass{
            Tags{"LightMode"="ForwardBase"}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            sampler2D _MainTex;
            sampler2D _ShadowMap;

            fixed _IsUI;
            float4 _MainTex_ST;
            float4 _ShadowMap_ST;
            fixed4 _BaseColor;
            fixed4 _MainColor;
            fixed4 _ShadowColor;
            fixed _ShadowThreshold;
            //float _faceFrontX,_faceFrontY, _faceFrontZ;
            //float _faceUpX,_faceUpY,_faceUpZ;
            float4 _Front, _Up;
            float _lightProIndensity;
    
            struct v2f{
                float4 pos :SV_POSITION;
                float3 vertexLight : COLOR;
                float3 worldPos : TEXCOORD1;
                float2 uv: TEXCOORD2;
            };

            v2f vert(appdata_full v){
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld,v.vertex).xyz;
                o.uv = v.texcoord.xy* _MainTex_ST.xy + _MainTex_ST.zw;
				fixed3 worldNormal = UnityObjectToWorldNormal(v.normal);
                float3 shlight = ShadeSH9(float4(worldNormal, 1.0));
                o.vertexLight = shlight;	
                return o;
            }

            fixed4 frag(v2f i):SV_Target{

                float3 L = normalize(UnityWorldSpaceLightDir(i.worldPos));
                fixed3 BaseColor = tex2D(_MainTex,i.uv) * _BaseColor;
                float3 faceLightMap = tex2D(_ShadowMap, i.uv);


                //计算得到模型左侧和右侧向量
                float3 _Left = cross(_Up.xyz, _Front.xyz);
                float3 _Right = -_Left;
					  //光线与模型正前的夹角余弦
                float FL = dot(normalize(_Front.xz), normalize(L.xz));
                //光线与模型正左的夹角余弦
                float LL = dot(normalize(_Left.xz), normalize(L.xz));
                //光线与模型正右的夹角余弦
                float RL = dot(normalize(_Right.xz), normalize(L.xz));
                //采样好阈值图的两个通道
              	 //接下来我们将根据光线与面部正前方的夹角判断使用哪个通道
                float faceLightRightFace = faceLightMap.g;
                float faceLightLeftFace = faceLightMap.r;
                //比较RL和LL的大小来判断光线从左侧还是右侧射入,
              	 //LL更大则光线从左侧射入,RL大则从右侧射入
                //根据光线方向决定使用哪个通道的颜色，也就是选择哪张阈值图
                float vts = step(0, RL - LL);
                float valueToSample = faceLightRightFace * (1 - vts) + faceLightLeftFace * vts;
                //将阈值图上的值和1-FL比较,
                
                float k = 1 - (FL + _ShadowThreshold) / (1 + _ShadowThreshold);
                
                float isShadow = step(0, valueToSample - k);

                float result = step(0, k) * isShadow;

                float3 Diffuse = lerp(_ShadowColor*BaseColor, BaseColor, result);
                float3 finalColor = Diffuse;

				half inUI = step(0, _IsUI - 0.5); 				
				finalColor.rgb = (finalColor.rgb * _lightProIndensity + BaseColor.rgb * i.vertexLight) * (1 - inUI) + finalColor.rgb * inUI;				

                return float4(finalColor,1);
            }
        ENDCG

        }
    }
}