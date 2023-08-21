using System;

namespace ET.Server;

[FriendOf(typeof (AbilityComponent))]
public static class AbilitySystem
{
    public static void AddAbility(this AbilityComponent self, int ability)
    {
        self.AbilityList[ability] += 1;
        int value = 0;
        for (var i = 0; i < self.AbilityList.Length; i++)
        {
            var v = self.AbilityList[i];
            if (v != 0)
            {
                value |= 1 << i;
            }
        }

        self.Value = value;
    }

    public static void RemoveAbility(this AbilityComponent self, int ability)
    {
        self.AbilityList[ability] -= 1;
        int value = self.Value;
        for (var i = 0; i < self.AbilityList.Length; i++)
        {
            var v = self.AbilityList[i];
            if (v == 0)
            {
                value &= ~(1 << i);
            }
        }

        self.Value = value;
    }

    public static bool HasAbility(this AbilityComponent self, int ability)
    {
        return (self.Value & ability) == 0;
    }

    public static bool HasAbility(this AbilityComponent self, RoleAbility ability)
    {
        return (self.Value & (int) ability) == 0;
    }
}