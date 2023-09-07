namespace ET.Server;

[MessageHandler(SceneType.Map)]
public class W2Map_SaveDataRequest: MessageHandler<Scene, W2Other_SaveDataRequest, Other2W_SaveDataResponse>
{
    protected override async ETTask Run(Scene unit, W2Other_SaveDataRequest request, Other2W_SaveDataResponse response)
    {
        await ETTask.CompletedTask;
        unit.Fiber().Info("保存玩家数据!");
    }
}