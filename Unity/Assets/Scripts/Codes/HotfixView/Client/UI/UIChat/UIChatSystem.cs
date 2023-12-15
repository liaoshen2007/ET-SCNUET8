using System.Collections.Generic;
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
    [FriendOf(typeof (ClientChatComponent))]
    public static partial class UIChatSystem
    {
        public static void RegisterUIEvent(this UIChat self)
        {
            self.moveTween = self.View.EG_ChatRectTransform.GetComponent<TweenAnchorPosition>();
            self.moveTween.TweenComplete(self.MoveComplete);
            self.View.E_BackBtnButton.AddListener(self.BackBtnClick);
            self.View.E_SettingBtnButton.AddListener(self.SettingBtnClick);

            self.View.E_MenuListLoopHorizontalScrollRect.AddMenuRefreshListener(self, SystemMenuType.Chat);
            self.View.E_MsgListLoopVerticalScrollRect.AddItemRefreshListener(self.MsgListRefresh);
        }

        public static void ShowWindow(this UIChat self, Entity contextData = null)
        {
            self.moveTween.PlayForward();
            self.View.E_TitleExtendText.SetTitle(WindowID.Win_UIChat);

            //菜单列表
            self.View.E_MenuListLoopHorizontalScrollRect.SetMenuVisible(self, SystemMenuType.Chat);
        }

        public static void HideWindow(this UIChat self)
        {
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

        private static void MsgListRefresh(this UIChat self, Transform transform, int idx)
        {
            int index = self.GetComponent<MenuDictComponent>().SelectId;
            List<ClientChatUnit> chatUnitList = self.GetChatUnitList(index);
            Scroll_Item_Chat item =  self.msgDic[idx].BindTrans(transform);
            item.DataId = idx;
            ClientChatUnit unit = chatUnitList[idx];
            item.Refresh(unit);
        }

        private static List<ClientChatUnit> GetChatUnitList(this UIChat self, int index)
        {
            List<ClientChatUnit> chatUnitList = null;
            switch (index)
            {
                //世界
                case 0:
                    chatUnitList = self.Scene().GetComponent<ClientChatComponent>().worldMsgList;
                    break;
                //帮会
                case 1:
                    chatUnitList = self.Scene().GetComponent<ClientChatComponent>().leagueMsgList;
                    break;
                case 2:
                    break;
            }

            return chatUnitList;
        }

        public static void AddMsg(this UIChat self, ClientChatUnit unit, bool animate = true)
        {
            int index = self.GetComponent<MenuDictComponent>().SelectId;
            List<ClientChatUnit> chatUnitList = self.GetChatUnitList(index);
            if (chatUnitList == null)
            {
                return;
            }
            
            self.View.E_MsgListLoopVerticalScrollRect.totalCount = chatUnitList.Count;
            Scroll_Item_Chat item = self.AddChild<Scroll_Item_Chat>();
            self.msgDic.Add(chatUnitList.Count - 1, item);
            if (animate)
            {
                self.View.E_MsgListLoopVerticalScrollRect.SrollToCellWithinTime(chatUnitList.Count - 1, 0.3f);
            }
            else
            {
                self.View.E_MsgListLoopVerticalScrollRect.SetVisible(true, chatUnitList.Count, true);
            }
        }

        public static void RefreshChatList(this UIChat self, int index)
        {
            List<ClientChatUnit> chatUnitList = self.GetChatUnitList(index);
            if (chatUnitList == null)
            {
                return;
            }

            self.AddUIScrollItems(self.msgDic, chatUnitList.Count);
            self.View.E_MsgListLoopVerticalScrollRect.SetVisible(true, chatUnitList.Count, true);
        }
    }
}