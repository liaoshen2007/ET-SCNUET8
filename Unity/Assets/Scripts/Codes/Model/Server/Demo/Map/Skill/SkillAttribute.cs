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
        public abstract void Run(SkillComponent self, SkillUnit skill, EffectArg effectArg, List<long> RoleList);
    }
}