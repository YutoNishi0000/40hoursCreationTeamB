Shader "Custom/GaussianBlur"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Dispersion("Dispersion", float) = 1 //���U��𒲐�
        _SamplingTexelAmount("Sampling Texel Amount", int) = 1 //����̃e�N�Z���܂ŃT���v�����O���邩
        _TexelInterval("Texel Interval", float) = 2 //�T���v�����O����e�N�Z���̊Ԋu
    }

    SubShader
    {
        Cull Off        // �J�����O�͕s�v
        ZTest Always    // ZTest�͏�ɒʂ�
        ZWrite Off      // ZWrite�͕s�v

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
            // x : 1.0 / ��
            // y : 1.0 / ����
            // z : ��
            // w : ����
            float2 _MainTex_TexelSize; //�e�N�Z���T�C�Y

            float2 _Direction; //C#����n�����u���[������������̕ϐ�
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
                float2 dir = _Direction * _MainTex_TexelSize.xy; //�T���v�����O�̕���������

                //�E�F�C�g�𓮓I�ɓ��o����ꍇ
                fixed4 color = 0;
                for (int j = 0; j < _SamplingTexelAmount; j++)
                {
                    float2 offset = dir * ((j + 1) * _TexelInterval - 1); //_TexelInterval�ŃT���v�����O�����𒲐�
                    float weight = GetGaussianWeight(j + 1);              //�E�F�C�g���v�Z
                    color.rgb += tex2D(_MainTex, i.uv + offset) * weight; //���������T���v�����O���d�݂Â����ĉ��Z
                    color.rgb += tex2D(_MainTex, i.uv - offset) * weight; //�t�������T���v�����O���d�݂Â����ĉ��Z
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
