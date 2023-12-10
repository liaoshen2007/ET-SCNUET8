using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class WindowCategory : Singleton<WindowCategory>, IMerge, IConfigCategory
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, Window> dict = new();
		
        public void Merge(object o)
        {
            WindowCategory s = o as WindowCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public Window Get(int id)
        {
            this.dict.TryGetValue(id, out Window item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (Window)}，配置id: {id}");
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

        public Dictionary<int, Window> GetAll()
        {
            return this.dict;
        }
        
        public object GetAllConfig()
        {
            return this.dict.Values;
        }

        public Window GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

	public partial class Window: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		public int Id { get; set; }
		/// <summary>标题名称</summary>
		public int Title { get; set; }
		/// <summary>窗口类型</summary>
		public string Type { get; set; }
		/// <summary>帮助ID</summary>
		public int HelpId { get; set; }
		/// <summary>货币ID</summary>
		public int CurrencyID { get; set; }
		/// <summary>资源路径</summary>
		public string Path { get; set; }

	}
}
