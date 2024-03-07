using System.Collections.Generic;
using System.Linq;

namespace ET.Server
{
    [EntitySystemOf(typeof (ItemComponent))]
[FriendOf(typeof (ItemComponent))]
public static partial class ItemComponentSystem
{
    [EntitySystem]
    private static void Awake(this ItemComponent self)
    {
    }

    [EntitySystem]
    private static void Destroy(this ItemComponent self)
    {
    }

    [EntitySystem]
    private static void Load(this ItemComponent self)
    {
    }

    public static List<ItemProto> GetItemList(this ItemComponent self)
    {
        var list = new List<ItemProto>();
        foreach (var value in self.ItemDict.Values)
        {
            list.Add(value.ToItemProto());
        }

        return list;
    }

    public static void CheckItem(this ItemComponent self)
    {
        //检测默认道具
        var list = new List<ItemArg>();
        bool updateCache = false;
        foreach (var config in InitItemConfigCategory.Instance.GetAll())
        {
            if (!self.InitItemIds.Contains(config.Key))
            {
                list.Add(new ItemArg() { Id = config.Key, Count = config.Value.Count });
                self.InitItemIds.Add(config.Key);
                updateCache = true;
            }
        }

        if (list.Count > 0)
        {
            self.AddItemList(list, new AddItemData() { NotUpdate = true, LogEvent = LogDef.ItemInit });
        }

        //道具ID检测
        foreach (var itemData in self.ItemDict.Values)
        {
            if (ItemConfigCategory.Instance.Contain((int)itemData.Id))
            {
                continue;
            }

            updateCache = true;
            Log.Info($"道具因配置变化而删除, 道具ID: {itemData.Id}");
            self.ValidItemDict.Remove((int)itemData.Id);
            self.ClearItem((int)itemData.Id, LogDef.ItemConfigRemove);
        }

        if (updateCache)
        {
            self.UpdateCache().Coroutine();
        }
    }

    public static long GetItemCount(this ItemComponent self, int itemId)
    {
        if (self.ItemDict.TryGetValue(itemId, out var itemData))
        {
            return itemData.Count;
        }

        return 0;
    }

    private static void UpdateItem(this ItemComponent self, int itemId)
    {
        if (!self.ItemDict.TryGetValue(itemId, out var itemData))
        {
            self.GetParent<Unit>().GetComponent<PacketComponent>().UpdateItem(new ItemProto() { Id = itemId, Count = 0 });
            return;
        }

        var proto = itemData.ToItemProto();
        self.GetParent<Unit>().GetComponent<PacketComponent>().UpdateItem(proto);
    }

    public static void AddItemList(this ItemComponent self, List<ItemArg> itemList, AddItemData data)
    {
        if (itemList.IsNullOrEmpty())
        {
            return;
        }

        foreach (var arg in itemList)
        {
            if (arg.Count <= 0)
            {
                continue;
            }

            var count = arg.Count;
            if (self.ItemDict.TryGetValue(arg.Id, out var itemData))
            {
                itemData.Count += count;
            }
            else
            {
                itemData = self.AddChildWithId<ItemData>(arg.Id);
                itemData.Count = count;
                self.ItemDict.Add(arg.Id, itemData);
            }

            if (!data.NotUpdate)
            {
                self.UpdateItem(arg.Id);
            }

            EventSystem.Instance.Publish(self.Scene(), new AddItem() { ItemId = arg.Id, Count = count, LogEvent = data.LogEvent });
        }
    }

    /// <summary>
    /// 消耗道具列表
    /// </summary>
    /// <param name="self"></param>
    /// <param name="itemList"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static MessageReturn ConsumeItemList(this ItemComponent self, List<ItemArg> itemList, AddItemData data)
    {
        var ret = self.ItemEnough(itemList);
        if (ret.Errno != ErrorCode.ERR_Success)
        {
            return ret;
        }

        foreach (var arg in itemList)
        {
            if (arg.Count <= 0)
            {
                continue;
            }

            self.ItemDict[arg.Id].Count -= arg.Count;
            if (self.ItemDict[arg.Id].Count < 0)
            {
                Log.Error($"道具数量出现负数: {arg.Id}");
            }

            if (self.ItemDict[arg.Id].Count <= 0)
            {
                self.ItemDict.Remove(arg.Id);
            }

            self.UpdateItem(arg.Id);
            EventSystem.Instance.Publish(self.Scene(), new RemoveItem() { ItemId = arg.Id, Count = arg.Count, LogEvent = data.LogEvent });
        }

        return MessageReturn.Success();
    }

    /// <summary>
    /// 清除道具
    /// </summary>
    /// <param name="self"></param>
    /// <param name="itemId"></param>
    /// <param name="logEvent"></param>
    public static void ClearItem(this ItemComponent self, int itemId, int logEvent)
    {
        var count = self.GetItemCount(itemId);
        self.ConsumeItemList(new List<ItemArg>() { new ItemArg() { Id = itemId, Count = count } }, new AddItemData() { LogEvent = logEvent });
    }

    /// <summary>
    /// 道具数量是否足够
    /// </summary>
    /// <param name="self"></param>
    /// <param name="itemList"></param>
    /// <returns></returns>
    public static MessageReturn ItemEnough(this ItemComponent self, List<ItemArg> itemList)
    {
        if (itemList.IsNullOrEmpty())
        {
            return MessageReturn.Success();
        }

        var map = new Dictionary<int, long>();
        map = itemList.GroupBy(v => v.Id).ToDictionary(v => v.Key, v => v.Sum(v1 => v1.Count));
        foreach ((int id, long count) in map)
        {
            if (count <= 0)
            {
                continue;
            }

            long owenCount = self.GetItemCount(id);
            if (owenCount < count)
            {
                return MessageReturn.Create(ErrorCode.ERR_ItemNotEnough, MiscHelper.GetItemError(id));
            }
        }

        return MessageReturn.Success();
    }
}
}

