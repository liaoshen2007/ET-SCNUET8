using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class ActivityConfigCategory : Singleton<ActivityConfigCategory>, IMerge, IConfigCategory
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, ActivityConfig> dict = new();
		
        public void Merge(object o)
        {
            ActivityConfigCategory s = o as ActivityConfigCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public ActivityConfig Get(int id)
        {
            this.dict.TryGetValue(id, out ActivityConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (ActivityConfig)}，配置id: {id}");
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

        public Dictionary<int, ActivityConfig> GetAll()
        {
            return this.dict;
        }
        
        public object GetAllConfig()
        {
            return this.dict.Values;
        }

        public ActivityConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

	public partial class ActivityConfig: ProtoObject, IConfig
	{
		/// <summary>id</summary>
		public int Id { get; set; }
		/// <summary>是否隐藏</summary>
		public bool IsHide { get; set; }
		/// <summary>帮助ID（界面用）</summary>
		public int HelpId { get; set; }
		/// <summary>名称</summary>
		public int Name { get; set; }
		/// <summary>描述</summary>
		public int Desc { get; set; }
		/// <summary>图标</summary>
		public string Icon { get; set; }
		/// <summary>活动类型</summary>
		public int ActivityType { get; set; }
		/// <summary>主窗口</summary>
		public int WindowId { get; set; }
		/// <summary>背景图片</summary>
		public string Pic { get; set; }
		/// <summary>活动显示条件列表</summary>
		public string ConditionList { get; set; }
		/// <summary>额外领奖时长</summary>
		public int CloseLastSec { get; set; }
		/// <summary>活动参数</summary>
		public string[] Args { get; set; }
		/// <summary>预显示时间(秒)</summary>
		public int ShowTime { get; set; }
		/// <summary>开始时间</summary>
		public string OpenTime { get; set; }
		/// <summary>持续时间(秒)</summary>
		public int LastSec { get; set; }
		/// <summary>间隔时间(天)</summary>
		public int IntervalDay { get; set; }
		/// <summary>显示奖励</summary>
		public string ShowItemList { get; set; }
		/// <summary>客户端参数</summary>
		public string[] Ext { get; set; }
		/// <summary>数据列表(列表)</summary>
		public string[] DataListSource { get; set; }

	}
}
