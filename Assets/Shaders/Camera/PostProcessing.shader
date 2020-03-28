﻿Shader "PostProcess/BWDiffuse" {
 Properties {
 _MainTex ("Base (RGB)", 2D) = "white" {}
 _bwBlend ("Black & White blend", Range (0, 1)) = 0
 _AddColor("Add Color", Color) = (0,0.016,0.073,0)
 }
 SubShader {
 
 Pass {
 Stencil
{
    Ref 13
    Comp NotEqual
    Pass keep
}
 CGPROGRAM
 #pragma vertex vert_img
 #pragma fragment frag
 
 #include "UnityCG.cginc"
 
 uniform sampler2D _MainTex;
 uniform float _bwBlend;
 uniform fixed4 _AddColor;
 
 float4 frag(v2f_img i) : COLOR {
 float4 c = tex2D(_MainTex, i.uv);
 
 float lum = c.r*.3 + c.g*.59 + c.b*.11;
 float3 bw = float3( lum, lum, lum ); 
 
 float4 result = c;
 result.rgb = lerp(c.rgb + _AddColor.rgb, bw, _bwBlend);
 return result;
 }
 ENDCG
 }
 }
}