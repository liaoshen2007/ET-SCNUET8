using UnityEditor;
using UnityEngine;

namespace ET
{
    /// <summary>
    /// 编辑器输入框
    /// </summary>
    public class EditorInputWin : EditorWindow
    {
        #region Properties
        public static void ShowWin()
        {
            var win = GetWindow<EditorInputWin>();
            win.Show();
        }

        public EditorInputWin()
        {
            titleContent = new GUIContent("间距");
        }
        #endregion

        #region Internal Methods
        private void OnGUI()
        {
            var str = EditorGUILayout.TextField("Bug Name", "20");
            float.TryParse(str, out index);
        }
        #endregion

        #region Internal Fields
        private float index;
        #endregion
    }
}