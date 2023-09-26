namespace ET.Server
{
    [Invoke((long)SceneType.Game)]
    public class FiberInit_Game: AInvokeHandler<FiberInit, ETTask>
    {
        public override async ETTask Handle(FiberInit fiberInit)
        {
            Scene root = fiberInit.Fiber.Root;
            root.AddComponent<MailBoxComponent, MailBoxType>(MailBoxType.UnOrderedMessage);
            root.AddComponent<TimerComponent>();
            root.AddComponent<CoroutineLockComponent>();
            root.AddComponent<ProcessInnerSender>();
            root.AddComponent<DBManagerComponent>();

            await ETTask.CompletedTask;
        }
    }
}