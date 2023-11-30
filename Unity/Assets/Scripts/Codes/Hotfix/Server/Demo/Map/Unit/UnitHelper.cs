using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Server
{
    [FriendOf(typeof (MoveComponent))]
    [FriendOf(typeof (NumericComponent))]
    public static partial class UnitHelper
    {
        public static async ETTask<(bool, Unit)> LoadUnit(Player player)
        {
            // 在Gate上动态创建一个Map Scene，把Unit从DB中加载放进来，然后传送到真正的Map中，这样登陆跟传送的逻辑就完全一样了
            GateMapComponent gateMapComponent = player.AddComponent<GateMapComponent>();
            gateMapComponent.Scene = await GateMapFactory.Create(gateMapComponent, player.Id, IdGenerater.Instance.GenerateInstanceId(), "GateMap");

            Scene scene = gateMapComponent.Scene;
            Unit unit = await CacheHelper.GetCache(scene, player.Scene(), player.Id);
            bool isNewPlayer = unit == null;
            if (isNewPlayer)
            {
                unit = UnitFactory.Create(scene, player.Id, UnitType.Player);
                CacheHelper.UpdateAllCache(player.Scene(), unit);
            }

            return (isNewPlayer, unit);
        }

        public static async ETTask InitUnit(Unit unit, bool isNewPlayer)
        {
            await ETTask.CompletedTask;
        }

        public static UnitInfo CreateUnitInfo(Unit unit)
        {
            UnitInfo unitInfo = new();
            NumericComponent nc = unit.GetComponent<NumericComponent>();
            unitInfo.UnitId = unit.Id;
            unitInfo.ConfigId = unit.ConfigId;
            unitInfo.Type = (int)unit.Type();
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
        /// 获取unit看见的玩家
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static List<Unit> GetSeePlayers(this Unit self)
        {
            var dict = self.GetComponent<AOIEntity>().GetSeePlayers();
            var list = new List<Unit>();
            foreach (var (_, aoiEntity) in dict)
            {
                list.Add(aoiEntity.Unit);
            }

            return list;
        }

        public static List<Unit> GetFoucslayers(this Unit self, FocusType fT, bool isCanAttack = true)
        {
            var list = self.GetSeePlayers();
            return list;
        }

        /// <summary>
        /// 获取修正位置
        /// </summary>
        /// <param name="srcX">x坐标</param>
        /// <param name="srcZ">y坐标</param>
        /// <param name="direct">方向</param>
        /// <param name="repairPos">修改距离</param>
        /// <returns></returns>
        public static Pair<float, float> GetRepairPos(float srcX, float srcZ, float direct, float repairPos)
        {
            if (repairPos != 0)
            {
                srcX = (float)Math.Floor(srcX + Math.Cos(Math.Atan(direct)) * repairPos);
                srcZ = (float)Math.Floor(srcZ + Math.Cos(Math.Atan(direct)) * repairPos);
            }

            return new Pair<float, float>(srcX, srcZ);
        }

        public static Unit GetOwner(this Unit self)
        {
            return default;
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
        /// 获取当前角色的的属性值
        /// </summary>
        /// <param name="self"></param>
        /// <param name="attrType">属性类型</param>
        /// <returns></returns>
        public static long GetAttrValue(this Unit self, int attrType)
        {
            var numeric = self.GetComponent<NumericComponent>();
            return numeric.GetAsLong(attrType);
        }

        /// <summary>
        /// 是否暴击
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsCrit(this Unit self)
        {
            return self.Scene().GetComponent<FightFormula>().IsCrit(self);
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