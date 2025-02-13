﻿Shader "UI/UILineShader"
{
	Properties
    {
        _Color   ("Main Color", Color) = (1,1,1,1)
		_CrossColor ("Cross color", Color) = (0,0,0,1)
        _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
        _Cutoff  ("Alpha cutoff", Range(0,1)) = 0.5
		_Distance ("Cross Distance", Float) = 0.5
		_Size ("Cross Size", Float) = 0.1
		_Speed ("Speed", Vector) = (-0.2,-0.2, 0, 0)
		_Border ("Border Size", Range(0., 1.)) = 0.1
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
        #pragma surface surf Lambert alphatest:_Cutoff
 
        sampler2D _MainTex;
        fixed4 _Color;
		fixed4 _CrossColor;
		float4 _Speed;
		float _Size;
		float _Distance;
		float _Border;
 
        struct Input
        {
            float2 uv_MainTex;
        };

		float drawCross(float2 pos, float2 uv, float size)
		{   
		    if(abs(pos.x - uv.x + uv.y) < size/4. && abs(pos.y - uv.y) < size) return 1.;	
			return 0.;
		}

		float4 _MainTex_TexelSize;
 
        void surf (Input IN, inout SurfaceOutput o)
        {
			float aspect = _MainTex_TexelSize.z/_MainTex_TexelSize.w;
			float2 uv = IN.uv_MainTex;
			float4 res;

			uv.x *= aspect;
			
			if(uv.x > 1. - _Border || uv.x < _Border || uv.y < _Border || uv.y > 1. - _Border)
			{
			uv -= fmod(_Time.y*_Speed.xy, _Distance*(aspect));
			float a = 0.;
			for(float i = (-1. - _Distance)*(aspect); i <= (1. + _Distance)*(aspect); i+=_Distance*(aspect))
			{
			    for(float j = (0. - _Distance)*(aspect); j <= 1. + _Distance; j+= _Distance*(aspect))
			    {
			        a += drawCross(float2(i,j), uv, _Size);
			    }
			}
			if(a == 0.) res = _Color; else res = _CrossColor;
			}
			else
			{
				res = float4(0.,0.,0.,0.);
			}
			

            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * res;
            o.Emission = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
}
