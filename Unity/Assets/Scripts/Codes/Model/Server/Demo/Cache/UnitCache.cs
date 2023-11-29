using System.Collections.Generic;

namespace ET.Server
{
    /// <summary>
    /// 需要缓存的实体继承此接口
    /// </summary>
    public interface ICache
    {
        
    }
    
    [ChildOf(typeof(CacheComponent))]
    public class UnitCache : Entity, IAwake, IDestroy
    {
        public string TypeName { get; set; }

        public Dictionary<long, Entity> ComponentDict { get; } = new Dictionary<long, Entity>();
        
        public Dictionary<long, long> UpdateTimeDict { get; } = new Dictionary<long, long>();

        /// <summary>
        /// 数据缓存间隔
        /// </summary>
        public const int Interval = 3 * 3600;
    }
}