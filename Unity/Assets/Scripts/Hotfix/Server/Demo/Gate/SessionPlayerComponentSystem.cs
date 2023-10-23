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
            root.GetComponent<MessageLocationSenderComponent>().Get(LocationType.Unit).Send(self.Player.Id, new G2M_SessionDisconnect());
            self.Scene().GetComponent<PlayerComponent>()?.Remove(self.Player);
        }

        [EntitySystem]
        private static void Awake(this SessionPlayerComponent self)
        {
        }
    }
}