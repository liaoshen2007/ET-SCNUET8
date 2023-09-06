using ET;

[MessageHandler(SceneType.Rank)]
public class C2Rank_GetRankHandler : MessageHandler<Scene, C2Rank_GetRankRequest, Ran2C_GetRankResponse>
{
    protected override async ETTask Run(Scene scene, C2Rank_GetRankRequest request, Ran2C_GetRankResponse response)
    {
        await ETTask.CompletedTask;
    }
}