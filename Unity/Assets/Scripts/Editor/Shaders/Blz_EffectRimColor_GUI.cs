using UnityEngine;
using UnityEditor;
using System;

public class Blz_EffectRimColor_GUI : ShaderGUI
{
    public enum BlendMode
    {
        AlphaBlend,
        Additive
    }

    private static class Styles
    {
        public static string renderingMode = "渲染类型";
        public static readonly string[] blendNames = Enum.GetNames(typeof(BlendMode));
        //		public static bool useHDR = false;
    }

    MaterialProperty blendMode = null;
    MaterialProperty mainColor = null;
    MaterialProperty outlineColor = null;
    MaterialProperty atten = null;
    MaterialProperty rimlength = null;

    MaterialEditor mMatEditor;
    bool mFirstTimeApply = true;

    public void FindProperties(MaterialProperty[] properties)
    {
        blendMode = FindProperty("_Mode", properties);
        mainColor = FindProperty("_Color", properties);
        outlineColor = FindProperty("_OutLineColor", properties);
        rimlength = FindProperty("_RimLength", properties);
        atten = FindProperty("_Atten", properties);
    }

    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        FindProperties(properties);
        mMatEditor = materialEditor;
        Material material = materialEditor.target as Material;
        if (mFirstTimeApply)
        {
            MaterialChanged(material);
            mFirstTimeApply = false;
        }
        ShaderPropertiesGUI(material, properties);
    }

    public void ShaderPropertiesGUI(Material mat, MaterialProperty[] properties)
    {
        EditorGUIUtility.labelWidth = 0;
        EditorGUI.BeginChangeCheck();
        {
            BlendModePopup();
            EditorGUILayout.Space();
            DoRenderQueue(mat);
            //			Styles.useHDR = EditorGUILayout.Toggle ("启用HDR", Styles.useHDR, GUILayout.Width (300f));
            //			DoHDR (mat);

        }
        if (EditorGUI.EndChangeCheck())
        {
            foreach (var obj in blendMode.targets)
                MaterialChanged((Material)obj);
        }
    }

    static void MaterialChanged(Material mat)
    {
        SetupMatBlendMode(mat, (BlendMode)mat.GetFloat("_Mode"));
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

    void BlendModePopup()
    {
        EditorGUI.showMixedValue = blendMode.hasMixedValue;
        var mode = (BlendMode)blendMode.floatValue;

        EditorGUI.BeginChangeCheck();
        mode = (BlendMode)EditorGUILayout.Popup(Styles.renderingMode, (int)mode, Styles.blendNames);
        if (EditorGUI.EndChangeCheck())
        {
            mMatEditor.RegisterPropertyChangeUndo("渲染类型");
            blendMode.floatValue = (float)mode;
        }

        EditorGUI.showMixedValue = false;
    }

    void DoRenderQueue(Material mat)
    {
        mMatEditor.ShaderProperty(mainColor, "主颜色");
        mMatEditor.ShaderProperty(atten, "流光中心衰减");
        mMatEditor.ShaderProperty(outlineColor, "边缘光颜色");
        mMatEditor.ShaderProperty(rimlength, "边缘光强度");
        EditorGUILayout.LabelField("渲染顺序");
        mat.renderQueue = EditorGUILayout.IntField(mat.renderQueue, GUILayout.Width(200f));
    }
}
