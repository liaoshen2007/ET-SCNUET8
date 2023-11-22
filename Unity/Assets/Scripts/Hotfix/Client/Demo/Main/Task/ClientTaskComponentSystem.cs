using System.Collections.Generic;

namespace ET.Client;

[EntitySystemOf(typeof (ClientTaskComponent))]
[FriendOf(typeof (ClientTaskComponent))]
public static partial class ClientTaskComponentSystem
{
    [EntitySystem]
    private static void Awake(this ClientTaskComponent self)
    {
        self.TaskDict = new Dictionary<int, TaskData>();
        self.FinishTaskDict = new Dictionary<int, long>();
    }

    [EntitySystem]
    private static void Destroy(this ClientTaskComponent self)
    {
        self.TaskDict.Clear();
        self.FinishTaskDict.Clear();
    }

    private static void AddUpdateTask(this ClientTaskComponent self, TaskProto proto)
    {
        var task = self.AddChildWithId<TaskData>(proto.Id);
        task.ToTask(proto);
        self.TaskDict.Add(proto.Id, task);
        EventSystem.Instance.Publish(self.Scene(), new AddUpdateTask() { TaskData = task });
    }

    public static void AddUpdateTask(this ClientTaskComponent self, List<TaskProto> list)
    {
        foreach (var proto in list)
        {
            self.AddUpdateTask(proto);
        }
    }

    public static void UpdateFinishTask(this ClientTaskComponent self, Dictionary<int, long> finishMap)
    {
        foreach (var pair in finishMap)
        {
            self.FinishTaskDict.Add(pair.Key, pair.Value);
        }
    }

    public static void RemoveTask(this ClientTaskComponent self, List<int> list)
    {
        foreach (int taskId in list)
        {
            self.RemoveTask(taskId);
        }
    }

    public static void RemoveTask(this ClientTaskComponent self, int taskId)
    {
        if (self.TaskDict.TryGetValue(taskId, out var task))
        {
            self.TaskDict.Remove(taskId);
            EventSystem.Instance.Publish(self.Scene(), new DeleteTask() { TaskData = task });
        }

        self.FinishTaskDict.Remove(taskId);
    }

    public static bool TaskIsFinish(this ClientTaskComponent self, int taskId)
    {
        if (self.FinishTaskDict.TryGetValue(taskId, out _))
        {
            return true;
        }

        if (!self.TaskDict.TryGetValue(taskId, out var task))
        {
            return false;
        }

        return task.Status == TaskStatus.Commit;
    }
}