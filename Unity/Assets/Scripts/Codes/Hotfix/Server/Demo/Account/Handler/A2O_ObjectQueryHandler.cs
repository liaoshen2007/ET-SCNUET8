namespace ET.Server
{
    [MessageHandler(SceneType.Account)]
    public class A2O_ObjectQueryHandler: MessageHandler<Scene, ObjectQueryRequest, ObjectQueryResponse>
    {
        protected override async ETTask Run(Scene scene, ObjectQueryRequest request, ObjectQueryResponse response)
        {
            await ETTask.CompletedTask;
        }
    }
}