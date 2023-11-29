namespace ET.Server;

/// <summary>
/// 添加能力码
/// </summary>
[Buff("AddAbility")]
public class AddAbilityEffect: ABuffEffect
{
    protected override void OnCreate(BuffComponent self, Buff buff, EffectArg effectArg)
    {
        foreach (int arg in effectArg.Args)
        {
            self.GetParent<Unit>().GetComponent<AbilityComponent>().AddAbility(arg);
        }
    }

    protected override void OnRemove(BuffComponent self, Buff buff, EffectArg effectArg)
    {
        foreach (int arg in effectArg.Args)
        {
            self.GetParent<Unit>().GetComponent<AbilityComponent>().RemoveAbility(arg);
        }
    }
}