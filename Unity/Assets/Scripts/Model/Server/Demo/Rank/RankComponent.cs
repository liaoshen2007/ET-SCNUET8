using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace ET;

public class RankCompare : IComparer<RankInfo>
{
    public int Compare(RankInfo x, RankInfo y)
    {
        if (x.Score != y.Score)
        {
            return x.Score.CompareTo(y.Score);
        }
        
        return x.Time.CompareTo(y.Time);
    }
}

[ComponentOf(typeof (Scene))]
public class RankComponent: Entity, IAwake, IDestroy
{
    public long Timer = 0;

    public Dictionary<RankType, List<int>> LoadRankDict;

    /// <summary>
    /// 排行榜排序方法
    /// </summary>
    [BsonIgnore]
    public IComparer<RankInfo> RankComparer => new RankCompare();

    public HashSet<long> NeedSaveObj { get; set; } = new HashSet<long>(100);

    /// <summary>
    /// 角色数据
    /// </summary>
    public Dictionary<long, IRankObj> RankObjDict { get; set; } = new();

    public Dictionary<string, RankItemComponent> RankItem { get; set; } = new();

    /// <summary>
    /// 排行榜名称对应排行数据字典
    /// </summary>
    public Dictionary<string, SortedList<RankInfo, long>> RankDict { get; set; } = new();
}

