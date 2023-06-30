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
            float4 _CircleColor1;
            float4 _CircleColor2;
            float4 _CircleColor3;
            float _DiffuseSpeed;
            float _Cycle;
            float _CenterPosX;
            float _CenterPosY;
            float _ColorRate;
            float _ColorBlend;

            fixed4 frag(v2f i) : SV_Target
            {
                //uvの中心座標からの距離を取得し_Time * DiffuseSpeedを引くことでsin波の進む方向が外側になる
                //そして、sin波をuvのx座標、y座標に加算することでゆがんで見えるようになる
                fixed len = (distance(i.uv, fixed2(_CenterPosX, _CenterPosY)) - _Time * _DiffuseSpeed) + sin((i.uv.y + i.uv.x) * 10) * 0.05;
                float height = sin(len * _Cycle);
                //上式のsin()をlenで微分した値
                float differential = cos(len * _Cycle);

                if (abs(height) >= _ColorRate)
                {
                    return _CircleColor1;
                }
                else
                {
                    //微分した値が0以上か0以下かで判別することで交互に色が出力される
                    if (differential >= 0)
                    {
                        return _CircleColor2;
                    }
                    else
                    {
                        //カメラに写っている情報と指定した色でブレンドする
                        return tex2D(_MainTex, i.uv + sin(i.uv.y * _Time.y * 10) * 0.05) * (1 - _ColorBlend) + _CircleColor3 * _ColorBlend;
                    }
                }
            }
            ENDCG
        }
    }
}
