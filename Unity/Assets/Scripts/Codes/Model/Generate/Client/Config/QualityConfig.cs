using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class QualityConfigCategory : Singleton<QualityConfigCategory>, IMerge
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, QualityConfig> dict = new();
		
        public void Merge(object o)
        {
            QualityConfigCategory s = o as QualityConfigCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public QualityConfig Get(int id)
        {
            this.dict.TryGetValue(id, out QualityConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (QualityConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, QualityConfig> GetAll()
        {
            return this.dict;
        }

        public QualityConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

	public partial class QualityConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		public int Id { get; set; }
		/// <summary>道具品质颜色</summary>
		public string ItemNameColor { get; set; }
		/// <summary>道具品质框</summary>
		public string ItemFrame { get; set; }

	}
}
