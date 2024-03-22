namespace ET.Client
{
	[FriendOf(typeof(WindowCoreData))]
	[FriendOf(typeof(UIBaseWindow))]
	[AUIEvent(WindowID.Win_UIRoleProperties)]
	public  class UIRolePropertiesEventHandler : IAUIEventHandler
	{
		public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.WindowData.WindowType = UIWindowType.Normal; 
		}

		public void OnInitComponent(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.AddComponent<UIRolePropertiesViewComponent>(); 
			uiBaseWindow.AddComponent<UIRoleProperties>(); 
		}

		public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.GetComponent<UIRoleProperties>().RegisterUIEvent(); 
		}

		public void OnFocus(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.GetComponent<UIRoleProperties>().Focus(); 
		}

		public void OnUnFocus(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.GetComponent<UIRoleProperties>().UnFocus(); 
		}

		public void OnShowWindow(UIBaseWindow uiBaseWindow, Entity contextData = null)
		{
			uiBaseWindow.GetComponent<UIRoleProperties>().ShowWindow(contextData); 
		}

		public void OnHideWindow(UIBaseWindow uiBaseWindow)
		{

		}

		public void BeforeUnload(UIBaseWindow uiBaseWindow)
		{

		}
	}
}
