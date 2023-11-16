namespace ET.Client
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
        public UIWindowType WindowType = UIWindowType.Normal;
        
        public bool NeedMask = true;
    }

    public class ShowWindowData: Entity
    {
        
    }
}