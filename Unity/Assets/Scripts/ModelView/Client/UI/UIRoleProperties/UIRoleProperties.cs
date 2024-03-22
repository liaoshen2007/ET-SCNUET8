namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public  class UIRoleProperties : Entity, IAwake, IUILogic
	{
		public UIRolePropertiesViewComponent View { get => GetParent<UIBaseWindow>().GetComponent<UIRolePropertiesViewComponent>();} 
	}
}
