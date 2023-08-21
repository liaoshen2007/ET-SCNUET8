namespace ET.Server;

public static class ShieldComponentSystem
{
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
            self.ReCaculate();
        }

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
    public static void ReCaculate(this ShieldComponent self)
    {
        self.GetParent<Unit>().GetComponent<NumericComponent>().SetNoEvent(NumericType.Sp, 0);
        long total = 0;
        foreach (var (_, value) in self.ShieldIdDict)
        {
            total += value;
        }

        self.GetParent<Unit>().GetComponent<NumericComponent>().SetNoEvent(NumericType.Sp, total);
    }
}