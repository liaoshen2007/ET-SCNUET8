using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Server;

public class SkillDyna
{
    public List<Unit> LastHurtList;
    public int Direct;
    public List<long> DstList;
    public List<float3> DstPosition;
}

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