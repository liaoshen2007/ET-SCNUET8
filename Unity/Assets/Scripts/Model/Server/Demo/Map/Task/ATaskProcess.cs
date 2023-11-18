using System.Collections.Generic;

namespace ET.Server
{
    public class TaskProcessAttribute: BaseAttribute
    {
        public string Key { get; }

        public TaskProcessAttribute(string key)
        {
            this.Key = key;
        }
    }

    public abstract class ATaskProcess
    {
        public abstract KeyValuePair<long, long> Run(TaskComponent self, TaskData task, long[] cfgArgs);
    }
}