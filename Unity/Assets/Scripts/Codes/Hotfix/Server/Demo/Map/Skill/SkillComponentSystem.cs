namespace ET.Server.Skill;

[EntitySystemOf(typeof (SkillComponent))]
public static partial class SkillComponentSystem
{
    [EntitySystem]
    private static void Awake(this SkillComponent self)
    {
    }
}