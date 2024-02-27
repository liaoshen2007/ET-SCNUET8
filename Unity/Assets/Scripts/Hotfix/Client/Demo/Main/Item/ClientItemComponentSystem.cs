using System.Collections.Generic;

namespace ET.Client
{
    [EntitySystemOf(typeof (ClientItemComponent))]
    [FriendOf(typeof (ClientItemComponent))]
    public static partial class ClientItemComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ClientItemComponent self)
        {
            self.ItemDict = new Dictionary<int, ItemData>();
        }

        [EntitySystem]
        private static void Destroy(this ClientItemComponent self)
        {
            self.ItemDict.Clear();
        }

        private static void AddUpdateItem(this ClientItemComponent self, ItemProto proto)
        {
            // if (!self.ItemDict.TryGetValue(proto.CfgId, out var item))
            // {
            //     item = self.AddChildWithId<ItemData>(proto.Id);
            //     self.ItemDict.Add(proto.CfgId, item);
            // }
            //
            // long oldCount = item.Count;
            // item.ToItem(proto);
            // EventSystem.Instance.Publish(self.Scene(), new AddItem() { ItemId = proto.CfgId, Count = proto.Count, ChangeCount = proto.Count - oldCount });
        }

        /// <summary>
        /// 初始化玩家道具
        /// </summary>
        public static void AddUpdateItem(this ClientItemComponent self, List<ItemProto> list)
        {
            foreach (var item in list)
            {
                self.AddUpdateItem(item);
            }
        }
    }
}