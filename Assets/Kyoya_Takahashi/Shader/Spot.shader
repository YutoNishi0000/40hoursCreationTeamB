Shader "Custom/Spot"
{
    Properties
    {
    }
        SubShader
    {
        //�����x�Ɋւ���ݒ�
       Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        Pass
        {
            CGPROGRAM
            // Upgrade NOTE: excluded shader from DX11, OpenGL ES 2.0 because it uses unsized arrays
            #pragma exclude_renderers d3d11 gles
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"


            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 worldPos : WORLD_POS;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.uv = v.uv;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex); //���[�J�����W�n�����[���h���W�n�ɕϊ�
                return o;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                fixed4 col;
            fixed4 baseCol = fixed4(0, 0, 0, 0);
            fixed4 spotCol = fixed4(1, 1, 1, 1);
            
                col = fixed4(lerp(baseCol, sonarCol, 0.5));

            //Alpha��0�ȉ��Ȃ�`�悵�Ȃ�
            clip(col);
            //�ŏI�I�ȃs�N�Z���̐F��Ԃ�
            return col;
            }
            ENDCG
        }
    }
}
