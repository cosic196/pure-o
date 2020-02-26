// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Gradient/UnlitGradientCastShadow" {
	Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
		_Color2("Second Color", Color) = (0,0,0,1)
        _MainTex ("Base (RGB)", 2D) = "white" {}
		_Spread ("Spread", Float) = 1
		_Offset ("Offset", Float) = 0
      //  _Cutoff  ("Alpha cutoff", Range(0,1)) = 0.5


    }

    SubShader {
       

    Tags {"Queue" = "Geometry" "RenderType" = "Opaque"}

//   Tags {"Queue" = "AlphaTest" "RenderType" = "TransparentCutout"}

 
        Pass {
            Tags {"LightMode" = "ForwardBase"}
            CGPROGRAM

                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile_fwdbase
                #pragma fragmentoption ARB_fog_exp2
                #pragma fragmentoption ARB_precision_hint_fastest
          //    #pragma alphatest:_Cutoff
              
                #include "UnityCG.cginc"
                #include "AutoLight.cginc"
              
                struct v2f
                {
                    float4  pos         : SV_POSITION;
                    float2  uv          : TEXCOORD0;
					float4 screenPos : TEXCOORD1;
                    LIGHTING_COORDS(1,2)
                };
 
                v2f vert (appdata_tan v)
                {
                    v2f o;
                  
                    o.pos = UnityObjectToClipPos( v.vertex);
                    o.uv = v.texcoord.xy;       
					o.screenPos = ComputeScreenPos(o.pos);
                    TRANSFER_VERTEX_TO_FRAGMENT(o);
                    return o;
                }

				float GetY(float uvY, float spread, float offset)
				{
				    float y;
				    float minVal = 0.5 - 1./spread + offset;
				    float maxVal = 0.5 + 1./spread + offset;
				    if(uvY <= minVal) y = 0.;
				    else if(uvY >= maxVal) y = 1.;
				    else if(uvY >= minVal) y = (uvY - minVal) / (maxVal - minVal);
				    return y;
				}
 
                sampler2D _MainTex;
             // float _Cutoff;
                fixed4 _Color;
				fixed4 _Color2;
				float _Spread;
				float _Offset;
              
                fixed4 frag(v2f i) : COLOR
                {
                    fixed4 color = tex2D(_MainTex, i.uv);
                // clip(color.a - _Cutoff);
                  
                    //fixed atten = LIGHT_ATTENUATION(i); // Light attenuation + shadows.
                    fixed atten = SHADOW_ATTENUATION(i); // Shadows ONLY.
                   // fixed4 ambient = UNITY_LIGHTMODEL_AMBIENT.rgba;
				   float4 uv = i.screenPos / i.screenPos.w;
				   float y = GetY(uv.y, _Spread, _Offset);
				   fixed4 color1 = _Color * y;
				   fixed4 color2 = _Color2 * (1. - y);

                    return color * atten * (color1 + color2) ;
                }

            ENDCG
        }

 
        Pass {

            Tags {"LightMode" = "ForwardAdd"}
            //Blend One One


            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile_fwdadd_fullshadows
                #pragma fragmentoption ARB_fog_exp2
                #pragma fragmentoption ARB_precision_hint_fastest
              
                #include "UnityCG.cginc"
                #include "AutoLight.cginc"
              
                struct v2f
                {
                    float4  pos         : SV_POSITION;
                    float2  uv          : TEXCOORD2;
					float4 screenPos : TEXCOORD3;
                    LIGHTING_COORDS(4,5)
                };
 
                v2f vert (appdata_tan v)
                {
                    v2f o;
                  
                    o.pos = UnityObjectToClipPos( v.vertex);
                    o.uv = v.texcoord.xy;
					o.screenPos = ComputeScreenPos(o.pos);
                    TRANSFER_VERTEX_TO_FRAGMENT(o);
                    return o;
                }

				float GetY(float uvY, float spread, float offset)
				{
				    float y;
				    float minVal = 0.5 - 1./spread + offset;
				    float maxVal = 0.5 + 1./spread + offset;
				    if(uvY <= minVal) y = 0.;
				    else if(uvY >= maxVal) y = 1.;
				    else if(uvY >= minVal) y = (uvY - minVal) / (maxVal - minVal);
				    return y;
				}
 
                sampler2D _MainTex;
                float _Cutoff;
                fixed4 _Color;
				fixed4 _Color2;
				float _Spread;
				float _Offset;
              
                fixed4 frag(v2f i) : COLOR
                {
                    fixed4 color = tex2D(_MainTex, i.uv);
              //   clip(color.a - _Cutoff);  
                                  
                    //fixed atten = LIGHT_ATTENUATION(i); // Light attenuation + shadows.
                   fixed atten = SHADOW_ATTENUATION(i); // Shadows ONLY.    
				   float4 uv = i.screenPos / i.screenPos.w;
				   float y = GetY(uv.y, _Spread, _Offset);
				   fixed4 color1 = _Color * y;
				   fixed4 color2 = _Color2 * (1. - y);
                    return tex2D(_MainTex, i.uv) * atten * (color1 + color2) ;
                }

            ENDCG
        }
    }

    Fallback "Diffuse"  

   // Fallback "Transparent/Cutout/VertexLit"
}
