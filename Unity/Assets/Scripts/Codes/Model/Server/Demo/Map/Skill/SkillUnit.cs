using System.Collections.Generic;

namespace ET.Server;

[ChildOf(typeof (SkillComponent))]
public class SkillUnit: Entity, IAwake
{
    public SkillConfig Config
    {
        get
        {
            return SkillConfigCategory.Instance.Get((int)this.Id);
        }
    }

    public List<long> cdList = new List<long>();
}