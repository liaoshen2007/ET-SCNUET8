namespace ET.Client
{
    [EntitySystemOf(typeof (ServerInfoComponent))]
    [FriendOf(typeof (ServerInfoComponent))]
    public static partial class ServerInfoComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ServerInfoComponent self)
        {
        }

        [EntitySystem]
        private static void Destroy(this ServerInfoComponent self)
        {
            foreach (var serverInfo in self.ServerInfoList)
            {
                serverInfo?.Dispose();
            }

            self.ServerInfoList.Clear();
            self.CurrentServerId = 0;
        }

        public static void Add(this ServerInfoComponent self, ServerInfo serverInfo)
        {
            self.ServerInfoList.Add(serverInfo);
        }
    }
}