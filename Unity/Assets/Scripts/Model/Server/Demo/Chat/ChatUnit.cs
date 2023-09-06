namespace ET.Server;

[ChildOf(typeof(ChatComponent))]
public class ChatUnit : Entity, IAwake<long>, IDestroy
{
    public long PlayerId { get; set; }
}