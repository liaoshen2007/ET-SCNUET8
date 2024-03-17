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
            self.SendLeaveMsg().Coroutine();
            
            Log.Debug("Play leaves:"+self.Player.Account);
        }

        //bug 几个问题：下线的时候没有报错到数据库，不能主动下线或者不能检测到幽灵session后下线~~
        //todo 有空还是直接回去运行游戏看看ET6的下线怎么处理主动下线的~~
        private static async ETTask SendLeaveMsg(this SessionPlayerComponent self)
        {
            Scene root = self.Root();
            await root.GetComponent<MessageLocationSenderComponent>().Get(LocationType.Unit).Call(self.Player.Id, new G2M_SessionDisconnect());
            await self.Player.RemoveLocation(LocationType.Player);
            await self.Player.RemoveLocation(LocationType.Unit);
            root.GetComponent<MessageLocationSenderComponent>().Get(LocationType.Unit).Remove(self.Player.Id);
            root.GetComponent<PlayerComponent>()?.Remove(self.Player);
        }

        [EntitySystem]
        private static void Awake(this SessionPlayerComponent self)
        {
        }
    }
}