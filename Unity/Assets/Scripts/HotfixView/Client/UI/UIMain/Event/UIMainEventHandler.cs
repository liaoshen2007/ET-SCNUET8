namespace ET.Client
{
	[FriendOf(typeof(WindowCoreData))]
	[FriendOf(typeof(UIBaseWindow))]
	[AUIEvent(WindowID.Win_UIMain)]
	public  class UIMainEventHandler : IAUIEventHandler
	{
		public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.WindowData.WindowType = UIWindowType.Normal; 
		}

		public void OnInitComponent(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.AddComponent<UIMainViewComponent>(); 
			uiBaseWindow.AddComponent<UIMain>(); 
		}

		public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.GetComponent<UIMain>().RegisterUIEvent(); 
		}
		
		public void OnFocus(UIBaseWindow uiBaseWindow)
		{
			
		}

		public void OnUnFocus(UIBaseWindow uiBaseWindow)
		{
			
		}

		public void OnShowWindow(UIBaseWindow uiBaseWindow, Entity contextData = null)
		{
			uiBaseWindow.GetComponent<UIMain>().ShowWindow(contextData); 
		}

		public void OnHideWindow(UIBaseWindow uiBaseWindow)
		{

		}

		public void BeforeUnload(UIBaseWindow uiBaseWindow)
		{

		}
	}
}
