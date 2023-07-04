Shader "Custom/GaussianBlur"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Dispersion("Dispersion", float) = 1 //分散具合を調整
        _SamplingTexelAmount("Sampling Texel Amount", int) = 1 //何個先のテクセルまでサンプリングするか
        _TexelInterval("Texel Interval", float) = 2 //サンプリングするテクセルの間隔
    }

    SubShader
    {
        Cull Off        // カリングは不要
        ZTest Always    // ZTestは常に通す
        ZWrite Off      // ZWriteは不要

        Tags { "RenderType" = "Opaque" }

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

            sampler2D _MainTex;
            float4 _MainTex_ST;

            // _MainTex_TexelSize
            // x : 1.0 / 幅
            // y : 1.0 / 高さ
            // z : 幅
            // w : 高さ
            float2 _MainTex_TexelSize; //テクセルサイズ

            float2 _Direction; //C#から渡されるブラーをかける方向の変数
            float _Dispersion;
            int _SamplingTexelAmount;
            float _TexelInterval;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float GetGaussianWeight(float distance);

            fixed4 frag(v2f i) : SV_Target
            {
                float2 dir = _Direction * _MainTex_TexelSize.xy; //サンプリングの方向を決定

                //ウェイトを動的に導出する場合
                fixed4 color = 0;
                for (int j = 0; j < _SamplingTexelAmount; j++)
                {
                    float2 offset = dir * ((j + 1) * _TexelInterval - 1); //_TexelIntervalでサンプリング距離を調整
                    float weight = GetGaussianWeight(j + 1);              //ウェイトを計算
                    color.rgb += tex2D(_MainTex, i.uv + offset) * weight; //順方向をサンプリング＆重みづけして加算
                    color.rgb += tex2D(_MainTex, i.uv - offset) * weight; //逆方向をサンプリング＆重みづけして加算
                }
                return color;
            }

            inline float GetGaussianWeight(float distance)
            {
                return exp((-distance * distance) / (2 * _Dispersion * _Dispersion)) / _Dispersion;
            }
            ENDCG
        }
    }
}
