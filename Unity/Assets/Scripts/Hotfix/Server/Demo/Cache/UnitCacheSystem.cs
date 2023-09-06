namespace ET.Server
{
    public static class UnitCacheSystem
    {
        public class UnitCacheDestorySystem: DestroySystem<UnitCache>
        {
            protected override void Destroy(UnitCache self)
            {
                foreach (var entity in self.ComponentDict.Values)
                {
                    entity.Dispose();
                }

                self.ComponentDict.Clear();
                self.TypeName = null;
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
        }

        public static async ETTask<Entity> Get(this UnitCache self, long id)
        {
            if (!self.ComponentDict.TryGetValue(id, out var entity))
            {
                entity = await self.Root().GetComponent<DBManagerComponent>().GetZoneDB(self.Zone()).Query<Entity>(id, self.TypeName);

                self.AddOrUpdate(entity);
            }

            return entity;
        }

        public static void Delete(this UnitCache self, long id)
        {
            if (self.ComponentDict.TryGetValue(id, out var cache))
            {
                cache.Dispose();
                self.ComponentDict.Remove(id);
            }
        }
    }
}