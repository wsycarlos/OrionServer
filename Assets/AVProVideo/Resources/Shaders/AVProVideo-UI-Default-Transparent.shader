Shader "AVProVideo/UI/Transparent Packed"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		
		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255

		_ColorMask ("Color Mask", Float) = 15

		_VertScale("Vertical Scale", Range(-1, 1)) = 1.0

		[KeywordEnum(None, Top_Bottom, Left_Right)] AlphaPack("Alpha Pack", Float) = 0
		[Toggle(APPLY_GAMMA)] _ApplyGamma("Apply Gamma", Float) = 0
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}
		
		Stencil
		{
			Ref [_Stencil]
			Comp [_StencilComp]
			Pass [_StencilOp] 
			ReadMask [_StencilReadMask]
			WriteMask [_StencilWriteMask]
		}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest [unity_GUIZTestMode]
		Fog { Mode Off }
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask [_ColorMask]

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile ALPHAPACK_NONE ALPHAPACK_TOP_BOTTOM ALPHAPACK_LEFT_RIGHT
			#pragma multi_compile __ APPLY_GAMMA
#if APPLY_GAMMA
			#pragma target 3.0
#endif
			#include "UnityCG.cginc"
            // TODO: once we drop support for Unity 4.x then we can include this
			//#include "UnityUI.cginc"    
			#include "AVProVideo.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				half4 texcoord  : TEXCOORD0;
				float4 worldPosition : TEXCOORD1;
			};
			
			uniform fixed4 _Color;
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_TexelSize;
			uniform float _VertScale;
			uniform float4 _ClipRect;

#if UNITY_VERSION >= 520
			inline float UnityGet2DClipping (in float2 position, in float4 clipRect)
			{
			 	float2 inside = step(clipRect.xy, position.xy) * step(position.xy, clipRect.zw);
			 	return inside.x * inside.y;
			}
#endif

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.worldPosition = IN.vertex;
				OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);

#ifdef UNITY_HALF_TEXEL_OFFSET
				OUT.vertex.xy += (_ScreenParams.zw-1.0)*float2(-1,1);
#endif

				OUT.texcoord.xy = IN.texcoord.xy;

				// Horrible hack to undo the scale transform to fit into our UV packing layout logic...
				if (_VertScale < 0.0)
				{
					OUT.texcoord.y = 1.0 - OUT.texcoord.y;
				}

				OUT.texcoord = OffsetAlphaPackingUV(_MainTex_TexelSize.xy, OUT.texcoord.xy, _VertScale < 0.0);

				OUT.color = IN.color * _Color;
				return OUT;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				// Sample RGB
				half4 color = tex2D(_MainTex, IN.texcoord.xy);
#if APPLY_GAMMA
				color.rgb = GammaToLinear(color.rgb);
#endif

#if ALPHAPACK_TOP_BOTTOM | ALPHAPACK_LEFT_RIGHT
				// Sample the alpha
				half4 alpha = tex2D(_MainTex, IN.texcoord.zw);
#if APPLY_GAMMA
				alpha.rgb = GammaToLinear(alpha.rgb);
#endif
				color.a = (alpha.r + alpha.g + alpha.b) / 3.0;
#endif
				color *= IN.color;
				
#if UNITY_VERSION >= 520
				color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
#endif
				clip(color.a - 0.001);

				return color;
			}

		ENDCG
		}
	}
}
