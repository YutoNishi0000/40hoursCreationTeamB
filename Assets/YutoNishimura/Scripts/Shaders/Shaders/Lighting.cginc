#include "UnityCG.cginc"
#include "Fog.cginc"

struct appdata
{
    float4 vertex : POSITION;
    float2 uv : TEXCOORD0;
    half3 normal : NORMAL;
};

struct v2f
{
    float2 uv : TEXCOORD0;
    half3 normal : NORMAL;
    float3 viewDir : TEXCOORD1;
    float3 lightDir : TEXCOORD2;
    float4 vertex : SV_POSITION;
    float3 worldPosition : TEXCOORD3;
};

half _SpecularPow;
sampler2D _MainTex;
float4 _MainTex_ST;
// ���C�g�̐F���擾����
half4 _LightColor0;
float _FogDensity;
float _FogDensityAttenuation;

v2f vert(appdata v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);
    float4 worldPos = mul(unity_ObjectToWorld, v.vertex);
    o.worldPosition = worldPos;
    o.viewDir = normalize(_WorldSpaceCameraPos - worldPos.xyz);
    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
    float isDirectional = step(1, _WorldSpaceLightPos0.w);
    o.lightDir = normalize(_WorldSpaceLightPos0.xyz - (worldPos.xyz * isDirectional));

    o.normal = UnityObjectToWorldNormal(v.normal);
    return o;
}

fixed4 frag(v2f i) : SV_Target
{
    fixed4 texCol = tex2D(_MainTex, i.uv);
    fixed4 finalColor = (1, 1, 1, 1);

    // �g�U����
    float3 diffuse = saturate(dot(i.normal, i.lightDir)) * _LightColor0;
    // ����
    float3 ambient = ShadeSH9(float4(i.normal, 1));

    // _WorldSpaceLightPos0.w�̓f�B���N�V���i�����C�g��������0�A����ȊO��1�ƂȂ�̂ł��ꂼ��̏ꍇ�ɉ������F��Ԃ�
    if (_WorldSpaceLightPos0.w > 0)
    {
        float3 lightCol = max(normalize(dot(i.normal, i.lightDir)) * _LightColor0, 0);

        finalColor = fixed4(texCol.rgb * lightCol, 1);
    }
    else
    {
        finalColor = fixed4(texCol.rgb * (ambient + diffuse), texCol.a);
    }

    float fog = GetForHeightFogParameter(i.worldPosition, _WorldSpaceCameraPos, _FogDensity, _FogDensityAttenuation);
    //�擾�����}��ϐ������ƂɃt�H�O�̐F���ƃt���O�����g�̐F������
    finalColor.xyz = lerp(unity_FogColor.xyz, finalColor.xyz, fog);
    return finalColor;
}