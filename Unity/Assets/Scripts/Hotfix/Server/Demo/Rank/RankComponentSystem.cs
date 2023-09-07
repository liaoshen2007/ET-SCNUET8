using System;
using System.Collections.Generic;

namespace ET.Server;

[FriendOf(typeof (RankComponent))]
[EntitySystemOf(typeof (RankComponent))]
public static partial class RankComponentSystem
{
    [EntitySystem]
    private static void Awake(this RankComponent self)
    {
        self.Init();
    }

    [EntitySystem]
    private static void Destroy(this RankComponent self)
    {
        self.Fiber().TimerComponent.Remove(ref self.Timer);
        self.RankDict.Clear();
        self.RankObjDict.Clear();
        foreach (var (_, com) in self.RankItem)
        {
            com.NeedSaveInfo.Clear();
        }

        self.RankItem.Clear();
        self.NeedSaveObj.Clear();
    }

    [EntitySystem]
    private static void Load(this RankComponent self)
    {
        self.Init();
    }

    [Invoke(TimerInvokeType.SaveRank)]
    public class SaveRankTimer: ATimer<RankComponent>
    {
        protected override void Run(RankComponent self)
        {
            self.Save().Coroutine();
        }
    }

    private static void Init(this RankComponent self)
    {
        self.RankDict.Clear();
        self.RankObjDict.Clear();
        self.RankItem.Clear();
        self.NeedSaveObj.Clear();

        self.LoadRankDict = new Dictionary<RankType, List<int>>()
        {
            { RankType.Fight, new List<int>() { 0 } }, { RankType.Level, new List<int>() { 0 } },
        };
        self.LoadRank().Coroutine();
        self.Timer = self.Fiber().TimerComponent.NewRepeatedTimer(5 * 1000, TimerInvokeType.SaveRank, self);
    }

    /// <summary>
    /// 加载排行榜数据
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static async ETTask LoadRank(this RankComponent self)
    {
        var zoneDb = self.Scene().GetComponent<DBManagerComponent>().GetZoneDB(self.Zone());
        foreach (var (t, subList) in self.LoadRankDict)
        {
            foreach (int sub in subList)
            {
                var rankName = GetRankName(t, sub, self.Zone());
                var rankList = await zoneDb.Query<RankInfo>(info => true, rankName);
                if (!self.RankDict.TryGetValue(rankName, out var list))
                {
                    list = new SortedList<RankInfo, long>(self.RankComparer);
                    self.RankDict.Add(rankName, list);
                }

                if (!self.RankItem.TryGetValue(rankName, out var item))
                {
                    item = self.AddChild<RankItemComponent>();
                    self.RankItem.Add(rankName, item);
                }

                foreach (var info in rankList)
                {
                    item.AddChild(info);
                    list.Add(info, info.UnitId);
                }
            }
        }

        self.Fiber().Info("加载排行榜数据完成!");
    }

    /// <summary>
    /// 保存排行榜数据
    /// 
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static async ETTask Save(this RankComponent self)
    {
        var zoneDb = self.Scene().GetComponent<DBManagerComponent>().GetZoneDB(self.Zone());
        if (self.NeedSaveObj.Count > 0)
        {
            using var list = ListComponent<ETTask>.Create();
            foreach (long l in self.NeedSaveObj)
            {
                if (self.RankObjDict.TryGetValue(l, out var obj))
                {
                    list.Add(zoneDb.Save((Entity) obj, "RankObj"));
                }
            }

            self.NeedSaveObj.Clear();
            await ETTaskHelper.WaitAll(list);
        }

        foreach (var (_, item) in self.RankItem)
        {
            if (item.NeedSaveInfo.Count > 0)
            {
                using var list = ListComponent<ETTask>.Create();
                foreach (long l in item.NeedSaveInfo)
                {
                    var child = item.GetChild<RankInfo>(l);
                    if (child != null)
                    {
                        list.Add(zoneDb.Save(child, GetRankName(child.RankType, child.SubType, child.Zone)));
                    }
                }

                item.NeedSaveInfo.Clear();
                await ETTaskHelper.WaitAll(list);
            }
        }
    }

    public static void UpdateRankObj(this RankComponent self, long unitId, IRankObj obj)
    {
        if (obj == null)
        {
            return;
        }

        bool save;
        if (!self.RankObjDict.TryGetValue(unitId, out IRankObj info))
        {
            self.RankObjDict.Add(unitId, obj);
            save = true;
        }
        else
        {
            save = !Equals(info, obj);
            self.RankObjDict[unitId] = obj;
        }

        switch (obj)
        {
            case RankRoleInfo:
                if (save)
                {
                    self.NeedSaveObj.Add(unitId);
                }

                break;
            default:
                self.Fiber().Error($"{obj} 类型未继承Entity");
                return;
        }
    }

    /// <summary>
    /// 更新排行榜
    /// </summary>
    /// <param name="self"></param>
    /// <param name="unitId"></param>
    /// <param name="t">主榜类型</param>
    /// <param name="subT">子榜类型</param>
    /// <param name="time">更新时间</param>
    /// <param name="info"></param>
    /// <param name="score">分数</param>
    /// <returns></returns>
    public static void UpdateRank(this RankComponent self, long unitId, RankType t, int subT, long score, long? time = null, IRankObj info = null)
    {
        self.UpdateRankObj(unitId, info);
        var rankName = GetRankName(t, subT, self.Zone());
        if (!self.RankDict.TryGetValue(rankName, out var list))
        {
            list = new SortedList<RankInfo, long>(self.RankComparer);
            self.RankDict.Add(rankName, list);
        }

        if (!self.RankItem.TryGetValue(rankName, out var item))
        {
            item = self.AddChild<RankItemComponent>();
            self.RankItem.Add(rankName, item);
        }

        if (list.ContainsValue(unitId))
        {
            list.RemoveAt(list.IndexOfValue(unitId));
            item.RemoveChild(unitId);
        }

        var child = item.AddChildWithId<RankInfo>(unitId);
        child.UnitId = unitId;
        child.Score = score;
        child.Time = time ?? TimeInfo.Instance.ServerNow();
        child.RankType = t;
        child.SubType = subT;
        list.Add(child, unitId);
        item.NeedSaveInfo.Add(unitId);
    }

    public static (List<RankInfoProto>, RankInfoProto) GetRank(this RankComponent self, long unitId, RankType t, int subT, int page = 0)
    {
        var list = new List<RankInfoProto>();
        var rankName = GetRankName(t, subT, self.Zone());
        if (!self.RankDict.TryGetValue(rankName, out var sortList))
        {
            return (list, null);
        }

        RankInfoProto selfRank = null;
        for (int i = page * ConstValue.RankPage; i < Math.Min(sortList.Count, (page + 1) * ConstValue.RankPage); i++)
        {
            var info = sortList.Keys[i];
            list.Add(self.CreateProto(info, i));
        }

        if (sortList.ContainsValue(unitId))
        {
            var rank = sortList.IndexOfValue(unitId);
            selfRank = self.CreateProto(sortList.Keys[rank], rank);
        }

        return (list, selfRank);
    }

    private static RankInfoProto CreateProto(this RankComponent self, RankInfo info, long rank)
    {
        var proto = new RankInfoProto
        {
            UnitId = info.UnitId, Score = info.Score, Rank = rank, Time = info.Time,
        };

        if (self.RankObjDict.TryGetValue(proto.UnitId, out var obj))
        {
            var roleInfo = obj as RankRoleInfo;
            proto.RoleInfo = new RankRoleInfoProto
            {
                Name = roleInfo.Name,
                HeadIcon = roleInfo.HeadIcon,
                Level = roleInfo.Level,
                Fight = roleInfo.Fight,
                Sex = roleInfo.Sex,
            };
        }

        return proto;
    }

    private static string GetRankName(RankType t, int subT, int zone)
    {
        return $"Rank_{t}_{subT}_{zone}";
    }
}