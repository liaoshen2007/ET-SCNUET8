using System;
using Unity.Mathematics;

namespace ET.Server
{
    public static partial class UnitFactory
    {
        public static Unit Create(Scene scene, long id, UnitType unitType)
        {
            UnitComponent unitComponent = scene.GetComponent<UnitComponent>();
            Unit unit = null;
            switch (unitType)
            {
                case UnitType.Player:
                {
                    unit = unitComponent.AddChildWithId<Unit, int>(id, 1001);
                    unit.AddComponent<MoveComponent>();
                    unit.Position = new float3(-10, 0, -10);

                    NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
                    numericComponent.Set(NumericType.Speed, 6f); // 速度是6米每秒
                    numericComponent.Set(NumericType.AOI, 15000); // 视野15米

                    unitComponent.Add(unit);

                    // 加入aoi
                    unit.AddComponent<AOIEntity, int, float3>(9 * 1000, unit.Position);
                    break;
                }
                default:
                    throw new Exception($"not such unit type: {unitType}");
            }

            unit.AddComponent<BuffComponent>();
            unit.AddComponent<AbilityComponent>();
            unit.AddComponent<ShieldComponent>();
            return unit;
        }
    }
}