using UnityEngine;

namespace ET.Client
{
    public enum ChatMsgKeyWord
    {
        Emjo,
        Quote,
        At,
        Item,
    }
    
    [FriendOf(typeof (UIChat))]
    public static partial class UIChatSystem
    {
        public static void RegisterUIEvent(this UIChat self)
        {
            self.moveTween = self.View.EG_ChatRectTransform.GetComponent<TweenAnchorPosition>();
            self.moveTween.TweenComplete(self.MoveComplete);
            self.View.E_BackBtnButton.AddListener(self.BackBtnClick);
            self.View.E_SettingBtnButton.AddListener(self.SettingBtnClick);

            self.View.E_MenuListLoopHorizontalScrollRect.AddItemRefreshListener(self.MenuListRefresh);
            self.View.E_MsgListLoopVerticalScrollRect.AddItemRefreshListener(self.MsgListRefresh);
            self.View.E_MsgListLoopVerticalScrollRect.AddPrefabListener(self.MsgPrefabRefresh);
        }

        public static void ShowWindow(this UIChat self, Entity contextData = null)
        {
            self.moveTween.PlayForward();
            self.View.E_TitleExtendText.SetTitle(WindowID.Win_UIChat);
        }

        private static void BackBtnClick(this UIChat self)
        {
            self.moveTween.PlayReverse();
        }

        //打开设置界面
        private static void SettingBtnClick(this UIChat self)
        {
        }

        private static void MoveComplete(this UIChat self, Tweener t)
        {
            if (!t.IsReverse)
            {
                self.Scene().GetComponent<UIComponent>().HideWindow<UIChat>();
            }
        }

        private static void MenuListRefresh(this UIChat self, Transform transform, int idx)
        {
        }

        private static void MsgListRefresh(this UIChat self, Transform transform, int idx)
        {
        }

        private static string MsgPrefabRefresh(this UIChat self, int idx)
        {
            return default;
        }
    }
}