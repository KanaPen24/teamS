Shader "Hidden/Gaussian"
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
			float _Rate;
			static const int samplingCount = 7;
			static const half weights[samplingCount] = { 0.036, 0.113, 0.216, 0.269, 0.216, 0.113, 0.036 };

			struct Attributes
			{
				float4 positionOS : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct Varyings
			{
				float4 positionHCS : SV_POSITION;
				float2 coordV : TEXCOORD0;
				float2 coordH : TEXCOORD1;
				float2 offsetV: TEXCOORD2;
				float2 offsetH: TEXCOORD3;
				UNITY_VERTEX_OUTPUT_STEREO
			};

			Varyings vert(Attributes IN)
			{
				Varyings OUT;
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
				OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
				float rate = lerp(.0, 5.0f, _Rate);
				
				// �T���v�����O�|�C���g�̃I�t�Z�b�g
				OUT.offsetV = float2(0, 1 / _ScreenParams.y) * rate;
				OUT.offsetH = float2(1 / _ScreenParams.x, 0) * rate;

				// �T���v�����O�J�n�|�C���g��UV���W
				OUT.coordV = IN.uv - OUT.offsetV * ((samplingCount - 1) * 0.5);
				OUT.coordH = IN.uv - OUT.offsetH * ((samplingCount - 1) * 0.5);

				return OUT;
			}

			half4 frag(Varyings IN) : SV_Target
			{
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);
				
				half4 col = 0;

				// ��������
				for (int i = 0; i < samplingCount; ++i)
				{
					// �T���v�����O���ďd�݂��|����B��Ő����������������邽��0.5��������
					col += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.coordV) * weights[i] * 0.5;

					// offset�������T���v�����O�|�C���g�����炷
					IN.coordV += IN.offsetV;
				}

				// ��������
				for (int j = 0; j < samplingCount; ++j)
				{
					col += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.coordH) * weights[j] * 0.5;
					// offset�������T���v�����O�|�C���g�����炷
					IN.coordH += IN.offsetH;
				}

				return col;
			}
			ENDHLSL
		}
	}
}
