namespace ET.Server
{
    [MessageLocationHandler(SceneType.Map)]
    public class C2M_UpRoleLevelHandler:MessageLocationHandler<Unit,C2M_UpRoleLevel,M2C_UpRoleLevel>
    {
        protected override async ETTask Run(Unit unit, C2M_UpRoleLevel request, M2C_UpRoleLevel response)
        {
            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
            int UnitLevel = numericComponent.GetAsInt(NumericType.Level);
            var config = PlayerLevelConfigCategory.Instance.Get(UnitLevel);
            long exp = numericComponent.GetAsLong(NumericType.Exp);

            if (exp<config.NeedExp)
            {
                response.Error = ErrorCode.ERR_ExpNotEnough;
                return;
            }

            long newExp = exp - config.NeedExp;
            if (newExp<0)
            {
                response.Error = ErrorCode.ERR_ExpNumError;
                return;
            }

            numericComponent[NumericType.Exp] = newExp;
            numericComponent[NumericType.Level] = UnitLevel + 1;
            numericComponent[NumericType.AttrPoint] += 1;
            await ETTask.CompletedTask;
        }
    }
}

