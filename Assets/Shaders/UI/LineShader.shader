// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "UI/LineShader" {
     Properties{
         [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
         _Color("Base Color", Color) = (1,1,1,1)
         _CrossColor ("Line color", Color) = (0,0,0,1)
         _Cutoff  ("Alpha cutoff", Range(0,1)) = 0.5
		 _Distance ("Line Distance", Float) = 0.5
		 _Size ("Line Size", Float) = 0.1
		 _Speed ("Speed", Vector) = (-0.2,-0.2, 0, 0)
		 _Border ("Border Size", Range(0., 1.)) = 0.1
 
         _StencilComp("Stencil Comparison", Float) = 8
         _Stencil("Stencil ID", Float) = 0
         _StencilOp("Stencil Operation", Float) = 0
         _StencilWriteMask("Stencil Write Mask", Float) = 255
         _StencilReadMask("Stencil Read Mask", Float) = 255
         _ColorMask("Color Mask", Float) = 15
         // see for example
         // http://answers.unity3d.com/questions/980924/ui-mask-with-shader.html
 
     }
 
         SubShader{
         Tags{ "Queue" = "Transparent"
             "IgnoreProjector" = "True"
             "RenderType" = "Transparent"
             "PreviewType" = "Plane"
             "CanUseSpriteAtlas" = "True" }
     
 
         Stencil
         {
             Ref[_Stencil]
             Comp[_StencilComp]
             Pass[_StencilOp]
             ReadMask[_StencilReadMask]
             WriteMask[_StencilWriteMask]
         }
 
         Cull Off
         Lighting Off
         ZWrite Off
         ZTest[unity_GUIZTestMode]
         Fog{ Mode Off }
         Blend SrcAlpha OneMinusSrcAlpha
         ColorMask[_ColorMask]
 
         Pass{
         CGPROGRAM
 #pragma vertex vert
 #pragma fragment frag
 #include "UnityCG.cginc"
 
         struct appdata_t
     {
         float4 vertex   : POSITION;
         float4 color    : COLOR;
         float2 texcoord : TEXCOORD0;
     };
 
     struct v2f
     {
         float4 vertex   : SV_POSITION;
         fixed4 color : COLOR;
         half2 texcoord  : TEXCOORD0;
     };
 
     fixed4 _Color;
     fixed4 _Color2;
	 fixed4 _CrossColor;
	 float4 _Speed;
	 float _Size;
	 float _Distance;
	 float _Border;

	 float drawCross(float2 pos, float2 uv, float size)
		{   
		    if(abs(pos.x - uv.x + uv.y) < size/4. && abs(pos.y - uv.y) < size) return 1.;	
			return 0.;
		}
 
     v2f vert(appdata_t IN)
     {
         v2f OUT;
         OUT.vertex = UnityObjectToClipPos(IN.vertex);
         OUT.texcoord = IN.texcoord;
 #ifdef UNITY_HALF_TEXEL_OFFSET
         OUT.vertex.xy += (_ScreenParams.zw - 1.0)*float2(-1,1);
 #endif
         OUT.color = lerp(_Color, _Color2, IN.texcoord.x);
         return OUT;
     }
 
     sampler2D _MainTex;
 
     fixed4 frag(v2f i) : COLOR{
		 float aspect = 1.;
		 float2 uv = i.texcoord;
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
         fixed4 c = tex2D(_MainTex, i.texcoord) * res;
         clip(c.a - 0.01);
         return c;
     }
         ENDCG
     }
     }
 }