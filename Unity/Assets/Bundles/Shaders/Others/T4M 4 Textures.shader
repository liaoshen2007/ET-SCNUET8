Shader "T4MShaders/ShaderModel2/Diffuse/T4M 4 Textures" {
Properties {
	_Splat0 ("Layer 1", 2D) = "white" {}
	_Splat1 ("Layer 2", 2D) = "white" {}
	_Splat2 ("Layer 3", 2D) = "white" {}
	_Splat3 ("Layer 4", 2D) = "white" {}
	[HDR]_Splat1Color ("Splat1Color", Color) = (1, 1, 1, 1)
	[HDR]_Splat2Color ("Splat2Color", Color) = (1, 1, 1, 1)
	[HDR]_Splat3Color ("Splat3Color", Color) = (1, 1, 1, 1)
	[HDR]_Splat4Color ("Splat4Color", Color) = (1, 1, 1, 1)
	_Tiling3("_Tiling4 x/y", Vector)=(1,1,0,0)
	_Control ("Control (RGBA)", 2D) = "white" {}
	_MainTex ("Never Used", 2D) = "white" {}
}
                
SubShader {
	Tags {
   "SplatCount" = "4"
   "RenderType" = "Opaque"
	}
CGPROGRAM
#pragma surface surf Lambert
#pragma exclude_renderers xbox360 ps3


struct Input {
	float2 uv_Control : TEXCOORD0;
	float2 uv_Splat0 : TEXCOORD1;
	float2 uv_Splat1 : TEXCOORD2;
	float2 uv_Splat2 : TEXCOORD3;
	float2 uv_Splat3 : TEXCOORD4;
};
 
sampler2D _Control;
sampler2D _Splat0,_Splat1,_Splat2,_Splat3;
fixed4 _Splat1Color,_Splat2Color,_Splat3Color,_Splat4Color;
float4 _Tiling3;
void surf (Input IN, inout SurfaceOutput o) {
	fixed4 splat_control = tex2D (_Control, IN.uv_Control).rgba;
		
	fixed3 lay1 = tex2D (_Splat0, IN.uv_Splat0);
	fixed3 lay2 = tex2D (_Splat1, IN.uv_Splat1);
	fixed3 lay3 = tex2D (_Splat2, IN.uv_Splat2);
	fixed3 lay4 = tex2D (_Splat3, IN.uv_Control*_Tiling3.xy);
	o.Alpha = 0.0;
	o.Albedo.rgb = (lay1 * splat_control.r * _Splat1Color + lay2 * splat_control.g * _Splat2Color + lay3 * splat_control.b * _Splat3Color + lay4 * splat_control.a * _Splat4Color);
}
ENDCG 
}
// Fallback to Diffuse
Fallback "Diffuse"
}
