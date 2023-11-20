using System.Collections.Generic;

namespace ET.Client
{
    /// <summary>
    /// 初始化任务
    /// </summary>
    public struct InitTask
    {
        public List<TaskData> List { get; set; }
    }

    [ComponentOf(typeof (Scene))]
    public class ClientTaskComponent: Entity, IAwake, IDestroy
    {
        public Dictionary<int, TaskData> TaskDict;

        public Dictionary<int, FinishTask> FinishTaskDict;
    }
}