﻿using UnityEngine;
using System;
using System.Collections.Generic;

namespace ET.Client
{
    public class UIComponentAwakeSystem: AwakeSystem<UIComponent>
    {
        protected override void Awake(UIComponent self)
        {
            self.Awake();
        }
    }

    public class UIComponentDestroySystem: DestroySystem<UIComponent>
    {
        protected override void Destroy(UIComponent self)
        {
            self.Destroy();
        }
    }

    [FriendOf(typeof (ShowWindowData))]
    [FriendOf(typeof (WindowCoreData))]
    [FriendOf(typeof (UIBaseWindow))]
    public static class UIComponentSystem
    {
        public static void Awake(this UIComponent self)
        {
            self.AddComponent<UIMask, bool>(true);
            self.IsPopStackWndStatus = false;
            self.AllWindowsDic?.Clear();
            self.VisibleWindowsDic?.Clear();
            self.StackWindowsQueue?.Clear();
            self.UIBaseWindowlistCached?.Clear();
        }

        /// <summary>
        /// 窗口是否是正在显示的 
        /// </summary>
        /// <OtherParam name="id"></OtherParam>
        /// <returns></returns>
        public static bool IsWindowVisible(this UIComponent self, WindowID id)
        {
            return self.VisibleWindowsDic.ContainsKey((int) id);
        }

        /// <summary>
        /// 根据泛型获得UI窗口逻辑组件
        /// </summary>
        /// <param name="self"></param>
        /// <param name="isNeedShowState"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetDlgLogic<T>(this UIComponent self, bool isNeedShowState = false) where T : Entity, IUILogic
        {
            WindowID windowsId = self.GetWindowIdByGeneric<T>();
            UIBaseWindow baseWindow = self.GetUIBaseWindow(windowsId);
            if (null == baseWindow)
            {
                self.Fiber().Warning($"{windowsId} is not created!");
                return null;
            }

            if (!baseWindow.IsPreLoad)
            {
                self.Fiber().Warning($"{windowsId} is not loaded!");
                return null;
            }

            if (isNeedShowState)
            {
                if (!self.IsWindowVisible(windowsId))
                {
                    self.Fiber().Warning($"{windowsId} is need show state!");
                    return null;
                }
            }

            return baseWindow.GetComponent<T>();
        }

        /// <summary>
        /// 根据泛型类型获取窗口Id
        /// </summary>
        /// <param name="self"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static WindowID GetWindowIdByGeneric<T>(this UIComponent self) where T : Entity
        {
            if (UIPath.Instance.WindowTypeIdDict.TryGetValue(typeof (T).Name, out int windowsId))
            {
                return (WindowID) windowsId;
            }

            self.Fiber().Error($"{typeof (T).FullName} is not have any windowId!");
            return WindowID.Win_Invaild;
        }

        /// <summary>
        /// 压入一个进栈队列界面
        /// </summary>
        /// <param name="self"></param>
        /// <typeparam name="T"></typeparam>
        public static void ShowStackWindow<T>(this UIComponent self) where T : Entity, IUILogic
        {
            WindowID id = self.GetWindowIdByGeneric<T>();
            self.ShowStackWindow(id);
        }

        /// <summary>
        /// 压入一个进栈队列界面
        /// </summary>
        /// <param name="self"></param>
        /// <param name="id"></param>
        public static void ShowStackWindow(this UIComponent self, WindowID id)
        {
            self.StackWindowsQueue.Enqueue(id);

            if (self.IsPopStackWndStatus)
            {
                return;
            }

            self.IsPopStackWndStatus = true;
            self.PopStackUIBaseWindow();
        }

        /// <summary>
        /// 弹出并显示一个栈队列中的界面
        /// </summary>
        /// <param name="self"></param>
        private static void PopStackUIBaseWindow(this UIComponent self)
        {
            if (self.StackWindowsQueue.Count > 0)
            {
                WindowID windowID = self.StackWindowsQueue.Dequeue();
                self.ShowWindow(windowID);
                UIBaseWindow uiBaseWindow = self.GetUIBaseWindow(windowID);
                uiBaseWindow.IsInStackQueue = true;
            }
            else
            {
                self.IsPopStackWndStatus = false;
            }
        }

        /// <summary>
        /// 弹出并显示下一个栈队列中的界面
        /// </summary>
        /// <param name="self"></param>
        /// <param name="id"></param>
        private static void PopNextStackUIBaseWindow(this UIComponent self, WindowID id)
        {
            UIBaseWindow uiBaseWindow = self.GetUIBaseWindow(id);
            if (uiBaseWindow is { IsDisposed: false } && self.IsPopStackWndStatus && uiBaseWindow.IsInStackQueue)
            {
                uiBaseWindow.IsInStackQueue = false;
                self.PopStackUIBaseWindow();
            }
        }

        /// <summary>
        /// 根据指定Id的显示UI窗口
        /// </summary>
        /// <OtherParam name="id"></OtherParam>
        /// <OtherParam name="showData"></OtherParam>
        public static void ShowWindow(this UIComponent self, WindowID id, ShowWindowData showData = null)
        {
            UIBaseWindow baseWindow = self.ReadyToShowBaseWindow(id);
            if (null != baseWindow)
            {
                self.RealShowWindow(baseWindow, id, showData);
            }
        }

        /// <summary>
        /// 根据泛型类型显示UI窗口
        /// </summary>
        /// <param name="self"></param>
        /// <param name="showData"></param>
        /// <typeparam name="T"></typeparam>
        public static void ShowWindow<T>(this UIComponent self, ShowWindowData showData = null) where T : Entity, IUILogic
        {
            WindowID windowsId = self.GetWindowIdByGeneric<T>();
            self.ShowWindow(windowsId, showData);
        }

        /// <summary>
        /// 根据指定Id的异步加载显示UI窗口
        /// </summary>
        /// <param name="self"></param>
        /// <param name="id"></param>
        /// <param name="showData"></param>
        public static async ETTask ShowWindowAsync(this UIComponent self, WindowID id, ShowWindowData showData = null)
        {
            UIBaseWindow baseWindow = self.GetUIBaseWindow(id);
            try
            {
                baseWindow = await self.ShowBaseWindowAsync(id);
                if (null != baseWindow)
                {
                    self.RealShowWindow(baseWindow, id, showData);
                }
            }
            catch (Exception e)
            {
                self.Fiber().Error(e);
            }
        }

        /// <summary>
        /// 根据泛型类型异步加载显示UI窗口
        /// </summary>
        /// <param name="self"></param>
        /// <param name="showData"></param>
        /// <typeparam name="T"></typeparam>
        public static async ETTask ShowWindowAsync<T>(this UIComponent self, ShowWindowData showData = null) where T : Entity, IUILogic
        {
            WindowID windowsId = self.GetWindowIdByGeneric<T>();
            await self.ShowWindowAsync(windowsId, showData);
        }

        /// <summary>
        /// 隐藏ID指定的UI窗口
        /// </summary>
        /// <OtherParam name="id"></OtherParam>
        /// <OtherParam name="onComplete"></OtherParam>
        public static void HideWindow(this UIComponent self, WindowID id)
        {
            if (!self.VisibleWindowsDic.ContainsKey((int) id))
            {
                self.Fiber().Warning($"检测关闭 WindowsID: {id} 失败！");
                return;
            }

            UIBaseWindow baseWindow = self.VisibleWindowsDic[(int) id];
            if (baseWindow == null || baseWindow.IsDisposed)
            {
                self.Fiber().Error($"UIBaseWindow is null  or isDisposed ,  WindowsID: {id} 失败！");
                return;
            }

            baseWindow.UIPrefabGameObject?.SetActive(false);
            UIEvent.Instance.GetUIEventHandler(id).OnHideWindow(baseWindow);

            self.VisibleWindowsDic.Remove((int) id);

            self.PopNextStackUIBaseWindow(id);
        }

        /// <summary>
        /// 根据泛型类型隐藏UI窗口
        /// </summary>
        /// <param name="self"></param>
        /// <typeparam name="T"></typeparam>
        public static void HideWindow<T>(this UIComponent self) where T : Entity
        {
            WindowID hideWindowId = self.GetWindowIdByGeneric<T>();
            self.HideWindow(hideWindowId);
        }

        /// <summary>
        /// 卸载指定的UI窗口实例
        /// </summary>
        /// <OtherParam name="id"></OtherParam>
        public static void UnLoadWindow(this UIComponent self, WindowID id, bool isDispose = true)
        {
            UIBaseWindow baseWindow = self.GetUIBaseWindow(id);
            if (null == baseWindow)
            {
                self.Fiber().Error($"UIBaseWindow WindowId {id} is null!!!");
                return;
            }

            UIEvent.Instance.GetUIEventHandler(id).BeforeUnload(baseWindow);
            if (baseWindow.IsPreLoad)
            {
                UnityEngine.Object.Destroy(baseWindow.UIPrefabGameObject);
                baseWindow.UIPrefabGameObject = null;
            }

            if (isDispose)
            {
                self.AllWindowsDic.Remove((int) id);
                self.VisibleWindowsDic.Remove((int) id);
                baseWindow.Dispose();
            }
        }

        /// <summary>
        /// 根据泛型类型卸载UI窗口实例
        /// </summary>
        /// <param name="self"></param>
        /// <typeparam name="T"></typeparam>
        public static void UnLoadWindow<T>(this UIComponent self) where T : Entity, IUILogic
        {
            WindowID hideWindowId = self.GetWindowIdByGeneric<T>();
            self.UnLoadWindow(hideWindowId);
        }

        private static UIBaseWindow ReadyToShowBaseWindow(this UIComponent self, WindowID id)
        {
            UIBaseWindow baseWindow = self.GetUIBaseWindow(id);

            // 如果UI不存在开始实例化新的窗口
            if (baseWindow == null)
            {
                baseWindow = self.AddChild<UIBaseWindow>();
                baseWindow.WindowID = id;
                self.LoadBaseWindows(baseWindow);
            }

            if (!baseWindow.IsPreLoad)
            {
                self.LoadBaseWindows(baseWindow);
            }

            return baseWindow;
        }

        private static async ETTask<UIBaseWindow> ShowBaseWindowAsync(this UIComponent self, WindowID id)
        {
            CoroutineLock coroutineLock = null;
            try
            {
                coroutineLock = await self.Fiber().CoroutineLockComponent.Wait(CoroutineLockType.LoadUIBaseWindows, (int) id);
                UIBaseWindow baseWindow = self.GetUIBaseWindow(id);
                if (null == baseWindow)
                {
                    if (UIPath.Instance.WindowPrefabPath.ContainsKey((int) id))
                    {
                        baseWindow = self.AddChild<UIBaseWindow>();
                        baseWindow.WindowID = id;
                        await self.LoadBaseWindowsAsync(baseWindow);
                    }
                }

                if (!baseWindow.IsPreLoad)
                {
                    await self.LoadBaseWindowsAsync(baseWindow);
                }

                return baseWindow;
            }
            catch (Exception e)
            {
                self.Fiber().Error(e);
                return default;
            }
            finally
            {
                coroutineLock?.Dispose();
            }
        }

        private static void RealShowWindow(this UIComponent self, UIBaseWindow baseWindow, WindowID id, ShowWindowData showData = null)
        {
            baseWindow.UIPrefabGameObject?.SetActive(true);
            UIEvent.Instance.GetUIEventHandler(id).OnShowWindow(baseWindow, showData);

            self.VisibleWindowsDic[(int) id] = baseWindow;
            self.Fiber().Info("<color=magenta>### current Navigation window </color>" + baseWindow.WindowID);
        }

        public static void Destroy(this UIComponent self)
        {
            self.CloseAllWindow();
        }

        /// <summary>
        /// 根据窗口Id获取UIBaseWindow
        /// </summary>
        /// <param name="self"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private static UIBaseWindow GetUIBaseWindow(this UIComponent self, WindowID id)
        {
            if (self.AllWindowsDic.ContainsKey((int) id))
            {
                return self.AllWindowsDic[(int) id];
            }

            return null;
        }

        /// <summary>
        /// 根据窗口Id隐藏并完全关闭卸载UI窗口实例
        /// </summary>
        /// <param name="self"></param>
        /// <param name="windowId"></param>
        public static void CloseWindow(this UIComponent self, WindowID windowId)
        {
            if (!self.VisibleWindowsDic.ContainsKey((int) windowId))
            {
                return;
            }

            self.HideWindow(windowId);
            self.UnLoadWindow(windowId);
            self.Fiber().Info("<color=magenta>## close window without PopNavigationWindow() ##</color>");
        }

        /// <summary>
        /// 根据窗口泛型类型隐藏并完全关闭卸载UI窗口实例
        /// </summary>
        /// <param name="self"></param>
        /// <typeparam name="T"></typeparam>
        public static void CloseWindow<T>(this UIComponent self) where T : Entity, IUILogic
        {
            WindowID hideWindowId = self.GetWindowIdByGeneric<T>();
            self.CloseWindow(hideWindowId);
        }

        /// <summary>
        /// 关闭并卸载所有的窗口实例
        /// </summary>
        /// <param name="self"></param>
        public static void CloseAllWindow(this UIComponent self)
        {
            self.IsPopStackWndStatus = false;
            if (self.AllWindowsDic == null)
            {
                return;
            }

            foreach (KeyValuePair<int, UIBaseWindow> window in self.AllWindowsDic)
            {
                UIBaseWindow baseWindow = window.Value;
                if (baseWindow == null || baseWindow.IsDisposed)
                {
                    continue;
                }

                self.HideWindow(baseWindow.WindowID);
                self.UnLoadWindow(baseWindow.WindowID, false);
                baseWindow.Dispose();
            }

            self.AllWindowsDic.Clear();
            self.VisibleWindowsDic.Clear();
            self.StackWindowsQueue.Clear();
            self.UIBaseWindowlistCached.Clear();
        }

        /// <summary>
        /// 隐藏所有已显示的窗口
        /// </summary>
        /// <param name="self"></param>
        /// <param name="includeFixed"></param>
        public static void HideAllShownWindow(this UIComponent self, bool includeFixed = false)
        {
            self.IsPopStackWndStatus = false;
            self.UIBaseWindowlistCached.Clear();
            foreach (KeyValuePair<int, UIBaseWindow> window in self.VisibleWindowsDic)
            {
                if (window.Value.WindowData.windowType == UIWindowType.Fixed && !includeFixed)
                    continue;
                if (window.Value.IsDisposed)
                {
                    continue;
                }

                self.UIBaseWindowlistCached.Add((WindowID) window.Key);
                window.Value.UIPrefabGameObject?.SetActive(false);
                UIEvent.Instance.GetUIEventHandler(window.Value.WindowID).OnHideWindow(window.Value);
            }

            if (self.UIBaseWindowlistCached.Count > 0)
            {
                for (int i = 0; i < self.UIBaseWindowlistCached.Count; i++)
                {
                    self.VisibleWindowsDic.Remove((int) self.UIBaseWindowlistCached[i]);
                }
            }

            self.StackWindowsQueue.Clear();
        }

        private static Transform GetTargetRoot(this UIComponent self, UIWindowType type)
        {
            switch (type)
            {
                case UIWindowType.Normal:
                    return Global.Instance.NormalRoot;
                case UIWindowType.Fixed:
                    return Global.Instance.FixedRoot;
                case UIWindowType.PopUp:
                    return Global.Instance.PopUpRoot;
                case UIWindowType.Other:
                    return Global.Instance.OtherRoot;
                default:
                    self.Fiber().Error("uiroot type is error: " + type);
                    return null;
            }
        }

        /// <summary>
        /// 同步加载UI窗口实例
        /// </summary>
        private static void LoadBaseWindows(this UIComponent self, UIBaseWindow baseWindow)
        {
            if (!UIPath.Instance.WindowPrefabPath.TryGetValue((int) baseWindow.WindowID, out string value))
            {
                self.Fiber().Error($"{baseWindow.WindowID} uiPath is not Exist!");
                return;
            }

            GameObject go = self.Root().GetComponent<ResourcesLoaderComponent>().LoadAsset<GameObject>(value.ToUIPath());
            baseWindow.UIPrefabGameObject = UnityEngine.Object.Instantiate(go);
            baseWindow.UIPrefabGameObject.name = go.name;

            UIEvent.Instance.GetUIEventHandler(baseWindow.WindowID).OnInitWindowCoreData(baseWindow);

            var root = self.GetTargetRoot(baseWindow.WindowData.windowType);
            self.GetComponent<UIMask>().SetAsLastSibling(root);
            baseWindow.SetRoot(root);
            baseWindow.UITransform.SetAsLastSibling();

            UIEvent.Instance.GetUIEventHandler(baseWindow.WindowID).OnInitComponent(baseWindow);
            UIEvent.Instance.GetUIEventHandler(baseWindow.WindowID).OnRegisterUIEvent(baseWindow);

            self.AllWindowsDic[(int) baseWindow.WindowID] = baseWindow;
        }

        /// <summary>
        /// 异步加载UI窗口实例
        /// </summary>
        private static async ETTask LoadBaseWindowsAsync(this UIComponent self, UIBaseWindow baseWindow)
        {
            if (!UIPath.Instance.WindowPrefabPath.TryGetValue((int) baseWindow.WindowID, out string value))
            {
                self.Fiber().Error($"{baseWindow.WindowID} is not Exist!");
                return;
            }

            GameObject go = await self.Root().GetComponent<ResourcesLoaderComponent>().LoadAssetAsync<GameObject>(value.ToUIPath());
            baseWindow.UIPrefabGameObject = UnityEngine.Object.Instantiate(go);
            baseWindow.UIPrefabGameObject.name = go.name;

            UIEvent.Instance.GetUIEventHandler(baseWindow.WindowID).OnInitWindowCoreData(baseWindow);

            var root = self.GetTargetRoot(baseWindow.WindowData.windowType);
            self.GetComponent<UIMask>().SetAsLastSibling(root);
            baseWindow.SetRoot(root);
            baseWindow.UITransform.SetAsLastSibling();

            UIEvent.Instance.GetUIEventHandler(baseWindow.WindowID).OnInitComponent(baseWindow);
            UIEvent.Instance.GetUIEventHandler(baseWindow.WindowID).OnRegisterUIEvent(baseWindow);

            self.AllWindowsDic[(int) baseWindow.WindowID] = baseWindow;
        }
    }
}