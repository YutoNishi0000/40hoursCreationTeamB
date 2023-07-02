Shader "Custom/PicturedTarget"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _CircleColor1("CircleColor1", Color) = (0, 0, 0, 0)
        _CircleColor2("CircleColor2", Color) = (0, 0, 0, 0)
        _CircleColor3("CircleColor3", Color) = (0, 0, 0, 0)
        _DiffuseSpeed("DiffuseSpeed", float) = 1
        _Cycle("Cycle", float) = 1
        _CenterPosX("CenterPosX", float) = 0.5
        _CenterPosY("CenterPosY", float) = 0.5
        _ColorRate("ColorRate", float) = 0.9
        _ColorBlend("ColorBlend", Range(0, 1)) = 0.3

        _SinWave("SinWave", int) = 20
        _SinWidth("SinWidth", Range(0, 0.1)) = 0.05
        _SinSpeed("SinSpeed", int) = 4
        _SinStrength("SinStrength", float) = 10       //�c�݂̋���

        _Trigger("Trigger", Range(0, 1)) = 0          //���̃V�F�[�_�[�𔭓����邽�߂̃v���p�e�B
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always
        Tags { "RenderType" = "Background" "Queue" = "Background"}
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
                float3 worldPosition : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.worldPosition = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            sampler2D _MainTex;
            float4 _CircleColor1;  //��F��
            float4 _CircleColor2;  //��F��
            float4 _CircleColor3;  //�O�F��
            float _DiffuseSpeed;  //�g�U�X�s�[�h
            float _Cycle;        //����
            float _CenterPosX;   //��ʂ̒��SX���W
            float _CenterPosY;   //��ʂ̒��SY���W
            float _ColorRate;    //�����̑���
            float _ColorBlend;   //�J���[�u�����h�̊���

            float _SinWave;        //�E�F�[�u�̏c�̊Ԋu
            float _SinWidth;       //�h�ꕝ
            float _SinSpeed;       //�h��鑬�x
            float _SinStrength;    //�c�݂̋���
            float _Trigger;

            fixed4 frag(v2f i) : SV_Target
            {
                //uv�̒��S���W����̋������擾��_Time * DiffuseSpeed���������Ƃ�sin�g�̐i�ޕ������O���ɂȂ�
                //�����āAsin�g��uv��x���W�Ay���W�ɉ��Z���邱�Ƃł䂪��Ō�����悤�ɂȂ�
                fixed len = (distance(i.uv, fixed2(_CenterPosX, _CenterPosY)) - _Time * _DiffuseSpeed) + sin((i.uv.y + i.uv.x) * _SinStrength) * _SinWidth;
                float height = sin(len * _Cycle);
                //�㎮��sin()��len�Ŕ��������l
                float differential = cos(len * _Cycle);
                fixed4 finalColor = fixed4(1, 1, 1, 1);

                //���߂������̐�Βl���w�肵���������傫��������
                if (abs(height) >= _ColorRate)
                {
                    finalColor = _CircleColor1;
                }
                else
                {
                    //���������l�isin�֐��̌X���j��0�ȏォ0�ȉ����Ŕ��ʂ��邱�ƂŌ��݂ɐF���o�͂����
                    if (differential >= 0)
                    {
                        finalColor = _CircleColor2;
                    }
                    else
                    {
                        //x�������ɂ䂪�܂���i�����sin�֐����g���j
                        float mysin = sin(i.uv.y * _SinWave + _Time.y * _SinSpeed) * _SinWidth;
                        fixed4 color = tex2D(_MainTex, i.uv + float2(mysin, 0));
                        //�J�����Ɏʂ��Ă�����Ǝw�肵���F�Ńu�����h����
                        finalColor = color * (1 - _ColorBlend) + _CircleColor3 * _ColorBlend;
                    }
                }

                finalColor *= _Trigger;
                return finalColor;
            }
            ENDCG
        }
    }
}
