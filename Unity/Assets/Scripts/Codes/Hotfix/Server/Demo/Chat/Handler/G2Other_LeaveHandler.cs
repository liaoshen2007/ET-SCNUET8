namespace ET.Server;

[MessageHandler(SceneType.Chat)]
public class G2Other_LeaveHandler: MessageHandler<Scene, G2Other_LeaveRequest, Other2G_LeaveResponse>
{
    protected override async ETTask Run(Scene scene, G2Other_LeaveRequest request, Other2G_LeaveResponse response)
    {
        scene.GetComponent<ChatComponent>().Leave(request.PlayerId);
        await ETTask.CompletedTask;
    }
}