using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[EntitySystemOf(typeof(Scroll_Item_Role))]
	[FriendOf(typeof(Scroll_Item_Role))]
	public static partial class Scroll_Item_RoleSystem 
	{
		[EntitySystem]
		private static void Awake(this Scroll_Item_Role self)
		{
		}

		[EntitySystem]
		private static void Destroy(this Scroll_Item_Role self)
		{
			self.DestroyWidget();
		}

		public static Scroll_Item_Role BindTrans(this Scroll_Item_Role self, Transform trans)
		{
			self.uiTransform = trans;
			return self;
		}
	}
}
