namespace ET.Server;

[MessageHandler(SceneType.Chat)]
public class C2Chat_SendHandler: MessageHandler<ChatUnit, C2Chat_SendRequest, Chat2C_SendResponse>
{
    protected override async ETTask Run(ChatUnit unit, C2Chat_SendRequest request, Chat2C_SendResponse response)
    {
        await ETTask.CompletedTask;
    }
}