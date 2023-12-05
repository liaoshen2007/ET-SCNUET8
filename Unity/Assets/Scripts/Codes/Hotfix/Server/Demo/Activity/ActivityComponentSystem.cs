namespace ET.Server;

[EntitySystemOf(typeof (ActivityComponent))]
[FriendOf(typeof (ActivityComponent))]
[FriendOf(typeof (ActivityData))]
public static partial class ActivityComponentSystem
{
    [EntitySystem]
    private static void Awake(this ActivityComponent self)
    {
        self.Timer = self.Scene().GetComponent<TimerComponent>().NewRepeatedTimer(1000, TimerInvokeType.ActivityUpdate, self);
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

    private static void GetOpenTime(ActivityConfig config, long roleCreateTime = 0)
    {
        
    }
    
    private static void AddActivity(this ActivityComponent self, ActivityConfig config, long roleId = 0)
    {
        var actData = self.AddChildWithId<ActivityData>(config.Id);
        actData.Key = $"{self}_{roleId}";
        actData.Type = (ActivityType) config.ActivityType;
        actData.RoleId = roleId;
        self.ActivityDataDict.Add(actData.Key, actData);
        if (roleId > 0)
        {
            self.RoleActivityDataDict.Add(actData.Key, actData);
        }
        // var status = ActivityStatus.Close;
        long now = TimeInfo.Instance.ServerFrameTime();
    }
}