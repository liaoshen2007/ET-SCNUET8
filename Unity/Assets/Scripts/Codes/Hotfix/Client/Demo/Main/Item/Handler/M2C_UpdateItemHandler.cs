namespace ET.Client
{
    [MessageHandler(SceneType.Demo)]
    public class M2C_UpdateItemHandler: MessageHandler<Scene, M2C_UpdateItem>
    {
        protected override async ETTask Run(Scene entity, M2C_UpdateItem message)
        {
            await ETTask.CompletedTask;
        }
    }
}