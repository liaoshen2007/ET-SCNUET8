using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class NumericCategory : Singleton<NumericCategory>, IMerge, IConfigCategory
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, Numeric> dict = new();
		
        public void Merge(object o)
        {
            NumericCategory s = o as NumericCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public Numeric Get(int id)
        {
            this.dict.TryGetValue(id, out Numeric item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (Numeric)}，配置id: {id}");
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

        public Dictionary<int, Numeric> GetAll()
        {
            return this.dict;
        }
        
        public object GetAllConfig()
        {
            return this.dict.Values;
        }

        public Numeric GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

	public partial class Numeric: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		public int Id { get; set; }
		/// <summary>是否用于加成点</summary>
		public int IsAddPoint { get; set; }
		/// <summary>是否有加成属性</summary>
		public int Addition { get; set; }
		/// <summary>基础值</summary>
		public int BaseValue { get; set; }
		/// <summary>名称</summary>
		public string Name { get; set; }
		/// <summary>描述</summary>
		public string Desc { get; set; }
		/// <summary>战力</summary>
		public long Fight { get; set; }

	}
}
