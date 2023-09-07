using System;
using System.Diagnostics;

namespace ET.Server
{
    [EntitySystemOf(typeof (WatcherComponent))]
    [FriendOf(typeof (WatcherComponent))]
    public static partial class WatcherComponentSystem
    {
        [Invoke(TimerInvokeType.WatcherCheck)]
        public class WatcherCheckTimer: ATimer<WatcherComponent>
        {
            protected override void Run(WatcherComponent self)
            {
                self.Check();
            }
        }

        [EntitySystem]
        private static void Awake(this WatcherComponent self)
        {
            self.Timer = self.Fiber().TimerComponent.NewRepeatedTimer(5 * 1000, TimerInvokeType.WatcherCheck, self);
            self.Start();
        }

        [EntitySystem]
        private static void Destroy(this WatcherComponent self)
        {
            self.Fiber().TimerComponent.Remove(ref self.Timer);
        }

        public static void Check(this WatcherComponent self)
        {
            self.Start();
        }

        public static void Start(this WatcherComponent self)
        {
            string[] localIP = NetworkHelper.GetAddressIPs();
            var processConfigs = StartProcessConfigCategory.Instance.GetAll();
            foreach (StartProcessConfig startProcessConfig in processConfigs.Values)
            {
                if (!WatcherHelper.IsThisMachine(startProcessConfig.InnerIP, localIP))
                {
                    continue;
                }

                if (self.Processes.TryGetValue(startProcessConfig.Id, out var p) && !p.HasExited)
                {
                    continue;
                }

                if (self.Processes.Remove(startProcessConfig.Id))
                {
                    self.Fiber().Console($"删除已经退出的进程: {startProcessConfig.Id}");
                }

                Process process = WatcherHelper.StartProcess(startProcessConfig.Id);
                self.Processes.Add(startProcessConfig.Id, process);
            }
        }

        public static async ETTask SaveData(this WatcherComponent self)
        {
            var message = new W2Other_SaveDataRequest();

            async ETTask SaveDataAsync(StartSceneConfig config, IRequest m)
            {
                try
                {
                    var resp = await self.Root().GetComponent<MessageSender>().Call(config.ActorId, m);
                    if (resp.Error != ErrorCode.ERR_Success)
                    {
                        self.Fiber().Console($"保存数据错误: {resp.Error}, {config.Name} - {config.Id}");
                    }
                }
                catch (Exception e)
                {
                    self.Fiber().Error($"保存数据异常: {e}");
                }
            }

            using (ListComponent<ETTask> list = ListComponent<ETTask>.Create())
            {
                foreach (var process in self.Processes.Keys)
                {
                    var sceneCfgList = StartSceneConfigCategory.Instance.GetByProcess(process);
                    foreach (var config in sceneCfgList)
                    {
                        list.Add(SaveDataAsync(config, message));
                    }
                }

                await ETTaskHelper.WaitAll(list);
            }
        }

        public static void OpenProcess(this WatcherComponent self, int processId)
        {
            if (processId < 0)
            {
                Start(self);
            }
            else
            {
                if (self.Processes.ContainsKey(processId))
                {
                    return;
                }

                var processCfg = StartProcessConfigCategory.Instance.Get(processId);
                if (processCfg == null)
                {
                    return;
                }

                string[] localIP = NetworkHelper.GetAddressIPs();
                if (!WatcherHelper.IsThisMachine(processCfg.InnerIP, localIP))
                {
                    return;
                }

                Process process = WatcherHelper.StartProcess(processCfg.Id);
                self.Processes.Add(processCfg.Id, process);
            }
        }

        public static void CloseProcess(this WatcherComponent self, int processId)
        {
            if (processId < 0)
            {
                try
                {
                    foreach (Process process in self.Processes.Values)
                    {
                        process.Kill();
                    }
                }
                catch (Exception e)
                {
                    self.Fiber().Error(e);
                }

                self.Processes.Clear();
            }
            else
            {
                if (self.Processes.TryGetValue(processId, out var process))
                {
                    self.Processes.Remove(processId);
                    process.Kill();
                }
            }
        }

        public static async ETTask GG(this WatcherComponent self)
        {
            self.Fiber().Console("准备关服...");
            await self.Fiber().TimerComponent.WaitAsync(1000);
            self.Fiber().Console("正在保存数据...");
            await self.SaveData();
            await self.Fiber().TimerComponent.WaitAsync(1000);
            self.CloseProcess(-1);

            Process.GetCurrentProcess().Kill();
        }
    }
}