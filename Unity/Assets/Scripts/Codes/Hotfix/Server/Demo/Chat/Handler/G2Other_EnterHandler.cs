namespace ET.Server;

[MessageHandler(SceneType.Chat)]
public class G2Other_EnterHandler: MessageHandler<Scene, G2Other_EnterRequest, Other2G_EnterResponse>
{
    protected override async ETTask Run(Scene scene, G2Other_EnterRequest request, Other2G_EnterResponse response)
    {
        var unit = scene.GetComponent<ChatComponent>().Enter(request.PlayerId);
        var roleInfo = await CacheHelper.GetPlayerInfo(scene, request.PlayerId);
        unit.UpdateInfo(roleInfo);
        response.Id = unit.InstanceId;

        await ETTask.CompletedTask;
    }
}