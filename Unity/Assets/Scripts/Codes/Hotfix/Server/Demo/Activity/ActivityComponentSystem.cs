namespace ET.Server;

[EntitySystemOf(typeof (ActivityComponent))]
[FriendOf(typeof (ActivityComponent))]
public static partial class ActivityComponentSystem
{
    [EntitySystem]
    private static void Awake(this ActivityComponent self)
    {
        self.Timer = self.Scene().GetComponent<TimerComponent>().NewRepeatedTimer(5000, TimerInvokeType.ActivityUpdate, self);
    }

    [EntitySystem]
    private static void Destroy(this ActivityComponent self)
    {
        self.Scene().GetComponent<TimerComponent>().Remove(ref self.Timer);
        self.ActivityDataDict.Clear();
    }

    [EntitySystem]
    private static void Load(this ActivityComponent self)
    {
    }

    [Invoke(TimerInvokeType.ActivityUpdate)]
    private class ActivityCheck: ATimer<ActivityComponent>
    {
        protected override void Run(ActivityComponent self)
        {
            self.Check();
        }
    }

    private static void Check(this ActivityComponent self)
    {
    }

    private static void AddActivity(this ActivityComponent self)
    {
    }
}