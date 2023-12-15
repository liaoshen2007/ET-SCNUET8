using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    public static class EUIModelViewHelper
    {
        public static void AddUIScrollItems<K, T>(this K self, Dictionary<int, T> dictionary, int count) where K : Entity, IUILogic
                where T : Entity, IAwake, IUIScrollItem
        {
            if (count <= 0)
            {
                return;
            }

            foreach (var item in dictionary)
            {
                item.Value.Dispose();
            }

            dictionary.Clear();
            for (int i = 0; i < count; i++)
            {
                T itemServer = self.AddChild<T>(true);
                dictionary.Add(i, itemServer);
            }
        }

        public static void SetMenuVisible<T>(this LoopScrollRect self, T entity, SystemMenuType menu) where T : Entity, IUILogic
        {
            var list = entity.Scene().GetComponent<MenuComponent>().GetMenuList(menu);
            MenuDictComponent c = entity.GetComponent<MenuDictComponent>();
            entity.AddUIScrollItems(c.MenuDic, list.Count);
            self.SetVisible(true, list.Count);
        }

        public static void AddMenuRefreshListener<T>(this LoopScrollRect self, T entity, SystemMenuType menu) where T : Entity
        {
            if (entity.GetComponent<MenuDictComponent>() == null)
            {
                entity.AddComponent<MenuDictComponent>();
            }

            self.AddItemRefreshListener((transform, i) => { MenuListRefresh(entity, transform, i, menu); });
        }

        private static void MenuListRefresh<T>(T self, Transform transform, int i, SystemMenuType menu) where T : Entity
        {
            var list = self.Scene().GetComponent<MenuComponent>().GetMenuList(menu);
            MenuDictComponent c = self.GetComponent<MenuDictComponent>();
            Scroll_Item_Menu item = c.MenuDic[i].BindTrans(transform);
            item.DataId = i;
            item.SetCacheMode(true);
            item.E_BtnButton.AddListener(() => { MenuBtnClick(self, i, menu); });
            item.Refresh(list[i], c.SelectId);
        }

        private static void MenuBtnClick<T>(T self, int i, SystemMenuType menu) where T : Entity
        {
            var list = self.Scene().GetComponent<MenuComponent>().GetMenuList(menu);
            MenuDictComponent c = self.GetComponent<MenuDictComponent>();
            c.SelectId = i;
            Scroll_Item_Menu item = c.MenuDic[i];
            item.Refresh(list[i], c.SelectId);
            item.RefreshAll(i);
        }
    }
}