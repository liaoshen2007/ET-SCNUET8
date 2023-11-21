namespace ET.Client
{
    [FriendOf(typeof (WindowCoreData))]
    [FriendOf(typeof (UIBaseWindow))]
    [AUIEvent(WindowID.Win_UILoading)]
    public class UILoadingEventHandler: IAUIEventHandler
    {
        public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.WindowData.WindowType = UIWindowType.Fixed;
            uiBaseWindow.WindowData.NeedMask = false;
        }

        public void OnInitComponent(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.AddComponent<UILoadingViewComponent>();
            uiBaseWindow.AddComponent<UILoading>();
        }

        public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)
        {
            uiBaseWindow.GetComponent<UILoading>().RegisterUIEvent();
        }

        public void OnFocus(UIBaseWindow uiBaseWindow)
        {
        }

        public void OnUnFocus(UIBaseWindow uiBaseWindow)
        {
        }

        public void OnShowWindow(UIBaseWindow uiBaseWindow, Entity contextData = null)
        {
            uiBaseWindow.GetComponent<UILoading>().ShowWindow(contextData);
        }

        public void OnHideWindow(UIBaseWindow uiBaseWindow)
        {
        }

        public void BeforeUnload(UIBaseWindow uiBaseWindow)
        {
        }
    }
}