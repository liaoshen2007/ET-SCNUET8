namespace ET.Server
{
    [EntitySystemOf(typeof (CacheComponent))]
    public static partial class CacheComponentSystem
    {
        [EntitySystem]
        private static void Awake(this CacheComponent self)
        {
            self.CacheDict.Clear();
            self.LoadCacheKey();
            Log.Info($"缓存服务器启动完成：{self.Zone()}");
        }

        [EntitySystem]
        private static void Destroy(this CacheComponent self)
        {
            self.CacheKeyList.Clear();
            foreach (var unitCache in self.CacheDict.Values)
            {
                unitCache.Dispose();
            }

            self.CacheDict.Clear();
        }

        private static void LoadCacheKey(this CacheComponent self)
        {
            self.CacheKeyList.Clear();
            foreach (var type in CodeTypes.Instance.GetTypes().Values)
            {
                if (!type.IsAbstract && type.IsAssignableTo(typeof (ICache)))
                {
                    self.CacheKeyList.Add(type.FullName);
                }
            }

            foreach (string s in self.CacheKeyList)
            {
                var unitCache = self.AddChild<UnitCache>();
                unitCache.TypeName = s;
                self.CacheDict.Add(s, unitCache);
            }
        }

        public static async ETTask<Entity> Get(this CacheComponent self, long id, string key)
        {
            if (!self.CacheDict.TryGetValue(key, out var unitCache))
            {
                unitCache = self.AddChild<UnitCache>();
                unitCache.TypeName = key;
                self.CacheDict.Add(key, unitCache);
            }

            return await unitCache.Get(id);
        }

        public static async ETTask UpdateCache(this CacheComponent self, long id, ListComponent<Entity> listComponent)
        {
            using (ListComponent<Entity> list = ListComponent<Entity>.Create())
            {
                foreach (Entity entity in listComponent)
                {
                    var name = entity.GetType().Name;
                    if (!self.CacheDict.TryGetValue(name, out var uniCache))
                    {
                        uniCache = self.AddChild<UnitCache>();
                        uniCache.TypeName = name;
                        self.CacheDict.Add(name, uniCache);
                    }

                    uniCache.AddOrUpdate(entity);
                    list.Add(entity);
                }

                if (list.Count > 0)
                {
                    await self.Root().GetComponent<DBManagerComponent>().GetZoneDB(self.Zone()).Save(id, list);
                }
            }

            await ETTask.CompletedTask;
        }

        public static void DeleteCache(this CacheComponent self, long id)
        {
            foreach (var unitCach in self.CacheDict.Values)
            {
                unitCach.Delete(id);
            }
        }
    }
}