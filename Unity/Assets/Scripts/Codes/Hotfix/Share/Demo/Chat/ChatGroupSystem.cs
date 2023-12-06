namespace ET
{
    [EntitySystemOf(typeof (ChatGroup))]
    [FriendOf(typeof(ChatGroup))]
    public static partial class ChatGroupSystem
    {
        [EntitySystem]
        private static void Awake(this ChatGroup self, string guid)
        {
            self.guid = guid;
        }
    }
}