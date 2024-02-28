using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class ErrorCfgCategory : Singleton<ErrorCfgCategory>, IMerge, IConfigCategory
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, ErrorCfg> dict = new();
		
        public void Merge(object o)
        {
            ErrorCfgCategory s = o as ErrorCfgCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public ErrorCfg Get(int id)
        {
            this.dict.TryGetValue(id, out ErrorCfg item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (ErrorCfg)}，配置id: {id}");
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

        public Dictionary<int, ErrorCfg> GetAll()
        {
            return this.dict;
        }
        
        public object GetAllConfig()
        {
            return this.dict.Values;
        }

        public ErrorCfg GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

	public partial class ErrorCfg: ProtoObject, IConfig
	{
		/// <summary>错误码(200001起)</summary>
		public int Id { get; set; }
		/// <summary>语言ID</summary>
		public int Desc { get; set; }

	}
}
