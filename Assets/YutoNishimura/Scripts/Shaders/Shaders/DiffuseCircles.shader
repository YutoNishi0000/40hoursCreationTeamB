Shader "Custom/DiffuseCircles"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _CircleColor1("CircleColor1", Color) = (0, 0, 0, 0)
        _CircleColor2("CircleColor2", Color) = (0, 0, 0, 0)
        _CircleColor3("CircleColor3", Color) = (0, 0, 0, 0)
        _DiffuseSpeed("DiffuseSpeed", float) = 1
        _Cycle("Cycle", float) = 1
        _CenterPosX("CenterPosX", float) = 0.5
        _CenterPosY("CenterPosY", float) = 0.5
        _ColorRate("ColorRate", float) = 0.9
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float4 _CircleColor1;
        float4 _CircleColor2;
        float4 _CircleColor3;
        float _DiffuseSpeed;
        float _Cycle;
        float _CenterPosX;
        float _CenterPosY;
        float _ColorRate;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            //正弦波の式を落とし込む
            float dist = (IN.uv_MainTex.x - _CenterPosX) * (IN.uv_MainTex.x - _CenterPosX) + (IN.uv_MainTex.y - _CenterPosY) * (IN.uv_MainTex.y - _CenterPosY);
            //ふり幅は今回は必要がないため１にする
            float height = sin((2 * 3.1415926535897) / _Cycle * (_Time.y - (dist / _DiffuseSpeed)));

            if (abs(height) >= _ColorRate)
            {
                //heightの絶対値が0.8よりも大きかったら黒色を返す
                o.Albedo = tex2D(_MainTex, IN.uv_MainTex) * _CircleColor1;
            }
            else
            {
                if (height >= 0)
                {
                    //０より大きかったら赤色を返す
                    o.Albedo = tex2D(_MainTex, IN.uv_MainTex) * _CircleColor2;
                }
                else
                {
                    //０より小さかったら青色を返す
                    o.Albedo = tex2D(_MainTex, IN.uv_MainTex) * _CircleColor3;
                }
            }



            //// Albedo comes from a texture tinted by color
            //fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            //o.Albedo = c.rgb;
            //// Metallic and smoothness come from slider variables
            //o.Metallic = _Metallic;
            //o.Smoothness = _Glossiness;
            //o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
