using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class WindowHelpCategory : Singleton<WindowHelpCategory>, IMerge, IConfigCategory
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, WindowHelp> dict = new();
		
        public void Merge(object o)
        {
            WindowHelpCategory s = o as WindowHelpCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public WindowHelp Get(int id)
        {
            this.dict.TryGetValue(id, out WindowHelp item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (WindowHelp)}，配置id: {id}");
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

        public Dictionary<int, WindowHelp> GetAll()
        {
            return this.dict;
        }
        
        public object GetAllConfig()
        {
            return this.dict.Values;
        }

        public WindowHelp GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

	public partial class WindowHelp: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		public int Id { get; set; }
		/// <summary>货币配置</summary>
		public int[] Currency { get; set; }

	}
}
