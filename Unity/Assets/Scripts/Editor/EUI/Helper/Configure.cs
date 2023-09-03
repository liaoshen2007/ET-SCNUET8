using UnityEngine;

namespace ET
{
    /// <summary>
    /// 功能和快捷键的配置
    /// </summary>
    public class Configure : ScriptableObject
    {
        #region Public Properties
        /// <summary>
        /// 是否开启场景中的右键菜单
        /// </summary>
        [SerializeField]
        [Tooltip("是否开启场景中的右键菜单")]
        public bool IsShowSceneMenu = true;

        /// <summary>
        /// 选中图片节点再选图片时，即帮节点赋上该图
        /// </summary>
        [SerializeField]
        [Tooltip("选中图片节点再选图片时，即帮节点赋上该图")]
        public bool IsEnableFastSelectImage = false;

        /// <summary>
        /// 选中图片节点再选图片时，帮节点赋上该图时自动设为原图大小
        /// </summary>
        [SerializeField]
        [Tooltip("选中图片节点再选图片时，帮节点赋上该图时自动设为原图大小")]
        public bool IsAutoSizeOnFastSelectImg = false;

        /// <summary>
        /// 拉UIprefab或者图片入scene界面时帮它找到鼠标下的Canvas并挂在其上，若鼠标下没有画布就创建一个
        /// </summary>
        [SerializeField]
        [Tooltip("拉UIPrefab或者图片入scene界面时帮它找到鼠标下的Canvas并挂在其上，若鼠标下没有画布就创建一个")]
        public bool IsEnableDragUIToScene = true;

        /// <summary>
        /// 是否开启用箭头按键移动UI节点
        /// </summary>
        [SerializeField]
        [Tooltip("是否开启用箭头按键移动UI节点")]
        public bool IsMoveNodeByArrowKey = true;

        /// <summary>
        /// 保存界面时是否需要显示保存成功的提示框
        /// </summary>
        [SerializeField]
        [Tooltip("保存界面时是否需要显示保存成功的提示框")]
        public bool IsShowDialogWhenSaveLayout = true;

        /// <summary>
        /// 结束游戏运行时是否重新加载运行期间修改过的界面
        /// </summary>
        [SerializeField]
        [Tooltip("结束游戏运行时是否重新加载运行期间修改过的界面")]
        public bool ReloadLayoutOnExitGame = true;

        /// <summary>
        /// 添加参考图就打开选择图片框
        /// </summary>
        [SerializeField]
        [Tooltip("添加参考图就打开选择图片框")]
        public bool OpenSelectPicDialogWhenAddDecorate = true;

        /// <summary>
        /// 此路径可以为空，设置后首次导入本插件时就会加载该目录下的所有prefab
        /// </summary>
        [SerializeField]
        [Tooltip("首次导入本插件时会加载该目录下的所有prefab")]
        public string PrefabWinFirstSearchPath = "Assets/Prefabs";

        /// <summary>
        /// 所有编辑界面的Canvas都放到此节点上，可定制节点名
        /// </summary>
        [SerializeField]
        [Tooltip("所有编辑界面的Canvas都放到此节点上，可定制节点名")]
        public string UINodeName = "UIRoot";

        /// <summary>
        /// UI节点初始位置
        /// </summary>
        [SerializeField]
        [Tooltip("UI节点初始位置")]
        public Vector3 UINodePosition = new Vector3(0, 0, 0);

        /// <summary>
        /// UI节点初始大小
        /// </summary>
        [SerializeField]
        [Tooltip("UI节点初始大小")]
        public Vector2 CanvasScale = new Vector2(1024, 768);

        [SerializeField]
        public KeyCode KeyCode;

        /// <summary>
        /// 资源路径
        /// </summary>
        public const string ResAssetsPath = "/UGUI-Editor/Resources";

        /// <summary>
        /// 预制体路径
        /// </summary>
        public const string PrefabPath = "/UGUI-Editor/Prefab";

        /// <summary>
        /// 复制选中节点全名的字符串到系统剪切板
        /// </summary>
        public const string CopyNodesName = CtrlStr + ShiftStr + "c";

        public const string AltStr = "&";
        public const string CtrlStr = "%";
        public const string ShiftStr = "#";
        #endregion
    }
}