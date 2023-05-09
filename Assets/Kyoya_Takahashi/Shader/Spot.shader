Shader "Custom/Spot"
{
    Properties
    {
    }
        SubShader
    {
        //透明度に関する設定
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
                o.worldPos = mul(unity_ObjectToWorld, v.vertex); //ローカル座標系をワールド座標系に変換
                return o;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                fixed4 col;
            fixed4 baseCol = fixed4(0, 0, 0, 0);
            fixed4 spotCol = fixed4(1, 1, 1, 1);
            
                col = fixed4(lerp(baseCol, sonarCol, 0.5));

            //Alphaが0以下なら描画しない
            clip(col);
            //最終的なピクセルの色を返す
            return col;
            }
            ENDCG
        }
    }
}
