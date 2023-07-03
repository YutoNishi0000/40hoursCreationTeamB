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
                o.normal = mul(unity_ObjectToWorld, v.normal);//各頂点が持つ法線（オブジェクト座標系）をワールド座標系に変換
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float3 ligDirection = normalize(i.worldPos - _WorldSpaceLightPos0.xyz); //フラグメントのワールド座標から見たディレクショナルライトの方向ベクトルを取得
                fixed3 ligColor = _LightColor0.xyz; //ディレクショナルライトのカラーを取得
                float3 refVec = normalize(reflect(-ligDirection, i.normal)); //ライト方向と法線方向から正規化された反射クトルを計算
                float3 toEye = normalize(_WorldSpaceCameraPos - i.worldPos); //カメラからの正規化された視線ベクトルを計算
                float t = dot(refVec, toEye); //反射ベクトルと視線ベクトルで内積を計算
                t = pow(max(0, t), _ReflectionLevel); //反射の絞りを調整
                float3 specularLig = ligColor * t; //内積が１に近いほど照り返しが強いとみなし、ライトカラーを強く乗算
                float4 finalColor = tex2D(_MainTex, i.uv);
                finalColor += float4(specularLig, 1);
                return finalColor;
            }
            ENDCG
        }
    }
}
