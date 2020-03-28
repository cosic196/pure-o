Shader "Enemy/UltimateOutlineShadows"
{
	Properties
	{
		_Color("Main Color", Color) = (0.5,0.5,0.5,1)
		_MainTex("Texture", 2D) = "white" {}

		_FirstOutlineColor("Outline color", Color) = (1,0,0,0.5)
		_FirstOutlineWidth("Outlines width", Range(0.0, 2.0)) = 0.15

		_SecondOutlineColor("Outline color", Color) = (0,0,1,1)
		_SecondOutlineWidth("Outlines width", Range(0.0, 2.0)) = 0.025

		_Angle("Switch shader on angle", Range(0.0, 180.0)) = 89

		_CrossColor ("Cross color", Color) = (0,0,0,1)
        _Cutoff  ("Alpha cutoff", Range(0,1)) = 0.5
		_Distance ("Cross Distance", Range(0.2, 0.8)) = 0.5
		_StencilRef ("Stencil Ref", Float) = 13
		[PerRendererData]_Size ("Cross Size", Float) = 0.1
		[PerRendererData]_Speed ("Speed", Vector) = (-0.2,-0.2, 0, 0)
	}

	CGINCLUDE
	#include "UnityCG.cginc"

	struct appdata {
		float4 vertex : POSITION;
		float4 normal : NORMAL;
	};

	uniform float4 _FirstOutlineColor;
	uniform float _FirstOutlineWidth;

	uniform float4 _SecondOutlineColor;
	uniform float _SecondOutlineWidth;

	uniform sampler2D _MainTex;
	uniform float4 _Color;
	uniform float _Angle;

	ENDCG

	SubShader{
		//First outline
		Pass{
			Tags{ "Queue" = "Geometry" }
			Cull Front
			CGPROGRAM

			struct v2f {
				float4 pos : SV_POSITION;
			};

			#pragma vertex vert
			#pragma fragment frag

			v2f vert(appdata v) {
				appdata original = v;

				float3 scaleDir = normalize(v.vertex.xyz - float4(0,0,0,1));
				//This shader consists of 2 ways of generating outline that are dynamically switched based on demiliter angle
				//If vertex normal is pointed away from object origin then custom outline generation is used (based on scaling along the origin-vertex vector)
				//Otherwise the old-school normal vector scaling is used
				//This way prevents weird artifacts from being created when using either of the methods
				if (degrees(acos(dot(scaleDir.xyz, v.normal.xyz))) > _Angle) {
					v.vertex.xyz += normalize(v.normal.xyz) * _FirstOutlineWidth;
				}else {
					v.vertex.xyz += scaleDir * _FirstOutlineWidth;
				}

				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				return o;
			}

			half4 frag(v2f i) : COLOR{
				float4 color = _FirstOutlineColor;
				color.a = 1;
				return color;
			}

			ENDCG
		}
		

		//Second outline
		Pass{
			Tags{ "Queue" = "Geometry" }

			Cull Front
			CGPROGRAM

			struct v2f {
				float4 pos : SV_POSITION;
			};

			#pragma vertex vert
			#pragma fragment frag

			v2f vert(appdata v) {
				appdata original = v;

				float3 scaleDir = normalize(v.vertex.xyz - float4(0,0,0,1));
				//This shader consists of 2 ways of generating outline that are dynamically switched based on demiliter angle
				//If vertex normal is pointed away from object origin then custom outline generation is used (based on scaling along the origin-vertex vector)
				//Otherwise the old-school normal vector scaling is used
				//This way prevents weird artifacts from being created when using either of the methods
				if (degrees(acos(dot(scaleDir.xyz, v.normal.xyz))) > _Angle) {
					v.vertex.xyz += normalize(v.normal.xyz) * _SecondOutlineWidth;
				}
			else {
				v.vertex.xyz += scaleDir * _SecondOutlineWidth;
			}

			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);
			return o;
			}

			half4 frag(v2f i) : COLOR{
				float4 color = _SecondOutlineColor;
				color.a = 1;
				return color;
			}

			ENDCG
		}

		//Surface shader
		Tags{ "Queue" = "Geometry" "RenderType" = "Opaque" }
		Lighting Off
		Stencil
		{
		    Ref [_StencilRef]
		    Comp always
		    Pass replace
		}

		CGPROGRAM
		#pragma surface surf Lambert fullforwardshadows

		fixed4 _CrossColor;
		float4 _Speed;
		float _Size;
		float _Distance;

		struct Input {
			float2 uv_MainTex;
		};

		float drawCross(float2 pos, float2 uv, float size)
		{   
		    if(abs(pos.x - uv.x) < size && abs(pos.y - uv.y) < size/4.) return 1.;
		    if(abs(pos.x - uv.x) < size/4. && abs(pos.y - uv.y) < size) return 1.;	
			return 0.;
		}

		float4 _MainTex_TexelSize;

		void surf(Input IN, inout SurfaceOutput  o) {
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
			o.Alpha = 1;
		}
		ENDCG
	}
	Fallback "Diffuse"
}