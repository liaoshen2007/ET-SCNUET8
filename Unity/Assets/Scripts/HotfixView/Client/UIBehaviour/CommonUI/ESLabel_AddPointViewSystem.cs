using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[EntitySystemOf(typeof(ESLabel_AddPoint))]
	[FriendOf(typeof(ESLabel_AddPoint))]
	public static partial class ESLabel_AddPointSystem 
	{
		[EntitySystem]
		private static void Awake(this ESLabel_AddPoint self, Transform transform)
		{
			self.uiTransform = transform;
		}

		[EntitySystem]
		private static void Destroy(this ESLabel_AddPoint self)
		{
			self.DestroyWidget();
		}
	}


}
