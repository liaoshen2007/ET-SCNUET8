using System.Collections.Generic;

namespace ET.Server;

[EntitySystemOf(typeof (TaskComponent))]
public static partial class TaskComponentSystem
{
    [EntitySystem]
    private static void Awake(this TaskComponent self)
    {
    }

    [EntitySystem]
    private static void Destroy(this TaskComponent self)
    {
    }

    private static void UpdateTask(this TaskComponent self, TaskData task)
    {
        self.GetParent<Unit>().SendToClient(new M2C_UpdateTask() { List = { task.ToTaskProto() } });
    }

    /// <summary>
    /// 添加任务
    /// </summary>
    /// <param name="self"></param>
    /// <param name="taskId">任务ID</param>
    /// <param name="data">任务数据</param>
    /// <returns></returns>
    public static int AddTsak(this TaskComponent self, int taskId, AddTaskData data)
    {
        if (data.Replace)
        {
            self.DelTask(taskId, LogDef.TaskAdd);
        }
        else
        {
            if (self.TaskDict.TryGetValue(taskId, out var task))
            {
                return ErrorCode.ERR_Success;
            }

            var taskCfg = TaskConfigCategory.Instance.Get(taskId);
            var ret = Cmd.Instance.ProcessCmdList(self.GetParent<Unit>(), taskCfg.GetCmdList);
            if (ret.Errco != ErrorCode.ERR_Success)
            {
                return ret.Errco;
            }

            task = self.AddChildWithId<TaskData>(taskId);
            task.Status = TaskStatus.Accept;
            task.AcceptTime = TimeInfo.Instance.ServerFrameTime();
            self.TaskDict.Add(taskId, task);
            EventSystem.Instance.Publish(self.Scene(), new AddTask() { TaskData = task });
            self.UpdateTask(task);
        }

        return ErrorCode.ERR_Success;
    }

    /// <summary>
    /// 删除玩家任务
    /// </summary>
    /// <param name="self"></param>
    /// <param name="taskId">任务ID</param>
    /// <param name="logEvent">来源日志</param>
    public static void DelTask(this TaskComponent self, int taskId, int logEvent)
    {
        if (self.TaskDict.TryGetValue(taskId, out var task))
        {
            self.TaskDict.Remove(taskId);
            EventSystem.Instance.Publish(self.Scene(), new DeleteTask() { TaskData = task });
            self.GetParent<Unit>().SendToClient(new M2C_DeleteTask() { List = { taskId } });
            task.Dispose();
        }

        self.FinishTaskDict.Remove(taskId);
    }

    public static int CommitTask(this TaskComponent self, int taskId, int logEvent)
    {
        if (!self.TaskDict.TryGetValue(taskId, out var task))
        {
            return ErrorCode.ERR_CantFindCfg;
        }

        return ErrorCode.ERR_Success;
    }

    public static int TimeoutTask(this TaskComponent self, int taskId, int logEvent)
    {
        if (!self.TaskDict.TryGetValue(taskId, out var task))
        {
            return ErrorCode.ERR_CantFindCfg;
        }

        task.Status = TaskStatus.Timeout;
        self.UpdateTask(task);
        return ErrorCode.ERR_Success;
    }

    public static int FinishTask(this TaskComponent self, int taskId)
    {
        if (!self.TaskDict.TryGetValue(taskId, out var task))
        {
            return ErrorCode.ERR_CantFindCfg;
        }

        task.Args[0] = task.Config.Args[0];
        task.Status = TaskStatus.Finish;
        task.FinishTime = TimeInfo.Instance.ServerFrameTime();
        EventSystem.Instance.Publish(self.Scene(), new FinishTask() { TaskData = task });
        self.UpdateTask(task);
        if (task.Config.AutoCommit)
        {
            self.CommitTask(taskId, LogDef.TaskAutoCommit);
        }

        if (task.Config.FinishShow)
        {
            self.FinishTaskDict.Add(taskId, new FinishTaskData() { Args = task.Args, FinishTime = task.FinishTime });
        }

        return ErrorCode.ERR_Success;
    }

    public static bool TaskIsCommit(this TaskComponent self, int taskId)
    {
        if (self.FinishTaskDict.ContainsKey(taskId))
        {
            return true;
        }

        if (!self.TaskDict.TryGetValue(taskId, out var task))
        {
            return false;
        }

        return task.Status == TaskStatus.Commit;
    }

    public static bool TaskIsFinish(this TaskComponent self, int taskId)
    {
        if (self.FinishTaskDict.ContainsKey(taskId))
        {
            return true;
        }

        if (!self.TaskDict.TryGetValue(taskId, out var task))
        {
            return false;
        }

        return task.Status is TaskStatus.Finish or TaskStatus.Commit;
    }

    public static void UpdateTaskStatus(this TaskComponent self, int taskId, TaskStatus status)
    {
        if (!self.TaskDict.TryGetValue(taskId, out var task))
        {
            return;
        }

        task.Status = status;
        self.UpdateTask(task);
    }

    public static void SetTaskArgs(this TaskComponent self, int taskId, List<long> args)
    {
        if (!self.TaskDict.TryGetValue(taskId, out var task))
        {
            return;
        }

        task.Args = args;
        self.UpdateTask(task);
    }
}