Shader "Unlit/PhotorealisticShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ReflectionLevel("Reflection Level", Range(0, 100)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
                float4 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _ReflectionLevel;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldPos = v.vertex;
                o.normal = mul(unity_ObjectToWorld, v.normal);//�e���_�����@���i�I�u�W�F�N�g���W�n�j�����[���h���W�n�ɕϊ�
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 ligDirection = normalize(i.worldPos - _WorldSpaceLightPos0.xyz); //�t���O�����g�̃��[���h���W���猩���f�B���N�V���i�����C�g�̕����x�N�g�����擾
                fixed3 ligColor = _LightColor0.xyz; //�f�B���N�V���i�����C�g�̃J���[���擾
                float3 refVec = normalize(reflect(-ligDirection, i.normal)); //���C�g�����Ɩ@���������琳�K�����ꂽ���˃N�g�����v�Z
                float3 toEye = normalize(_WorldSpaceCameraPos - i.worldPos); //�J��������̐��K�����ꂽ�����x�N�g�����v�Z
                float t = dot(refVec, toEye); //���˃x�N�g���Ǝ����x�N�g���œ��ς��v�Z
                t = pow(max(0, t), _ReflectionLevel); //���˂̍i��𒲐�
                float3 specularLig = ligColor * t; //���ς��P�ɋ߂��قǏƂ�Ԃ��������Ƃ݂Ȃ��A���C�g�J���[��������Z
                float4 finalColor = tex2D(_MainTex, i.uv);
                finalColor += float4(specularLig, 1);
                return finalColor;
            }
            ENDCG
        }
    }
}
