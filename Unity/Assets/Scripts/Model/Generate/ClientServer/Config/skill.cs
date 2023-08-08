﻿using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class skillCategory : Singleton<skillCategory>, IMerge
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, skill> dict = new();
		
        public void Merge(object o)
        {
            skillCategory s = o as skillCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public skill Get(int id)
        {
            this.dict.TryGetValue(id, out skill item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (skill)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, skill> GetAll()
        {
            return this.dict;
        }

        public skill GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

	public partial class skill: ProtoObject, IConfig
	{
		/// <summary>ID</summary>
		public int Id { get; set; }
		/// <summary>名称</summary>
		public int Name { get; set; }
		/// <summary>Buff描述</summary>
		public int Desc { get; set; }
		/// <summary>图标</summary>
		public string Icon { get; set; }
		/// <summary>动作显示</summary>
		public string ViewCmd { get; set; }
		/// <summary>持续时长</summary>
		public int Ms { get; set; }
		/// <summary>作用间隔时间</summary>
		public int Interval { get; set; }
		/// <summary>Buff效果列表</summary>
		public string EffectList { get; set; }
		/// <summary>叠加方式</summary>
		public int AddType { get; set; }
		/// <summary>叠加最大层数</summary>
		public int MaxLayer { get; set; }
		/// <summary>类型码列表</summary>
		public int[] Classify { get; set; }
		/// <summary>互斥列表</summary>
		public int[] Mutex { get; set; }
		/// <summary>互斥等级</summary>
		public int MutexLevel { get; set; }
		/// <summary>增益类型</summary>
		public int EffectType { get; set; }
		/// <summary>master_id</summary>
		public int MasterId { get; set; }
		/// <summary>元素类型</summary>
		public int Element { get; set; }

	}
}
