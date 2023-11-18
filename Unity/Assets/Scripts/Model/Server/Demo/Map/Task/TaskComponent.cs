using System.Collections.Generic;

namespace ET.Server
{
    public struct AddTaskData
    {
        public int LogEvent { get; set; }

        public bool Replace { get; set; }

        public object Arg1 { get; set; }

        public object Arg2 { get; set; }
    }

    public struct FinishTaskData
    {
        public List<long> Args { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        public long FinishTime { get; set; }
    }

    public struct TaskFunc
    {
        public TaskFunc(string taskArgs, KeyValuePair<int, int> process, string taskHandle = "Default", string taskProcess = "Default")
        {
            this.TaskArgs = taskArgs;
            this.Process = process;
            this.TaskHandle = taskHandle;
            this.TaskProcess = taskProcess;
        }

        public KeyValuePair<int, int> Process { get; }
        public string TaskArgs { get; }
        public string TaskHandle { get; }
        public string TaskProcess { get; }
    }

    [ComponentOf(typeof (Unit))]
    public class TaskComponent: Entity, IAwake, IDestroy, ILoad
    {
        public Dictionary<TaskEventType, TaskFunc> TaskFuncDict;

        public Dictionary<string, ATaskArgs> TaskArgDict;
        public Dictionary<string, ATaskHandler> TaskHanderDict;
        public Dictionary<string, ATaskProcess> TaskProcessDict;

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