Shader "Custom/FogFlagmentForHightShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FogDensity("FogDensity", float) = 1.0
        _FogDensityAttenuation("FogDensityAttenuation", float) = 1.0
        _FogHeight("FogHeight", float) = 3.0
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

            #include "UnityCG.cginc"
            #include "Fog.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float3 worldPosition : TEXCOORD2;
                float4 vertex : SV_POSITION;
            };

            float _FogDensity;
            float _FogDensityAttenuation;
            float _FogHeight;
            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldPosition = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float fog = GetForHeightFogParameter(i.worldPosition, _WorldSpaceCameraPos, _FogDensity, _FogDensityAttenuation);
                fixed4 col = tex2D(_MainTex, i.uv);
                col.xyz = lerp(unity_FogColor.xyz, col.xyz, fog);
                return col;
            }
            ENDCG
        }
    }
}
