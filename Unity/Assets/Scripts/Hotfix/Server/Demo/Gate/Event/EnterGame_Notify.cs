namespace ET.Server
{   
    [Event(SceneType.Gate)]
    public class EnterGame_Notify: AEvent<Scene, EnterGame>
    {
        protected override async ETTask Run(Scene scene, EnterGame a)
        {
            var player = a.Player;
            var oldId = player.InstanceId;

            // 通知聊天服
            var actorId = StartSceneConfigCategory.Instance.GetBySceneName(scene.Zone(), nameof (SceneType.Chat)).ActorId;
            var resp = (Other2G_EnterResponse) await scene.Root().GetComponent<MessageSender>()
                    .Call(actorId, new G2Other_EnterRequest() { PlayerId = a.RoleId});
            if (oldId != player.InstanceId)
            {
                return;
            }

            player.ChatUnitId = resp.Id;
        }
    }
}