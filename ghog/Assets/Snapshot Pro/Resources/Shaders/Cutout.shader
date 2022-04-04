﻿Shader "Hidden/SnapshotPro/Cutout"
{
	SubShader
	{
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			HLSLPROGRAM

			#pragma vertex VertDefault
			#pragma fragment Frag

			#include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

			TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
			TEXTURE2D_SAMPLER2D(_CutoutTex, sampler_CutoutTex);
			float4 _BorderColor;
			int _Stretch;

			float _Zoom;
			float2 _Offset;
			float _Rotation;

			float4 Frag(VaryingsDefault i) : SV_Target
			{
				float2 UVs = (i.texcoord - 0.5f) / _Zoom;

				float aspect = (_Stretch == 0) ? _ScreenParams.x / _ScreenParams.y : 1.0f;
				UVs = float2(aspect * UVs.x, UVs.y);

				float2x2 rotation = float2x2(cos(_Rotation), sin(_Rotation),
					-sin(_Rotation), cos(_Rotation));

				UVs = mul(rotation, UVs);

				UVs += float2(_Offset.x * aspect, _Offset.y) + 0.5f;

				float cutoutAlpha = SAMPLE_TEXTURE2D(_CutoutTex, sampler_CutoutTex, UVs).a;
				float4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);
				return lerp(col, _BorderColor, cutoutAlpha);
			}

			ENDHLSL
		}
	}
}
