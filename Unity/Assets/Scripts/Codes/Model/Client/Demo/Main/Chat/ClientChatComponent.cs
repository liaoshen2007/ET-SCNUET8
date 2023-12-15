using System.Collections.Generic;

namespace ET.Client
{
    public struct UpdateMsg
    {
        public ClientChatUnit Msg;
    }

    public struct UpdateGroup
    {
        public ChatGroup Group;
    }

    public struct DelGroup
    {
        public ChatGroup Group;
    }

    [ComponentOf(typeof (Scene))]
    public class ClientChatComponent: Entity, IAwake
    {
        /// <summary>
        /// 群组列表
        /// </summary>
        public Dictionary<string, ChatGroup> groupDict = new();

        public List<ClientChatUnit> worldMsgList = new();
        public List<ClientChatUnit> leagueMsgList = new();
        public Dictionary<string, List<ClientChatUnit>> groupMsgDict = new();
    }
}