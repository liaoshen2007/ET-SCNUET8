﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace ETEditor
{
    public static partial class UICodeSpawner
    {
        public static void SpawnEUICode(GameObject gameObject)
        {
            if (null == gameObject)
            {
                Debug.LogError("UICode Select GameObject is null!");
                return;
            }

            try
            {
                string uiName = gameObject.name;
                if (uiName.StartsWith(UIPanelPrefix))
                {
                    Debug.Log($"----------开始生成{uiName} 相关代码 ----------");
                    SpawnDlgCode(gameObject);
                    Debug.Log($"生成{uiName} 完毕!!!");
                    return;
                }
                else if (uiName.StartsWith(CommonUIPrefix))
                {
                    Debug.Log($"-------- 开始生成子UI: {uiName} 相关代码 -------------");
                    SpawnSubUICode(gameObject);
                    Debug.Log($"生成子UI: {uiName} 完毕!!!");
                    return;
                }
                else if (uiName.StartsWith(UIItemPrefix))
                {
                    Debug.Log($"-------- 开始生成滚动列表项: {uiName} 相关代码 -------------");
                    SpawnLoopItemCode(gameObject);
                    Debug.Log($" 开始生成滚动列表项: {uiName} 完毕！！！");
                    return;
                }

                Debug.LogError($"选择的预设物不属于 Win, 子UI，滚动列表项，请检查 {uiName}！！！！！！");
            }
            finally
            {
                Path2WidgetCachedDict?.Clear();
                Path2WidgetCachedDict = null;
            }
        }

        private static void SpawnDlgCode(GameObject gameObject)
        {
            Path2WidgetCachedDict?.Clear();
            Path2WidgetCachedDict = new Dictionary<string, List<Component>>();

            FindAllWidgets(gameObject.transform, "");

            SpawnCodeForDlg(gameObject);
            SpawnCodeForDlgEventHandle(gameObject);
            SpawnCodeForDlgModel(gameObject);

            SpawnCodeForDlgBehaviour(gameObject);
            SpawnCodeForDlgComponentBehaviour(gameObject);

            AssetDatabase.Refresh();
        }

        private static void SpawnCodeForDlg(GameObject gameObject)
        {
            string strDlgName = gameObject.name;
            string strFilePath = Application.dataPath + "/Scripts/HotfixView/Client/UI/" + strDlgName;

            if (!Directory.Exists(strFilePath))
            {
                Directory.CreateDirectory(strFilePath);
            }

            strFilePath = Application.dataPath + "/Scripts/HotfixView/Client/UI/" + strDlgName + "/" + strDlgName + "System.cs";
            if (File.Exists(strFilePath))
            {
                Debug.LogError("已存在 " + strDlgName + "System.cs,将不会再次生成。");
                return;
            }

            StreamWriter sw = new StreamWriter(strFilePath, false, Encoding.UTF8);
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendLine("using System.Collections;")
                    .AppendLine("using System.Collections.Generic;")
                    .AppendLine("using System;")
                    .AppendLine("using UnityEngine;")
                    .AppendLine("using UnityEngine.UI;\r\n");

            strBuilder.AppendLine("namespace ET.Client");
            strBuilder.AppendLine("{");

            strBuilder.AppendFormat("\t[FriendOf(typeof({0}))]\r\n", strDlgName);

            strBuilder.AppendFormat("\tpublic static partial class {0}\r\n", strDlgName + "System");
            strBuilder.AppendLine("\t{");

            strBuilder.AppendFormat("\t\tpublic static void RegisterUIEvent(this {0} self)\n", strDlgName)
                    .AppendLine("\t\t{")
                    .AppendLine("\t\t ")
                    .AppendLine("\t\t}")
                    .AppendLine();

            strBuilder.AppendFormat("\t\tpublic static void Focus(this {0} self)\n", strDlgName)
                    .AppendLine("\t\t{")
                    .AppendLine("\t\t ")
                    .AppendLine("\t\t}")
                    .AppendLine();

            strBuilder.AppendFormat("\t\tpublic static void UnFocus(this {0} self)\n", strDlgName)
                    .AppendLine("\t\t{")
                    .AppendLine("\t\t ")
                    .AppendLine("\t\t}")
                    .AppendLine();

            strBuilder.AppendFormat("\t\tpublic static void ShowWindow(this {0} self, Entity contextData = null)\n", strDlgName);
            strBuilder.AppendLine("\t\t{")
                    .AppendLine("\t\t ")
                    .AppendLine("\t\t}");

            strBuilder.AppendLine("\t}");
            strBuilder.AppendLine("}");

            sw.Write(strBuilder);
            sw.Flush();
            sw.Close();
        }

        private static void SpawnCodeForDlgEventHandle(GameObject gameObject)
        {
            string strDlgName = gameObject.name;
            string strFilePath = Application.dataPath + "/Scripts/HotfixView/Client/UI/" + strDlgName + "/Event";

            if (!Directory.Exists(strFilePath))
            {
                Directory.CreateDirectory(strFilePath);
            }

            strFilePath = Application.dataPath + "/Scripts/HotfixView/Client/UI/" + strDlgName + "/Event/" + strDlgName + "EventHandler.cs";
            if (File.Exists(strFilePath))
            {
                Debug.LogError("已存在 " + strDlgName + ".cs,将不会再次生成。");
                return;
            }

            StreamWriter sw = new StreamWriter(strFilePath, false, Encoding.UTF8);
            StringBuilder strBuilder = new StringBuilder();

            strBuilder.AppendLine("namespace ET.Client");
            strBuilder.AppendLine("{");
            strBuilder.AppendLine("\t[FriendOf(typeof(WindowCoreData))]");
            strBuilder.AppendLine("\t[FriendOf(typeof(UIBaseWindow))]");
            strBuilder.AppendFormat("\t[AUIEvent(WindowID.Win_{0})]\n", strDlgName);
            strBuilder.AppendFormat("\tpublic  class {0}EventHandler : IAUIEventHandler\r\n", strDlgName);
            strBuilder.AppendLine("\t{");

            strBuilder.AppendLine("\t\tpublic void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)")
                    .AppendLine("\t\t{");

            strBuilder.AppendFormat("\t\t\tuiBaseWindow.WindowData.WindowType = UIWindowType.Normal; \r\n");

            strBuilder.AppendLine("\t\t}")
                    .AppendLine();

            strBuilder.AppendLine("\t\tpublic void OnInitComponent(UIBaseWindow uiBaseWindow)")
                    .AppendLine("\t\t{");

            strBuilder.AppendFormat("\t\t\tuiBaseWindow.AddComponent<{0}ViewComponent>(); \r\n", strDlgName);
            strBuilder.AppendFormat("\t\t\tuiBaseWindow.AddComponent<{0}>(); \r\n", strDlgName);

            strBuilder.AppendLine("\t\t}")
                    .AppendLine();

            strBuilder.AppendLine("\t\tpublic void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)")
                    .AppendLine("\t\t{");
            strBuilder.AppendFormat("\t\t\tuiBaseWindow.GetComponent<{0}>().RegisterUIEvent(); \r\n", strDlgName);
            strBuilder.AppendLine("\t\t}")
                    .AppendLine();

            strBuilder.AppendLine("\t\tpublic void OnFocus(UIBaseWindow uiBaseWindow)")
                    .AppendLine("\t\t{");
            strBuilder.AppendFormat("\t\t\tuiBaseWindow.GetComponent<{0}>().Focus(); \r\n", strDlgName);
            strBuilder.AppendLine("\t\t}")
                    .AppendLine();

            strBuilder.AppendLine("\t\tpublic void OnUnFocus(UIBaseWindow uiBaseWindow)")
                    .AppendLine("\t\t{");
            strBuilder.AppendFormat("\t\t\tuiBaseWindow.GetComponent<{0}>().UnFocus(); \r\n", strDlgName);
            strBuilder.AppendLine("\t\t}")
                    .AppendLine();

            strBuilder.AppendLine("\t\tpublic void OnShowWindow(UIBaseWindow uiBaseWindow, Entity contextData = null)")
                    .AppendLine("\t\t{");
            strBuilder.AppendFormat("\t\t\tuiBaseWindow.GetComponent<{0}>().ShowWindow(contextData); \r\n", strDlgName);
            strBuilder.AppendLine("\t\t}").AppendLine();

            strBuilder.AppendLine("\t\tpublic void OnHideWindow(UIBaseWindow uiBaseWindow)")
                    .AppendLine("\t\t{")
                    .AppendLine("");

            strBuilder.AppendLine("\t\t}").AppendLine();

            strBuilder.AppendLine("\t\tpublic void BeforeUnload(UIBaseWindow uiBaseWindow)")
                    .AppendLine("\t\t{")
                    .AppendLine("");

            strBuilder.AppendLine("\t\t}");

            strBuilder.AppendLine("\t}");
            strBuilder.AppendLine("}");

            sw.Write(strBuilder);
            sw.Flush();
            sw.Close();
        }

        private static void SpawnCodeForDlgModel(GameObject gameObject)
        {
            string strDlgName = gameObject.name;
            string strFilePath = Application.dataPath + "/Scripts/ModelView/Client/UI/" + strDlgName;

            if (!Directory.Exists(strFilePath))
            {
                Directory.CreateDirectory(strFilePath);
            }

            strFilePath = Application.dataPath + "/Scripts/ModelView/Client/UI/" + strDlgName + "/" + strDlgName + ".cs";
            if (File.Exists(strFilePath))
            {
                return;
            }

            StreamWriter sw = new StreamWriter(strFilePath, false, Encoding.UTF8);
            StringBuilder strBuilder = new StringBuilder();

            strBuilder.AppendLine("namespace ET.Client");
            strBuilder.AppendLine("{");

            strBuilder.AppendLine("\t[ComponentOf(typeof(UIBaseWindow))]");
            strBuilder.AppendFormat("\tpublic  class {0} : Entity, IAwake, IUILogic\r\n", strDlgName);
            strBuilder.AppendLine("\t{");

            strBuilder.AppendLine("\t\tpublic " + strDlgName + "ViewComponent View { get => GetParent<UIBaseWindow>().GetComponent<" + strDlgName +
                "ViewComponent>();} ");

            strBuilder.AppendLine("\t}");
            strBuilder.AppendLine("}");

            sw.Write(strBuilder);
            sw.Flush();
            sw.Close();
        }

        private static void SpawnCodeForDlgBehaviour(GameObject gameObject)
        {
            if (!gameObject)
            {
                return;
            }

            string strDlgName = gameObject.name;
            string strDlgComponentName = gameObject.name + "ViewComponent";

            string strFilePath = Application.dataPath + "/Scripts/HotfixView/Client/UIBehaviour/" + strDlgName;

            if (!Directory.Exists(strFilePath))
            {
                Directory.CreateDirectory(strFilePath);
            }

            strFilePath = Application.dataPath + "/Scripts/HotfixView/Client/UIBehaviour/" + strDlgName + "/" + strDlgComponentName + "System.cs";

            StreamWriter sw = new StreamWriter(strFilePath, false, Encoding.UTF8);

            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendLine("namespace ET.Client");
            strBuilder.AppendLine("{");
            strBuilder.AppendLine($"\t[FriendOf(typeof({strDlgComponentName}))]");
            strBuilder.AppendFormat("\tpublic class {0}AwakeSystem : AwakeSystem<{1}> \r\n", strDlgComponentName, strDlgComponentName);
            strBuilder.AppendLine("\t{");
            strBuilder.AppendFormat("\t\tprotected override void Awake({0} self)\n", strDlgComponentName);
            strBuilder.AppendLine("\t\t{");
            strBuilder.AppendLine("\t\t\tself.uiTransform = self.GetParent<UIBaseWindow>().UITransform;");
            if (Path2WidgetCachedDict.TryGetValue("E_CloseBtn", out var list))
            {
                foreach (var v in list)
                {
                    if (v is Button)
                    {
                        strBuilder.AppendLine("\t\t\tself.RegisterCloseEvent(self.E_CloseBtnButton);");
                        break;
                    }
                }
            }

            strBuilder.AppendLine("\t\t}");
            strBuilder.AppendLine("\t}");
            strBuilder.AppendLine("\n");

            strBuilder.AppendLine($"\t[FriendOf(typeof({strDlgComponentName}))]");
            strBuilder.AppendFormat("\tpublic class {0}DestroySystem : DestroySystem<{1}> \r\n", strDlgComponentName, strDlgComponentName);
            strBuilder.AppendLine("\t{");
            strBuilder.AppendFormat("\t\tprotected override void Destroy({0} self)", strDlgComponentName);
            strBuilder.AppendLine("\n\t\t{");
            strBuilder.AppendLine("\t\t\tself.DestroyWidget();\r\n");
            strBuilder.AppendLine("\t\t}");
            strBuilder.AppendLine("\t}");
            strBuilder.AppendLine("}");
            sw.Write(strBuilder);
            sw.Flush();
            sw.Close();
        }

        private static void SpawnCodeForDlgComponentBehaviour(GameObject gameObject)
        {
            if (null == gameObject)
            {
                return;
            }

            string strDlgName = gameObject.name;
            string strDlgComponentName = gameObject.name + "ViewComponent";

            string strFilePath = Application.dataPath + "/Scripts/ModelView/Client/UIBehaviour/" + strDlgName;
            if (!Directory.Exists(strFilePath))
            {
                Directory.CreateDirectory(strFilePath);
            }

            strFilePath = strFilePath + "/" + strDlgComponentName + ".cs";
            StreamWriter sw = new StreamWriter(strFilePath, false, Encoding.UTF8);
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendLine("using UnityEngine;");
            strBuilder.AppendLine("using UnityEngine.UI;");
            strBuilder.AppendLine("namespace ET.Client");
            strBuilder.AppendLine("{");
            strBuilder.AppendLine("\t[ComponentOf(typeof(UIBaseWindow))]");
            strBuilder.AppendLine("\t[EnableMethod]");
            strBuilder.AppendFormat("\tpublic  class {0} : Entity, IAwake, IDestroy \r\n", strDlgComponentName)
                    .AppendLine("\t{");

            CreateWidgetBindCode(ref strBuilder, gameObject.transform);

            CreateDestroyWidgetCode(ref strBuilder);

            CreateDeclareCode(ref strBuilder);
            strBuilder.AppendFormat("\t\tpublic Transform uiTransform = null;\r\n");
            strBuilder.AppendLine("\t}");
            strBuilder.AppendLine("}");

            sw.Write(strBuilder);
            sw.Flush();
            sw.Close();
        }

        private static void CreateDestroyWidgetCode(ref StringBuilder strBuilder, bool isScrollItem = false)
        {
            strBuilder.AppendFormat("\t\tpublic void DestroyWidget()");
            strBuilder.AppendLine("\n\t\t{");
            CreateDlgWidgetDisposeCode(ref strBuilder);
            strBuilder.AppendFormat("\t\t\tuiTransform = null;\r\n");
            if (isScrollItem)
            {
                strBuilder.AppendLine("\t\t\tthis.DataId = 0;");
            }

            strBuilder.AppendLine("\t\t}\n");
        }

        public static void CreateDlgWidgetDisposeCode(ref StringBuilder strBuilder, bool isSelf = false)
        {
            string pointStr = isSelf? "self" : "this";
            foreach (KeyValuePair<string, List<Component>> pair in Path2WidgetCachedDict)
            {
                foreach (var info in pair.Value)
                {
                    Component widget = info;
                    string strClassType = widget.GetType().ToString();

                    if (pair.Key.StartsWith(CommonUIPrefix))
                    {
                        strBuilder.AppendFormat("\t\t	{0}.m_{1} = null;\r\n", pointStr, pair.Key.ToLower());
                        continue;
                    }

                    string widgetName = widget.name + strClassType.Split('.').ToList().Last();
                    strBuilder.AppendFormat("\t\t	{0}.m_{1} = null;\r\n", pointStr, widgetName);
                }
            }
        }

        public static void CreateWidgetBindCode(ref StringBuilder strBuilder, Transform transRoot)
        {
            foreach (KeyValuePair<string, List<Component>> pair in Path2WidgetCachedDict)
            {
                foreach (var info in pair.Value)
                {
                    Component widget = info;
                    string strPath = GetWidgetPath(widget.transform, transRoot);
                    string strClassType = widget.GetType().Name;
                    string strInterfaceType = strClassType;

                    if (pair.Key.StartsWith(CommonUIPrefix))
                    {
                        var subUIClassPrefab = PrefabUtility.GetCorrespondingObjectFromOriginalSource(widget);
                        if (subUIClassPrefab == null)
                        {
                            Debug.LogError($"公共UI找不到所属的Prefab! {pair.Key}");
                            return;
                        }

                        GetSubUIBaseWindowCode(ref strBuilder, pair.Key, strPath, subUIClassPrefab.name);
                        continue;
                    }

                    string widgetName = widget.name + strClassType.Split('.').ToList().Last();

                    strBuilder.AppendFormat("		public {0} {1}\r\n", strInterfaceType, widgetName);
                    strBuilder.AppendLine("     	{");
                    strBuilder.AppendLine("     		get");
                    strBuilder.AppendLine("     		{");

                    strBuilder.AppendLine("     			if (this.uiTransform == null)");
                    strBuilder.AppendLine("     			{");
                    strBuilder.AppendLine("     				Log.Error(\"uiTransform is null.\");");
                    strBuilder.AppendLine("     				return null;");
                    strBuilder.AppendLine("     			}");

                    if (transRoot.gameObject.name.StartsWith(UIItemPrefix))
                    {
                        strBuilder.AppendLine("     			if (this.isCacheNode)");
                        strBuilder.AppendLine("     			{");
                        strBuilder.AppendFormat("     				if( this.m_{0} == null )\n", widgetName);
                        strBuilder.AppendLine("     				{");
                        strBuilder.AppendFormat("		    			this.m_{0} = UIFindHelper.FindDeepChild<{2}>(this.uiTransform.gameObject,\"{1}\");\r\n",
                            widgetName, strPath, strInterfaceType);
                        strBuilder.AppendLine("     				}");
                        strBuilder.AppendFormat("     				return this.m_{0};\n", widgetName);
                        strBuilder.AppendLine("     			}");
                        strBuilder.AppendLine("     			else");
                        strBuilder.AppendLine("     			{");
                        strBuilder.AppendFormat("		    		return UIFindHelper.FindDeepChild<{2}>(this.uiTransform.gameObject,\"{1}\");\r\n",
                            widgetName, strPath, strInterfaceType);
                        strBuilder.AppendLine("     			}");
                    }
                    else
                    {
                        strBuilder.AppendFormat("     			if( this.m_{0} == null )\n", widgetName);
                        strBuilder.AppendLine("     			{");
                        strBuilder.AppendFormat("		    		this.m_{0} = UIFindHelper.FindDeepChild<{2}>(this.uiTransform.gameObject,\"{1}\");\r\n",
                            widgetName, strPath, strInterfaceType);
                        strBuilder.AppendLine("     			}");
                        strBuilder.AppendFormat("     			return this.m_{0};\n", widgetName);
                    }

                    strBuilder.AppendLine("     		}");
                    strBuilder.AppendLine("     	}\n");
                }
            }
        }

        public static void CreateDeclareCode(ref StringBuilder strBuilder)
        {
            foreach (KeyValuePair<string, List<Component>> pair in Path2WidgetCachedDict)
            {
                foreach (var info in pair.Value)
                {
                    Component widget = info;
                    string strClassType = widget.GetType().Name;

                    if (pair.Key.StartsWith(CommonUIPrefix))
                    {
                        var subUIClassPrefab = PrefabUtility.GetCorrespondingObjectFromOriginalSource(widget);
                        if (subUIClassPrefab == null)
                        {
                            Debug.LogError($"公共UI找不到所属的Prefab! {pair.Key}");
                            return;
                        }

                        string subUIClassType = subUIClassPrefab.name;
                        strBuilder.AppendFormat("\t\tprivate EntityRef<{0}> m_{1} = null;\r\n", subUIClassType, pair.Key.ToLower());
                        continue;
                    }

                    string widgetName = widget.name + strClassType.Split('.').ToList().Last();
                    strBuilder.AppendFormat("\t\tprivate {0} m_{1} = null;\r\n", strClassType, widgetName);
                }
            }
        }

        private static void FindAllWidgets(Transform trans, string strPath)
        {
            if (null == trans)
            {
                return;
            }

            for (int nIndex = 0; nIndex < trans.childCount; ++nIndex)
            {
                Transform child = trans.GetChild(nIndex);
                string strTemp = strPath + "/" + child.name;

                bool isSubUI = child.name.StartsWith(CommonUIPrefix);
                if (isSubUI || child.name.StartsWith(UIGameObjectPrefix))
                {
                    List<Component> rectTransfomrComponents = new List<Component>();
                    rectTransfomrComponents.Add(child.GetComponent<RectTransform>());
                    Path2WidgetCachedDict.Add(child.name, rectTransfomrComponents);
                }
                else if (child.name.StartsWith(UIWidgetPrefix))
                {
                    foreach (var uiComponent in WidgetInterfaceList)
                    {
                        Component component = child.GetComponent(uiComponent);
                        if (null == component)
                        {
                            continue;
                        }

                        if (Path2WidgetCachedDict.TryGetValue(child.name, out var value))
                        {
                            value.Add(component);
                            continue;
                        }

                        List<Component> componentsList = new List<Component>();
                        componentsList.Add(component);
                        Path2WidgetCachedDict.Add(child.name, componentsList);
                    }
                }

                if (isSubUI)
                {
                    Debug.Log($"遇到子UI：{child.name},不生成子UI项代码");
                    continue;
                }

                FindAllWidgets(child, strTemp);
            }
        }

        private static string GetWidgetPath(Transform obj, Transform root)
        {
            string path = obj.name;

            while (obj.parent != null && obj.parent != root)
            {
                obj = obj.transform.parent;
                path = obj.name + "/" + path;
            }

            return path;
        }

        static void GetSubUIBaseWindowCode(ref StringBuilder strBuilder, string widget, string strPath, string subUIClassType)
        {
            strBuilder.AppendFormat("		public {0} {1}\r\n", subUIClassType, widget);
            strBuilder.AppendLine("     	{");
            strBuilder.AppendLine("     		get");
            strBuilder.AppendLine("     		{");

            strBuilder.AppendLine("     			if (this.uiTransform == null)");
            strBuilder.AppendLine("     			{");
            strBuilder.AppendLine("     				Log.Error(\"uiTransform is null.\");");
            strBuilder.AppendLine("     				return null;");
            strBuilder.AppendLine("     			}");

            strBuilder.AppendFormat("     			if( this.m_{0} == null )\n", widget.ToLower());
            strBuilder.AppendLine("     			{");
            strBuilder.AppendFormat("		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,\"{0}\");\r\n",
                strPath);
            strBuilder.AppendFormat("		    	   this.m_{0} = this.AddChild<{1},Transform>(subTrans);\r\n", widget.ToLower(), subUIClassType);
            strBuilder.AppendLine("     			}");
            strBuilder.AppendFormat("     			return this.m_{0};\n", widget.ToLower());
            strBuilder.AppendLine("     		}");

            strBuilder.AppendLine("     	}\n");
        }

        /// <summary>
        /// 注意！！！Text一定要换成ExtendText才能使用！！不然不能读取text!强制让你多语言~~
        /// </summary>
        static UICodeSpawner()
        {
            WidgetInterfaceList = new List<string>();
            WidgetInterfaceList.Add("Button");
            WidgetInterfaceList.Add("ExtendText");
            WidgetInterfaceList.Add("SymbolText");
            WidgetInterfaceList.Add("Input");
            WidgetInterfaceList.Add("InputField");
            WidgetInterfaceList.Add("Scrollbar");
            WidgetInterfaceList.Add("ToggleGroup");
            WidgetInterfaceList.Add("Toggle");
            WidgetInterfaceList.Add("Dropdown");
            WidgetInterfaceList.Add("Slider");
            WidgetInterfaceList.Add("ScrollRect");
            WidgetInterfaceList.Add("ExtendImage");
            WidgetInterfaceList.Add("RawImage");
            WidgetInterfaceList.Add("Canvas");
            WidgetInterfaceList.Add("UIWarpContent");
            WidgetInterfaceList.Add("LoopVerticalScrollRect");
            WidgetInterfaceList.Add("LoopHorizontalScrollRect");
        }

        private static Dictionary<string, List<Component>> Path2WidgetCachedDict;
        private static readonly List<string> WidgetInterfaceList;
        private const string UIPanelPrefix = "UI";
        private const string CommonUIPrefix = "ES";
        private const string UIWidgetPrefix = "E";
        private const string UITweenPrefix = "ET";
        private const string UIGameObjectPrefix = "EG";
        private const string UIItemPrefix = "Item";
    }
}