Shader "Custom/FieldShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _FogDensity("FogDensity", float) = 1.0
    _FogDensityAttenuation("FogDensityAttenuation", float) = 1.0
    _FogHeight("FogHeight", float) = 3.0
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }

        //                    Pass
        //{
        //    CGPROGRAM
        //    #pragma vertex vert
        //    #pragma fragment frag

        //    #include "UnityCG.cginc"
        //    #include "Fog.cginc"

        //    struct appdata
        //    {
        //        float4 vertex : POSITION;
        //        float2 uv : TEXCOORD0;
        //    };

        //    struct v2f
        //    {
        //        float2 uv : TEXCOORD0;
        //        float3 worldPosition : TEXCOORD2;
        //        float4 vertex : SV_POSITION;
        //    };

        //    float _FogDensity;
        //    float _FogDensityAttenuation;
        //    float _FogHeight;
        //    //sampler2D _GrabTex;
        //    //float4 _GrabTex_ST;
        //    sampler2D _MainTex;
        //    float4 _MainTex_ST;

        //    v2f vert(appdata v)
        //    {
        //        v2f o;
        //        o.vertex = UnityObjectToClipPos(v.vertex);
        //        o.uv = TRANSFORM_TEX(v.uv, _MainTex);
        //        o.worldPosition = mul(unity_ObjectToWorld, v.vertex);
        //        return o;
        //    }

        //    fixed4 frag(v2f i) : SV_Target
        //    {
        //        //霧を考慮した媒介変数を取得
        //        float fog = GetForHeightFogParameter(i.worldPosition, _WorldSpaceCameraPos, _FogDensity, _FogDensityAttenuation);
        //        fixed4 col = tex2D(_MainTex, i.uv);
        //        //取得した媒介変数をもとにフォグの色情報とフラグメントの色情報を補間
        //        col.xyz = lerp(unity_FogColor.xyz, col.xyz, fog);
        //        return col;
        //    }
        //    ENDCG
        //}

        //            GrabPass{}

        Pass
        {
            Tags { "LightMode" = "ForwardBase" }

            CGPROGRAM
           #pragma vertex vert
           #pragma fragment frag
           #pragma multi_compile_fwdbase

           #include "Lighting.cginc"
            ENDCG
        }
        Pass 
        {
            Tags { "LightMode" = "ForwardAdd" }

            Blend One One
            ZWrite Off

            CGPROGRAM
           #pragma vertex vert
           #pragma fragment frag
           #pragma multi_compile_fwdadd

           #include "Lighting.cginc"

            ENDCG
        }
    }
}




    //Properties
    //{
    //    _MainTex("Texture", 2D) = "white" {}
    //    _SpecularPow("Speclar Pow", float) = 10
    //    _FogDensity("FogDensity", float) = 1.0
    //    _FogDensityAttenuation("FogDensityAttenuation", float) = 1.0
    //    _FogHeight("FogHeight", float) = 3.0
    //}

    //SubShader
    //{
    //    Tags { "RenderType" = "Opaque"}
    //    LOD 100

    //    CGINCLUDE

    //    sampler2D _MainTex;
    //    float4 _MainTex_ST;
    //    // ライトの色を取得する
    //    half4 _LightColor0;

    //    #pragma vertex vert
    //    #pragma fragment frag

    //    #include "UnityCG.cginc"
    //    #include "Fog.cginc"

    //    struct appdata
    //    {
    //        float4 vertex : POSITION;
    //        float2 uv : TEXCOORD0;
    //        half3 normal : NORMAL;
    //    };

    //    ENDCG

    //    Pass
    //    {
    //        Tags { "LightMode" = "ForwardBase" }

    //        CGPROGRAM

    //        struct v2f
    //        {
    //            float2 uv : TEXCOORD0;
    //            half3 normal : NORMAL;
    //            float3 viewDir : TEXCOORD1;
    //            float3 lightDir : TEXCOORD2;
    //            float4 vertex : SV_POSITION;
    //            float3 worldPosition : TEXCOORD3;
    //        };

    //        half _SpecularPow;
    //        float _FogDensity;
    //        float _FogDensityAttenuation;
    //        float _FogHeight;

    //        v2f vert(appdata v)
    //        {
    //            v2f o;
    //            o.vertex = UnityObjectToClipPos(v.vertex);
    //            float4 worldPos = mul(unity_ObjectToWorld, v.vertex);
    //            o.viewDir = normalize(_WorldSpaceCameraPos - worldPos.xyz);
    //            o.uv = TRANSFORM_TEX(v.uv, _MainTex);
    //            float isDirectional = step(1, _WorldSpaceLightPos0.w);
    //            o.lightDir = normalize(_WorldSpaceLightPos0.xyz - (worldPos.xyz * isDirectional));
    //            o.worldPosition = mul(unity_ObjectToWorld, v.vertex);
    //            o.normal = UnityObjectToWorldNormal(v.normal);
    //            return o;
    //        }

    //        fixed4 frag(v2f i) : SV_Target
    //        {
    //            fixed4 texCol = tex2D(_MainTex, i.uv);

    //            // 拡散反射
    //            float3 diffuse = saturate(dot(i.normal, i.lightDir)) * _LightColor0;

    //            // 鏡面反射
    //            float3 reflectVec = reflect(-i.lightDir, i.normal);
    //            float specular = pow(saturate(dot(reflectVec, i.viewDir)), _SpecularPow);

    //            // 環境光
    //            float3 ambient = ShadeSH9(float4(i.normal, 1));

    //            fixed4 col = fixed4(texCol.rgb * (ambient + diffuse)/* + specular*/, texCol.a);
    //            //霧を考慮した媒介変数を取得
    //            float fog = GetForHeightFogParameter(i.worldPosition, _WorldSpaceCameraPos, _FogDensity, _FogDensityAttenuation);
    //            //取得した媒介変数をもとにフォグの色情報とフラグメントの色情報を補間
    //            col.xyz = lerp(unity_FogColor.xyz, col.xyz, fog);
    //            return col;
    //        }

    //        ENDCG
    //    }
    //}
