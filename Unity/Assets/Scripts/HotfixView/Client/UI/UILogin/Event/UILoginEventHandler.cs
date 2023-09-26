namespace ET.Client
{
	[FriendOf(typeof(WindowCoreData))]
	[FriendOf(typeof(UIBaseWindow))]
	[AUIEvent(WindowID.Win_Login)]
	public  class UILoginEventHandler : IAUIEventHandler
	{
		public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.WindowData.windowType = UIWindowType.Normal; 
		}

		public void OnInitComponent(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.AddComponent<UILoginViewComponent>(); 
			uiBaseWindow.AddComponent<UILogin>(); 
		}

		public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.GetComponent<UILogin>().RegisterUIEvent(); 
		}

		public void OnShowWindow(UIBaseWindow uiBaseWindow, Entity contextData = null)
		{
			uiBaseWindow.GetComponent<UILogin>().ShowWindow(contextData); 
		}

		public void OnHideWindow(UIBaseWindow uiBaseWindow)
		{
			 
		}

		public void BeforeUnload(UIBaseWindow uiBaseWindow)
		{

		}
	}
}
