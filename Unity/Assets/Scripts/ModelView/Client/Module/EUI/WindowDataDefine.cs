﻿namespace ET.Client
{
    public enum UIWindowType
    {
        Normal, // 普通主界面
        Fixed, // 固定窗口
        PopUp, // 弹出窗口
        Other, //其他窗口
    }

    [ComponentOf(typeof(UIBaseWindow))]
    public class WindowCoreData: Entity, IAwake
    {
        public UIWindowType windowType = UIWindowType.Normal;
    }

    public class ShowWindowData: Entity
    {
        
    }
}