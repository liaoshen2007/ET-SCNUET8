namespace ET.Client
{
    [Invoke((long)SceneType.Robot)]
    public class FiberInit_Robot: AInvokeHandler<FiberInit, ETTask>
    {
        public override async ETTask Handle(FiberInit fiberInit)
        {
            Scene root = fiberInit.Fiber.Root;
            root.AddComponent<MailBoxComponent, MailBoxType>(MailBoxType.UnOrderedMessage);
            root.AddComponent<TimerComponent>();
            root.AddComponent<CoroutineLockComponent>();
            root.AddComponent<ProcessInnerSender>();
            root.AddComponent<PlayerComponent>();
            root.AddComponent<CurrentScenesComponent>();
            root.AddComponent<ObjectWait>();
            
            root.SceneType = SceneType.Client;
            Log.Error("Robot enter?!!"+root.Name);
            await EventSystem.Instance.PublishAsync(root, new AppStartInitFinish());
            
            //todo 我现在做一次性的Http指令，详见HttpRobotHandler，后期肯定是只能创建单个unit或者指定数量
            int randomId = RandomGenerator.RandomNumber(1,5000);
            await LoginHelper.Login(root, root.Name, "", randomId);
            await EnterMapHelper.EnterMapAsync(root);
            root.AddComponent<AIComponent, int>(1);
            Log.Error("AIComponent enter?!!");
        }
    }
}