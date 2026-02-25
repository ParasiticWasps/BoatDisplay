Shader "Custom/StandardWithAlphaScale"
{
    Properties
    {
        // 主颜色和纹理
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB) Alpha (A)", 2D) = "white" {}

        // 透明裁剪阈值
        _Cutoff("Alpha Cutoff", Range(0.0, 1.0)) = 0.5

        // 光滑度和金属度
        _Glossiness("Smoothness", Range(0.0, 1.0)) = 0.5
        _GlossMapScale("Smoothness Factor", Range(0.0, 1.0)) = 1.0
        [Enum(Metallic Alpha,0,Albedo Alpha,1)] _SmoothnessTextureChannel ("Smoothness texture channel", Float) = 0

        _Metallic("Metallic", Range(0.0, 1.0)) = 0.0
        _MetallicGlossMap("Metallic", 2D) = "white" {}

        // 高光和反射开关
        [ToggleOff] _SpecularHighlights("Specular Highlights", Float) = 1.0
        [ToggleOff] _GlossyReflections("Glossy Reflections", Float) = 1.0

        // 法线贴图
        _BumpScale("Normal Scale", Float) = 1.0
        [Normal] _BumpMap("Normal Map", 2D) = "bump" {}

        // 视差贴图
        _Parallax ("Height Scale", Range (0.005, 0.08)) = 0.02
        _ParallaxMap ("Height Map", 2D) = "black" {}

        // 环境遮挡
        _OcclusionStrength("Occlusion Strength", Range(0.0, 1.0)) = 1.0
        _OcclusionMap("Occlusion", 2D) = "white" {}

        // 自发光
        _EmissionColor("Emission Color", Color) = (0,0,0)
        _EmissionMap("Emission", 2D) = "white" {}

        // 细节贴图
        _DetailMask("Detail Mask", 2D) = "white" {}
        _DetailAlbedoMap("Detail Albedo x2", 2D) = "grey" {}
        _DetailNormalMapScale("Detail Normal Scale", Float) = 1.0
        [Normal] _DetailNormalMap("Detail Normal Map", 2D) = "bump" {}

        // UV 选择（用于细节贴图）
        [Enum(UV0,0,UV1,1)] _UVSec ("UV Set for secondary textures", Float) = 0

        // 透明度控制（新增）
        _AlphaScale("Global Alpha Scale", Range(0, 1)) = 1.0

        // 渲染模式控制（隐藏属性，由 StandardShaderGUI 管理）
        [HideInInspector] _Mode ("__mode", Float) = 0.0
        [HideInInspector] _SrcBlend ("__src", Float) = 1.0
        [HideInInspector] _DstBlend ("__dst", Float) = 0.0
        [HideInInspector] _ZWrite ("__zw", Float) = 1.0
        [HideInInspector] _ZTest ("__zt", Float) = 4.0
        [HideInInspector] _Cull ("__cull", Float) = 2.0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" "PerformanceChecks"="False" }
        LOD 300

        // 混合和深度设置从属性中获取，由 StandardShaderGUI 根据 _Mode 更新
        Blend [_SrcBlend] [_DstBlend]
        ZWrite [_ZWrite]
        ZTest [_ZTest]
        Cull [_Cull]

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows alpha:fade
        #pragma target 3.0

        // 声明所有贴图变量
        sampler2D _MainTex;
        sampler2D _MetallicGlossMap;
        sampler2D _BumpMap;
        sampler2D _ParallaxMap;
        sampler2D _OcclusionMap;
        sampler2D _EmissionMap;
        sampler2D _DetailMask;
        sampler2D _DetailAlbedoMap;
        sampler2D _DetailNormalMap;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpMap;
            float2 uv_ParallaxMap;
            float2 uv_OcclusionMap;
            float2 uv_EmissionMap;
            float2 uv_DetailMask;
            float2 uv_DetailAlbedoMap;
            float2 uv_DetailNormalMap;
        };

        // 声明所有属性变量（包括未使用的，以避免编译警告）
        half _Glossiness;
        half _GlossMapScale;
        half _Metallic;
        fixed4 _Color;
        half _BumpScale;
        half _Parallax;
        half _OcclusionStrength;
        fixed4 _EmissionColor;
        half _DetailNormalMapScale;
        half _AlphaScale; // 新增透明度控制

        // 以下变量虽未在 surf 中直接使用，但必须声明以消除警告
        float _Cutoff;
        float _SmoothnessTextureChannel;
        float _SpecularHighlights;
        float _GlossyReflections;
        float _UVSec;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // 基础颜色和Alpha
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;

            // 金属性与光滑度（简化实现，未使用贴图）
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;

            // 法线贴图（如果有关键字 _NORMALMAP，则应用）
            #ifdef _NORMALMAP
                o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap)) * _BumpScale;
            #endif

            // 环境遮挡（如果有关键字 _OCCLUSIONMAP）
            #ifdef _OCCLUSIONMAP
                o.Occlusion = tex2D(_OcclusionMap, IN.uv_OcclusionMap).g * _OcclusionStrength;
            #else
                o.Occlusion = 1.0;
            #endif

            // 自发光（如果有关键字 _EMISSION）
            #ifdef _EMISSION
                o.Emission = tex2D(_EmissionMap, IN.uv_EmissionMap).rgb * _EmissionColor.rgb;
            #endif

            // 最终透明度 = 纹理Alpha × 颜色Alpha × 全局系数
            o.Alpha = c.a * _AlphaScale;
        }
        ENDCG
    }
    FallBack "Diffuse"
    CustomEditor "StandardShaderGUI"
}