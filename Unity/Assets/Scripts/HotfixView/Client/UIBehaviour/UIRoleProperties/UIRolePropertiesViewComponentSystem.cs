namespace ET.Client
{
	[FriendOf(typeof(UIRolePropertiesViewComponent))]
	public class UIRolePropertiesViewComponentAwakeSystem : AwakeSystem<UIRolePropertiesViewComponent> 
	{
		protected override void Awake(UIRolePropertiesViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().UITransform;
		}
	}


	[FriendOf(typeof(UIRolePropertiesViewComponent))]
	public class UIRolePropertiesViewComponentDestroySystem : DestroySystem<UIRolePropertiesViewComponent> 
	{
		protected override void Destroy(UIRolePropertiesViewComponent self)
		{
			self.DestroyWidget();

		}
	}
}
