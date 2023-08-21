using System.Collections.Generic;

namespace ET.Server;

[ComponentOf(typeof(BuffComponent))]
public class HurtArgs : Entity, IAwake
{
    /// <summary>
    /// 受伤列表
    /// </summary>
    public List<HurtInfo> HurtList {get; set;}
    
    /// <summary>
    /// 是否物理伤害
    /// </summary>
    public bool IsPhysics {get; set;}
}