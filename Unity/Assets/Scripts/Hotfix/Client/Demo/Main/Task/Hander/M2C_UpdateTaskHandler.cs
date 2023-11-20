namespace ET.Client
{
    [MessageHandler(SceneType.Demo)]
    public class M2C_UpdateTaskHandler: MessageHandler<Scene, M2C_UpdateTask>
    {
        protected override async ETTask Run(Scene root, M2C_UpdateTask message)
        {
            var taskCom = root.GetComponent<ClientTaskComponent>();
            taskCom.AddUpdateTask(message.List);

            await ETTask.CompletedTask;
        }
    }
}