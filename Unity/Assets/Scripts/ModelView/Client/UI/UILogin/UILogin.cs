namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public  class UILogin : Entity, IAwake, IUILogic
	{
		public UILoginViewComponent View { get => GetParent<UIBaseWindow>().GetComponent<UILoginViewComponent>();} 
	}
}
