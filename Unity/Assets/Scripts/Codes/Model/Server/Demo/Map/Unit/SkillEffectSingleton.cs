using System;
using System.Collections.Generic;

namespace ET.Server;

[Code]
public class SkillEffectSingleton: Singleton<SkillEffectSingleton>, ISingletonAwake
{
    private Dictionary<string, Type> buffDict;

    public Dictionary<string, Type> BuffEffectDict => buffDict;

    private Dictionary<string, ASkillEffect> skillDict;

    public Dictionary<string, ASkillEffect> SkillEffectDict => skillDict;

    public void Awake()
    {
        buffDict = new Dictionary<string, Type>();
        skillDict = new Dictionary<string, ASkillEffect>();
        foreach (var v in CodeTypes.Instance.GetTypes(typeof (BuffAttribute)))
        {
            var attr = v.GetCustomAttributes(typeof (BuffAttribute), false)[0] as BuffAttribute;
            buffDict.Add(attr.Cmd, v);
        }

        foreach (var v in CodeTypes.Instance.GetTypes(typeof (SkillAttribute)))
        {
            var attr = v.GetCustomAttributes(typeof (SkillAttribute), false)[0] as SkillAttribute;
            skillDict.Add(attr.Cmd, Activator.CreateInstance(v) as ASkillEffect);
        }
    }
}