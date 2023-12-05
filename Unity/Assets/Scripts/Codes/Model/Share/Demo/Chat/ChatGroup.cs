using System.Collections.Generic;

namespace ET;

[ChildOf]
public class ChatGroup: Entity, IAwake<string>
{
    public string guid;
    public string name;
    public long leaderId;
    public List<long> roleList = new();
    public int channel;
}