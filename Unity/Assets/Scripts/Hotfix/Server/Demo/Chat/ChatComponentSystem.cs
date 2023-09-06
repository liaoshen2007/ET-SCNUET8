namespace ET.Server;

[EntitySystemOf(typeof (ChatComponent))]
public static partial class ChatComponentSystem
{
    [EntitySystem]
    private static void Awake(this ChatComponent self)
    {
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

        return self.AddChild<ChatUnit, long>(playerId).InstanceId;
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
}