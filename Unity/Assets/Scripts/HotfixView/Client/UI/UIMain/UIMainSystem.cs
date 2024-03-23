using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof (UIMain))]
    public static partial class UIMainSystem
    {
        public static void RegisterUIEvent(this UIMain self)
        {
            self.View.E_ChatButton.AddListener(self.ChatBtnClick);
            self.View.E_RoleInfoButton.AddListener(self.RoleInfoClick);
            self.View.E_NormHitButton.AddListenerAsync(self.OnNormalHitClickHandler);
        }

        public static void ShowWindow(this UIMain self, Entity contextData = null)
        {
        }

        private static void ChatBtnClick(this UIMain self)
        {
            self.Scene().GetComponent<UIComponent>().ShowWindow<UIChat>().Coroutine();
        }

        private static void RoleInfoClick(this UIMain self)
        {
            self.Scene().GetComponent<UIComponent>().ShowWindow<UIRoleProperties>().Coroutine();
        }
        
        private static async ETTask OnNormalHitClickHandler(this UIMain self)
        {
            try
            {
                Unit unit = UnitHelper.GetMyUnitFromClientScene(self.Root());
                //self.ZoneScene().GetComponent<TextAttackViewComponent>().SpawnTextAttackEffect(unit.Position,unit.Forward,"Haige!Qihao!YanTing!").Coroutine();
                int error = await NumericHelper.TestUpdateNumeric(self.Root());
                if (error!=ErrorCode.ERR_Success)
                {
                    return;
                }
                Log.Debug("发送更新属性测试消息成功！");

            }
            catch (Exception e)
            {
                Log.Debug(e.ToString());
            }
			

            await ETTask.CompletedTask;
        }

    }
}