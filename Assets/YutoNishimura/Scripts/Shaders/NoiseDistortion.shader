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
                fixed4 noise1 = tex2D(_NoiseTex, noiseUv1 * 0.005); //�摜���g��

                fixed2 noiseUv2 = fixed2(i.uv.x + _Time.y * -1.5, i.uv.y);
                fixed4 noise2 = tex2D(_NoiseTex, noiseUv2 * 0.005); //�摜���g��

                fixed4 noise = (noise1 + noise2);

                //noise = noise * 10 - 5; //remap 0�`1�͈̔͂� -5�`5�͈̔͂�remap
                noise = noise * 10 - 15; //remap 0�`2�͈̔͂� -5�`5�͈̔͂�remap
                noise = abs(noise); //��Βl���Ƃ�A�}�C�i�X�l���������̃v���X�ɕω� 0�`5�̒l�ɕϊ�
                noise = 1 - noise; //�����𔽓]������ -4�`1�̒l��
                noise = saturate(noise); //-4�`1���Ƃŉ��Z�����Ƃ��ɕςȒl�ɂȂ�̂� 0�`1�ɍi�� clamp01�Ɠ���

                fixed4 color = tex2D(_MainTex, i.uv) + noise; //�ѓd�ƌ��̉摜�����Z

                return color;
            }

            ENDCG
        }
    }
}
