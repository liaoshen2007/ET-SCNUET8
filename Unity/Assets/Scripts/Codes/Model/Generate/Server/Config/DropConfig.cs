using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class DropConfigCategory : Singleton<DropConfigCategory>, IMerge
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, DropConfig> dict = new();
		
        public void Merge(object o)
        {
            DropConfigCategory s = o as DropConfigCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public DropConfig Get(int id)
        {
            this.dict.TryGetValue(id, out DropConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (DropConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, DropConfig> GetAll()
        {
            return this.dict;
        }

        public DropConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

	public partial class DropConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		public int Id { get; set; }
		/// <summary>固定道具</summary>
		public string ItemRewardStr { get; set; }
		/// <summary>随机ID</summary>
		public int[] RandomId { get; set; }

	}
}
