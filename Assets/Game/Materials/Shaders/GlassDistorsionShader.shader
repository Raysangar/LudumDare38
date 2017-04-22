Shader "Hidden/GlassDistorsionShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
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

			fixed4 frag (v2f i) : SV_Target
			{
				float2 uvcenter = i.uv-float2(0.5,0.5);
				float2 uvscaled = uvcenter;
				uvscaled.y*=_ScreenParams.y/_ScreenParams.x;
				fixed4 colori = tex2D(_MainTex, i.uv);

				float factor = 0.25; 

				float2 uvdeform = uvcenter;
				uvdeform*=(sin(dot(uvdeform,uvdeform)));
				uvdeform = i.uv+10.0*uvdeform;
				//uvdeform = fmod(uvdeform+100.0,1.0);
				fixed4 coldeform = tex2D(_MainTex, uvdeform);

				coldeform = lerp(4.0*coldeform,coldeform,1.0-smoothstep(0.95*factor,factor,length(uvscaled)-0.003*sin(19.0*uvdeform.x)*sin(17.0*uvdeform.y)));
				colori = lerp(coldeform,colori,smoothstep(0.95,1.0,length(uvscaled)/factor));
				return colori;


			}
			ENDCG
		}
	}
}
