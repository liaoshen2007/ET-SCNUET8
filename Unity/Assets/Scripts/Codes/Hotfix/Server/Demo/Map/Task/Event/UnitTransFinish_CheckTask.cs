namespace ET.Server;

[Event(SceneType.Map)]
public class UnitTransFinish_CheckTask: AEvent<Scene, UnitTransFinish>
{
    protected override async ETTask Run(Scene scene, UnitTransFinish a)
    {
        a.Unit.GetComponent<TaskComponent>().CheckTask();
        await ETTask.CompletedTask;
    }
}