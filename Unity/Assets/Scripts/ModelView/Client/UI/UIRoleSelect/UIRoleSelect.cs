using System.Collections.Generic;

namespace ET.Client
{
	[ComponentOf(typeof(UIBaseWindow))]
	public  class UIRoleSelect : Entity, IAwake, IUILogic
	{
		public UIRoleSelectViewComponent View { get => GetParent<UIBaseWindow>().GetComponent<UIRoleSelectViewComponent>();} 
		
		public Dictionary<int, Scroll_Item_Role> ItemRolesDic;
	}
}
