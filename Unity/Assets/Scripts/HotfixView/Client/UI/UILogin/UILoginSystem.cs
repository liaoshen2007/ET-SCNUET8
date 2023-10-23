using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof (UILogin))]
    public static class UILoginSystem
    {
        public static void RegisterUIEvent(this UILogin self)
        {
            self.View.E_LoginButtonButton.AddListenerAsync(self.OnLoginClick);
        }

        public static void ShowWindow(this UILogin self, Entity contextData = null)
        {
            self.View.E_AccountInputInputField.text = "sb0001";
            self.View.E_PasswordInputInputField.text = "123456";
        }

        private static async ETTask OnLoginClick(this UILogin self)
        {
            var errno = await LoginHelper.GetServerInfos(self.Scene(), self.View.E_AccountInputInputField.text);
            if (errno != ErrorCode.ERR_Success)
            {
                Log.Error($"获取服务器列表失败: {errno}");
                return;
            }

            self.Scene().GetComponent<UIComponent>().HideWindow(WindowID.Win_Login);
            await self.Scene().GetComponent<UIComponent>().ShowWindowAsync(WindowID.Win_ServerList);
        }
    }
}