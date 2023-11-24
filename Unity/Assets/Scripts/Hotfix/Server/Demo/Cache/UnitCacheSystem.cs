namespace ET.Server
{
    [FriendOf(typeof (UnitCache))]
    public static class UnitCacheSystem
    {
        public class UnitCacheDestorySystem: DestroySystem<UnitCache>
        {
            protected override void Destroy(UnitCache self)
            {
                foreach (Entity entity in self.ComponentDict.Values)
                {
                    entity.Dispose();
                }

                self.UpdateTimeDict.Clear();
                self.ComponentDict.Clear();
                self.TypeName = null;
            }
        }

        /// <summary>
        /// 检测缓存
        /// </summary>
        /// <param name="self"></param>
        public static void Check(this UnitCache self)
        {
            using ListComponent<long> ids = ListComponent<long>.Create();
            ids.AddRange(self.ComponentDict.Keys);
            foreach (long id in ids)
            {
                if (!self.ComponentDict.TryGetValue(id, out Entity entity))
                {
                    continue;
                }

                if (TimeInfo.Instance.ServerFrameTime() - self.UpdateTimeDict.Get(id) <= UnitCache.Interval)
                {
                    continue;
                }

                entity.Dispose();
                self.ComponentDict.Remove(id);
                self.UpdateTimeDict.Remove(id);
            }
        }

        public static void AddOrUpdate(this UnitCache self, Entity entity)
        {
            if (entity == null || entity.IsDisposed)
            {
                return;
            }

            if (self.ComponentDict.TryGetValue(entity.Id, out var old))
            {
                if (old != entity)
                {
                    old.Dispose();
                }

                self.ComponentDict.Remove(entity.Id);
            }

            self.ComponentDict.Add(entity.Id, entity);
            self.UpdateTimeDict.Add(entity.Id, TimeInfo.Instance.ServerFrameTime());
        }

        public static async ETTask<Entity> Get(this UnitCache self, long id)
        {
            if (self.ComponentDict.TryGetValue(id, out var entity))
            {
                self.UpdateTimeDict[entity.Id] = TimeInfo.Instance.ServerFrameTime();
                return entity;
            }

            entity = await self.Root().GetComponent<DBManagerComponent>().GetZoneDB(self.Zone()).Query<Entity>(id, self.TypeName);
            self.AddOrUpdate(entity);

            return entity;
        }

        public static void Delete(this UnitCache self, long id)
        {
            if (!self.ComponentDict.TryGetValue(id, out var cache))
            {
                return;
            }

            cache.Dispose();
            self.ComponentDict.Remove(id);
            self.UpdateTimeDict.Remove(id);
        }
    }
}