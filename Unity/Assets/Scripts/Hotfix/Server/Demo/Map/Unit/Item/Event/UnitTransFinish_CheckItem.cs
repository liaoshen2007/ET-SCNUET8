namespace ET.Server
{
    [Event(SceneType.Map)]
    public class UnitTransFinish_CheckItem: AEvent<Scene, UnitEnterGame>
    {
        protected override async ETTask Run(Scene scene, UnitEnterGame a)
    {
        a.Unit.GetComponent<ItemComponent>().CheckItem();
        await ETTask.CompletedTask;
    }
    }
}