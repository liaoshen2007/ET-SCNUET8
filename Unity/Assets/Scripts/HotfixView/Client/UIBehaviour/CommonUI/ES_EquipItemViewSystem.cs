using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	[EntitySystemOf(typeof(ES_EquipItem))]
	[FriendOf(typeof(ES_EquipItem))]
	public static partial class ES_EquipItemSystem 
	{
		[EntitySystem]
		private static void Awake(this ES_EquipItem self, Transform transform)
		{
			self.uiTransform = transform;
		}

		[EntitySystem]
		private static void Destroy(this ES_EquipItem self)
		{
			self.DestroyWidget();
		}
	}


}
