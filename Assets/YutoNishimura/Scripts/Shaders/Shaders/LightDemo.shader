// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/LightDemo"
{
    Properties{
        _Color("Main Color", Color) = (1,1,1,1)
        _MainTex("Base (RGB) Alpha (A)", 2D) = "white" {}
    }
        SubShader{
            Tags {"Queue" = "Geometry" "RenderType" = "Opaque"}
            Pass {
                Tags {"LightMode" = "ForwardBase"}
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile_fwdbase

                #include "UnityCG.cginc"
                #include "AutoLight.cginc"

                struct vertex_input {
                    float4 vertex : POSITION;
                    float3 normal : NORMAL;
                    float4 texcoord  : TEXCOORD0;
                };

                struct vertex_output {
                    float4 pos : SV_POSITION;
                    float2 uv  : TEXCOORD0;
                    float3 lightDir : TEXCOORD1;
                    float3 normal   : TEXCOORD2;
                    LIGHTING_COORDS(3, 4)
                    float3 vertexLighting : TEXCOORD5;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;
                fixed4 _LightColor0;


                vertex_output vert(vertex_input v) {
                    vertex_output o;

                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.uv = v.texcoord.xy;
                    o.lightDir = ObjSpaceLightDir(v.vertex);
                    o.normal = v.normal;
                    TRANSFER_VERTEX_TO_FRAGMENT(o);

                    return o;
                }

                fixed4 frag(vertex_output i) : COLOR {
                                        i.lightDir = normalize(i.lightDir);
                    fixed atten = LIGHT_ATTENUATION(i);
                    fixed4 tex = tex2D(_MainTex, i.uv);
                    //tex *= _Color;
                    fixed3 normal = i.normal;
                    fixed diff = saturate(dot(normal, i.lightDir));

                    fixed4 c;
                    c.rgb = (tex.rgb * _LightColor0.rgb * diff) * (atten * 2);
                    c.a = tex.a;

                    return c;
                    //i.lightDir = normalize(i.lightDir);
                    //fixed atten = LIGHT_ATTENUATION(i);

                    //fixed4 tex = tex2D(_MainTex, i.uv);
                    //tex *= _Color;// + fixed4(i.vertexLighting, 1.0);

                    //fixed diff = saturate(dot(i.normal, i.lightDir));

                    //fixed4 c;
                    //c.rgb = UNITY_LIGHTMODEL_AMBIENT.rgb * 2 * tex.rgb;
                    //c.rgb = (tex.rgb * _LightColor0.rgb * diff) * (atten * 2);
                    //c.a = tex.a + _LightColor0.a * atten;

                    //return c;
                }

                ENDCG
            }

            Pass {
                Tags {"LightMode" = "ForwardAdd"}
                Blend One One
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile_fwdadd

                #include "UnityCG.cginc"
                #include "AutoLight.cginc"

                struct v2f {
                    float4 pos : SV_POSITION;
                    float2 uv  : TEXCOORD0;
                    float3 lightDir : TEXCOORD2;
                    float3 normal   : TEXCOORD1;
                    LIGHTING_COORDS(3, 4)
                };

                v2f vert(appdata_tan v) {
                    v2f o;

                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.uv = v.texcoord.xy;

                    o.lightDir = ObjSpaceLightDir(v.vertex);

                    o.normal = v.normal;
                    TRANSFER_VERTEX_TO_FRAGMENT(o);

                    return o;
                }

                sampler2D _MainTex;
                fixed4 _Color;
                fixed4 _LightColor0;

                fixed4 frag(v2f i) : COLOR {
                    i.lightDir = normalize(i.lightDir);
                    fixed atten = LIGHT_ATTENUATION(i);
                    fixed4 tex = tex2D(_MainTex, i.uv);
                    tex *= _Color;
                    fixed3 normal = i.normal;
                    fixed diff = saturate(dot(normal, i.lightDir));

                    fixed4 c;
                    c.rgb = (tex.rgb * _LightColor0.rgb * diff) * (atten * 2);
                    c.a = tex.a;

                    return c;
                }

                ENDCG
            }
    }
        Fallback "VertexLit"
}
