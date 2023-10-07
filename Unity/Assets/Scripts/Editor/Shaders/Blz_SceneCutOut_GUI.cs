using System;
using UnityEditor;
using UnityEngine;

public class Blz_SceneCutOut_GUI : ShaderGUI
{
    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
    {
        Material material = materialEditor.target as Material;
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
