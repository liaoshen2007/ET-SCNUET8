namespace ET.Client
{
    [EntitySystemOf(typeof(RoleInfoComponent))]
    [FriendOfAttribute(typeof(ET.Client.RoleInfoComponent))]
    public static partial class RoleInfoComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Client.RoleInfoComponent self)
        {

        }
        [EntitySystem]
        private static void Destroy(this ET.Client.RoleInfoComponent self)
        {
            foreach (var roleInfo in self.RoleInfos)
            {

                roleInfo?.Dispose();
            }
            self.RoleInfos.Clear();
            self.CurrentRoleId = 0;
        }
    }
}

