Shader "Custom/ColorLerpURP"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ColorA ("Color A", Color) = (1,0,0,1)
        _ColorB ("Color B", Color) = (0,0,1,1)
        _Speed ("Speed", Float) = 1.0
        _TimeOffset ("Time Offset", Float) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Overlay" }
        LOD 200

        Pass
        {
            HLSLPROGRAM
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #pragma vertex Vert
            #pragma fragment Frag

            struct Attributes
            {
                float4 position : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float2 uv : TEXCOORD0;
                float4 position : SV_POSITION;
            };

            TEXTURE2D(_MainTex);
            float4 _MainTex_ST;
            SAMPLER(sampler_MainTex);

            float4 _ColorA;
            float4 _ColorB;
            float _Speed;
            float _TimeOffset;

            Varyings Vert (Attributes v)
            {
                Varyings o;
                o.position = TransformObjectToHClip(v.position);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float4 Frag (Varyings i) : SV_Target
            {
                float2 uv = i.uv;
                float4 texColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv);

                // Расчет времени
                float t = _TimeOffset + _Time.y * _Speed;
                t = fmod(t, 2.0); // цикл между 0 и 2
                float lerpFactor = t < 1.0 ? t : 2.0 - t; // плавное движение туда-обратно

                float4 color = lerp(_ColorA, _ColorB, lerpFactor);
                return texColor * color;
            }
            ENDHLSL
        }
    }
}