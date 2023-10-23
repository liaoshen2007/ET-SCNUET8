using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
	public class Scroll_Item_ServerDestroySystem : DestroySystem<Scroll_Item_Server> 
	{
		protected override void Destroy( Scroll_Item_Server self )
		{
			self.DestroyWidget();
		}
	}
}
