using UnityEngine;
using UnityEditor;
using System;

public class Blz_EffectCustomParticle_GUI : ShaderGUI
{
    public enum BlendMode
    {
        AlphaBlend,
        Additive
    }

    private static class Styles
    {
        public static GUIContent albedoText = new GUIContent("主贴图(RGB)");
        public static GUIContent maskText = new GUIContent("遮罩贴图(RGB)");

        public static string renderingMode = "渲染类型";
        public static readonly string[] blendNames = Enum.GetNames(typeof(BlendMode));
        //		public static bool useHDR = false;
    }

    MaterialProperty blendMode = null;
    MaterialProperty albedoTex = null;
    MaterialProperty albedoColor = null;

    MaterialProperty maskTex = null;

    MaterialEditor mMatEditor;
    bool mFirstTimeApply = true;

    public void FindProperties(MaterialProperty[] properties)
    {
        blendMode = FindProperty("_Mode", properties);
        albedoTex = FindProperty("_MainTex", properties);
        albedoColor = FindProperty("_Color", properties);
        maskTex = FindProperty("_MaskTex", properties, false);
        //		itensity = FindProperty ("_Intensity", properties);
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
            DoAlbedo(mat);
            EditorGUILayout.Space();
            DoMask(mat);
            EditorGUILayout.Space();
            DoRenderQueue(mat);
            EditorGUILayout.Space();
            DoMD();
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

        if (mat.GetTexture("_MainTex") != null)
            mat.EnableKeyword("MainTex_On");
        else
            mat.DisableKeyword("MainTex_On");

        if (mat.GetTexture("_MaskTex") != null)
            mat.EnableKeyword("MaskMap_On");
        else
            mat.DisableKeyword("MaskMap_On");


        //		if(!Styles.useHDR)
        //			mat.DisableKeyword("HDR");
        //		else
        //			mat.EnableKeyword("HDR");
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

    void DoAlbedo(Material mat)
    {
        mMatEditor.TexturePropertySingleLine(Styles.albedoText, albedoTex, albedoColor);
        if (mat.GetTexture("_MainTex") != null)
        {
            //mMatEditor.TextureScaleOffsetProperty(albedoTex);
        }
    }

    void DoMask(Material mat)
    {
        mMatEditor.TexturePropertySingleLine(Styles.maskText, maskTex);
        if (mat.GetTexture("_MaskTex") != null)
        {
            //mMatEditor.TextureScaleOffsetProperty(maskTex);
        }
        EditorGUILayout.Space();
    }

    void DoRenderQueue(Material mat)
    {
        EditorGUILayout.LabelField("渲染顺序");
        mat.renderQueue = EditorGUILayout.IntField(mat.renderQueue, GUILayout.Width(200f));
    }

    void DoMD()
    {
        EditorGUILayout.LabelField("Custom Data说明");
        EditorGUILayout.LabelField("Custom1: 作用于主贴图. xy代表Tilling,zw代表uv流动值");
        EditorGUILayout.LabelField("Custom2: 作用于遮罩贴图. xy代表Tilling,zw代表uv流动值");
        EditorGUILayout.LabelField("Custom Vertex Streams说明");
        EditorGUILayout.LabelField("原参数不动，依次添加：\"Lifetime\\Agepercent\"、\"Custom\\Custom1.xyzw\"和\"Custom\\Custom2.xyzw\"");
    }
}
