using System.Collections.Generic;

namespace ET.Server
{
    /// <summary>
    /// 缓存组件
    /// </summary>
    [ComponentOf(typeof (Scene))]
    public class CacheComponent: Entity, IAwake, IDestroy, ILoad
    {
        public Dictionary<string, UnitCache> CacheDict { get; } = new(10);

        public List<string> CacheKeyList { get; } = new ListComponent<string>();

        public Dictionary<long, List<Entity>> needSaveDict = new(100);
        public long lastSaveTime;

        public long checkExpireTimer;
    }
}