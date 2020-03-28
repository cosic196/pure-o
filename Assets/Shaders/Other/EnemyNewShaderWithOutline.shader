// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Enemy/StaticShaderCrossesOutline" {
    Properties
    {
        _Color   ("Main Color", Color) = (1,1,1,1)
		_CrossColor ("Cross color", Color) = (0,0,0,1)
        _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
        _Cutoff  ("Alpha cutoff", Range(0,1)) = 0.5
		_Distance ("Cross Distance", Float) = 0.5
		[PerRendererData]_Size ("Cross Size", Float) = 0.1
		[PerRendererData]_Speed ("Speed", Vector) = (-0.2,-0.2, 0, 0)

		_FirstOutlineColor("Outline color", Color) = (1,0,0,0.5)
		_FirstOutlineWidth("Outlines width", Range(0.0, 2.0)) = 0.15

		_SecondOutlineColor("Outline color", Color) = (0,0,1,1)
		_SecondOutlineWidth("Outlines width", Range(0.0, 2.0)) = 0.025

		_Angle("Switch shader on angle", Range(0.0, 180.0)) = 89
    }
    SubShader
    {
        Tags
        {
            "Queue" = "AlphaTest"
            "IgnoreProjector" = "True"
            "RenderType" = "TransparentCutout"
        }
        LOD      100
        Lighting Off
	
        CGPROGRAM
        #pragma surface surf Lambert //alphatest:_Cutoff
 
        sampler2D _MainTex;
        fixed4 _Color;
		fixed4 _CrossColor;
		fixed4 _Speed;
		fixed _Size;
		fixed _Distance;
 
        struct Input
        {
            float2 uv_MainTex;
        };

		float drawCross(float2 pos, float2 uv, float size)
		{   
		    if(abs(pos.x - uv.x) < size && abs(pos.y - uv.y) < size/4.) return 1.;
		    if(abs(pos.x - uv.x) < size/4. && abs(pos.y - uv.y) < size) return 1.;	
			return 0.;
		}

		float4 _MainTex_TexelSize;
 
        void surf (Input IN, inout SurfaceOutput o)
        {
			float aspect = _MainTex_TexelSize.z/_MainTex_TexelSize.w;
			float2 uv = IN.uv_MainTex;

			uv.x *= aspect;
			uv -= fmod(_Time.y*_Speed.xy, _Distance*(aspect));
			float a = 0.;
			for(float i = (0. - _Distance)*(aspect); i <= (1. + _Distance)*(aspect); i+=_Distance*(aspect))
			{
			    for(float j = (0. - _Distance)*(aspect); j <= 1. + _Distance; j+= _Distance*(aspect))
			    {
			        a += drawCross(float2(i,j), uv, _Size);
			    }
			}
			float4 res;
			if(a == 0.) res = _Color; else res = _CrossColor;

            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * res;
            o.Emission = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
 
    Fallback "Transparent/Cutout/VertexLit"
}