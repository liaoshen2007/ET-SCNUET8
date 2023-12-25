using System.Collections.Generic;

namespace ET.Server;

/// <summary>
/// 治疗
/// 元素类型,技能发挥比例,附攻,最大数量,视图ID
/// </summary>
[Skill("Heal")]
public class HealEffect: ASkillEffect
{
    public override HurtPkg Run(SkillComponent self, SkillUnit skill, List<Unit> RoleList, SkillDyna dyna)
    {
        return default;
    }
}