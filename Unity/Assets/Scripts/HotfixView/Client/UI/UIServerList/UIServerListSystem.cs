using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOf(typeof (ServerInfoComponent))]
    [FriendOf(typeof (UIServerList))]
    public static partial class UIServerListSystem
    {
        public static void RegisterUIEvent(this UIServerList self)
        {
            self.View.E_ConfirmButton.AddListenerAsync(self.OnConfirmClickHandler);
            self.View.E_ServerListLoopVerticalScrollRect.AddItemRefreshListener(self.OnScrollItemRefreshHandler);
        }

        public static void ShowWindow(this UIServerList self, Entity contextData = null)
        {
            var serverCom = self.Scene().GetComponent<ServerInfoComponent>();
            serverCom.CurrentServerId = 1;
            int count = serverCom.ServerInfoList.Count;
            self.AddUIScrollItems(ref self.ScrollItemServerTests, count);
            self.View.E_ServerListLoopVerticalScrollRect.SetVisible(true, count);
        }

        public static void HideWindow(this UIServerList self)
        {
            self.RemoveUIScrollItems(ref self.ScrollItemServerTests);
            self.View.E_ServerListLoopVerticalScrollRect.ClearPool();
        }

        private static void OnScrollItemRefreshHandler(this UIServerList self, Transform transform, int index)
        {
            var serverTest = self.ScrollItemServerTests[index].BindTrans(transform);
            ServerInfo info = self.Scene().GetComponent<ServerInfoComponent>().ServerInfoList[index];
            serverTest.E_SelectImage.color = info.Id == self.Scene().GetComponent<ServerInfoComponent>().CurrentServerId? Color.red : Color.cyan;
            serverTest.E_serverTestTipText.SetText(info.ServerName);
            serverTest.E_SelectButton.AddListener(() => { self.OnSelectServerItemHandler(info.Id); });
        }

        private static void OnSelectServerItemHandler(this UIServerList self, long serverId)
        {
            self.Scene().GetComponent<ServerInfoComponent>().CurrentServerId = int.Parse(serverId.ToString());
            Log.Debug($"当前选择的服务器 Id 是:{serverId}");
            self.View.E_ServerListLoopVerticalScrollRect.RefillCells();
        }

        private static async ETTask OnConfirmClickHandler(this UIServerList self)
        {
            bool isSelect = self.Scene().GetComponent<ServerInfoComponent>().CurrentServerId != 0;

            if (!isSelect)
            {
                Log.Error("请先选择区服");
                return;
            }

            try
            {
                int errorCode = await LoginHelper.GetRoles(self.Scene());
                if (errorCode != ErrorCode.ERR_Success)
                {
                    Log.Error(errorCode.ToString());
                    return;
                }

                self.Scene().GetComponent<UIComponent>().HideWindow(WindowID.Win_ServerList);
                await self.Scene().GetComponent<UIComponent>().ShowWindowAsync(WindowID.Win_RoleList);
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
        }
    }
}