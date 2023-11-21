namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class AddItem_Event: AEvent<Scene, AddItem>
    {
        protected override async ETTask Run(Scene scene, AddItem a)
        {
            await ETTask.CompletedTask;
        }
    }
}