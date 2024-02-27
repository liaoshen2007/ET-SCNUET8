using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class EmojiConfigCategory : Singleton<EmojiConfigCategory>, IMerge, IConfigCategory
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, EmojiConfig> dict = new();
		
        public void Merge(object o)
        {
            EmojiConfigCategory s = o as EmojiConfigCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public EmojiConfig Get(int id)
        {
            this.dict.TryGetValue(id, out EmojiConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (EmojiConfig)}，配置id: {id}");
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

        public Dictionary<int, EmojiConfig> GetAll()
        {
            return this.dict;
        }
        
        public object GetAllConfig()
        {
            return this.dict.Values;
        }

        public EmojiConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

	public partial class EmojiConfig: ProtoObject, IConfig
	{
		/// <summary>表情id</summary>
		public int Id { get; set; }
		/// <summary>表情名称</summary>
		public int Name { get; set; }
		/// <summary>表情图片</summary>
		public string Icon { get; set; }
		/// <summary>顺序</summary>
		public int Weight { get; set; }
		/// <summary>表情组ID</summary>
		public int GroupId { get; set; }

	}
}
