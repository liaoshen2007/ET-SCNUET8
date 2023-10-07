using UnityEditor;
using UnityEngine;

public class Blz_ScenePBR_GUI : Blz_Base_GUI
{
    private static class Styles
    {
        public static GUIContent albedoText = EditorGUIUtility.TrTextContent("主贴图", "漫反射主贴图");
        public static GUIContent metallicGlossMapText = EditorGUIUtility.TrTextContent("金属流贴图", "金属流贴图(由金属流三张灰度图合成 r:金属图部分 g:粗糙图部分 b:ao图部分)");
        public static GUIContent bumpMapText = EditorGUIUtility.TrTextContent("法线贴图");
        public static GUIContent emissMapText = EditorGUIUtility.TrTextContent("自发光贴图", "自发光贴图(r:自发光部分 g:内部扰动 b:外部扰动)");
        public static GUIContent strandNoiseMapText = EditorGUIUtility.TrTextContent("毛发反光贴图");

        public static string advancedText = "Advanced Options";
    }
}
