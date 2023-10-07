using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Blz_CharactorPlayer_GUI  : ShaderGUI
{
    private static class Styles
    {
        public static GUIContent uvSetLabel = EditorGUIUtility.TrTextContent("UV Set");

        public static GUIContent albedoText = EditorGUIUtility.TrTextContent("主贴图", "漫反射主贴图");
        public static GUIContent lightMapText = EditorGUIUtility.TrTextContent("光信息贴图", "光信息贴图(r:强反光部分 g:毛发部分 b:弱泛光部分)");
        public static GUIContent emissMapText = EditorGUIUtility.TrTextContent("自发光贴图", "自发光贴图(r:自发光部分 g:内部扰动 b:外部扰动)");
        public static GUIContent strandNoiseMapText = EditorGUIUtility.TrTextContent("毛发反光贴图");

        public static GUIContent alphaCutoffText = EditorGUIUtility.TrTextContent("Alpha Cutoff", "Threshold for alpha cutoff");
        public static GUIContent specularMapText = EditorGUIUtility.TrTextContent("Specular", "Specular (RGB) and Smoothness (A)");
        public static GUIContent metallicMapText = EditorGUIUtility.TrTextContent("Metallic", "Metallic (R) and Smoothness (A)");
        public static GUIContent smoothnessText = EditorGUIUtility.TrTextContent("Smoothness", "Smoothness value");
        public static GUIContent smoothnessScaleText = EditorGUIUtility.TrTextContent("Smoothness", "Smoothness scale factor");
        public static GUIContent smoothnessMapChannelText = EditorGUIUtility.TrTextContent("Source", "Smoothness texture and channel");
        public static GUIContent highlightsText = EditorGUIUtility.TrTextContent("Specular Highlights", "Specular Highlights");
        public static GUIContent reflectionsText = EditorGUIUtility.TrTextContent("Reflections", "Glossy Reflections");
        public static GUIContent normalMapText = EditorGUIUtility.TrTextContent("Normal Map", "Normal Map");
        public static GUIContent heightMapText = EditorGUIUtility.TrTextContent("Height Map", "Height Map (G)");
        public static GUIContent occlusionText = EditorGUIUtility.TrTextContent("Occlusion", "Occlusion (G)");
        public static GUIContent emissionText = EditorGUIUtility.TrTextContent("Color", "Emission (RGB)");
        public static GUIContent detailMaskText = EditorGUIUtility.TrTextContent("Detail Mask", "Mask for Secondary Maps (A)");
        public static GUIContent detailAlbedoText = EditorGUIUtility.TrTextContent("Detail Albedo x2", "Albedo (RGB) multiplied by 2");
        public static GUIContent detailNormalMapText = EditorGUIUtility.TrTextContent("Normal Map", "Normal Map");

        public static string primaryMapsText = "Main Maps";
        public static string secondaryMapsText = "Secondary Maps";
        public static string forwardText = "Forward Rendering Options";
        public static string renderingMode = "Rendering Mode";
        public static string advancedText = "Advanced Options";
    }

    MaterialProperty mainTex = null;
    MaterialProperty baseColor = null;
    MaterialProperty outlineColor = null;
    MaterialProperty outlineLength = null;
    MaterialProperty outlineLightness = null;
    MaterialProperty shadowThreshold = null;
    MaterialProperty shadowColor = null;
    MaterialProperty lightMap = null;
    MaterialProperty lightMapRChannel = null;
    MaterialProperty specular1Shininess = null;
    MaterialProperty specular1Color = null;
    MaterialProperty specular1Multi = null;
    MaterialProperty lightMapGChannel = null;
    MaterialProperty strandNoiseMap = null;
    MaterialProperty strandFirstOff = null;
    MaterialProperty strandFirstSmooth = null;
    MaterialProperty strandSpecularColor = null;
    MaterialProperty strandSpecularStrength = null;
    MaterialProperty lightMapBChannel = null;
    MaterialProperty specular2Color = null;
    MaterialProperty specular2Multi = null;
    MaterialProperty emissMap = null;
    MaterialProperty emissColor = null;
    MaterialProperty emissSpeedX = null;
    MaterialProperty emissSpeedY = null;
    MaterialProperty emissNoiseFactor = null;
    MaterialProperty emissNoiseStrength = null;
    MaterialProperty emissBreathSpeed = null;
    MaterialProperty flashColor = null;
    MaterialProperty flashIntensity = null;

    MaterialEditor m_MaterialEditor;
    bool m_FirstTimeApply = true;

    bool mainTex_Foldout = true;
    bool lightMap_Foldout = true;
    bool emissMap_Foldout = true;
    bool outline_Foldout = true;
    bool shadow_Foldout = true;
    bool other_Foldout = true;

    void FindProperties(MaterialProperty[] properties)
    {
        mainTex = FindProperty("_MainTex", properties);
        baseColor = FindProperty("_Color", properties);
        outlineColor = FindProperty("_OutlineColor", properties);
        outlineLength = FindProperty("_OutlineLength", properties);
        outlineLightness = FindProperty("_OutlineLightness", properties);
        shadowThreshold = FindProperty("_ShadowThreshold", properties);
        shadowColor = FindProperty("_ShadowColor", properties);
        lightMap = FindProperty("_LightMap", properties);
        lightMapRChannel = FindProperty("_LightMapRChannel", properties);
        specular1Shininess = FindProperty("_Shininess", properties);
        specular1Color = FindProperty("_SpecularColor", properties);
        specular1Multi = FindProperty("_SpecularMulti", properties);
        lightMapGChannel = FindProperty("_LightMapGChannel", properties);
        strandNoiseMap = FindProperty("_StrandNoiseMap", properties, false);
        strandFirstOff = FindProperty("_StrandFirstOff", properties, false);
        strandFirstSmooth = FindProperty("_StrandFirstSmooth", properties, false);
        strandSpecularColor = FindProperty("_StrandSpecularColor", properties, false);
        strandSpecularStrength = FindProperty("_StrandSpecularStrength", properties, false);
        lightMapBChannel = FindProperty("_LightMapBChannel", properties);
        specular2Color = FindProperty("_SpecularColor2", properties, false);
        specular2Multi = FindProperty("_SpecularMulti2", properties, false);
        emissMap = FindProperty("_EmissMap", properties);
        emissColor = FindProperty("_EmissColor", properties);
        emissSpeedX = FindProperty("_EmissMaskSpeedX", properties);
        emissSpeedY = FindProperty("_EmissMaskSpeedY", properties);
        emissNoiseFactor = FindProperty("_EmissNoiseFactor", properties);
        emissNoiseStrength = FindProperty("_EmissNoiseStrength", properties);
        emissBreathSpeed = FindProperty("_EmissBreathSpeed", properties);
        flashColor = FindProperty("_FlashColor", properties);
        flashIntensity = FindProperty("_FlashIntensity", properties);
    }

    static bool Foldout(bool display, string title)
    {
        var style = new GUIStyle("ShurikenModuleTitle");
        style.font = new GUIStyle(EditorStyles.boldLabel).font;
        style.border = new RectOffset(15, 7, 4, 4);
        style.fixedHeight = 22;
        style.contentOffset = new Vector2(20f, -2f);

        var rect = GUILayoutUtility.GetRect(16f, 22f, style);
        GUI.Box(rect, title, style);

        var e = Event.current;

        var toggleRect = new Rect(rect.x + 4f, rect.y + 2f, 13f, 13f);
        if (e.type == EventType.Repaint)
        {
            EditorStyles.foldout.Draw(toggleRect, false, false, display, false);
        }

        if (e.type == EventType.MouseDown && rect.Contains(e.mousePosition))
        {
            display = !display;
            e.Use();
        }

        return display;
    }

    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        FindProperties(properties);
        m_MaterialEditor = materialEditor;
        Material material = materialEditor.target as Material;
        if (m_FirstTimeApply)
        {
            MaterialChanged(material);
            m_FirstTimeApply = false;
        }
        ShaderProptertiesGUI(material);
    }

    public void ShaderProptertiesGUI(Material material)
    {
        EditorGUI.BeginChangeCheck();
        {
            DoAlbedoArea(material);
            EditorGUILayout.Space();
            DoLightMapArea(material);
            EditorGUILayout.Space();
            DoEmissMapArea(material);
            EditorGUILayout.Space();
            DoOtherArea(material);
            EditorGUILayout.Space();
        }
        if (EditorGUI.EndChangeCheck())
        {
            MaterialChanged(material);
        }

        EditorGUILayout.Space();

        GUILayout.Label(Styles.advancedText, EditorStyles.boldLabel);
        m_MaterialEditor.RenderQueueField();
        m_MaterialEditor.EnableInstancingField();
        m_MaterialEditor.DoubleSidedGIField();
    }

    void DoAlbedoArea(Material material)
    {
        mainTex_Foldout = Foldout(mainTex_Foldout, "主贴图");
        if (mainTex_Foldout)
        {
            EditorGUI.indentLevel++;
            m_MaterialEditor.TexturePropertySingleLine(Styles.albedoText, mainTex, baseColor);
            if (material.GetTexture("_MainTex") != null)
                m_MaterialEditor.TextureScaleOffsetProperty(mainTex);
            EditorGUI.indentLevel--;
        }
    }

    void DoLightMapArea(Material material)
    {
        lightMap_Foldout = Foldout(lightMap_Foldout, "光信息贴图");
        if (lightMap_Foldout)
        {
            EditorGUI.indentLevel++;
            m_MaterialEditor.TexturePropertySingleLine(Styles.lightMapText, lightMap);
            if (material.GetTexture("_LightMap") != null)
            {
                m_MaterialEditor.TextureScaleOffsetProperty(lightMap);
                m_MaterialEditor.ShaderProperty(lightMapRChannel, "使用R通道");
                var r = material.GetFloat("_LightMapRChannel") != 0;
                if (r)
                {
                    m_MaterialEditor.ShaderProperty(specular1Color, "强反光颜色", MaterialEditor.kMiniTextureFieldLabelIndentLevel);
                    m_MaterialEditor.ShaderProperty(specular1Multi, "强反光强度", MaterialEditor.kMiniTextureFieldLabelIndentLevel);
                    m_MaterialEditor.ShaderProperty(specular1Shininess, "强反光光泽度", MaterialEditor.kMiniTextureFieldLabelIndentLevel);
                    EditorGUILayout.Space();
                }
                m_MaterialEditor.ShaderProperty(lightMapGChannel, "使用G通道");
                var g = material.GetFloat("_LightMapGChannel") != 0;
                if (g)
                {
                    m_MaterialEditor.ShaderProperty(strandNoiseMap, "毛发反光贴图", MaterialEditor.kMiniTextureFieldLabelIndentLevel);
                    m_MaterialEditor.ShaderProperty(strandFirstOff, "毛发反光偏移", MaterialEditor.kMiniTextureFieldLabelIndentLevel);
                    m_MaterialEditor.ShaderProperty(strandFirstSmooth, "毛发光滑度", MaterialEditor.kMiniTextureFieldLabelIndentLevel);
                    m_MaterialEditor.ShaderProperty(strandSpecularColor, "毛发反光颜色", MaterialEditor.kMiniTextureFieldLabelIndentLevel);
                    m_MaterialEditor.ShaderProperty(strandSpecularStrength, "毛发反光强度", MaterialEditor.kMiniTextureFieldLabelIndentLevel);
                    EditorGUILayout.Space();
                }
                m_MaterialEditor.ShaderProperty(lightMapBChannel, "使用B通道");
                var b = material.GetFloat("_LightMapBChannel") != 0;
                if (b)
                {
                    m_MaterialEditor.ShaderProperty(specular2Color, "弱泛光颜色", MaterialEditor.kMiniTextureFieldLabelIndentLevel);
                    m_MaterialEditor.ShaderProperty(specular2Multi, "弱泛光强度", MaterialEditor.kMiniTextureFieldLabelIndentLevel);
                    EditorGUILayout.Space();
                }
            }
            EditorGUI.indentLevel--;
        }
    }

    void DoEmissMapArea(Material material)
    {
        emissMap_Foldout = Foldout(emissMap_Foldout, "自发光贴图");
        if (emissMap_Foldout)
        {
            EditorGUI.indentLevel++;
            m_MaterialEditor.TexturePropertySingleLine(Styles.emissMapText, emissMap, emissColor);
            if (material.GetTexture("_EmissMap") != null)
            {
                m_MaterialEditor.TextureScaleOffsetProperty(emissMap);
                m_MaterialEditor.ShaderProperty(emissSpeedX, "内部X方向速度");
                m_MaterialEditor.ShaderProperty(emissSpeedY, "内部Y方向速度");
                m_MaterialEditor.ShaderProperty(emissNoiseFactor, "外部扰动时间参数");
                m_MaterialEditor.ShaderProperty(emissNoiseStrength, "外部扰动强度");
                m_MaterialEditor.ShaderProperty(emissBreathSpeed, "呼吸速度");
            }
            EditorGUI.indentLevel--;
        }
    }

    void DoOtherArea(Material material)
    {
        outline_Foldout = Foldout(outline_Foldout, "描边");
        if (outline_Foldout)
        {
            EditorGUI.indentLevel++;
            m_MaterialEditor.ShaderProperty(outlineColor, "描边颜色");
            m_MaterialEditor.ShaderProperty(outlineLength, "描边宽度");
            m_MaterialEditor.ShaderProperty(outlineLightness, "描边颜色强度");
            EditorGUI.indentLevel--;
        }

        shadow_Foldout = Foldout(shadow_Foldout, "阴影");
        if (shadow_Foldout)
        {
            EditorGUI.indentLevel++;
            m_MaterialEditor.ShaderProperty(shadowThreshold, "阴影阈值");
            m_MaterialEditor.ShaderProperty(shadowColor, "阴影颜色");
            EditorGUI.indentLevel--;
        }

        other_Foldout = Foldout(other_Foldout, "其他属性");
        if (other_Foldout)
        {
            EditorGUI.indentLevel++;
            m_MaterialEditor.ShaderProperty(flashColor, "被击闪光颜色");
            EditorGUI.indentLevel--;
        }
    }

    void MaterialChanged(Material material)
    {
        var emissMap = material.GetTexture("_EmissMap");
        SetKeyword(material, "EmissMap_ON", emissMap != null);
    }

    void SetKeyword(Material m, string keyword, bool state)
    {
        if (state)
            m.EnableKeyword(keyword);
        else
            m.DisableKeyword(keyword);
    }
}
