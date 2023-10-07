using UnityEngine;
using UnityEditor;
using System;

public class Blz_EffectDissolve_GUI : ShaderGUI
{
    public enum BlendMode
    {
        AlphaBlend,
        Additive
    }

    private static class Styles
    {
        public static GUIContent albedoText = new GUIContent("主贴图(RGB)");
        public static GUIContent noiseText = new GUIContent("噪声贴图(RGB)");

        public static string renderingMode = "渲染类型";
        public static readonly string[] blendNames = Enum.GetNames(typeof(BlendMode));
    }

    MaterialProperty blendModeProperty = null;

    public void FindProperties(MaterialProperty[] properties)
    {
        blendModeProperty = FindProperty("_Mode", properties);
    }

    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        FindProperties(properties);
        Material material = materialEditor.target as Material;
        EditorGUI.showMixedValue = blendModeProperty.hasMixedValue;
        EditorGUI.BeginChangeCheck();
        var blendMode = (BlendMode)EditorGUILayout.Popup("渲染类型", (int)blendModeProperty.floatValue, Enum.GetNames(typeof(BlendMode)));
        if (EditorGUI.EndChangeCheck())
        {
            materialEditor.RegisterPropertyChangeUndo("渲染类型");
            blendModeProperty.floatValue = (float)blendMode;
        }
        EditorGUI.showMixedValue = false;
        base.OnGUI(materialEditor, properties);
        if (material)
        {
            SetupMatBlendMode(material, (BlendMode)material.GetFloat("_Mode"));
        }
    }

    public static void SetupMatBlendMode(Material mat, BlendMode mode)
    {
        switch (mode)
        {

            case BlendMode.AlphaBlend:
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                break;

            case BlendMode.Additive:
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.One);
                break;
        }
    }
}
