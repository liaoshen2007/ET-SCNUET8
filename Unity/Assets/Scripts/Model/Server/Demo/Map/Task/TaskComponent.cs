using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ET.Server
{
    public struct AddTaskData
    {
        public int LogEvent { get; set; }

        public bool Replace { get; set; }
        
        public bool NotUpdate {get; set;}

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
    public class TaskComponent: Entity, IAwake, IDestroy, ILoad, ICache, ITransfer
    {
        [BsonIgnore]
        public Dictionary<TaskEventType, TaskFunc> TaskFuncDict;

        [BsonIgnore]
        public Dictionary<string, ATaskArgs> TaskArgDict;
        [BsonIgnore]
        public Dictionary<string, ATaskHandler> TaskHanderDict;
        [BsonIgnore]
        public Dictionary<string, ATaskProcess> TaskProcessDict;

        /// <summary>
        /// 所有的任务字典
        /// </summary>
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, TaskData> TaskDict;

        /// <summary>
        /// 完成任务字典
        /// </summary>
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, FinishTaskData> FinishTaskDict;
    }
}