namespace ET.Client
{
	[FriendOf(typeof(WindowCoreData))]
	[FriendOf(typeof(UIBaseWindow))]
	[AUIEvent(WindowID.Win_UIRoleSelect)]
	public  class UIRoleSelectEventHandler : IAUIEventHandler
	{
		public void OnInitWindowCoreData(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.WindowData.WindowType = UIWindowType.Normal; 
		}

		public void OnInitComponent(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.AddComponent<UIRoleSelectViewComponent>(); 
			uiBaseWindow.AddComponent<UIRoleSelect>(); 
		}

		public void OnRegisterUIEvent(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.GetComponent<UIRoleSelect>().RegisterUIEvent(); 
		}

		public void OnFocus(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.GetComponent<UIRoleSelect>().Focus(); 
		}

		public void OnUnFocus(UIBaseWindow uiBaseWindow)
		{
			uiBaseWindow.GetComponent<UIRoleSelect>().UnFocus(); 
		}

		public void OnShowWindow(UIBaseWindow uiBaseWindow, Entity contextData = null)
		{
			uiBaseWindow.GetComponent<UIRoleSelect>().ShowWindow(contextData); 
		}

		public void OnHideWindow(UIBaseWindow uiBaseWindow)
		{

		}

		public void BeforeUnload(UIBaseWindow uiBaseWindow)
		{

		}
	}
}
