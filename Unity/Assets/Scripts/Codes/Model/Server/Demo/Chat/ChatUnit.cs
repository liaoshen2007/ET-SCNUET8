namespace ET.Server;

[ChildOf(typeof (ChatComponent))]
public class ChatUnit: Entity, IAwake, IDestroy
{
    public string name;
    public string headIcon;
    public int level;
    public long fight;
    public int sex;
    public bool isOnline;
}