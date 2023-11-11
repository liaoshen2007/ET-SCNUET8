using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class ItemCategory : Singleton<ItemCategory>, IMerge
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, Item> dict = new();
		
        public void Merge(object o)
        {
            ItemCategory s = o as ItemCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public Item Get(int id)
        {
            this.dict.TryGetValue(id, out Item item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (Item)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, Item> GetAll()
        {
            return this.dict;
        }

        public Item GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

	public partial class Item: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		public int Id { get; set; }
		/// <summary>功能分类</summary>
		public int ItemMode { get; set; }
		/// <summary>功能参数</summary>
		public int[] ModeArgs { get; set; }
		/// <summary>品质</summary>
		public int Quality { get; set; }
		/// <summary>道具类型</summary>
		public int Type { get; set; }
		/// <summary>物品不足时提示</summary>
		public string LackTip { get; set; }

	}
}
