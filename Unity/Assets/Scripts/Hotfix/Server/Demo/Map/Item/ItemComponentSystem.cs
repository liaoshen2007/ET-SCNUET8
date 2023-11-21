using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ET.Server;

[EntitySystemOf(typeof (ItemComponent))]
[FriendOf(typeof (ItemComponent))]
[FriendOf(typeof (ItemData))]
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

    public static void ClearItem(this ItemComponent self, int itemId, int logEvent)
    {
        var count = self.GetItemCount(itemId);
        self.ConsumeItemList(new List<ItemArg>() { new ItemArg() { Id = itemId, Count = count } }, new AddItemData() { LogEvent = logEvent });
    }

    private static List<string> GetItemError(this ItemComponent self, int id)
    {
        var itemCfg = ItemConfigCategory.Instance.Get(id);
        if (itemCfg.LackTip > 0)
        {
            return new List<string>() { id.ToString(), itemCfg.LackTip.ToString() };
        }

        return new List<string>() { id.ToString() };
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
                return MessageReturn.Create(ErrorCode.ERR_ItemNotEnough, self.GetItemError(id));
            }
        }

        return MessageReturn.Success();
    }

    public static ReadOnlyCollection<ItemArg> GetDropItemList(this ItemComponent self, int dropId)
    {
        var dropCfg = DropConfigCategory.Instance.Get(dropId);
        if (dropCfg.RandomId.Length == 0)
        {
            return dropCfg.ItemList.AsReadOnly();
        }

        var list = new List<List<ItemArg>>();
        list.Add(dropCfg.ItemList);
        foreach (int id in dropCfg.RandomId)
        {
            var random = RandomConfigCategory.Instance.Get(id);
            switch (random.Type)
            {
                // 不放回抽奖
                case 1:
                    var weightList = random.GetCopyRandomList();
                    for (int i = 0; i < random.Parameter; i++)
                    {
                        var totalWeight = weightList.Sum(w => w.Weight);
                        List<ItemArg> arg = default;
                        int index = 0;
                        if (totalWeight > 1)
                        {
                            var randomV = RandomGenerator.RandomNumber(1, totalWeight);
                            for (int j = 0; j < weightList.Count; j++)
                            {
                                if (randomV <= weightList[j].Weight)
                                {
                                    arg = weightList[j].ItemList;
                                    index = j;
                                    break;
                                }

                                randomV -= weightList[j].Weight;
                            }
                        }
                        else
                        {
                            Log.Error("随机权重出错, 请检查配置");
                            break;
                        }

                        if (arg != null)
                        {
                            weightList.RemoveAt(index);
                            list.Add(arg);
                        }

                        if (weightList.Count == 0)
                        {
                            break;
                        }
                    }

                    break;
                // 放回抽奖
                case 2:
                    for (int i = 0; i < random.Parameter; i++)
                    {
                        var totalWeight = random.RandomList.Sum(itemArg => itemArg.Weight);
                        List<ItemArg> arg = default;
                        if (totalWeight > 1)
                        {
                            var randomV = RandomGenerator.RandomNumber(1, totalWeight);
                            for (int j = 0; j < random.RandomList.Count; j++)
                            {
                                if (randomV <= random.RandomList[j].Weight)
                                {
                                    arg = random.RandomList[j].ItemList;
                                    break;
                                }

                                randomV -= random.RandomList[j].Weight;
                            }
                        }
                        else
                        {
                            Log.Error("随机权重出错, 请检查配置");
                            break;
                        }

                        if (arg != null)
                        {
                            list.Add(arg);
                        }
                    }

                    break;
                // 按照其各自的概率独立随机奖品
                case 3:
                    foreach (var itemArg in random.RandomList)
                    {
                        if (itemArg.Weight.IsHit())
                        {
                            list.Add(itemArg.ItemList);
                        }
                    }

                    break;
            }
        }

        if (list.Count > 0)
        {
            var dict = new Dictionary<int, long>();
            foreach (var ll in list)
            {
                foreach (var itemArg in ll)
                {
                    if (dict.ContainsKey(itemArg.Id))
                    {
                        dict[itemArg.Id] += itemArg.Count;
                    }
                    else
                    {
                        dict[itemArg.Id] = itemArg.Count;
                    }
                }
            }

            return dict.Select(v => new ItemArg() { Id = v.Key, Count = v.Value, }).ToList().AsReadOnly();
        }

        return default;
    }
}