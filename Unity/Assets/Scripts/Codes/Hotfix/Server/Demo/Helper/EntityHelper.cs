namespace ET.Server;

public static class EntityHelper
{
    public static async ETTask<long> GetServerOpenTime(Entity self, int zoneId)
    {
        if (self.Scene().SceneType == SceneType.Account)
        {
            var serverIfo = self.Scene().GetComponent<ServerInfoComponent>().GetServerInfo(zoneId);
            return serverIfo.OpenTime;
        }

        var account = StartSceneConfigCategory.Instance.Account.ActorId;
        var r = await self.Scene().GetComponent<MessageSender>()
                .Call(account, new O2A_GetServerTime() { ZoneId = zoneId }) as A2O_GetServerTime;
        return r.OpenTime;
    }
}