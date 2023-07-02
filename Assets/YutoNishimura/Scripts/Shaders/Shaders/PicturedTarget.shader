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
        _SinStrength("SinStrength", float) = 10       //歪みの強さ

        _Trigger("Trigger", Range(0, 1)) = 0          //このシェーダーを発動するためのプロパティ
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
            float4 _CircleColor1;  //一色目
            float4 _CircleColor2;  //二色目
            float4 _CircleColor3;  //三色目
            float _DiffuseSpeed;  //拡散スピード
            float _Cycle;        //周期
            float _CenterPosX;   //画面の中心X座標
            float _CenterPosY;   //画面の中心Y座標
            float _ColorRate;    //黒線の太さ
            float _ColorBlend;   //カラーブレンドの割合

            float _SinWave;        //ウェーブの縦の間隔
            float _SinWidth;       //揺れ幅
            float _SinSpeed;       //揺れる速度
            float _SinStrength;    //歪みの強さ
            float _Trigger;

            fixed4 frag(v2f i) : SV_Target
            {
                //uvの中心座標からの距離を取得し_Time * DiffuseSpeedを引くことでsin波の進む方向が外側になる
                //そして、sin波をuvのx座標、y座標に加算することでゆがんで見えるようになる
                fixed len = (distance(i.uv, fixed2(_CenterPosX, _CenterPosY)) - _Time * _DiffuseSpeed) + sin((i.uv.y + i.uv.x) * _SinStrength) * _SinWidth;
                float height = sin(len * _Cycle);
                //上式のsin()をlenで微分した値
                float differential = cos(len * _Cycle);
                fixed4 finalColor = fixed4(1, 1, 1, 1);

                //求めた高さの絶対値が指定した高さより大きかったら
                if (abs(height) >= _ColorRate)
                {
                    finalColor = _CircleColor1;
                }
                else
                {
                    //微分した値（sin関数の傾き）が0以上か0以下かで判別することで交互に色が出力される
                    if (differential >= 0)
                    {
                        finalColor = _CircleColor2;
                    }
                    else
                    {
                        //x軸方向にゆがませる（今回はsin関数を使う）
                        float mysin = sin(i.uv.y * _SinWave + _Time.y * _SinSpeed) * _SinWidth;
                        fixed4 color = tex2D(_MainTex, i.uv + float2(mysin, 0));
                        //カメラに写っている情報と指定した色でブレンドする
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
