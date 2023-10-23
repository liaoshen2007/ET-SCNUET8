namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public  class UIRoleList : Entity, IAwake, IUILogic
	{
		public UIRoleListViewComponent View { get => GetParent<UIBaseWindow>().GetComponent<UIRoleListViewComponent>();} 
	}
}
