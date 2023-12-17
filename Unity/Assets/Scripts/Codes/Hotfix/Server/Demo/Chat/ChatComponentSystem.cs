using System.Collections.Generic;
using MongoDB.Driver;

namespace ET.Server;

[EntitySystemOf(typeof (ChatComponent))]
[FriendOf(typeof (ChatComponent))]
[FriendOf(typeof (ChatUnit))]
[FriendOf(typeof (ChatGroup))]
[FriendOf(typeof (ChatGroupMember))]
public static partial class ChatComponentSystem
{
    [EntitySystem]
    private static void Awake(this ChatComponent self)
    {
        self.zone = self.Zone();
        self.worldId = ConstValue.ChatSendId;
        self.nSaveChannel = new HashSet<ChatChannelType>() { ChatChannelType.TV };
        self.useWolrdChannel = new HashSet<ChatChannelType>() { ChatChannelType.World, ChatChannelType.TV };
        self.findOption =
                new FindOptions<ChatSaveItem>() { Limit = ConstValue.ChatGetCount, Sort = Builders<ChatSaveItem>.Sort.Descending(item => item.Id), };

        var collection = self.Scene().GetComponent<DBManagerComponent>().GetZoneDB(self.Zone()).GetCollection<ChatSaveItem>();
        collection.Indexes.CreateMany(new[]
        {
            new CreateIndexModel<ChatSaveItem>(Builders<ChatSaveItem>.IndexKeys.Ascending(info => info.Channel)),
            new CreateIndexModel<ChatSaveItem>(Builders<ChatSaveItem>.IndexKeys.Ascending(info => info.GroupId)),
            new CreateIndexModel<ChatSaveItem>(Builders<ChatSaveItem>.IndexKeys.Ascending(info => info.SendRoleId)),
        });

        self.timer = self.Root().GetComponent<TimerComponent>().NewRepeatedTimer(1000, TimerInvokeType.ChatSaveCheck, self);
        self.CreateGroup(ChatChannelType.World);
        self.LoadData().Coroutine();
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

    /// <summary>
    /// 保存聊天服数据
    /// </summary>
    /// <param name="self"></param>
    public static async ETTask Save(this ChatComponent self)
    {
        await self.SaveChat();
        var zoneDB = self.Scene().GetComponent<DBManagerComponent>().GetZoneDB(self.Zone());
        await zoneDB.Save(self);
    }

    private static async ETTask LoadData(this ChatComponent self)
    {
        var list = await self.Scene().GetComponent<DBManagerComponent>().GetZoneDB(self.Zone()).Query<ChatComponent>(d => d.zone == self.Zone());
        if (list.Count == 0)
        {
            return;
        }

        var chat = list[0];
        foreach ((long id, ChatUnit unit) in chat.unitDict)
        {
            unit.isOnline = false;
            self.AddChild(unit);
            self.unitDict.Add(id, unit);
        }

        foreach ((string guid, ChatGroup group) in chat.groupDict)
        {
            if (group.channel == ChatChannelType.Group)
            {
                self.AddChild(group);
                self.groupDict.Add(guid, group);
            }
        }
    }

    private static async ETTask SaveChat(this ChatComponent self)
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

    private static async ETTask UpdateAllGroupChat(this ChatComponent self, long playerId)
    {
        ChatUnit unit = self.GetChild<ChatUnit>(playerId);
        var list = await self.CacheGet(playerId, (int)ChatChannelType.World, self.worldId, 0);
        self.Send2Client(playerId, new C2C_UpdateChat() { List = list });
    }

    // 进入聊天服
    public static ChatUnit Enter(this ChatComponent self, long playerId)
    {
        var child = self.GetChild<ChatUnit>(playerId) ?? self.AddChildWithId<ChatUnit>(playerId);

        child.isOnline = true;
        self.AddMember(self.worldId, new List<long>() { playerId });
        self.unitDict.TryAdd(playerId, child);
        self.UpdateAllGroupChat(playerId).Coroutine();
        return child;
    }

    /// <summary>
    /// 离开聊天服
    /// </summary>
    /// <param name="self"></param>
    /// <param name="playerId"></param>
    public static void Leave(this ChatComponent self, long playerId)
    {
        var child = self.GetChild<ChatUnit>(playerId);
        child.isOnline = false;
        self.RemoveMember(self.worldId, false, new List<long>() { playerId });
        self.Scene().GetComponent<MessageLocationSenderComponent>().Get(LocationType.GateSession).Remove(playerId);
    }

    private static string GetPersonGroup(this ChatComponent self, long dstId, long roleId)
    {
        if (dstId > roleId)
        {
            return $"{dstId}_ss_{roleId}";
        }

        return $"{roleId}_ss_{dstId}";
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

    public static MessageReturn SendMessage(this ChatComponent self, long sendRoleId, ChatChannelType channel, string message, string groupId)
    {
        if (self.useWolrdChannel.Contains(channel))
        {
            groupId = self.worldId;
        }

        if (channel == ChatChannelType.League && sendRoleId != 0)
        {
            //获取联盟Id
            //groupId = 0;
        }

        List<long> roleList = null;
        string group = groupId;
        if (channel == ChatChannelType.Personal)
        {
            roleList = new List<long>() { sendRoleId, groupId.ToLong() };
            group = self.GetPersonGroup(roleList[0], roleList[1]);
        }
        else
        {
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
            self.Send2Client(sendRoleId, new C2C_UpdateChat() { List = new List<ChatMsgProto>() { proto } });
            var dstMsg = proto.Clone() as ChatMsgProto;
            dstMsg.GroupId = sendRoleId.ToString();
            dstMsg.RoleInfo = self.GetPlayerInfo(sendRoleId);
            self.Send2Client(groupId.ToLong(), new C2C_UpdateChat() { List = new List<ChatMsgProto>() { dstMsg } });
        }
        else
        {
            proto.RoleInfo = self.GetPlayerInfo(sendRoleId);
            self.Broadcast(roleList, new C2C_UpdateChat() { List = new List<ChatMsgProto>() { proto } });
        }

        if (!self.nSaveChannel.Contains(channel))
        {
            using var item = self.AddChildWithId<ChatSaveItem>(proto.Id);
            item.GroupId = group;
            item.Time = proto.Time;
            item.Channel = proto.Channel;
            item.Message = proto.Message;
            item.SendRoleId = proto.RoleInfo.Id;
            self.saveList.Add(item);
        }

        return MessageReturn.Success();
    }

    public static MessageReturn SetGroupName(this ChatComponent self, string groupId, string groupName)
    {
        if (groupName.IsNullOrEmpty())
        {
            return MessageReturn.Create(ErrorCode.ERR_InputInvaid);
        }

        if (!self.groupDict.TryGetValue(groupId, out var group))
        {
            return MessageReturn.Create(ErrorCode.ERR_ChatCantFindGroup);
        }

        group.name = groupName;
        self.GroupUpdate(groupId);
        return MessageReturn.Success();
    }

    public static MessageReturn CreateGroup(this ChatComponent self, ChatChannelType channel, long leaderId = 0, string groupId = default,
    List<long> memebrList = default)
    {
        if (self.useWolrdChannel.Contains(channel))
        {
            groupId = self.worldId;
        }

        string guid = groupId ?? IdGenerater.Instance.GenerateId().ToString();
        ChatGroup group = self.AddChild<ChatGroup, string>(guid);
        group.leaderId = leaderId;
        group.channel = channel;
        group.name = self.GetGroupName(channel, leaderId, memebrList);

        self.groupDict.Add(guid, group);
        if (leaderId > 0)
        {
            memebrList.Add(leaderId);
            self.AddMember(guid, memebrList);
        }

        Log.Info($"创建讨论组: {channel} {guid}");
        return MessageReturn.Success();
    }

    public static void AddMember(this ChatComponent self, string groupId, List<long> memebrList)
    {
        if (!self.groupDict.TryGetValue(groupId, out ChatGroup group))
        {
            return;
        }

        foreach (long l in memebrList)
        {
            if (group.HasChild(l))
            {
                continue;
            }

            group.leaderId = group.leaderId == 0? l : group.leaderId;
            group.roleList.Add(l);
            var member = group.AddChildWithId<ChatGroupMember>(l);
            member.sort = TimeInfo.Instance.FrameTime + group.Children.Count;
            var roleInfo = self.GetPlayerInfo(l);
            member.headIcon = roleInfo.HeadIcon;
            member.noDisturbing = false;
            group.memberDic.Add(l, member);
        }

        self.GroupUpdate(groupId);
    }

    public static MessageReturn RemoveMember(this ChatComponent self, string groupId, bool isKick, List<long> memebrList)
    {
        if (!self.groupDict.TryGetValue(groupId, out ChatGroup group))
        {
            return MessageReturn.Create(ErrorCode.ERR_ChatCantFindGroup);
        }

        foreach (long l in memebrList)
        {
            if (!group.HasChild(l))
            {
                continue;
            }

            group.roleList.Remove(l);
            group.memberDic.Remove(l);
            group.RemoveChild(l);
            if (group.leaderId == l)
            {
                long minSort = 0;
                long memberId = 0;
                foreach (var v in group.Children.Values)
                {
                    var member = v as ChatGroupMember;
                    minSort = minSort == 0? member.sort : minSort;
                    memberId = memberId == 0? member.Id : memberId;
                    if (member.sort >= minSort)
                    {
                        continue;
                    }

                    minSort = member.sort;
                    memberId = member.Id;
                }

                group.leaderId = memberId;
            }

            self.Send2Client(l, new C2C_GroupDel() { GroupId = groupId });
        }

        self.GroupUpdate(groupId);
        if (group.channel == ChatChannelType.Group)
        {
            if (group.Children.Count == 1)
            {
                self.RemoveMember(groupId, false, new List<long>() { group.leaderId });
            }
            else if (group.Children.Count == 0)
            {
                group.Dispose();
                self.groupDict.Remove(groupId);
                Log.Info($"删除讨论组: {groupId}");
            }
        }

        return MessageReturn.Success();
    }

    public static async ETTask<List<ChatMsgProto>> CacheGet(this ChatComponent self, long roleId, int channel, string groupId, long id)
    {
        string group = groupId;
        switch (channel)
        {
            case (int)ChatChannelType.World:
                group = self.worldId;
                break;
            case (int)ChatChannelType.League:
                group = "league";
                break;
            case (int)ChatChannelType.Personal:
                group = self.GetPersonGroup(roleId, groupId.ToLong());
                break;
        }

        if (group.IsNullOrEmpty())
        {
            return new List<ChatMsgProto>();
        }

        var zoneDB = self.Scene().GetComponent<DBManagerComponent>().GetZoneDB(self.Zone());
        List<ChatSaveItem> list = default;
        if (id == 0)
        {
            list = await zoneDB.Query(item => item.Channel == channel && item.GroupId == group, self.findOption);
        }
        else
        {
            list = await zoneDB.Query(item => item.Channel == channel && item.GroupId == group && item.Id < id, self.findOption);
        }

        list.Reverse();
        var result = new List<ChatMsgProto>();
        foreach (ChatSaveItem item in list)
        {
            var roleInfo = item.Channel == (int)ChatChannelType.Personal? self.GetPlayerInfo(group.ToLong()) : self.GetPlayerInfo(item.SendRoleId);
            result.Add(new ChatMsgProto()
            {
                Id = item.Id,
                Time = item.Time,
                Channel = item.Channel,
                RoleInfo = roleInfo,
                Message = item.Message,
                GroupId = item.GroupId,
            });
        }

        return result;
    }

    private static string GetGroupName(this ChatComponent self, ChatChannelType channel, long leaderId, List<long> memebrList)
    {
        if (channel == ChatChannelType.Group)
        {
            List<string> list = new();
            list.Add(self.GetPlayerInfo(leaderId).Name);
            foreach (long l in memebrList)
            {
                var roleInfo = self.GetPlayerInfo(l);
                list.Add(roleInfo.Name);
            }

            return string.Join(",", list);
        }

        return string.Empty;
    }

    private static void GroupUpdate(this ChatComponent self, string groupId = default, long roleId = 0)
    {
        List<long> roleList = new();
        if (roleId > 0)
        {
            roleList.Add(roleId);
        }

        List<ChatGroupProto> list = new();
        foreach (var group in self.groupDict.Values)
        {
            if (group.guid == (groupId ?? group.guid) &&
                (roleId == 0 || group.HasChild(roleId)) &&
                group.channel != ChatChannelType.League && group.channel != ChatChannelType.World)
            {
                var proto = ChatGroupProto.Create(false);
                proto.GroupId = group.guid;
                proto.Name = group.name;
                proto.LeaderId = group.leaderId;
                proto.MemberList = new List<ChatGroupMemberProto>();
                foreach (var entity in group.Children.Values)
                {
                    var member = (ChatGroupMember)entity;
                    proto.MemberList.Add(new ChatGroupMemberProto()
                    {
                        RoleId = member.Id, HeadIcon = member.headIcon, NoDisturbing = member.noDisturbing, Sort = member.sort,
                    });
                }

                proto.MemberList.Sort((l, r) => l.Sort.CompareTo(r.Sort));
                list.Add(proto);
                if (roleList.Count == 0)
                {
                    roleList.AddRange(group.roleList);
                }
            }
        }

        if (list.Count > 0)
        {
            self.Broadcast(roleList, new C2C_GroupUpdate() { List = list });
        }
    }

    private static void Send2Client(this ChatComponent self, long id, IMessage message)
    {
        ChatUnit child = self.GetChild<ChatUnit>(id);
        if (child is not { isOnline: true })
        {
            return;
        }

        self.Root().GetComponent<MessageLocationSenderComponent>().Get(LocationType.GateSession)
                .Send(id, message);
    }

    private static void Broadcast(this ChatComponent self, List<long> roelList, IMessage message)
    {
        // 网络底层做了优化，同一个消息不会多次序列化
        MessageLocationSenderOneType oneTypeMessageLocationType =
                self.Root().GetComponent<MessageLocationSenderComponent>().Get(LocationType.GateSession);
        foreach (long id in roelList)
        {
            ChatUnit child = self.GetChild<ChatUnit>(id);
            if (child is not { isOnline: true })
            {
                continue;
            }

            oneTypeMessageLocationType.Send(id, message);
        }
    }
}