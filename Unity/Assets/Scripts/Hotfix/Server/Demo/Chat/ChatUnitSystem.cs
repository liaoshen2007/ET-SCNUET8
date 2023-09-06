namespace ET.Server;

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
}