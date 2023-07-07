//テクスチャの色を変更したいときに使用するシェーダー
Shader "Custom/BlendColor"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}                   //ブレンドしたいテクスチャ
        _BlendColor("BlendColor", Color) = (0, 1, 1, 1)         //ブレンドしたいい色
        _Trigger("Trigger", Range(0, 1)) = 0                          //ブレンド割合
    }
    SubShader
    {
        Tags {"Queue" = "Transparent"}

        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _BlendColor;
            float _Trigger;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                col.rgb = lerp(col.rgb, _BlendColor.rgb, _Trigger);
                return col;
            }
            ENDCG
        }
    }
}
