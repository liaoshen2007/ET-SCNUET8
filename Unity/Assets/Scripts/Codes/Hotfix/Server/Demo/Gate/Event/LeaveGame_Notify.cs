namespace ET.Server;

[Event(SceneType.Gate)]
public class LeaveGame_Notify: AEvent<Scene, LeaveGame>
{
    protected override async ETTask Run(Scene scene, LeaveGame a)
    {
        // 通知聊天服
        var actorId = StartSceneConfigCategory.Instance.GetBySceneName(scene.Zone(), nameof (SceneType.Chat)).ActorId;
        scene.Root().GetComponent<MessageSender>().Call(actorId, new G2Other_LeaveRequest() { PlayerId = a.Player.Id }).Coroutine();

        await ETTask.CompletedTask;
    }
}