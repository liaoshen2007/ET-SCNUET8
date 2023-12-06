namespace ET;

[ChildOf(typeof (ChatGroup))]
public class ChatGroupMember: Entity, IAwake
{
    public long sort;

    /// <summary>
    /// 消息免打扰
    /// </summary>
    public bool noDisturbing;

    public string headIcon;
}