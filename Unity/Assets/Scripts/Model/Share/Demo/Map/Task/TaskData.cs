using System.Collections.Generic;

namespace ET
{
    public enum TaskStatus
    {
        /// <summary>
        /// 已接取
        /// </summary>
        Accept = 1,

        /// <summary>
        /// 已完成
        /// </summary>
        Finish = 2,

        /// <summary>
        /// 已提交
        /// </summary>
        Commit = 3,

        /// <summary>
        /// 超时
        /// </summary>
        Timeout = 4,
    }

    [ChildOf]
    public class TaskData: Entity, IAwake
    {
        /// <summary>
        /// 任务状态
        /// </summary>
        public TaskStatus Status { get; set; }

        /// <summary>
        /// 活动参数
        /// </summary>
        public List<long> Args { get; set; } = new List<long>();

        public long Min { get; set; }

        public long Max { get; set; }

        /// <summary>
        /// 接取时间
        /// </summary>
        public long AcceptTime { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        public long FinishTime { get; set; }
        
        /// <summary>
        /// 超时时间
        /// </summary>
        public long TimeoutTime { get; set; }

        /// <summary>
        /// 任务配置
        /// </summary>
        public TaskConfig Config
        {
            get
            {
                return TaskConfigCategory.Instance.Get((int) this.Id);
            }
        }
    }
}