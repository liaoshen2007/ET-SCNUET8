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
            var errno = await LoginHelper.Login(
                self.Root(), 
                self.View.E_AccountInputInputField.text,
                self.View.E_PasswordInputInputField.text);

            if (errno != ErrorCode.ERR_Success)
            {
                Log.Error($"登录失败: {errno}");
                return;
            }

            errno = await EnterMapHelper.EnterMapAsync(self.Root());
            if (errno != ErrorCode.ERR_Success)
            {
                Log.Error($"进入地图失败: {errno}");
            }
        }
    }
}