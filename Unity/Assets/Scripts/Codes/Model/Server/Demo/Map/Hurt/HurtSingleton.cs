using System;
using System.Collections.Generic;

namespace ET.Server;

public enum HurtEffectType
{
    /// <summary>
    /// 伤害前(全体)
    /// </summary>
    EffectBefore = 10,

    /// <summary>
    /// 伤害前(单个) 
    /// </summary>
    HurtBefore,

    /// <summary>
    /// 伤害后(单个)
    /// </summary>
    HurtAfter,

    /// <summary>
    /// 暴击后
    /// </summary>
    HurtCrit,

    /// <summary>
    /// 格挡后
    /// </summary>
    HurtFender,

    /// <summary>
    /// 伤害后(全体)
    /// </summary>
    EffectAfter,
}

/// <summary>
/// 用于动态计算伤害的临时类
/// </summary>
public class FightUnit
{
    public long roleId;
    public Dictionary<int, long> numericDic;
    public int cfgId;
    public int level;
}

public class HurtTemp
{
    public int id;
    public List<Unit> objectList;
    public int skillExtra;
    public int skillAdjust;
    public int maxCount;
    public SkillDyna skillDyna;
}

[Code]
public class HurtSingleton: Singleton<HurtSingleton>, ISingletonAwake
{
    private Dictionary<string, ASubHurt> subDict;

    public Dictionary<string, ASubHurt> SubHurtDict => subDict;

    public void Awake()
    {
        this.subDict = new Dictionary<string, ASubHurt>();
        foreach (var v in CodeTypes.Instance.GetTypes(typeof (SubHurtAttribute)))
        {
            var attr = v.GetCustomAttributes(typeof (SubHurtAttribute), false)[0] as SubHurtAttribute;
            subDict.Add(attr.Cmd, Activator.CreateInstance(v) as ASubHurt);
        }
    }

    public void Process(FightUnit attack,
    FightUnit defend,
    List<SubEffectArgs> subList,
    HurtEffectType eT,
    HurtTemp hT,
    HurtInfo info = default,
    List<HurtInfo> hurtInfos = default)
    {
        foreach (SubEffectArgs args in subList)
        {
            var func = this.subDict.Get(args.Cmd);
            if (func == default)
            {
                continue;
            }

            if (func.GetHurtEffectType() == eT)
            {
                func.Run(attack, defend, args.Args, hT, info, hurtInfos);
            }
        }
    }
}