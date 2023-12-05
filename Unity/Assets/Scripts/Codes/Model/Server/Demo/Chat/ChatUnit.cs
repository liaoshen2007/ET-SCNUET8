namespace ET.Server;

[ChildOf(typeof(ChatComponent))]
public class ChatUnit : Entity, IAwake<long>, IDestroy
{
    public long PlayerId { get; set; }
    
    public string name;
    public string headIcon;
    public int level;
    public long fight;
    public int sex;
}