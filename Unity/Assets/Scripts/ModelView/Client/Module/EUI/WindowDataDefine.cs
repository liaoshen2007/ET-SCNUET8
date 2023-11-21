namespace ET.Client
{
    public enum UIWindowType
    {
        Normal, // 普通界面
        PopUp, // 弹出窗口
        Fixed, // 固定窗口
        Other, //其他窗口
    }

    [ComponentOf(typeof(UIBaseWindow))]
    public class WindowCoreData: Entity, IAwake
    {
        public UIWindowType WindowType = UIWindowType.Normal;
        
        public bool NeedMask = true;
    }

    public class ShowWindowData: Entity
    {
        
    }
}