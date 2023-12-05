using System.Collections.Generic;

namespace ET.Server;

[ChildOf(typeof (ChatComponent))]
public class ChatSaveItem: Entity, IAwake
{
    public long Time { get; set; }
    
    public int Channel { get; set; }
    
    public string Message { get; set; }
    
    public string GroupId { get; set; }
    
    public PlayerInfoProto RoleInfo { get; set; }
}

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
    public List<ChatSaveItem> saveList = new List<ChatSaveItem>();
    public long timer;

    public long lastMsgTime;
    public long count;
}