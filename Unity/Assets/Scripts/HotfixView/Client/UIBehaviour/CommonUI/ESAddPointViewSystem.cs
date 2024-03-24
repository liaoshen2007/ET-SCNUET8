using System;
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
		
		public static void Refresh(this ESAddPoint self,int numerictype)
		{
			string numericName = "属性";
			switch (numerictype)
			{
				case NumericType.Strength:
					numericName = "体质 ";
					break;
				case NumericType.Energy:
					numericName = "优美 ";
					break;
				case NumericType.Agility:
					numericName = "品德 ";
					break;
				case NumericType.Mind:
					numericName = "智力 ";
					break;
                
			}
            
			self.E_AddPointExtendText.text =numericName+ UnitHelper.GetMyUnitFromCurrentScene(self.Root().CurrentScene()).
					GetComponent<NumericComponent>().GetAsLong(numerictype);
		}

		public static void RegisterEvent(this ESAddPoint self,int numerictype)
		{

			self.EButton_AddPointButton.AddListenerAsync(() => { return self.RequestAddPoint(numerictype);});
		}

		public static async ETTask RequestAddPoint(this ESAddPoint self,int numerictype)
		{
			try
			{
				Log.Debug("ESAddPoint:"+numerictype);
				int errorcode = await NumericHelper.RequestAddAttributePoint(self.Root(), numerictype);
				if (errorcode!=ErrorCode.ERR_Success)
				{
					return;
				}
				Log.Debug("加点成功");
				//self.Root().GetComponent<UIComponent>().GetDlgLogic<UIRoleProperties>()?.Refresh();
				EventSystem.Instance.Publish(self.Root(), new RefreshRoleInfo() );
			}
			catch (Exception e)
			{
				Log.Debug(e.ToString());
			}

			await ETTask.CompletedTask;
		}
	}


}
