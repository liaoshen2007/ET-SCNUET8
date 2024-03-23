namespace ET.Server
{
    [MessageLocationHandler(SceneType.Map)]
    public class C2M_AddAttributePointHandler:MessageLocationHandler<Unit,C2M_AddAttributePoint,M2C_AddAttributePoint>
    {
        protected override async ETTask Run(Unit unit, C2M_AddAttributePoint request, M2C_AddAttributePoint response)
        {
            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
            int targetNumericType = request.NumericType;

            if (!NumericCategory.Instance.Contain(targetNumericType))
            {
                response.Error = ErrorCode.ERR_NumericTypeNotExist;

                return;
            }

            Numeric config = NumericCategory.Instance.Get(targetNumericType);
            if (config.IsAddPoint==0)
            {
                response.Error = ErrorCode.ERR_NumericTypeNotAddpoint;
            
                return;
            }

            int AttributePointCount = numericComponent.GetAsInt(NumericType.AttrPoint);

            if (AttributePointCount<=0)
            {
                response.Error = ErrorCode.ERR_AddpointNotEnough;

                return;
            }

            --AttributePointCount;
            numericComponent.Set(NumericType.AttrPoint,AttributePointCount);

            int targetAttributeCount = numericComponent.GetAsInt(targetNumericType) + 1;
            numericComponent.Set(targetNumericType,targetAttributeCount);

            //await numericComponent.AddorUpdateUnitCache();//关键数据立即存库
            await ETTask.CompletedTask;
        }
    }
}

