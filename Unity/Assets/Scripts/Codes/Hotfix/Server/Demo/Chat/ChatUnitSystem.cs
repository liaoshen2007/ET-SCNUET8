namespace ET.Server;

[FriendOf(typeof(ChatUnit))]
public static class ChatUnitSystem
{
    public class ChatUnitAwakeSystem : AwakeSystem<ChatUnit, long>
    {
        protected override void Awake(ChatUnit self, long pId)
        {
            self.PlayerId = pId;
        }
    }
    
    public class ChatUnitDestroySystem : DestroySystem<ChatUnit>
    {
        protected override void Destroy(ChatUnit self)
        {
            self.PlayerId = 0;
        }
    }

    public static void UpdateInfo(this ChatUnit self, PlayerInfoProto playerInfo)
    {
        self.name = playerInfo.Name;
        self.headIcon = playerInfo.HeadIcon;
        self.level = playerInfo.Level;
        self.fight = playerInfo.Fight;
        self.sex = playerInfo.Sex;
    }
}