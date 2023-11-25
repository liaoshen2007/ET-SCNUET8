using System.Collections.Generic;
using System.Linq;

namespace ET.Server;

[EntitySystemOf(typeof (PacketComponent))]
[FriendOf(typeof (PacketComponent))]
public static partial class PacketComponentSystem
{
    [EntitySystem]
    private static void Awake(this PacketComponent self)
    {
        self.AddTasks = new List<TaskProto>(20);
        self.DelTasks = new List<int>(20);
        self.AddItems = new List<ItemProto>(20);
        self.Timer = self.Scene().GetComponent<TimerComponent>().NewRepeatedTimer(200, TimerInvokeType.PacketUpdate, self);
    }

    [EntitySystem]
    private static void Destroy(this PacketComponent self)
    {
        self.AddTasks.Clear();
        self.DelTasks.Clear();
        self.AddItems.Clear();
        self.Scene().GetComponent<TimerComponent>().Remove(ref self.Timer);
    }

    [Invoke(TimerInvokeType.PacketUpdate)]
    private class PacketUpdate: ATimer<PacketComponent>
    {
        protected override void Run(PacketComponent self)
        {
            self.SyncPacket();
        }
    }

    public static void UpdateTask(this PacketComponent self, TaskProto task)
    {
        self.AddTasks.Add(task);
    }

    public static void UpdateTask(this PacketComponent self, int id)
    {
        self.DelTasks.Add(id);
    }

    public static void UpdateItem(this PacketComponent self, ItemProto item)
    {
        self.AddItems.Add(item);
    }

    public static void SyncPacket(this PacketComponent self)
    {
        if (self.AddTasks.Count > 0)
        {
            var list = self.AddTasks.ToList();
            self.GetParent<Unit>().SendToClient(new M2C_UpdateTask() { List = list });
            self.AddTasks.Clear();
        }

        if (self.DelTasks.Count > 0)
        {
            var list = self.DelTasks.ToList();
            self.GetParent<Unit>().SendToClient(new M2C_DeleteTask() { List = list });
            self.DelTasks.Clear();
        }

        if (self.AddItems.Count > 0)
        {
            var list = self.AddItems.ToList();
            self.GetParent<Unit>().SendToClient(new M2C_UpdateItem() { List = list });
            self.AddItems.Clear();
        }
    }
}