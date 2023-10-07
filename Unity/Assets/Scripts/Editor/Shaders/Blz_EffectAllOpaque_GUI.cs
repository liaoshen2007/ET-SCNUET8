using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.Rendering;

public class Blz_EffectAllOpaque_GUI : ShaderGUI 
{
	public enum BlendMode
	{
		Opaque,
		Cutout,
	}

	private static class Styles
	{
		public static GUIContent albedoText = new GUIContent ("主贴图(RGB)");
		public static GUIContent alphaText = new GUIContent ("Alpha贴图(R)");
		public static GUIContent glitterText = new GUIContent ("流光贴图(RGB)");
		public static GUIContent illnumText = new GUIContent ("自发光贴图(RGB)");
		public static GUIContent maskText = new GUIContent ("遮罩贴图(RGB)");

		public static GUIContent cutoffText = new GUIContent ("CutOff");
		public static string renderingMode = "渲染类型";
		public static readonly string[] blendNames = Enum.GetNames(typeof(BlendMode));
	}

	MaterialProperty blendMode = null;
	MaterialProperty albedoTex = null;
	MaterialProperty albedoColor = null;
	MaterialProperty glitterTex = null;
	MaterialProperty glitterColor = null;
	MaterialProperty speedx = null;
	MaterialProperty speedy = null;
	MaterialProperty maskTex = null;
	MaterialProperty illnumTex = null;
	MaterialProperty illnumColor = null;
	MaterialProperty illnumBreath = null;
	MaterialProperty minalpha = null;
	MaterialProperty maxalpha = null;
	MaterialProperty atten = null;
	MaterialProperty cutoffValue = null;
	MaterialProperty outlineColor = null;
	MaterialProperty rimlength = null;

	MaterialEditor mMatEditor;
	bool mFirstTimeApply = true;

	public void FindProperties (MaterialProperty[] properties)
	{
		blendMode = FindProperty ("_Mode", properties);
		albedoTex = FindProperty ("_MainTex", properties);
		albedoColor = FindProperty ("_Color", properties);
		glitterTex = FindProperty ("_Glittering", properties);
		glitterColor = FindProperty ("_GlitterColor", properties);
		speedx = FindProperty ("_SpeedX", properties);
		speedy = FindProperty ("_SpeedY", properties);
		maskTex = FindProperty ("_MaskTex", properties, false);
		illnumTex = FindProperty ("_IllumTex", properties, false);
		illnumColor = FindProperty ("_IllumColor", properties, false);
		illnumBreath = FindProperty ("_IllumBreath", properties, false);
		minalpha = FindProperty ("_MinAlpha", properties, false);
		maxalpha = FindProperty ("_MaxAlpha", properties, false);
		atten = FindProperty ("_Atten", properties, false);
		cutoffValue = FindProperty ("_Cutoff", properties, false);
		outlineColor = FindProperty ("_OutLineColor", properties);
		rimlength = FindProperty ("_RimLength", properties);
	}

	public override void OnGUI (MaterialEditor materialEditor, MaterialProperty[] properties)
	{
		FindProperties (properties);
		mMatEditor = materialEditor;
		Material material = materialEditor.target as Material;
		if (mFirstTimeApply) 
		{
			MaterialChanged (material);
			mFirstTimeApply = false;
		}
		ShaderPropertiesGUI (material, properties);
	}

	public void ShaderPropertiesGUI(Material mat, MaterialProperty[] properties)
	{
		EditorGUIUtility.labelWidth = 0;
		EditorGUI.BeginChangeCheck ();
		{
			BlendModePopup ();
			EditorGUILayout.Space ();
			DoAlbedo (mat);
			EditorGUILayout.Space ();
			DoIllnum (mat);
			EditorGUILayout.Space ();
			DoGlitter (mat);
			EditorGUILayout.Space ();
			DoMask (mat);
			EditorGUILayout.Space ();
			DoOutLight (mat);
//			DoPBS (mat);
			EditorGUILayout.Space ();
//			DoLightMapping (mat);
//			EditorGUILayout.Space ();
//			DoReflect (mat);

		}
		if (EditorGUI.EndChangeCheck ()) 
		{
			foreach(var obj in blendMode.targets)
				MaterialChanged ((Material)obj);
		}
	}

	static void MaterialChanged(Material mat)
	{
		SetupMatBlendMode (mat, (BlendMode)mat.GetFloat ("_Mode"));

		if (mat.GetTexture ("_MaskTex") != null)
			mat.EnableKeyword ("MaskMap_On");
		else
			mat.DisableKeyword ("MaskMap_On");

		if (mat.GetTexture ("_IllumTex") != null) 
		{
			mat.EnableKeyword ("IllumMap_On");
			if (mat.GetFloat ("_IllumBreath") > 0)
				mat.EnableKeyword ("Breath_On");
			else
				mat.DisableKeyword ("Breath_On");
		} 
		else 
		{
			mat.DisableKeyword ("IllumMap_On");
			mat.DisableKeyword ("Breath_On");
		}
	}

	public static void SetupMatBlendMode(Material mat, BlendMode mode)
	{
		switch (mode) 
		{
		case BlendMode.Opaque:
			mat.SetOverrideTag ("RenderType", "Opaque");
//			mat.SetInt ("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
//			mat.SetInt ("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
//			mat.SetInt ("_ZWrite", 1);
			mat.DisableKeyword ("AlphaTest_On");
//			mat.DisableKeyword ("AlphaBlend_On");
//			mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest + 2;
			break;

		case BlendMode.Cutout:
			mat.SetOverrideTag ("RenderType", "TransparentCutout");
//			mat.SetInt ("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
//			mat.SetInt ("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
//			mat.SetInt ("_ZWrite", 1);
			mat.EnableKeyword ("AlphaTest_On");
//			mat.DisableKeyword ("AlphaBlend_On");
//			mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest + 2;
			break;

//		case BlendMode.AlphaBlend:
//			mat.SetOverrideTag ("RenderType", "Transparent");
//			mat.SetInt ("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
//			mat.SetInt ("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
//			mat.SetInt ("_ZWrite", 0);
//			mat.DisableKeyword ("AlphaTest_On");
//			mat.EnableKeyword ("AlphaBlend_On");
//			mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent + 10;
//			break;
//
//		case BlendMode.Additive:
//			mat.SetOverrideTag ("RenderType", "Transparent");
//			mat.SetInt ("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
//			mat.SetInt ("_DstBlend", (int)UnityEngine.Rendering.BlendMode.One);
//			mat.SetInt ("_ZWrite", 0);
//			mat.DisableKeyword ("AlphaTest_On");
//			mat.EnableKeyword ("AlphaBlend_On");
//			mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent + 10;
//			break;
		}
	}

	void BlendModePopup()
	{
		EditorGUI.showMixedValue = blendMode.hasMixedValue;
		var mode = (BlendMode)blendMode.floatValue;

		EditorGUI.BeginChangeCheck ();
		mode = (BlendMode)EditorGUILayout.Popup (Styles.renderingMode, (int)mode, Styles.blendNames);
		if (EditorGUI.EndChangeCheck ())
		{
			mMatEditor.RegisterPropertyChangeUndo ("渲染类型");
			blendMode.floatValue = (float)mode;
		}

		EditorGUI.showMixedValue = false;
	}

	void DoAlbedo(Material mat)
	{
		mMatEditor.TexturePropertySingleLine (Styles.albedoText, albedoTex, albedoColor);
		if (((BlendMode)mat.GetFloat ("_Mode")).Equals (BlendMode.Cutout)) 
		{
			mMatEditor.ShaderProperty (cutoffValue, Styles.cutoffText, MaterialEditor.kMiniTextureFieldLabelIndentLevel + 1);
		}
	}

	void DoIllnum(Material mat)
	{
		mMatEditor.TexturePropertySingleLine (Styles.illnumText, illnumTex, illnumColor);
		if (mat.GetTexture ("_IllumTex") != null) {
			mMatEditor.ShaderProperty (illnumBreath, "自发光呼吸速度");
			if (mat.GetFloat ("_IllumBreath") > 0) {
				mMatEditor.ShaderProperty (minalpha, "最小Alpha", MaterialEditor.kMiniTextureFieldLabelIndentLevel + 1);
				mMatEditor.ShaderProperty (maxalpha, "最大Alpha", MaterialEditor.kMiniTextureFieldLabelIndentLevel + 1);
			}
		}
	}

	void DoGlitter(Material mat)
	{
		mMatEditor.TexturePropertySingleLine (Styles.glitterText, glitterTex, glitterColor);
		if (mat.GetTexture ("_Glittering") == null)
			return;
		mMatEditor.TextureScaleOffsetProperty (glitterTex);
		mMatEditor.ShaderProperty (speedx, "横向速度", MaterialEditor.kMiniTextureFieldLabelIndentLevel + 1);
		mMatEditor.ShaderProperty (speedy, "纵向速度", MaterialEditor.kMiniTextureFieldLabelIndentLevel + 1);
	}

	void DoMask(Material mat)
	{
		mMatEditor.TexturePropertySingleLine (Styles.maskText, maskTex);
		if (!mat.IsKeywordEnabled ("MaskMap_On"))
		{
			mMatEditor.ShaderProperty (atten, "流光中心衰减");
		} 
		EditorGUILayout.Space ();
	}

	void DoOutLight(Material mat)
	{
		mMatEditor.ShaderProperty (outlineColor, "边缘光颜色");
		mMatEditor.ShaderProperty (rimlength, "边缘光强度");
	}

}
