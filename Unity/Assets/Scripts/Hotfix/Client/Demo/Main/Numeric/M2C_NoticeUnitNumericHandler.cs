namespace ET.Client
{
    [MessageHandler(SceneType.Client)]
    public class M2C_NoticeUnitNumericHandler:MessageHandler<Scene, M2C_NoticeUnitNumerice>
    {
        protected override async ETTask Run(Scene root, M2C_NoticeUnitNumerice message)
        {
            root.CurrentScene().GetComponent<UnitComponent>()?.Get(message.UnitId)?.
                    GetComponent<NumericComponent>()?.Set(message.NumericType,message.NewValue);
            await ETTask.CompletedTask;
        }
    }
}

