namespace ET.Server
{
    [MessageLocationHandler(SceneType.Map)]
    public class C2M_TestNumericHandler:MessageLocationHandler<Unit,C2M_TestUnitNumeric,M2C_TestUnitNumeric>
    {
        protected override async ETTask Run(Unit unit, C2M_TestUnitNumeric request, M2C_TestUnitNumeric response)
        {
            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();

            long newGold = numericComponent.GetAsLong(NumericType.Coin) + 100;
            long newExp = numericComponent.GetAsLong(NumericType.Exp) + 50;
            long level = numericComponent.GetAsInt(NumericType.Level) + 1;
            numericComponent.Set(NumericType.Coin,newGold);
            numericComponent.Set(NumericType.Exp,newExp);
            numericComponent.Set(NumericType.Level,level);
            
            // numericComponent[NumericType.IronStone] += 3600;
            // numericComponent[NumericType.Fur]       += 3600;
            
            // numericComponent.Set(NumericType.IronStone,3600);
            // numericComponent.Set(NumericType.Fur,3600);
            //
            // for (int i = 0; i < 30; i++)
            // {
            //     if (!BagHelper.AddItemByConfigId(unit, RandomHelper.RandomNumber(1002, 1018)))
            //     {
            //         Log.Error("增加背包物品失败");
            //     }
            //
            // }
            await ETTask.CompletedTask;
        }
    }
}

