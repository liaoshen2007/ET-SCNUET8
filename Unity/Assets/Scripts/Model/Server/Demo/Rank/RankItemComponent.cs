using System.Collections.Generic;

namespace ET
{
    [ChildOf(typeof(RankComponent))]
    public class RankItemComponent: Entity, IAwake
    {
        public HashSet<long> NeedSaveInfo { get; set; } = new HashSet<long>(100);
    }
}