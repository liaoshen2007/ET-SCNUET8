using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof (UIChat))]
    public static partial class UIChatSystem
    {
        public static void RegisterUIEvent(this UIChat self)
        {
            self.moveTween = self.View.EG_ChatRectTransform.GetComponent<TweenAnchorPosition>();
            self.moveTween.TweenComplete(self.MoveComplete);
            self.View.E_BackBtnButton.AddListener(self.BackBtnClick);
        }

        public static void ShowWindow(this UIChat self, Entity contextData = null)
        {
            self.moveTween.PlayForward();
        }

        private static void BackBtnClick(this UIChat self)
        {
            self.moveTween.PlayReverse();
        }

        private static void MoveComplete(this UIChat self, Tweener t)
        {
            if (!t.IsReverse)
            {
                self.Scene().GetComponent<UIComponent>().HideWindow<UIChat>();    
            }
        }
    }
}