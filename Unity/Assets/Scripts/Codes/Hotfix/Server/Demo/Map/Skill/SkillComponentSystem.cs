using System;
using System.Linq;

namespace ET.Server;

[EntitySystemOf(typeof (SkillComponent))]
[FriendOf(typeof (SkillComponent))]
[FriendOf(typeof (SkillUnit))]
public static partial class SkillComponentSystem
{
    [EntitySystem]
    private static void Awake(this SkillComponent self)
    {
    }

    [Invoke(TimerInvokeType.SkillSing)]
    private class SkillSingTimer: ATimer<SkillComponent>
    {
        protected override void Run(SkillComponent self)
        {
            self.ProcessSkill();
        }
    }
    
    [Invoke(TimerInvokeType.SKillEffect)]
    private class SkillEffectTimer: ATimer<SkillComponent>
    {
        protected override void Run(SkillComponent self)
        {
            
        }
    }
    
    private static void ProcessSKillEffect(this SkillComponent self, ASkillEffect effect)
    {
    }

    private static void ProcessSkill(this SkillComponent self)
    {
        var skill = self.skillDict.Get(self.usingSkillId);
        if (skill == default)
        {
            return;
        }

        //天赋修改技能
        var effectCfg = skill.Config.EffectList.Get(self.oft);
        if (effectCfg == default)
        {
            return;
        }

        if (!SkillEffectSingleton.Instance.SkillEffectDict.TryGetValue(effectCfg.Cmd, out var effect))
        {
            Log.Error($"获取技能效果失败: {effectCfg.Cmd}");
            return;
        }

        self.skillEffect = effect;
        if (effectCfg.Ms > 0)
        {
            self.effectTimer = self.Scene().GetComponent<TimerComponent>().NewOnceTimer(effectCfg.Ms, TimerInvokeType.SKillEffect, self);
            return;
        }
        
        self.ProcessSKillEffect(effect);
    }

    private static MessageReturn CheckCondition(this SkillComponent self, SkillUnit skill)
    {
        return MessageReturn.Success();
    }

    public static MessageReturn UseSKill(this SkillComponent self, int id, C2M_UseSkill request)
    {
        var skill = self.skillDict.Get(id);
        if (skill == default)
        {
            return MessageReturn.Create(ErrorCode.ERR_CantFindCfg);
        }

        var unit = self.GetParent<Unit>();
        if (!unit.IsAlive())
        {
            return MessageReturn.Create(ErrorCode.ERR_UnitDead);
        }

        switch (skill.Config.RangeType)
        {
            case (int)RangeType.Single:
                if (request.DstList.IsNullOrEmpty())
                {
                    return MessageReturn.Create(ErrorCode.ERR_InputInvaid);
                }

                break;
            case (int)RangeType.SelfLine:
            case (int)RangeType.DstLine:
                if (request.DstPosition.Count == 0)
                {
                    return MessageReturn.Create(ErrorCode.ERR_InputInvaid);
                }

                break;
        }

        var ret = self.CheckCondition(skill);
        if (ret.Errno != ErrorCode.ERR_Success)
        {
            return ret;
        }

        int cdTime = 0;
        if (skill.Config.ColdTime > 0 && self.reduceCdRate < 10000)
        {
            cdTime = Math.Max(skill.Config.ColdTime * (10000 - self.reduceCdRate) / 10000, 0);
        }

        skill.cdList.Sort((l, r) => l.CompareTo(r));
        skill.cdList[0] = Math.Max(TimeInfo.Instance.FrameTime, skill.cdList.Last()) + cdTime;

        if (cdTime > 0)
        {
            M2C_UpdateSkill pkg = M2C_UpdateSkill.Create();
            pkg.CdList.AddRange(skill.cdList);
            unit.SendToClient(pkg);
        }

        M2C_UseSkill useSkill = M2C_UseSkill.Create();
        useSkill.Id = id;
        useSkill.RoleId = unit.Id;
        useSkill.Position = unit.Position;
        useSkill.DstList.AddRange(request.DstList);
        useSkill.DstPosition.AddRange(request.DstPosition);
        useSkill.Direct = request.Direct;
        MapMessageHelper.Broadcast(unit, useSkill);

        self.oft = 0;
        self.usingSkillId = id;
        self.singTimer = self.Scene().GetComponent<TimerComponent>().NewOnceTimer(skill.Config.SingTime, TimerInvokeType.SkillSing, self);

        return MessageReturn.Success();
    }

    public static bool BreakSkill(this SkillComponent self, bool isForce, params int[] classify)
    {
        if (self.singTimer > 0 || self.effectTimer > 0)
        {
            if (!isForce)
            {
                SkillUnit skill = self.skillDict.Get(self.usingSkillId);
                foreach (int c in classify)
                {
                    if (skill.Config.Interrupt.Exists(c))
                    {
                        isForce = true;
                        break;
                    }
                }
            }

            if (!isForce)
            {
                return false;
            }

            self.Scene().GetComponent<TimerComponent>().Remove(ref self.singTimer);
            self.Scene().GetComponent<TimerComponent>().Remove(ref self.effectTimer);
            var unit = self.GetParent<Unit>();
            MapMessageHelper.Broadcast(unit, new M2C_BreakSkill() { Id = self.usingSkillId, RoleId = unit.Id });
            self.usingSkillId = 0;
        }

        return false;
    }
}