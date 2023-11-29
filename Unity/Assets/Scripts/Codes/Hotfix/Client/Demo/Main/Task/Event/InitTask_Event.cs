namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class InitTask_Event: AEvent<Scene, InitTask>
    {
        protected override async ETTask Run(Scene scene, InitTask a)
        {
            await ETTask.CompletedTask;
        }
    }
}