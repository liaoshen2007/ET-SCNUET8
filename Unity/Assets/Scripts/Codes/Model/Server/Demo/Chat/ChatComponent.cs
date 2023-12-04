using System.Collections.Generic;

namespace ET.Server;

[ComponentOf(typeof (Scene))]
public class ChatComponent: Entity, IAwake, IDestroy
{
    /// <summary>
    /// 不保存记录的频道
    /// </summary>
    public HashSet<ChatChannelType> nSaveChannel;

    /// <summary>
    /// 使用世界组的频道
    /// </summary>
    public HashSet<ChatChannelType> useWolrdChannel;

    public string worldId;
    public Dictionary<string, ChatGroup> groupDict = new Dictionary<string, ChatGroup>();
    public Dictionary<string, string> relataDict = new Dictionary<string, string>();

    public long lastMsgTime;
    public long count;
}