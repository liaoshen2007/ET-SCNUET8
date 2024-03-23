using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof(UIRoleProperties))]
    [FriendOfAttribute(typeof(ET.Client.RoleInfoComponent))]
    public static partial class UIRolePropertiesSystem
    {
        public static void RegisterUIEvent(this UIRoleProperties self)
        {
            self.RegisterCloseEvent(self.View.EButton_CloseButton);
            self.View.ESAddPoint.RegisterEvent(NumericType.Strength);
            self.View.ESAddPoint1.RegisterEvent(NumericType.Energy);
            self.View.ESAddPoint2.RegisterEvent(NumericType.Agility);
            self.View.ESAddPoint3.RegisterEvent(NumericType.Mind);
            self.View.EButton_UpLevelButton.AddListenerAsync(self.OnUpRoleLevelHandler);

            self.View.ES_EquipItem_Head.RegisterEventHandler(EquipPosition.Head);
            self.View.ES_EquipItem_Cloths.RegisterEventHandler(EquipPosition.Clothes);
            self.View.ES_EquipItem_Shoes.RegisterEventHandler(EquipPosition.Shoes);
            self.View.ES_EquipItem_Shield.RegisterEventHandler(EquipPosition.Shield);
            self.View.ES_EquipItem_Ring.RegisterEventHandler(EquipPosition.Ring);
            self.View.ES_EquipItem_Weapon.RegisterEventHandler(EquipPosition.Weapon);
        }

        public static void Focus(this UIRoleProperties self)
        {

        }

        public static void UnFocus(this UIRoleProperties self)
        {

        }

        public static void ShowWindow(this UIRoleProperties self, Entity contextData = null)
        {
            self.Refresh();
            self.RefreshEquipShowItems();

            Unit unit = UnitHelper.GetMyUnitFromClientScene(self.Root());

            //todo 下面最好做成一个通用接口！！
            var curRoleInfo = self.Root().GetComponent<RoleInfoComponent>().RoleInfos.Find(x => x.Id == unit.Id);
            self.View.ELabel_NickNameExtendText.SetText(curRoleInfo.Name);
        }

        public static async ETTask OnUpRoleLevelHandler(this UIRoleProperties self)
        {
            try
            {
            	int errorcode = await NumericHelper.RequestRoleLevel(self.Root());
            	if (errorcode!=ErrorCode.ERR_Success)
            	{
            		return;
            	}
            }
            catch (Exception e)
            {
            	Log.Error(e.ToString());
            }

        }

        public static void RefreshEquipShowItems(this UIRoleProperties self)
        {
            self.View.ES_EquipItem_Head.RefreshShowItem(EquipPosition.Head);
            self.View.ES_EquipItem_Cloths.RefreshShowItem(EquipPosition.Clothes);
            self.View.ES_EquipItem_Shoes.RefreshShowItem(EquipPosition.Shoes);
            self.View.ES_EquipItem_Ring.RefreshShowItem(EquipPosition.Ring);
            self.View.ES_EquipItem_Shield.RefreshShowItem(EquipPosition.Shield);
            self.View.ES_EquipItem_Weapon.RefreshShowItem(EquipPosition.Weapon);

        }

        public static void Refresh(this UIRoleProperties self)
        {
            self.View.ESAddPoint.Refresh(NumericType.Strength);
            self.View.ESAddPoint1.Refresh(NumericType.Energy);
            self.View.ESAddPoint2.Refresh(NumericType.Agility);
            self.View.ESAddPoint3.Refresh(NumericType.Mind);
        
            NumericComponent numericComponent = UnitHelper.GetMyUnitNumericComponent(self.Root().CurrentScene());
            self.View.ELabel_LevelExtendText.SetText("Lv." + numericComponent.GetAsInt(NumericType.Level));
            self.View.ELabel_ArmorChangeExtendText.text = "战力值：" + numericComponent.GetAsLong(NumericType.CombatEffectiveness);
            self.View.ELabel_PointExtendText.text = "可加点数:" + numericComponent.GetAsInt(NumericType.AttrPoint);
        
            self.View.ELabel_DamageExtendText.text = "伤害：" + numericComponent.GetAsInt(NumericType.Attack);
            self.View.ELabel_HealthExtendText.text = "生命：" + numericComponent.GetAsInt(NumericType.Hp);
            self.View.ELabel_ManaExtendText.text = "魔法值：" + numericComponent.GetAsInt(NumericType.Mana);
            self.View.ELabel_ArmorExtendText.text = "护甲值：" + numericComponent.GetAsInt(NumericType.Defense);
        
            Numeric config = NumericCategory.Instance.Get(NumericType.Cirt);
        
            self.View.ELabel_CriticalChanceExtendText.text = config.Fight >100 ? "暴击率：" + numericComponent.GetAsFloat(NumericType.Cirt) :
                    "暴击率： " + numericComponent.GetAsFloat(NumericType.Cirt) + "%";
        
            //拓展GetShowCofigCount
            //int count = PlayerNumericConfigCategory.Instance.GetShowCofigCount();
        }
    }
}
