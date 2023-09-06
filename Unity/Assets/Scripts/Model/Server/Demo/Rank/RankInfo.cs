namespace ET
{
    public interface IRankObj
    {
    }

    [ChildOf(typeof(RankItemComponent))]
    public class RankInfo: Entity, IAwake
    {
        public long UnitId { get; set; }

        public long Score { get; set; }

        public long Time { get; set; }

        public RankType RankType { get; set; }

        public int SubType { get; set; }

        public IRankObj RoleInfo { get; set; }
    }
}