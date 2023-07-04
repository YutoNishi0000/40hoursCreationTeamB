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