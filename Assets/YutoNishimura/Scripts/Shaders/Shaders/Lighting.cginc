#include "UnityCG.cginc"
#include "AutoLight.cginc"
#include "Fog.cginc"

struct vertex_input 
{
    float4 vertex : POSITION;
    float3 normal : NORMAL;
    float4 texcoord  : TEXCOORD0;
};

struct vertex_output 
{
    float4 pos : SV_POSITION;
    float2 uv  : TEXCOORD0;
    float3 lightDir : TEXCOORD1;
    float3 normal   : TEXCOORD2;
    LIGHTING_COORDS(3, 4)
    float3 worldPosition : TEXCOORD5;
};

sampler2D _MainTex;
float4 _MainTex_ST;
fixed4 _LightColor0;
float _FogDensity;
float _FogDensityAttenuation;


vertex_output vert(vertex_input v) {
    vertex_output o;

    o.pos = UnityObjectToClipPos(v.vertex);
    o.uv = v.texcoord.xy;
    o.lightDir = ObjSpaceLightDir(v.vertex);
    o.normal = v.normal;
    float4 worldPos = mul(unity_ObjectToWorld, v.vertex);
    o.worldPosition = worldPos;
    TRANSFER_VERTEX_TO_FRAGMENT(o);

    return o;
}

fixed4 frag(vertex_output i) : COLOR{
    i.lightDir = normalize(i.lightDir);
    fixed atten = LIGHT_ATTENUATION(i);
    fixed4 tex = tex2D(_MainTex, i.uv);
    fixed3 normal = i.normal;
    fixed diff = saturate(dot(normal, i.lightDir));

    fixed4 c;
    c.rgb = (tex.rgb * _LightColor0.rgb * diff) * (atten);
    c.a = tex.a;

    float fog = GetForHeightFogParameter(i.worldPosition, _WorldSpaceCameraPos, _FogDensity, _FogDensityAttenuation);
    //取得した媒介変数をもとにフォグの色情報とフラグメントの色情報を補間
    c.xyz = lerp(unity_FogColor.xyz, c.xyz, fog);

    return c;
}