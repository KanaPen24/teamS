Shader "Unlit/Flare"
{
	SubShader
	{
		Pass
		{
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

			TEXTURE2D(_MainTex);
			SAMPLER(sampler_MainTex);
			half4 _FlareColor;
			float4 _FlareVec;
			half4 _ParaColor;
			float4 _ParaVec;

			struct Attributes
			{
				float4 positionOS : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct Varyings
			{
				float4 positionCS : SV_POSITION;
				float2 uv : TEXCOORD0;
				UNITY_VERTEX_OUTPUT_STEREO
			};

			Varyings vert(Attributes IN)
			{
				Varyings OUT;
				VertexPositionInputs vertexInput = GetVertexPositionInputs(IN.positionOS);
				OUT.positionCS = vertexInput.positionCS;
				OUT.uv = IN.uv;
				return OUT;
			}

			half4 frag(Varyings IN) : SV_Target
			{
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);
				
				half4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv);

				float2 uv = IN.uv - 0.5f;

				float flare = (1 - saturate(length(uv - _FlareVec.xy))) * _FlareVec.z;
				flare = saturate(flare);

				float para = (1 - saturate(length(uv - _ParaVec.xy))) * _ParaVec.z;
				para = saturate(para);

				col = col + lerp(half4(0, 0, 0, 0), _FlareColor, flare) * lerp(half4(1, 1, 1, 1), _ParaColor, para);//lerp( _FlareColor, half4(0, 0, 0, 0), flare);
				return col;
			}
			ENDHLSL
		}
	}
}
