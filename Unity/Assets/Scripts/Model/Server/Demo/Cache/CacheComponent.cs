using System.Collections.Generic;

namespace ET.Server
{
    /// <summary>
    /// 缓存组件
    /// </summary>
    [ComponentOf(typeof(Scene))]
    public class CacheComponent : Entity, IAwake, IDestroy
    {
        public Dictionary<string, UnitCache> CacheDict { get; } = new Dictionary<string, UnitCache>();

        public List<string> CacheKeyList { get; } = new ListComponent<string>();
    }
}