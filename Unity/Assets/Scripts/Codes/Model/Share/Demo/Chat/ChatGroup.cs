using System.Collections.Generic;

namespace ET;

public class ChatGroup: Entity, IAwake
{
    public List<long> RoleList { get; set; } = new List<long>();
}