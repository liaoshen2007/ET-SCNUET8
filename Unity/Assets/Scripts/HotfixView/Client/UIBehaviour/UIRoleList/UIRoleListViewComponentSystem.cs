namespace ET.Client
{
	[FriendOf(typeof(UIRoleListViewComponent))]
	public class UIRoleListViewComponentAwakeSystem : AwakeSystem<UIRoleListViewComponent> 
	{
		protected override void Awake(UIRoleListViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().UITransform;
		}
	}


	[FriendOf(typeof(UIRoleListViewComponent))]
	public class UIRoleListViewComponentDestroySystem : DestroySystem<UIRoleListViewComponent> 
	{
		protected override void Destroy(UIRoleListViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
