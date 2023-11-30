using UnityEngine;
using UnityEngine.UI;
namespace ET.Client

{
	[EntitySystemOf(typeof(ESItem))]
	[FriendOf(typeof(ESItem))]
	public static partial class ESItemSystem 
	{
		[EntitySystem]
		private static void Awake(this ESItem self, Transform transform)
		{
			self.uiTransform = transform;
		}

		[EntitySystem]
		private static void Destroy(this ESItem self)
		{
			self.DestroyWidget();
		}
	}


}
