Shader "Custom/NoiseDistortion"
{
    Properties
    {
        _MainTex("MainTexture", 2D) = "white" {}
        _NoiseTex("NoiseTexture", 2D) = "white" {}
    }

    SubShader
    {
        Cull Off

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
                float2 uv : TEXCOORD;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD;
            };

            sampler2D _MainTex;
            sampler2D _NoiseTex;

            float _Border;
            float4 _Color;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed2 noiseUv1 = fixed2(i.uv.x + _Time.y * 1.2, i.uv.y);
                fixed4 noise1 = tex2D(_NoiseTex, noiseUv1 * 0.005); //‰æ‘œ‚ğŠg‘å

                fixed2 noiseUv2 = fixed2(i.uv.x + _Time.y * -1.5, i.uv.y);
                fixed4 noise2 = tex2D(_NoiseTex, noiseUv2 * 0.005); //‰æ‘œ‚ğŠg‘å

                fixed4 noise = (noise1 + noise2);

                //noise = noise * 10 - 5; //remap 0`1‚Ì”ÍˆÍ‚ğ -5`5‚Ì”ÍˆÍ‚Éremap
                noise = noise * 10 - 15; //remap 0`2‚Ì”ÍˆÍ‚ğ -5`5‚Ì”ÍˆÍ‚Éremap
                noise = abs(noise); //â‘Î’l‚ğ‚Æ‚èAƒ}ƒCƒiƒX’l‚¾‚Á‚½‚à‚Ìƒvƒ‰ƒX‚É•Ï‰» 0`5‚Ì’l‚É•ÏŠ·
                noise = 1 - noise; //•”’‚ğ”½“]‚³‚¹‚é -4`1‚Ì’l‚É
                noise = saturate(noise); //-4`1‚¾‚Æ‚Å‰ÁZ‚µ‚½‚Æ‚«‚É•Ï‚È’l‚É‚È‚é‚Ì‚Å 0`1‚Éi‚é clamp01‚Æ“¯‚¶

                fixed4 color = tex2D(_MainTex, i.uv) + noise; //‘Ñ“d‚ÆŒ³‚Ì‰æ‘œ‚ğ‰ÁZ

                return color;
            }

            ENDCG
        }
    }
}
