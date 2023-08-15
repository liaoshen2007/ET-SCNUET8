using System.Collections.Generic;

namespace ET.Server;


[EntitySystemOf(typeof(HurtArgs))]
public static partial class HurtArgsSystem
{
    [EntitySystem]
    private static void Awake(this HurtArgs self)
    {
        self.HurtList = new List<HurtInfo>();
    }
}

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