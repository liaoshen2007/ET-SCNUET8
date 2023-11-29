namespace ET.Server;

public static class ShieldComponentSystem
{
    /// <summary>
    /// 护盾修改事件
    /// </summary>
    public struct UnitShieldChange
    {
        public Unit Unit { get; set; }
    }

    public class ShieldComponentAwakeSystem: AwakeSystem<ShieldComponent>
    {
        protected override void Awake(ShieldComponent self)
        {
        }
    }

    public static void AddShield(this ShieldComponent self, int buffId, long value)
    {
        self.ShieldIdDict[buffId] = value;
        self.ReCaculate();
    }

    public static long RemoveShield(this ShieldComponent self, int buffId)
    {
        if (self.ShieldIdDict.TryGetValue(buffId, out var value))
        {
            self.ShieldIdDict.Remove(buffId);
        }

        self.ReCaculate();
        return value;
    }

    public static void ClearShield(this ShieldComponent self)
    {
        self.ShieldIdDict.Clear();
        self.ReCaculate();
    }

    /// <summary>
    /// 重新计算护盾值
    /// </summary>
    /// <param name="self"></param>
    private static void ReCaculate(this ShieldComponent self)
    {
        EventSystem.Instance.Publish(self.Scene(), new UnitShieldChange() { Unit = self.GetParent<Unit>() });
    }
}