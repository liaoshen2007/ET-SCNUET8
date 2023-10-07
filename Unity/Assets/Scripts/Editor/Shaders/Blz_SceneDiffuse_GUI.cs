using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Blz_SceneDiffuse_GUI : ShaderGUI
{
    public enum DiffuseType
    {
        diffuse,
        illumin_diffuse
    }

    MaterialProperty diffuseTypeProp = null;


    public void FindProperties(MaterialProperty[] properties)
    {
        diffuseTypeProp = FindProperty("_DiffuseType", properties);
    }


    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        FindProperties(properties);
        Material material = materialEditor.target as Material;
        EditorGUI.showMixedValue = diffuseTypeProp.hasMixedValue;
        EditorGUI.BeginChangeCheck();
        var diffuseType = (DiffuseType)EditorGUILayout.Popup("Diffuse 类型", (int)diffuseTypeProp.floatValue, Enum.GetNames(typeof(DiffuseType)));
        if (EditorGUI.EndChangeCheck())
        {
            materialEditor.RegisterPropertyChangeUndo("Diffuse 类型");
            diffuseTypeProp.floatValue = (float)diffuseType;
        }
        EditorGUI.showMixedValue = false;
        base.OnGUI(materialEditor, properties);
        if (material)
        {
            var emissTex = material.GetTexture("_EmissTex");
            if (emissTex != null) material.EnableKeyword("EmissTex_ON");
            else material.DisableKeyword("EmissTex_ON");
            var emissBreathTex = material.GetTexture("_EmissBreathTex");
            if (emissBreathTex != null) material.EnableKeyword("EmissBreathTex_ON");
            else material.DisableKeyword("EmissBreathTex_ON");
        }
    }
}
