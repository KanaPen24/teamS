Shader "Unlit/Flash"
{
	Properties
	{
		//主纹理
		_MainTexture("Main Texture", 2D) = "white" {}
		//流光纹理
		_FlashTex("Flash Texture",2D) = "white"{}
		//遮罩纹理
		_MaskTex("Mask Texture",2D) = "white"{}
		//流光颜色
		_FlashColor("Flash Color",Color) = (1,1,1,1)
		//流光强度
		_FlashIntensity("Flash Intensity", Range(0, 1)) = 0.6
		//流光区域缩放
		_FlashScale("Flash Scale", Range(0.1, 1)) = 0.5
		//水平流动速度
		_FlashSpeedX("Flash Speed X", Range(-5, 5)) = 0.5
		//垂直流动速度
		_FlashSpeedY("Flash Speed Y", Range(-5, 5)) = 0
		//主纹理凸起值
		_RaisedValue("Raised Value", Range(-0.5, 0.5)) = -0.01
		//流光能见度
		_Visibility("Visibility", Range(0, 1)) = 1
	}
	SubShader
	{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		LOD 100

		Blend SrcAlpha OneMinusSrcAlpha
		
			Pass
		{
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

			TEXTURE2D(_MainTexture);
			SAMPLER(sampler_MainTexture);
			TEXTURE2D(_FlashTex);
			SAMPLER(sampler_FlashTex);
			TEXTURE2D(_MaskTex);
			SAMPLER(sampler_MaskTex);

			CBUFFER_START(UnityPerMaterial)
			half4 _FlashColor;
			float _FlashIntensity;
			float _FlashScale;
			float _FlashSpeedX;
			float _FlashSpeedY;
			float _RaisedValue;
			float _Visibility;
			CBUFFER_END

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				UNITY_VERTEX_OUTPUT_STEREO
			};

			float4 _MainTexture_ST;
			// sampler2D _MainTexture;
			// sampler2D _FlashTex;
			// sampler2D _MaskTex;
			// half4 _FlashColor;
			// float _FlashIntensity;
			// float _FlashScale;
			// float _FlashSpeedX;
			// float _FlashSpeedY;
			// float _RaisedValue;			
			// float _Visibility;

			v2f vert(appdata v)
			{
				v2f o;
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				o.vertex = TransformObjectToHClip(v.vertex.xyz);
				o.uv = v.uv;
				return o;
			}

			half4 frag(v2f i) : SV_Target
			{
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				//=====================计算流光贴图的uv=====================
				//缩放流光区域
				float2 flashUV = i.uv * _FlashScale;
				//不断改变uv的x轴，让他往x轴方向移动
				flashUV.x += sin(_Time.y*_FlashSpeedX);
				//不断改变uv的y轴，让他往y轴方向移动
				flashUV.y += sin(_Time.y*_FlashSpeedY);

				//=====================计算流光贴图的可见区域=====================
				//取流光贴图的alpha值
				half flashAlpha = SAMPLE_TEXTURE2D(_FlashTex, sampler_FlashTex, flashUV).a;
				//取遮罩贴图的alpha值
				half maskAlpha = SAMPLE_TEXTURE2D(_MaskTex, sampler_MaskTex, i.uv).a;
				//最终在主纹理上的可见值（flashAlpha和maskAl,pha任意为0则该位置不可见）
				half visible = flashAlpha*maskAlpha*_FlashIntensity*_Visibility;

				//=====================计算主纹理的uv=====================
				//被流光贴图覆盖的区域凸起（uv的y值增加）
				float2 mainUV = i.uv;
				mainUV.y += visible*_RaisedValue;

				//=====================最终输出=====================
				//主纹理 + 可见的流光
				half4 col = SAMPLE_TEXTURE2D(_MainTexture, sampler_MainTexture, mainUV) + visible*_FlashColor;
				return col;
			}
			ENDHLSL
		}
	}
}

