namespace ET.Client
{
	[FriendOf(typeof(WindowCoreData))]
	[FriendOf(typeof(UIBaseWindow))]
	[AUIEvent(WindowID.Win_RoleList)]
	public  class UIRoleListEventHandler : IAUIEventHandler
	{
		public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.WindowData.windowType = UIWindowType.Normal; 
		}

		public void OnInitComponent(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.AddComponent<UIRoleListViewComponent>(); 
			uiBaseWindow.AddComponent<UIRoleList>(); 
		}

		public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.GetComponent<UIRoleList>().RegisterUIEvent(); 
		}

		public void OnShowWindow(UIBaseWindow uiBaseWindow, Entity contextData = null)
		{
			uiBaseWindow.GetComponent<UIRoleList>().ShowWindow(contextData); 
		}

		public void OnHideWindow(UIBaseWindow uiBaseWindow)
		{

		}

		public void BeforeUnload(UIBaseWindow uiBaseWindow)
		{

		}
	}
}
