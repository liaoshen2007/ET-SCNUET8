using System.Collections.Generic;

namespace ET.Client;

[EntitySystemOf(typeof (ClientChatComponent))]
[FriendOf(typeof (ClientChatComponent))]
public static partial class ClientChatComponentSystem
{
    [EntitySystem]
    private static void Awake(this ClientChatComponent self)
    {
    }
    
    public static void CheckMsgCount(this ClientChatComponent self)
    {
        if (self.chatMsgList.Count > 100)
        {
            self.chatMsgList.RemoveRange(0, self.chatMsgList.Count - 100);
        }
    }

    public static void UpdateMsg(this ClientChatComponent self, List<ChatMsgProto> msgList)
    {
        foreach (var proto in msgList)
        {
            using var chatUnit = self.AddChildWithId<ClientChatUnit>(proto.Id);
            chatUnit.FromProto(proto);
            self.chatMsgDict.Add(proto.Id, chatUnit);
            self.chatMsgList.Add(chatUnit);
            EventSystem.Instance.Publish(self.Scene(), new UpdateMsg() { Msg = chatUnit });
        }
    }

    public static void UpdateGroup(this ClientChatComponent self, List<ChatGroupProto> groupList)
    {
        foreach (var group in groupList)
        {
            var groupUnit = self.AddChild<ChatGroup, string>(group.GroupId);
            groupUnit.FromProto(group);
            self.groupDict.Add(group.GroupId, groupUnit);
            EventSystem.Instance.Publish(self.Scene(), new UpdateGroup() { Group = groupUnit });
        }
    }

    public static void DelGroup(this ClientChatComponent self, string groupId)
    {
        if (!self.groupDict.TryGetValue(groupId, out var group))
        {
            return;
        }

        self.groupDict.Remove(groupId);
        self.RemoveChild(group.Id);
        EventSystem.Instance.Publish(self.Scene(), new DelGroup() { Group = group });
    }
}