using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class SkillConfigCategory : Singleton<SkillConfigCategory>, IMerge, IConfigCategory
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, SkillConfig> dict = new();
		
        public void Merge(object o)
        {
            SkillConfigCategory s = o as SkillConfigCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public SkillConfig Get(int id)
        {
            this.dict.TryGetValue(id, out SkillConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (SkillConfig)}，配置id: {id}");
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

        public Dictionary<int, SkillConfig> GetAll()
        {
            return this.dict;
        }
        
        public object GetAllConfig()
        {
            return this.dict.Values;
        }

        public SkillConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

	public partial class SkillConfig: ProtoObject, IConfig
	{
		/// <summary>ID</summary>
		public int Id { get; set; }
		/// <summary>名称</summary>
		public string Name { get; set; }
		/// <summary>技能描述</summary>
		public string Desc { get; set; }
		/// <summary>图标</summary>
		public string Icon { get; set; }
		/// <summary>初始层数</summary>
		public int PreLayer { get; set; }
		/// <summary>最大层数</summary>
		public int MaxLayer { get; set; }
		/// <summary>连招顺序</summary>
		public int Sort { get; set; }
		/// <summary>仇恨比例</summary>
		public int HateRate { get; set; }
		/// <summary>仇恨基础</summary>
		public int HateBase { get; set; }
		/// <summary>目标类型</summary>
		public int DstType { get; set; }
		/// <summary>范围类型</summary>
		public int RangeType { get; set; }
		/// <summary>目标最大距离</summary>
		public int MaxDistance { get; set; }
		/// <summary>技能被中断类型</summary>
		public int[] Interrupt { get; set; }
		/// <summary>技能类型</summary>
		public int Classify { get; set; }
		/// <summary>下一技能</summary>
		public int NextId { get; set; }
		/// <summary>动作</summary>
		public string ActName { get; set; }
		/// <summary>元素类型</summary>
		public int Element { get; set; }
		/// <summary>冷却时间</summary>
		public int ColdTime { get; set; }
		/// <summary>吟唱时间</summary>
		public int SingTime { get; set; }
		/// <summary>释放消耗</summary>
		public int ConsumeList { get; set; }
		/// <summary>效果列表</summary>
		public string EffectStr { get; set; }

	}
}
