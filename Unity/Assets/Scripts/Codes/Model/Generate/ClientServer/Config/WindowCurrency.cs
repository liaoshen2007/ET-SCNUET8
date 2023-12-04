using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class WindowCurrencyCategory : Singleton<WindowCurrencyCategory>, IMerge, IConfigCategory
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, WindowCurrency> dict = new();
		
        public void Merge(object o)
        {
            WindowCurrencyCategory s = o as WindowCurrencyCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public WindowCurrency Get(int id)
        {
            this.dict.TryGetValue(id, out WindowCurrency item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (WindowCurrency)}，配置id: {id}");
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

        public Dictionary<int, WindowCurrency> GetAll()
        {
            return this.dict;
        }
        
        public object GetAllConfig()
        {
            return this.dict.Values;
        }

        public WindowCurrency GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

	public partial class WindowCurrency: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		public int Id { get; set; }
		/// <summary>标题名称</summary>
		public string Title { get; set; }
		/// <summary>内容</summary>
		public string Content { get; set; }

	}
}
