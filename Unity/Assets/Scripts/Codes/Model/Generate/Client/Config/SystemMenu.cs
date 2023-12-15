using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class SystemMenuCategory : Singleton<SystemMenuCategory>, IMerge, IConfigCategory
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, SystemMenu> dict = new();
		
        public void Merge(object o)
        {
            SystemMenuCategory s = o as SystemMenuCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public SystemMenu Get(int id)
        {
            this.dict.TryGetValue(id, out SystemMenu item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (SystemMenu)}，配置id: {id}");
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

        public Dictionary<int, SystemMenu> GetAll()
        {
            return this.dict;
        }
        
        public object GetAllConfig()
        {
            return this.dict.Values;
        }

        public SystemMenu GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

	public partial class SystemMenu: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		public int Id { get; set; }
		/// <summary>名称</summary>
		public int Name { get; set; }
		/// <summary>图标</summary>
		public string Icon { get; set; }
		/// <summary>分类</summary>
		public int Classify { get; set; }
		/// <summary>排序</summary>
		public int Rank { get; set; }
		/// <summary>帮助ID</summary>
		public int HelpId { get; set; }
		/// <summary>显示条件</summary>
		public string ShowCmdStr { get; set; }
		/// <summary>开启条件</summary>
		public string OpenCmdStr { get; set; }
		/// <summary>关闭条件</summary>
		public string CloseCmdStr { get; set; }

	}
}
