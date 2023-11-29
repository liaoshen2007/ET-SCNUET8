namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class DeleteTask_Event: AEvent<Scene, DeleteTask>
    {
        protected override async ETTask Run(Scene scene, DeleteTask a)
        {
            await ETTask.CompletedTask;
        }
    }
}