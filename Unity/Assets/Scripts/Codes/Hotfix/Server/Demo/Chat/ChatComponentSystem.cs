using System.Collections.Generic;
using MongoDB.Driver;

namespace ET.Server;

[EntitySystemOf(typeof (ChatComponent))]
[FriendOf(typeof (ChatComponent))]
[FriendOf(typeof (ChatGroup))]
public static partial class ChatComponentSystem
{
    [EntitySystem]
    private static void Awake(this ChatComponent self)
    {
        self.worldId = "world";
        self.nSaveChannel = new HashSet<ChatChannelType>() { ChatChannelType.TV };
        self.useWolrdChannel = new HashSet<ChatChannelType>() { ChatChannelType.World, ChatChannelType.TV };

        var collection = self.Scene().GetComponent<DBManagerComponent>().GetZoneDB(self.Zone()).GetCollection<ChatSaveItem>();
        collection.Indexes.CreateMany(new[]
        {
            new CreateIndexModel<ChatSaveItem>(Builders<ChatSaveItem>.IndexKeys.Ascending(info => info.Channel)),
            new CreateIndexModel<ChatSaveItem>(Builders<ChatSaveItem>.IndexKeys.Ascending(info => info.GroupId)),
            new CreateIndexModel<ChatSaveItem>(Builders<ChatSaveItem>.IndexKeys.Ascending(info => info.RoleInfo.Id)),
        });

        self.timer = self.Root().GetComponent<TimerComponent>().NewRepeatedTimer(30 * 1000, TimerInvokeType.ChatSaveCheck, self);
    }

    [EntitySystem]
    private static void Destroy(this ChatComponent self)
    {
        self.Root().GetComponent<TimerComponent>().Remove(ref self.timer);
    }

    [Invoke(TimerInvokeType.ChatSaveCheck)]
    private class SaveChatMsg: ATimer<ChatComponent>
    {
        protected override void Run(ChatComponent self)
        {
            self.SaveChat().Coroutine();
        }
    }

    public static async ETTask SaveChat(this ChatComponent self)
    {
        if (self.saveList.Count == 0)
        {
            return;
        }

        var newList = self.saveList.ToArray();
        self.saveList.Clear();
        var zoneDB = self.Scene().GetComponent<DBManagerComponent>().GetZoneDB(self.Zone());
        using var list = ListComponent<ETTask>.Create();
        for (int i = 0; i < newList.Length; i++)
        {
            var item = newList[i];
            list.Add(zoneDB.Save(item));
        }

        await ETTaskHelper.WaitAll(list);
    }

    // 进入聊天服
    public static long Enter(this ChatComponent self, long playerId)
    {
        var child = self.GetChild<ChatUnit>(playerId);
        if (child != null)
        {
            return child.InstanceId;
        }

        child = self.AddChildWithId<ChatUnit>(playerId);
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

    private static PlayerInfoProto GetPlayerInfo(this ChatComponent self, long dstId)
    {
        var chatUnit = self.GetChild<ChatUnit>(dstId);
        if (chatUnit == null)
        {
            //取离线数据
            return null;
        }

        return chatUnit.ToPlayerInfo();
    }

    public static MessageReturn SendMessage(this ChatComponent self, long dstRoleId, ChatChannelType channel, string message, string groupId)
    {
        if (self.useWolrdChannel.Contains(channel))
        {
            groupId = self.worldId;
        }

        if (channel == ChatChannelType.League && dstRoleId != 0)
        {
            //获取联盟Id
            //groupId = 0;
        }

        List<long> roleList = null;
        string group = groupId;
        if (channel == ChatChannelType.Personal)
        {
            roleList = new List<long>() { dstRoleId, groupId.ToLong() };
            group = self.GetPersonGroup(roleList[0], roleList[1]);
        }
        else
        {
            if (self.relataDict.TryGetValue(groupId, out string uid))
            {
                groupId = uid;
            }

            group = groupId;
            if (!self.groupDict.TryGetValue(group, out var g))
            {
                return MessageReturn.Create(ErrorCode.ERR_ChatCantFindGroup);
            }

            roleList = g.roleList;
        }

        var proto = ChatMsgProto.Create();
        proto.Message = message;
        proto.Channel = (int)channel;
        long now = TimeInfo.Instance.FrameTime;
        proto.Time = now;
        if (now != self.lastMsgTime)
        {
            self.lastMsgTime = now;
            self.count = 0;
        }

        self.count++;
        proto.Id = now * 10000 + self.count;
        proto.GroupId = groupId;
        if (channel == ChatChannelType.Personal)
        {
            proto.RoleInfo = self.GetPlayerInfo(groupId.ToLong());
            self.Send2Client(dstRoleId, new List<ChatMsgProto>() { proto });
            var dstMsg = proto.Clone() as ChatMsgProto;
            dstMsg.GroupId = dstRoleId.ToString();
            dstMsg.RoleInfo = self.GetPlayerInfo(dstRoleId);
            self.Send2Client(groupId.ToLong(), new List<ChatMsgProto>() { dstMsg });
        }
        else
        {
            proto.RoleInfo = self.GetPlayerInfo(dstRoleId);
            self.Broadcast(roleList, new List<ChatMsgProto>() { proto });
        }

        if (!self.nSaveChannel.Contains(channel))
        {
            using var item = self.AddChildWithId<ChatSaveItem>(proto.Id);
            item.GroupId = group;
            item.Time = proto.Time;
            item.Channel = proto.Channel;
            item.Message = proto.Message;
            item.RoleInfo = proto.RoleInfo;
            self.saveList.Add(item);
        }

        return MessageReturn.Success();
    }

    private static void Send2Client(this ChatComponent self, long id, List<ChatMsgProto> list)
    {
        self.Root().GetComponent<MessageLocationSenderComponent>().Get(LocationType.GateSession)
                .Send(id, new C2C_UpdateChat() { List = list });
    }

    private static void Broadcast(this ChatComponent self, List<long> roelList, List<ChatMsgProto> list)
    {
        // 网络底层做了优化，同一个消息不会多次序列化
        MessageLocationSenderOneType oneTypeMessageLocationType =
                self.Root().GetComponent<MessageLocationSenderComponent>().Get(LocationType.GateSession);
        foreach (long id in roelList)
        {
            oneTypeMessageLocationType.Send(id, new C2C_UpdateChat() { List = list });
        }
    }
}