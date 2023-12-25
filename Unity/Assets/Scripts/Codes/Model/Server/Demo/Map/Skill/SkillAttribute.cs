using System.Collections.Generic;

namespace ET.Server
{
    public class SkillAttribute: BaseAttribute
    {
        public string Cmd { get; }

        public SkillAttribute(string cmd)
        {
            Cmd = cmd;
        }
    }

    public abstract class ASkillEffect
    {
        public SkillEffectArg EffectArg { get; private set; }

        public void SetEffectArg(SkillEffectArg effectArg)
        {
            EffectArg = effectArg;
        }

        public abstract HurtPkg Run(SkillComponent self, SkillUnit skill, List<Unit> RoleList, SkillDyna dyna);
    }
}