﻿namespace ET.Server
{
    /// <summary>
    /// 使用技能
    /// </summary>
    [MessageLocationHandler(SceneType.Map)]
    public class C2M_UseSkillHandler: MessageLocationHandler<Unit, C2M_UseSkillRequest, M2C_UseSkillResponse>
    {
        protected override async ETTask Run(Unit unit, C2M_UseSkillRequest request, M2C_UseSkillResponse response)
        {
            await ETTask.CompletedTask;
            SkillDyna dyna = new() { Direct = request.Direct, DstList = request.DstList, DstPosition = request.DstPosition };
            var ret = unit.GetComponent<SkillComponent>().UseSKill(request.Id, dyna);
            response.Error = ret.Errno;
            response.Message = ret.Message;
        }
    }
}

