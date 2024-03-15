using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof(UIRoleSelect))]
    [FriendOfAttribute(typeof(ET.Client.RoleInfoComponent))]
    public static partial class UIRoleSelectSystem
    {
        public static void RegisterUIEvent(this UIRoleSelect self)
        {
            self.View.E_CloseBtnButton.AddListenerAsync(self.OnBackClick);
            self.View.E_RoleListLoopHorizontalScrollRect.AddItemRefreshListener(self.OnScrollItemRefreshHandler);
            self.View.E_EnterGameButton.AddListenerAsync(self.OnEnterGameClickHandler);
            self.View.E_CreateRoleButton.AddListenerAsync(self.OnCreateRoleClickHandler);
            self.View.E_DeleteRoleButton.AddListenerAsync(self.OnDeleteRoleClickHandler);
        }

        public static void Focus(this UIRoleSelect self)
        {

        }

        public static void UnFocus(this UIRoleSelect self)
        {

        }

        public static void ShowWindow(this UIRoleSelect self, Entity contextData = null)
        {
            var roleCom = self.Root().GetComponent<RoleInfoComponent>();
            int count = roleCom.RoleInfos.Count;
            self.AddUIScrollItemsWithRef(ref self.ItemRolesDic, count);
            self.View.E_RoleListLoopHorizontalScrollRect.SetVisible(true, count);
        }

        private static async ETTask OnBackClick(this UIRoleSelect self)
        {
            await self.Scene().GetComponent<UIComponent>().ShowWindowAsync(WindowID.Win_UILogin);
            self.Scene().GetComponent<UIComponent>().HideWindow(WindowID.Win_UIRoleSelect);
        }

        private static void OnScrollItemRefreshHandler(this UIRoleSelect self, Transform transform, int index)
        {
            var roleItem = self.ItemRolesDic[index].BindTrans(transform);
            RoleInfo info = self.Scene().GetComponent<RoleInfoComponent>().RoleInfos[index];
            roleItem.E_NameExtendText.SetText(info.Name);
            roleItem.E_RoleImgButton.AddListener(() => { self.OnSelectRoleItemHandler(info.RoleId); });
        }
        
        public static void RefreshRoleItems(this UIRoleSelect self)
        {
            int count = self.Root().GetComponent<RoleInfoComponent>().RoleInfos.Count;
            self.AddUIScrollItemsWithRef(ref self.ItemRolesDic,count);
            self.View.E_RoleListLoopHorizontalScrollRect.SetVisible(true,count);
        }
        
        private static void OnSelectRoleItemHandler(this UIRoleSelect self, long roleId)
        {
            self.Scene().GetComponent<RoleInfoComponent>().CurrentRoleId = roleId;
            Log.Debug($"当前选择的角色 Id 是:{roleId}");
        }
        
        public static async ETTask OnEnterGameClickHandler(this UIRoleSelect self)
        {
            var currentRoleId = self.Root().GetComponent<RoleInfoComponent>().CurrentRoleId;
            if (currentRoleId==0)
            {
                Log.Error("未选择角色");
                return;
            }

            try
            {
                //bug et6的代码
                // int errcode = await LoginHelper.GetRealmKey(self.DomainScene());
                // if (errcode!=ErrorCode.ERR_Success)
                // {
                //     Log.Debug(errcode.ToString());
                //     return;
                // }
                //
                // errcode = await LoginHelper.EnterGame(self.DomainScene());
                // if (errcode!=ErrorCode.ERR_Success)
                // {
                //     Log.Debug(errcode.ToString());
                //     return;
                // }


                //int errcode = await EnterMapHelper.EnterMapAsync(self.Root());
                await self.Root().GetComponent<UIComponent>().ShowWindowAsync(WindowID.Win_UILoading);
                int errcode = await EnterMapHelper.EnterGameMap(self.Root(),currentRoleId);
                if (errcode!=ErrorCode.ERR_Success)
                {
                    Log.Debug(errcode.ToString());
                    return;
                }
                //todo 关掉角色窗口打开游戏主界面
                //self.DomainScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_MainView);
                UIHelper.PopMsg(self.Root(),LanguageCategory.Instance.Get(20001).Msg);

                self.Scene().GetComponent<UIComponent>().HideWindow(WindowID.Win_UIRoleSelect);

            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
			
			
        }

        public static async ETTask OnCreateRoleClickHandler(this UIRoleSelect self)
        {
            string name = self.View.E_NewRoleNameInputField.text;
            if (string.IsNullOrEmpty(name))
            {
                Log.Error("Name is null");
                return;
            }

            try
            {
                int errorcode = await LoginHelper.CreateHttpRoles(self.Root(), name);
                if (errorcode!=ErrorCode.ERR_Success)
                {
                    Log.Error(errorcode.ToString());
                    return;
                }
                self.RefreshRoleItems();

            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
			
            await ETTask.CompletedTask;
        }
		
        public static async ETTask OnDeleteRoleClickHandler(this UIRoleSelect self)
        {
            if (self.Root().GetComponent<RoleInfoComponent>().CurrentRoleId==0)
            {
                Log.Error("请选择需要删除的角色");
                return;
            }

            try
            {
                int errorcode = await LoginHelper.DeleteHttpRole(self.Root());
                if (errorcode!=ErrorCode.ERR_Success)
                {
                    Log.Error(errorcode.ToString());
                    return;
                }
                self.RefreshRoleItems();
				
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
			
			
            await ETTask.CompletedTask;
        }


    }
}
