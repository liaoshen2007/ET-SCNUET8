using System.Collections.Generic;

namespace ET.Server;

[ComponentOf(typeof (Unit))]
public class ShieldComponent: Entity, IAwake
{
    /// <summary>
    /// 护盾字典
    /// </summary>
    public Dictionary<int, long> ShieldIdDict {get;} = new();
}