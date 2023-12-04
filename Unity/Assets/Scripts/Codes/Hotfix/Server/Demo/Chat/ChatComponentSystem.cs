using System.Collections.Generic;

namespace ET.Server;

[EntitySystemOf(typeof (ChatComponent))]
[FriendOf(typeof (ChatComponent))]
public static partial class ChatComponentSystem
{
    [EntitySystem]
    private static void Awake(this ChatComponent self)
    {
        self.worldId = "world";
        self.nSaveChannel = new HashSet<ChatChannelType>() { ChatChannelType.TV };
        self.useWolrdChannel = new HashSet<ChatChannelType>() { ChatChannelType.World, ChatChannelType.TV };
    }

    [EntitySystem]
    private static void Destroy(this ChatComponent self)
    {
    }

    // 进入聊天服
    public static long Enter(this ChatComponent self, long playerId)
    {
        var child = self.GetChild<ChatUnit>(playerId);
        if (child != null)
        {
            return child.InstanceId;
        }

        child = self.AddChild<ChatUnit, long>(playerId);
        child.AddComponent<MailBoxComponent, MailBoxType>(MailBoxType.OrderedMessage);
        return child.InstanceId;
    }

    /// <summary>
    /// 离开聊天服
    /// </summary>
    /// <param name="self"></param>
    /// <param name="playerId"></param>
    public static void Leave(this ChatComponent self, long playerId)
    {
        self.RemoveChild(playerId);
    }

    private static string GetPersonGroup(this ChatComponent self, long dstId, long roleId)
    {
        if (dstId > roleId)
        {
            return $"{dstId}_ss_{roleId}";
        }
        else
        {
            return $"{roleId}_ss_{dstId}";
        }
    }

    public static MessageReturn SendMessage(this ChatComponent self, long id, ChatChannelType channel, string message, string groupId)
    {
        if (self.useWolrdChannel.Contains(channel))
        {
            groupId = self.worldId;
        }

        if (channel == ChatChannelType.League && id != 0)
        {
            //获取联盟Id
            //groupId = 0;
        }

        List<long> roleList = null;
        string group = groupId;
        if (channel == ChatChannelType.Personal)
        {
            roleList = new List<long>() { id, groupId.ToLong() };
            group = self.GetPersonGroup(roleList[0], roleList[1]);
        }
        else
        {
            if (self.relataDict.TryGetValue(groupId, out string uid))
            {
                groupId = uid;
            }

            if (!self.groupDict.TryGetValue(groupId, out var g))
            {
                return MessageReturn.Create(ErrorCode.ERR_InputInvaid);
            }
            
            roleList = g.RoleList;
        }
        
        return MessageReturn.Success();
    }
}