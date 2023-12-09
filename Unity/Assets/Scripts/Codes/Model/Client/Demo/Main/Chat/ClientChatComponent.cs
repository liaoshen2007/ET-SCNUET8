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
        public Dictionary<string, ChatGroup> groupDict = new Dictionary<string, ChatGroup>();

        public List<ClientChatUnit> chatMsgList = new List<ClientChatUnit>();
        public Dictionary<long, ClientChatUnit> chatMsgDict = new Dictionary<long, ClientChatUnit>();
    }
}