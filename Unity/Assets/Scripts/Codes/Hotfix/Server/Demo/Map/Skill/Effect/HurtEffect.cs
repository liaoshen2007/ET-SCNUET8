using System.Collections.Generic;

namespace ET.Server;

/// <summary>
/// 伤害
/// 技能修复参数,最大数量,视图ID
/// </summary>
[Skill("Hurt")]
public class HurtEffect: ASkillEffect
{
    public override HurtPkg Run(SkillComponent self, SkillUnit skill, List<Unit> RoleList, SkillDyna dyna)
    {
        return default;
    }
}