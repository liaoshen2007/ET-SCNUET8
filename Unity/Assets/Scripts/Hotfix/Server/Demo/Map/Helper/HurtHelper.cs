using System;
using System.Collections.Generic;

namespace ET.Server
{
    public static class HurtHelper
    {
        private static FightUnit CopyUnit(Unit self)
    {
        FightUnit f = new();
        f.numericDic = self.GetComponent<NumericComponent>().CopyDict();
        f.cfgId = self.ConfigId;
        f.roleId = self.Id;
        return f;
    }

        public static bool IsCrit(Unit self)
    {
        var f = CopyUnit(self);
        return FightFormula.Instance.IsCrit(f);
    }

        public static long BeHurt(Unit self, Unit dst, long hurt, int id)
    {
        if (hurt <= 0)
        {
            return hurt;
        }

        UnitPerHurt e = new() { Attacker = self, Unit = dst, Hurt = hurt, Id = id };
        EventSystem.Instance.Publish(self.Scene(), e);
        return Math.Max(e.Hurt - e.ShieldHurt, 0);
    }

        /// <summary>
        /// 攻击
        /// </summary>
        /// <param name="unit">攻击者</param>
        /// <param name="objectList">受攻击列表</param>
        /// <param name="id">技能或BuffId</param>
        /// <param name="skillAdjust">发挥比例</param>
        /// <param name="skillExtra">额外附加攻击</param>
        /// <param name="maxCount">最大攻击数量</param>
        /// <param name="hateBase">仇恨基础值</param>
        /// <param name="hateRate">仇恨系数</param>
        /// <param name="subList">伤害子效果</param>
        /// <param name="skillArgs">技能参数</param>
        /// <param name="element">元素伤害类型</param>
        /// <returns></returns>
        public static List<HurtInfo> StandHurt(Unit unit,
        List<Unit> objectList,
        int id,
        int skillAdjust,
        int skillExtra,
        int maxCount,
        int hateBase,
        int hateRate,
        List<SubEffectArgs> subList,
        SkillDyna skillArgs = default,
        int element = 0)
    {
        FightUnit attack = CopyUnit(unit);
        HurtTemp ht = new()
        {
            id = id,
            objectList = objectList,
            skillAdjust = skillAdjust,
            skillExtra = skillExtra,
            maxCount = maxCount,
            skillDyna = skillArgs,
        };
        HurtSingleton.Instance.Process(attack, default, subList, HurtEffectType.EffectBefore, ht);
        List<HurtInfo> hurtList = new();
        int count = 0;
        foreach (Unit u in objectList)
        {
            if (!u.IsAlive() || u.IsInvincible())
            {
                continue;
            }

            FightUnit dst = CopyUnit(u);
            HurtSingleton.Instance.Process(attack, dst, subList, HurtEffectType.HurtBefore, ht);
            bool isCrit = FightFormula.Instance.IsCrit(attack);
            if (isCrit)
            {
                HurtSingleton.Instance.Process(attack, dst, subList, HurtEffectType.HurtCrit, ht);
            }

            bool isFender = FightFormula.Instance.IsFender(attack);
            if (isFender)
            {
                HurtSingleton.Instance.Process(attack, dst, subList, HurtEffectType.HurtFender, ht);
            }

            bool isDirect = FightFormula.Instance.IsDirect(attack);
            long hurt = FightFormula.Instance.CalcHurt(attack, dst, skillExtra, skillAdjust, element, isDirect, isCrit, isFender);
            long suckHp = FightFormula.Instance.SuckHp(attack, hurt);
            HurtInfo info = new()
            {
                Id = dst.roleId,
                Hurt = hurt,
                SuckHp = suckHp,
                IsCrit = isCrit,
                IsFender = isFender,
                IsDirect = isDirect,
            };
            HurtSingleton.Instance.Process(attack, dst, subList, HurtEffectType.HurtAfter, ht, info);
            count++;
            if (maxCount > 0 && count >= maxCount)
            {
                break;
            }
        }

        EventSystem.Instance.Publish(unit.Scene(), new UnitDoAttack() { Unit = unit, HurtList = hurtList, Element = element });
        HurtSingleton.Instance.Process(attack, default, subList, HurtEffectType.EffectAfter, ht, default, hurtList);
        long totalHurt = 0;
        foreach (HurtInfo info in hurtList)
        {
            totalHurt += info.SuckHp;
            Unit hurtDst = unit.Scene().GetComponent<UnitComponent>().Get(info.Id);
            BeHurt(unit, hurtDst, info.Hurt, id);
            hurtDst.AddHate(unit.Id, Math.Max((info.Hurt * hateRate / 10000f).Ceil() + hateBase, 1));
            unit.AddHate(hurtDst.Id, 1);
        }

        if (unit.IsAlive())
        {
            unit.AddHp(totalHurt, unit.Id, id);
        }

        return hurtList;
    }
    }
}