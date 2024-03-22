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
		
		public static void RegisterEventHandler(this ES_EquipItem self,EquipPosition equipPosition)
		{
            
			self.E_SelectButton.AddListenerWithId(self.OnSelectHandler,(int)equipPosition);
		}

		public static void OnSelectHandler(this ES_EquipItem self,int equipPosition)
		{
			// Item item = self.ZoneScene().GetComponent<EquipmentsComponent>().GetItemByPosition((EquipPosition)equipPosition);
			//
			// if (null==item)
			// {
			// 	return;
			// }
   //          
			// self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_ItemPopUp);
			// self.ZoneScene().GetComponent<UIComponent>().GetDlgLogic<DlgItemPopUp>().RefreshInfo(item,ItemContainerType.RoleInfo);
		}

		public static void RefreshShowItem(this ES_EquipItem self,EquipPosition equipPosition)
		{
			// Item item = self.ZoneScene().GetComponent<EquipmentsComponent>().GetItemByPosition(equipPosition);
			// if (null!=item)
			// {
			// 	self.E_IconImage.overrideSprite = IconHelper.LoadIconSprite("UIAtlas_InventoryIcons", item.Config.Icon);
			// 	self.E_QualityImage.color = item.ItemQualityColor();
			// }
			// else
			// {
			// 	self.E_IconImage.sprite = null;
			// 	self.E_IconImage.overrideSprite = null;
			// 	self.E_QualityImage.color=Color.gray;
			// }
		}

		public static void RefreshShowItem(this ES_EquipItem self, int itemConfigId)
		{
			// self.E_QualityImage.color=Color.gray;
			// self.E_IconImage.overrideSprite = IconHelper.LoadIconSprite("UIAtlas_InventoryIcons", ItemConfigCategory.Instance.Get(itemConfigId).Icon);
		}
	}


}
