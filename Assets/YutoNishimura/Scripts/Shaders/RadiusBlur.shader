//放射線ブラーのポストエフェクト
Shader "Custom/RadiusBlur"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        //_SampleCount("Sample Count", Range(4, 16)) = 8
        //_Strength("Strength", Range(0.0, 1.0)) = 0.5
        _BlurDegree("BlurDegree", float) = 0
        _SampleCount("SampleCount", int) = 5
    }
    SubShader
    {
        Cull Off
        ZWrite Off
        ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex   : POSITION;
                float2 uv       : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv       : TEXCOORD0;
                float4 vertex   : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            //half _SampleCount;
            //half _Strength;
            float _BlurDegree;
            uniform float4 _BlurCenter;
            int _SampleCount;

            fixed4 frag(v2f i) : SV_Target
            {
                float2 dir = i.uv - _BlurCenter.xy;
                float4 color = 0;
                //指定した回数だけuvの中心座標からの距離に応じてサンプラーを加算する
                for (int j = 0; j < _SampleCount; j++)
                {
                    //指定した距離を一辺とするブロックごとに値を足していくイメージ
                    float2 uv = i.uv + _BlurDegree * j * dir;
                    color += tex2D(_MainTex, uv);
                }
                //平均値を取得
                color /= _SampleCount;
                return color;
            }

            ENDCG
        }
    }
}
