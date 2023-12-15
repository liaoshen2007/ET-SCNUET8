using System;
using System.Collections.Generic;

namespace ET.Client;

[EntitySystemOf(typeof (MenuComponent))]
[FriendOf(typeof (MenuComponent))]
public static partial class MenuComponentSystem
{
    [EntitySystem]
    private static void Awake(this MenuComponent self)
    {
        foreach (SystemMenuType value in Enum.GetValues(typeof (SystemMenuType)))
        {
            var list = new List<MeunData>();
            foreach (var cfg in SystemMenuCategory.Instance.GetList((int)value))
            {
                var menu = self.AddChild<MeunData, int>(cfg.Id);
                list.Add(menu);
            }

            list.Sort((a, b) => a.Config.Rank.CompareTo(b.Config.Rank));
            self.menuDict.Add(value, list);
        }
    }

    public static List<MeunData> GetMenuList(this MenuComponent self, SystemMenuType type)
    {
        if (self.menuDict.TryGetValue(type, out var list))
        {
            return list;
        }

        return new List<MeunData>();
    }
}