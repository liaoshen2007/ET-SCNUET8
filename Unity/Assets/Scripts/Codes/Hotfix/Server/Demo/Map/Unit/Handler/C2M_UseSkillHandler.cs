using System.Collections.Generic;

namespace ET.Server;

/// <summary>
/// 使用技能
/// </summary>
[MessageLocationHandler(SceneType.Map)]
public class C2M_UseSkillDataHandler: MessageLocationHandler<Unit, C2M_UseSkill>
{
    protected override async ETTask Run(Unit unit, C2M_UseSkill request)
    {
        await ETTask.CompletedTask;
    }
}