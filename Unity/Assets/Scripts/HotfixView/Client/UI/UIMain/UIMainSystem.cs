﻿using System.Collections;
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

    }
}