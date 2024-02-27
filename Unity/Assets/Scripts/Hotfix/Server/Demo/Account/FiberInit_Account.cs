namespace ET.Server
{
    [Invoke((long) SceneType.Account)]
    public class FiberInit_Account: AInvokeHandler<FiberInit, ETTask>
    {
        public override async ETTask Handle(FiberInit fiberInit)
        {
            Scene root = fiberInit.Fiber.Root;
            root.AddComponent<MailBoxComponent, MailBoxType>(MailBoxType.UnOrderedMessage);
            root.AddComponent<TimerComponent>();
            root.AddComponent<CoroutineLockComponent>();
            root.AddComponent<ProcessInnerSender>();
            root.AddComponent<DBManagerComponent>();
            root.AddComponent<MessageSender>();

            StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.Get((int)root.Id);
            root.AddComponent<HttpComponent, string>($"http://*:{startSceneConfig.Port}/");
            root.AddComponent<ServerInfoComponent>();
            root.AddComponent<RoleInfosComponent>();
            root.AddComponent<AccountComponent>();

            await ETTask.CompletedTask;
        }
    }
}