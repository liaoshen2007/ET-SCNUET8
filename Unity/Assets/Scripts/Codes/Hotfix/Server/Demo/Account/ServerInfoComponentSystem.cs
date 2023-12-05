using System.Collections.Generic;

namespace ET.Server
{
    [EntitySystemOf(typeof (ServerInfoComponent))]
    [FriendOf(typeof (ServerInfoComponent))]
    public static partial class ServerInfoComponentSystem
    {
        [Invoke(TimerInvokeType.ServerCheck)]
        public class CheckServerListTimer: ATimer<ServerInfoComponent>
        {
            protected override void Run(ServerInfoComponent self)
            {
                self.Init().Coroutine();
            }
        }

        [EntitySystem]
        private static void Awake(this ServerInfoComponent self)
        {
            self.Init().Coroutine();
            self.Timer = self.Fiber().Root.GetComponent<TimerComponent>().NewRepeatedTimer(10000, TimerInvokeType.ServerCheck, self);
        }

        [EntitySystem]
        private static void Destroy(this ServerInfoComponent self)
        {
            self.Fiber().Root.GetComponent<TimerComponent>()?.Remove(ref self.Timer);
            foreach (ServerInfo serverInfo in self.ServerInfos)
            {
                serverInfo?.Dispose();
            }

            self.ServerInfos.Clear();
        }

        /// <summary>
        /// 获取指定类型的服务器列表
        /// </summary>
        /// <param name="self"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<ServerInfoProto> GetServerList(this ServerInfoComponent self, int type)
        {
            var list = new List<ServerInfoProto>();
            foreach (ServerInfo info in self.ServerInfos)
            {
                if ((int) info.Status == type)
                {
                    var proto = new ServerInfoProto() { Id = info.Id, ServerName = info.ServerName, Status = (int) info.Status, };
                    list.Add(proto);
                }
            }

            return list;
        }

        public static ServerInfo GetServerInfo(this ServerInfoComponent self, int zoneId)
        {
            var info = self.ServerInfos.Find(serverInfo => serverInfo.Id == zoneId);
            return info;
        }

        private static async ETTask Init(this ServerInfoComponent self)
        {
            var serverInfoList = await self.Scene().GetComponent<DBManagerComponent>().GetDB().Query<ServerInfo>(d => true);
            if (serverInfoList is not { Count: > 0 })
            {
                Log.Info("serverInfo  count is zero");
                self.ServerInfos.Clear();
                var serverInfoConfigs = ServerInfoConfigCategory.Instance.GetAll();

                foreach (ServerInfoConfig info in serverInfoConfigs.Values)
                {
                    ServerInfo newServerInfo = self.AddChildWithId<ServerInfo>(info.Id);
                    newServerInfo.ServerName = info.ServerName;
                    newServerInfo.Status = (ServerStatus) info.State;
                    self.ServerInfos.Add(newServerInfo);
                    await self.Scene().GetComponent<DBManagerComponent>().GetDB().Save(newServerInfo);
                }

                return;
            }

            foreach (ServerInfo info in self.ServerInfos)
            {
                self.RemoveChild(info.Id);
            }

            self.ServerInfos.Clear();
            foreach (var serverInfo in serverInfoList)
            {
                if (serverInfo.OpenTime == 0)
                {
                    serverInfo.OpenTime = TimeInfo.Instance.ServerFrameTime();
                }
                
                self.AddChild(serverInfo);
                self.ServerInfos.Add(serverInfo);
            }

            await ETTask.CompletedTask;
        }
    }
}