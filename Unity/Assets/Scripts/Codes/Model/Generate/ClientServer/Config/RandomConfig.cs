using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class RandomConfigCategory : Singleton<RandomConfigCategory>, IMerge, IConfigCategory
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, RandomConfig> dict = new();
		
        public void Merge(object o)
        {
            RandomConfigCategory s = o as RandomConfigCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public RandomConfig Get(int id)
        {
            this.dict.TryGetValue(id, out RandomConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (RandomConfig)}，配置id: {id}");
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

        public Dictionary<int, RandomConfig> GetAll()
        {
            return this.dict;
        }
        
        public object GetAllConfig()
        {
            return this.dict.Values;
        }

        public RandomConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

	public partial class RandomConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		public int Id { get; set; }
		/// <summary>随机方式</summary>
		public int Type { get; set; }
		/// <summary>随机方式参数</summary>
		public int Parameter { get; set; }
		/// <summary>道具列表</summary>
		public string ItemList { get; set; }

	}
}
