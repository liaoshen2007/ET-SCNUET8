namespace ET.Server
{
    public static class SessionHelper
    {
        public static async ETTask Disconnect(this Session self)
        {
            if (self == null || self.IsDisposed)
            {
                return;
            }

            var lastId = self.InstanceId;
            await self.Fiber().Root.GetComponent<TimerComponent>().WaitAsync(1000);
            if (lastId != self.InstanceId)
            {
                return;
            }

            self.Dispose();
        }

        /// <summary>
        /// 根据Session获取DB
        /// </summary>
        /// <param name="session"></param>
        /// <param name="zone"></param>
        /// <returns></returns>
        public static DBComponent DBComponent(this Session session, int? zone = null)
        {
            var db = session.Scene().GetComponent<DBManagerComponent>();
            if (zone.HasValue)
            {
                return db.GetZoneDB(zone.Value);
            }

            return db.GetZoneDB(session.Zone());
        }
    }
}