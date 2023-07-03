Shader "Custom/ElectricCurrent"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NoiseTex("NoiseTexture", 2D) = "white" {}
        _ElectricColor("ElectricColor", Color) = (1,1,1,1)
        _BackGroundColor("BackGroundColor", Color) = (1, 1, 1, 1)
        _Blend("Blend", float) = 0
        _BlurDegree("BlurDegree", float) = 0
        _SampleCount("SampleCount", int) = 1
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
            float _Blend;
            float _BlurDegree;
            uniform float4 _BlurCenter;
            int _SampleCount;

            fixed4 frag (v2f i) : SV_Target
            {
                //��ڂ�uv�l���擾�i���Ԍo�߂ƂƂ��Ɏ擾�ʒu�����ɂ����j
                fixed2 noiseUv1 = fixed2(i.uv.x + _Time.y * 1.2, i.uv.y);
                //�m�C�Y�摜���g��
                fixed4 noise1 = tex2D(_NoiseTex, noiseUv1 * 0.005); 

                //��ڂ�uv�l���擾�i���Ԍo�߂ƂƂ��Ɏ擾�ʒu�����ɂ����j
                fixed2 noiseUv2 = fixed2(i.uv.x + _Time.y * -1.5, i.uv.y);
                //�m�C�Y�摜���g��
                fixed4 noise2 = tex2D(_NoiseTex, noiseUv2 * 0.005);

                //�擾�������uv�l�����Z���邱�ƂŎ擾����uv���W���d���̂悤�ɂȂ�
                fixed4 noise = noise1 + noise2;

                //remap 0�`2�͈̔͂� -5�`5�͈̔͂�remap
                noise = noise * 10 - 15; 
                //��Βl���Ƃ�A�}�C�i�X�l���������̃v���X�ɕω� 0�`5�̒l�ɕϊ�
                noise = abs(noise); 
                //�����𔽓]������ -4�`1�̒l��
                noise = 1 - noise; 
                //-4�`1���Ƃŉ��Z�����Ƃ��ɕςȒl�ɂȂ�̂� 0�`1�ɍi��
                noise = saturate(noise);

                //�����ŁA�t���O�����g�Ɠd���ɐF�����ꂼ���Z���邱�ƂŔC�ӂ̐F�ɂ��邱�Ƃ��ł���
                fixed4 color = (tex2D(_MainTex, i.uv) * _BackGroundColor + noise * _ElectricColor);
                color.a = _Blend;

                return color;
            }
            ENDCG
        }
    }
}
