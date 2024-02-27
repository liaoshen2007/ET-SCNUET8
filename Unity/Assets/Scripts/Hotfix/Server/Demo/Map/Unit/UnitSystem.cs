namespace ET.Server
{
    public static partial class UnitSystem
    {
        public static void BroadCastHurt(this Unit self, int id, HurtPkg pkg)
    {
        if (pkg.HurtInfos.Count == 0)
        {
            return;
        }

        MapMessageHelper.Broadcast(self, new M2C_HurtList() { Id = id, RoleId = self.Id, HurtList = pkg.HurtInfos, ViewCmd = pkg.ViewCmd });
    }
    }
}