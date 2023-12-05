namespace ET.Client
{
    [MessageHandler(SceneType.Demo)]
    public class C2C_UpdateChatHandler: MessageHandler<Scene, C2C_UpdateChat>
    {
        protected override async ETTask Run(Scene entity, C2C_UpdateChat message)
        {
            await ETTask.CompletedTask;
        }
    }
}