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

        public Dictionary<long, Entity> ComponentDict { get; set; } = new Dictionary<long, Entity>();
    }
}