using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Server
{
    [FriendOf(typeof (MoveComponent))]
    [FriendOf(typeof (NumericComponent))]
    public static partial class UnitHelper
    {
        public static UnitInfo CreateUnitInfo(Unit unit)
        {
            UnitInfo unitInfo = new();
            NumericComponent nc = unit.GetComponent<NumericComponent>();
            unitInfo.UnitId = unit.Id;
            unitInfo.ConfigId = unit.ConfigId;
            unitInfo.Type = (int) unit.Type;
            unitInfo.Position = unit.Position;
            unitInfo.Forward = unit.Forward;

            MoveComponent moveComponent = unit.GetComponent<MoveComponent>();
            if (moveComponent != null)
            {
                if (!moveComponent.IsArrived())
                {
                    unitInfo.MoveInfo = new MoveInfo();
                    unitInfo.MoveInfo.Points.Add(unit.Position);
                    for (int i = moveComponent.N; i < moveComponent.Targets.Count; ++i)
                    {
                        float3 pos = moveComponent.Targets[i];
                        unitInfo.MoveInfo.Points.Add(pos);
                    }
                }
            }

            foreach ((int key, long value) in nc.NumericDic)
            {
                unitInfo.KV.Add(key, value);
            }

            return unitInfo;
        }

        /// <summary>
        /// 获取看见unit的玩家，主要用于广播
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Dictionary<long, AOIEntity> GetBeSeePlayers(this Unit self)
        {
            return self.GetComponent<AOIEntity>().GetBeSeePlayers();
        }

        /// <summary>
        /// 重置当前血量为最大血量
        /// </summary>
        /// <param name="self"></param>
        public static void ResetHp(this Unit self)
        {
            var numeric = self.GetComponent<NumericComponent>();
            numeric.Set(NumericType.Hp, numeric.GetAsLong(NumericType.MaxHp));
        }

        /// <summary>
        /// 获取当前角色的Hp
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static long GetHp(this Unit self)
        {
            var numeric = self.GetComponent<NumericComponent>();
            return numeric.GetAsLong(NumericType.Hp);
        }

        /// <summary>
        /// 创建受伤参数
        /// </summary>
        /// <param name="self"></param>
        /// <param name="list"></param>
        /// <param name="isPhysics"></param>
        /// <returns></returns>
        public static HurtArgs CreateHurtArgs(BuffComponent self, List<HurtInfo> list, bool isPhysics = false)
        {
            var args = self.GetComponent<HurtArgs>();
            if (args != null)
            {
                return args;
            }

            args = self.AddComponent<HurtArgs>();
            args.IsPhysics = false;
            if (list is { Count: > 0 })
            {
                foreach (var hurtInfo in list)
                {
                    args.HurtList.Add(hurtInfo);
                }
            }

            return args;
        }
    }
}