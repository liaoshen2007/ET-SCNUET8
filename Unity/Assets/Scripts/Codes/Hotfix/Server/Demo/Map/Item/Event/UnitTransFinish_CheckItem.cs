namespace ET.Server;

[Event(SceneType.Map)]
public class UnitTransFinish_CheckItem: AEvent<Scene, UnitTransFinish>
{
    protected override async ETTask Run(Scene scene, UnitTransFinish a)
    {
        a.Unit.GetComponent<ItemComponent>().CheckItem();
        await ETTask.CompletedTask;
    }
}