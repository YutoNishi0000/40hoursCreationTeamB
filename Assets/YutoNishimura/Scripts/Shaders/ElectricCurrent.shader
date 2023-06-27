Shader "Custom/ElectricCurrent"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NoiseTex("NoiseTexture", 2D) = "white" {}
        _ElectricColor("ElectricColor", Color) = (1,1,1,1)
        _BackGroundColor("BackGroundColor", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha

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
            sampler2D _NoiseTex;
            float4 _ElectricColor;
            float4 _BackGroundColor;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed2 noiseUv1 = fixed2(i.uv.x + _Time.y * 1.2, i.uv.y);
                fixed4 noise1 = tex2D(_NoiseTex, noiseUv1 * 0.005); //画像を拡大

                fixed2 noiseUv2 = fixed2(i.uv.x + _Time.y * -1.5, i.uv.y);
                fixed4 noise2 = tex2D(_NoiseTex, noiseUv2 * 0.005); //画像を拡大

                fixed4 noise = noise1 + noise2;

                noise = noise * 10 - 15; //remap 0～2の範囲を -5～5の範囲にremap
                noise = abs(noise); //絶対値をとり、マイナス値だったものプラスに変化 0～5の値に変換
                noise = 1 - noise; //黒白を反転させる -4～1の値に
                noise = saturate(noise); //-4～1だとで加算したときに変な値になるので 0～1に絞る clamp01と同じ

                //ここで、フラグメントと電流に色をそれぞれ蒸散することで任意の色にすることができる
                fixed4 color = tex2D(_MainTex, i.uv) * _BackGroundColor + noise * _ElectricColor;

                return color;
            }
            ENDCG
        }
    }
}
