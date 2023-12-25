namespace ET.Server;

/// <summary>
/// 战斗公式
/// </summary>
[Code]
public class FightFormula: Singleton<FightFormula>, ISingletonAwake
{
    private int CirtDamage;
    private float K;

    public void Awake()
    {
        this.CirtDamage = 25000;
        this.K = 0.5f;
    }

    /// <summary>
    /// 计算伤害
    /// </summary>
    /// <returns>返回伤害结果</returns>
    public long CalcHurt(
    NumericComponent attack,
    NumericComponent dst,
    int extraAttack,
    int skillAdjust = 10000,
    int element = 0,
    int judgment = 0,
    bool isCrit = false,
    bool isSymptom = false)
    {
        int dmgRate = isCrit? this.CirtDamage : 10000;
        var att = attack.GetAsLong(NumericType.Attack) + extraAttack;
        var defense = dst.GetAsLong(NumericType.Defense);
        var hp = attack.GetAsLong(NumericType.MaxHp);
        if (defense == 0)
        {
            return 1;
        }

        float d2;
        float skillAdj = skillAdjust / 10000f;
        switch (judgment)
        {
            case 1:
                d2 = att * att * skillAdj / (this.K * defense);
                break;
            case 2:
                d2 = defense * defense * skillAdj * skillAdj / (this.K * defense);
                break;
            case 3:
                d2 = hp * hp * skillAdj * skillAdj / (this.K * defense);
                break;
            default:
                return 0;
        }

        if (isSymptom)
        {
            d2 *= (1 + attack.GetAsLong(NumericType.Symptom) / 200f);
        }

        var d4 = d2 * dmgRate / 10000;
        var hurtRate = (1 + attack.GetAsLong(NumericType.HurtAddRate) / 10000f) * (1 - dst.GetAsLong(NumericType.HurtReduceRate) / 10000f);

        return (d4 * hurtRate * this.GetElementRate(attack, dst, element) + 0.0001f).Ceil();
    }

    //元素伤害比例
    private float GetElementRate(NumericComponent attack,
    NumericComponent dst, int element)
    {
        var el = (ElementType)element;
        switch (el)
        {
            case ElementType.None:
                return 1;
            case ElementType.Fire:
                return 1 + (attack.GetAsLong(NumericType.FireAdd) - dst.GetAsLong(NumericType.FireAvoid)) / 200f;
            case ElementType.Thunder:
                return 1 + (attack.GetAsLong(NumericType.ThunderAdd) - dst.GetAsLong(NumericType.ThunderAvoid)) / 200f;
            case ElementType.Ice:
                return 1 + (attack.GetAsLong(NumericType.IceAdd) - dst.GetAsLong(NumericType.IceAvoid)) / 200f;
            default:
                return 1;
        }
    }

    /// <summary>
    /// 是否暴击
    /// </summary>
    /// <returns></returns>
    public bool IsCrit(Unit target)
    {
        return target.GetComponent<NumericComponent>().GetAsLong(NumericType.CirtRate).IsHit();
    }

    /// <summary>
    /// 是否暴击
    /// </summary>
    /// <returns></returns>
    public bool IsCrit(NumericComponent target)
    {
        return target.GetAsLong(NumericType.CirtRate).IsHit();
    }

    /// <summary>
    /// 是否命中
    /// </summary>
    /// <returns></returns>
    public bool IsHit(Unit attack, Unit dst)
    {
        var hitRate = attack.GetComponent<NumericComponent>().GetAsLong(NumericType.HitRate);
        var avoidRate = dst.GetComponent<NumericComponent>().GetAsLong(NumericType.AvoidRate);

        return (hitRate - avoidRate).IsHit();
    }

    /// <summary>
    /// 是否命中
    /// </summary>
    /// <returns></returns>
    public bool IsHit(NumericComponent attack, NumericComponent dst)
    {
        var hitRate = attack.GetAsLong(NumericType.HitRate);
        var avoidRate = dst.GetAsLong(NumericType.AvoidRate);

        return (hitRate - avoidRate).IsHit();
    }
}