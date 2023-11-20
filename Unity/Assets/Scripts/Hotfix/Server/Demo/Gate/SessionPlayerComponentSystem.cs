namespace ET.Server
{
    [EntitySystemOf(typeof (SessionPlayerComponent))]
    public static partial class SessionPlayerComponentSystem
    {
        [EntitySystem]
        private static void Destroy(this SessionPlayerComponent self)
        {
            Scene root = self.Root();
            if (root.IsDisposed)
            {
                return;
            }

            EventSystem.Instance.Publish(self.Scene(), new LeaveGame() { Player = self.Player });

            // 发送断线消息
            SendLeaveMsg().Coroutine();

            async ETTask SendLeaveMsg()
            {
                await root.GetComponent<MessageLocationSenderComponent>().Get(LocationType.Unit).Call(self.Player.Id, new G2M_SessionDisconnect());
                await self.Player.RemoveLocation(LocationType.Player);
                await self.Player.RemoveLocation(LocationType.Unit);
                root.GetComponent<MessageLocationSenderComponent>().Get(LocationType.Unit).Remove(self.Player.Id);
                root.GetComponent<PlayerComponent>()?.Remove(self.Player);
            }
        }

        [EntitySystem]
        private static void Awake(this SessionPlayerComponent self)
        {
        }
    }
}