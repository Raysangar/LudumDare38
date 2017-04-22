Shader "LD38/OutlineShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_OutlineColor ("Outline Color", Color) = (1,1,1,1)
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _OutlineTexture;
			float4 _OutlineColor;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 colbase = tex2D(_MainTex, i.uv);

				float offsetTex = 1.5/_ScreenParams.x;
				fixed4 coloutline00 = tex2D(_OutlineTexture, i.uv+float2(0.0,0.0));
				coloutline00.rgb = coloutline00.a;
				fixed4 coloutline10 = tex2D(_OutlineTexture, i.uv+float2(offsetTex,0.0));
				coloutline10.rgb = coloutline10.a;
				fixed4 coloutline01 = tex2D(_OutlineTexture, i.uv+float2(0.0,offsetTex));
				coloutline01.rgb = coloutline01.a;
				fixed4 coloutline20 = tex2D(_OutlineTexture, i.uv+float2(-offsetTex,0.0));
				coloutline20.rgb = coloutline20.a;
				fixed4 coloutline02 = tex2D(_OutlineTexture, i.uv+float2(0.0,-offsetTex));
				coloutline02.rgb = coloutline02.a;

				return 	lerp(colbase,_OutlineColor,max(abs(coloutline10.a-coloutline20.a)/(2.0*offsetTex),abs(coloutline01.a-coloutline02.a)/(2.0*offsetTex)));
			}
			ENDCG
		}
	}
}
