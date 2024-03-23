namespace ET.Client
{
    [MessageHandler(SceneType.Client)]
    public class M2C_NoticeUnitNumericHandler:MessageHandler<Scene, M2C_NoticeUnitNumerice>
    {
        protected override async ETTask Run(Scene root, M2C_NoticeUnitNumerice message)
        {
            UnitComponent unitComponent=root.CurrentScene().GetComponent<UnitComponent>();
            NumericComponent numeric=unitComponent.Get(message.UnitId).GetComponent<NumericComponent>();
            numeric.Set(message.NumericType,message.NewValue);
            Log.Error("M2C_NoticeUnitNumerice:"+message.NumericType);
            await ETTask.CompletedTask;
        }
    }
}

