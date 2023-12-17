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
            self.emojiTween = self.View.EG_AnimRectTransform.GetComponent<TweenAnchorPosition>();
            self.View.E_BackBtnButton.AddListener(self.BackBtnClick);
            self.View.E_SettingBtnButton.AddListener(self.SettingBtnClick);
            self.View.E_SendButton.AddListener(self.SendBtnClick);
            self.View.E_EmjoButton.AddListener(self.EmjoBtnClick);
            self.View.E_CloseEmojButton.AddListener(self.CloseEmjoBtnClick);

            self.View.E_InputInputField.onSubmit.AddListener(self.OnSubmit);

            self.View.E_MenuListLoopHorizontalScrollRect.AddMenuRefreshListener(self, SystemMenuType.Chat);
            self.View.E_MsgListLoopVerticalScrollRect.AddItemRefreshListener(self.MsgListRefresh);
            self.View.E_EmotionMeuListLoopHorizontalScrollRect.AddMenuRefreshListener(self, SystemMenuType.ChatEmojMenu);
        }

        public static void ShowWindow(this UIChat self, Entity contextData = null)
        {
            self.View.E_CloseEmojButton.SetActive(false);
            self.moveTween.PlayForward();
            self.View.E_TitleExtendText.SetTitle(WindowID.Win_UIChat);

            //菜单列表
            self.View.E_MenuListLoopHorizontalScrollRect.SetMenuVisible(self, SystemMenuType.Chat);
            self.View.E_EmotionMeuListLoopHorizontalScrollRect.SetMenuVisible(self, SystemMenuType.ChatEmojMenu);
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

        private static void SendBtnClick(this UIChat self)
        {
            string msg = self.View.E_InputInputField.text;
            if (string.IsNullOrEmpty(msg))
            {
                return;
            }

            int index = self.GetChild<MenuDict>(SystemMenuType.Chat).SelectId;
            C2Chat_SendRequest chat = C2Chat_SendRequest.Create();
            chat.Message = msg;
            chat.RoleInfo = new PlayerInfoProto()
            {
                Id = self.Scene().GetComponent<PlayerComponent>().MyId,
                Name = "222222",
                Level = 1,
                Fight = 1,
                HeadIcon = default,
            };
            switch (index)
            {
                //世界
                case 0:
                    chat.Channel = (int)ChatChannelType.World;
                    chat.GroupId = ConstValue.ChatSendId;
                    break;
                //帮会
                case 1:
                    chat.Channel = (int)ChatChannelType.League;
                    break;
            }

            self.Root().GetComponent<ClientSenderCompnent>().Send(chat);
            self.View.E_InputInputField.text = string.Empty;
        }

        private static void EmjoBtnClick(this UIChat self)
        {
            self.emojiTween.PlayForward();
            self.View.E_CloseEmojButton.SetActive(true);
        }

        private static void CloseEmjoBtnClick(this UIChat self)
        {
            self.View.E_CloseEmojButton.SetActive(false);
            self.emojiTween.PlayReverse();
        }

        private static void OnSubmit(this UIChat self, string value)
        {
            self.SendBtnClick();
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
            int index = self.GetChild<MenuDict>(SystemMenuType.Chat).SelectId;
            List<ClientChatUnit> chatUnitList = self.GetChatUnitList(index);
            Scroll_Item_Chat item = self.msgDic[idx].BindTrans(transform);
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
            int index = self.GetChild<MenuDict>(SystemMenuType.Chat).SelectId;
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
                self.View.E_MsgListLoopVerticalScrollRect.SrollToCell(chatUnitList.Count - 1, 700);
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