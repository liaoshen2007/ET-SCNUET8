using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[EntitySystemOf(typeof(ESAddPoint))]
	[FriendOf(typeof(ESAddPoint))]
	public static partial class ESAddPointSystem 
	{
		[EntitySystem]
		private static void Awake(this ESAddPoint self, Transform transform)
		{
			self.uiTransform = transform;
		}

		[EntitySystem]
		private static void Destroy(this ESAddPoint self)
		{
			self.DestroyWidget();
		}
	}


}
