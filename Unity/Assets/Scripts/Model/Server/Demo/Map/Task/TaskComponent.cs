using System.Collections.Generic;

namespace ET.Server
{
    public struct AddTaskData
    {
        public int LogEvent { get; set; }

        public bool Replace { get; set; }
    }

    public struct FinishTaskData
    {
        public List<long> Args { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        public long FinishTime { get; set; }
    }

    [ComponentOf(typeof (Unit))]
    public class TaskComponent: Entity, IAwake, IDestroy
    {
        /// <summary>
        /// 所有的任务字典
        /// </summary>
        public Dictionary<int, TaskData> TaskDict { get; } = new Dictionary<int, TaskData>();

        /// <summary>
        /// 完成任务字典
        /// </summary>
        public Dictionary<int, FinishTaskData> FinishTaskDict { get; } = new Dictionary<int, FinishTaskData>();
    }
}