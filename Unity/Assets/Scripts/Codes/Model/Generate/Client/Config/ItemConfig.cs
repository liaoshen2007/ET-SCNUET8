using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class ItemConfigCategory : Singleton<ItemConfigCategory>, IMerge, IConfigCategory
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, ItemConfig> dict = new();
		
        public void Merge(object o)
        {
            ItemConfigCategory s = o as ItemConfigCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public ItemConfig Get(int id)
        {
            this.dict.TryGetValue(id, out ItemConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (ItemConfig)}，配置id: {id}");
            }

            return item;
        }
        
        public object GetConfig(int id)
        {
            return this.Get(id);
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, ItemConfig> GetAll()
        {
            return this.dict;
        }
        
        public object GetAllConfig()
        {
            return this.dict.Values;
        }

        public ItemConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

	public partial class ItemConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		public int Id { get; set; }
		/// <summary>名称</summary>
		public string Name { get; set; }
		/// <summary>描述</summary>
		public string Desc { get; set; }
		/// <summary>图标</summary>
		public string Icon { get; set; }
		/// <summary>功能分类</summary>
		public int ItemMode { get; set; }
		/// <summary>功能参数</summary>
		public int[] ModeArgs { get; set; }
		/// <summary>品质</summary>
		public int Quality { get; set; }
		/// <summary>道具类型</summary>
		public int Type { get; set; }
		/// <summary>物品不足时提示</summary>
		public int LackTip { get; set; }

	}
}
