namespace ET.Client
{
    [FriendOf(typeof (WindowCoreData))]
    [FriendOf(typeof (UIBaseWindow))]
    [AUIEvent(WindowID.Win_UIChat)]
    public class UIChatEventHandler: IAUIEventHandler
    {
        public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.WindowData.WindowType = UIWindowType.Normal;
            uiBaseWindow.WindowData.NeedMask = false;
        }

        public void OnInitComponent(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.AddComponent<UIChatViewComponent>();
            uiBaseWindow.AddComponent<UIChat>();
        }

        public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.GetComponent<UIChat>().RegisterUIEvent();
        }

        public void OnFocus(UIBaseWindow uiBaseWindow)
        {
        }

        public void OnUnFocus(UIBaseWindow uiBaseWindow)
        {
        }

        public void OnShowWindow(UIBaseWindow uiBaseWindow, Entity contextData = null)
        {
            uiBaseWindow.GetComponent<UIChat>().ShowWindow(contextData);
        }

        public void OnHideWindow(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.GetComponent<UIChat>().HideWindow();
        }

        public void BeforeUnload(UIBaseWindow uiBaseWindow)
        {
        }
    }
}