namespace ET.Client
{
	[FriendOf(typeof(UIRoleSelectViewComponent))]
	public class UIRoleSelectViewComponentAwakeSystem : AwakeSystem<UIRoleSelectViewComponent> 
	{
		protected override void Awake(UIRoleSelectViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().UITransform;
			self.RegisterCloseEvent(self.E_CloseBtnButton);
		}
	}


	[FriendOf(typeof(UIRoleSelectViewComponent))]
	public class UIRoleSelectViewComponentDestroySystem : DestroySystem<UIRoleSelectViewComponent> 
	{
		protected override void Destroy(UIRoleSelectViewComponent self)
		{
			self.DestroyWidget();

		}
	}
}
