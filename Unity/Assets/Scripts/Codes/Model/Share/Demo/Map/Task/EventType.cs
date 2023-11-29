using System.Collections.Generic;

namespace ET
{
    /// <summary>
    /// 添加任务
    /// </summary>
    public struct AddUpdateTask
    {
        public TaskData TaskData { get; set; }
    }

    /// <summary>
    /// 完成任务
    /// </summary>
    public struct FinishTask
    {
        public TaskData TaskData { get; set; }
    }

    /// <summary>
    /// 提交任务
    /// </summary>
    public struct CommitTask
    {
        public TaskData TaskData { get; set; }
    }

    /// <summary>
    /// 删除任务
    /// </summary>
    public struct DeleteTask
    {
        public TaskData TaskData { get; set; }
    }
}