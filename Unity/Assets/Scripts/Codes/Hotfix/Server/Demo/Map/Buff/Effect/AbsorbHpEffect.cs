namespace ET.Server;

/// <summary>
/// 护盾
/// 发挥比例;发挥值;暴击倍率(万比)
/// </summary>
[Buff("AbsorbHp")]
public class AbsorbHpEffect: ABuffEffect
{
    protected override void OnCreate(BuffComponent self, BuffUnit buff, EffectArg effectArg)
    {
        var effect = effectArg.Args;
        var rate = effect[0];
        var extra = effect[1];
        var unit = self.GetParent<Unit>();
        var value = (unit.GetAttrValue(NumericType.MaxHp) * rate / 10000 + extra).Ceil() * buff.Layer;
        if (HurtHelper.IsCrit(unit))
        {
            value = (value * effect[2] / 10000f).Ceil();
        }

        unit.GetComponent<ShieldComponent>().AddShield(buff.BuffId, value);
    }

    protected override void OnRemove(BuffComponent self, BuffUnit buff, EffectArg effectArg)
    {
        self.GetParent<Unit>().GetComponent<ShieldComponent>().RemoveShield(buff.BuffId);
    }
}