using System;

namespace ET.Server
{
    [FriendOf(typeof (DBManagerComponent))]
    public static partial class DBManagerComponentSystem
    {
        public static DBComponent GetZoneDB(this DBManagerComponent self, int zone)
        {
            DBComponent dbComponent = self.DBComponents[zone];
            if (dbComponent != null)
            {
                return dbComponent;
            }

            StartZoneConfig startZoneConfig = StartZoneConfigCategory.Instance.Get(zone);
            if (startZoneConfig.DBConnection == "")
            {
                throw new Exception($"zone: {zone} not found mongo connect string");
            }

            dbComponent = self.AddChild<DBComponent, string, string, int>(startZoneConfig.DBConnection, startZoneConfig.DBName, zone);
            self.DBComponents[zone] = dbComponent;
            return dbComponent;
        }

        public static DBComponent GetDB(this DBManagerComponent self)
        {
            if (self.CommonDB != null)
            {
                return self.CommonDB;
            }

            StartZoneConfig startZoneConfig = StartZoneConfigCategory.Instance.Get(0);
            if (startZoneConfig.DBConnection == "")
            {
                throw new Exception($"zone: {0} not found mongo connect string");
            }

            self.CommonDB = self.AddChild<DBComponent, string, string, int>(startZoneConfig.DBConnection, startZoneConfig.DBName, 0);
            self.DBComponents[0] = self.CommonDB;
            return self.CommonDB;
        }
    }
}