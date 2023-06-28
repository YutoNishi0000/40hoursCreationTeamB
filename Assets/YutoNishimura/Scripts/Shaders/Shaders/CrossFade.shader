Shader "Custom/CrossFade"
{
    Properties
    {
        _Texture1("Texture1", 2D) = "white" {}
        _Texture2("Texture2", 2D) = "white" {}
        _Blend("Blend", Range(0, 1)) = 0
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

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

            sampler2D _Texture1;
            sampler2D _Texture2;
            float _Blend;
            float4 _Texture1_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _Texture1);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //一つ目のフラグメントを取得
                fixed4 main = tex2D(_Texture1, i.uv);
                //二つ目のフラグメントを取得
                fixed4 sub = tex2D(_Texture2, i.uv);
                //クロスフェード
                fixed4 col = main * (1 - _Blend) + sub * _Blend;
                return col;
            }
            ENDCG
        }
    }
}
