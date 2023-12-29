namespace ET.Server;

[FriendOf(typeof (AbilityComponent))]
public static class AbilitySystem
{
    public static void AddAbility(this AbilityComponent self, int ability)
    {
        self.abilityList[ability] += 1;
        int value = 0;
        for (var i = 0; i < self.abilityList.Length; i++)
        {
            var v = self.abilityList[i];
            if (v != 0)
            {
                value |= 1 << i;
            }
        }

        self.Value = value;
    }

    public static void RemoveAbility(this AbilityComponent self, int ability)
    {
        self.abilityList[ability] -= 1;
        int value = self.Value;
        for (var i = 0; i < self.abilityList.Length; i++)
        {
            var v = self.abilityList[i];
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
        return (self.Value & (int)ability) == 0;
    }

    /// <summary>
    /// 能否普攻
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static bool CanNormalAttack(this Unit self)
    {
        return self.GetComponent<AbilityComponent>().HasAbility(RoleAbility.Attack);
    }

    /// <summary>
    /// 是否无敌
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static bool IsInvincible(this Unit self)
    {
        return self.GetComponent<AbilityComponent>().HasAbility(RoleAbility.Invincible);
    }

    /// <summary>
    /// 能否使用技能
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static bool CanSkill(this Unit self)
    {
        return self.GetComponent<AbilityComponent>().HasAbility(RoleAbility.Skill);
    }

    /// <summary>
    /// 能否移动
    /// </summary>
    /// <param name="self"></param>
    /// <returns></returns>
    public static bool CanMove(this Unit self)
    {
        return self.GetComponent<AbilityComponent>().HasAbility(RoleAbility.Move);
    }
}