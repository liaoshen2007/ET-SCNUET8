using System;

namespace ET.Server;

[MessageHandler(SceneType.Rank)]
public class W2Other_SaveRankRequest: MessageHandler<Scene, ET.W2Other_SaveDataRequest, Other2W_SaveDataResponse>
{
    protected override async ETTask Run(Scene scene, ET.W2Other_SaveDataRequest request, Other2W_SaveDataResponse response)
    {
        await ETTask.CompletedTask;
        scene.Fiber().Info("保存排行榜数据!");
    }
}