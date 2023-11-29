namespace ET.Server
{
    public static class CacheHelper
    {
        public static async ETTask UpdateCache<T>(this T self) where T : Entity, ICache
        {
            var cacheCfg = StartSceneConfigCategory.Instance.GetCache(self.Zone());
            var message = new Other2Cache_UpdateCache() { UnitId = self.Id, };
            message.EntityTypeList.Add(self.GetType().FullName);
            message.EntityData.Add(self.ToBson());
            await self.Scene().GetComponent<MessageSender>().Call(cacheCfg.ActorId, message);
        }

        public static void UpdateAllCache(Scene scene, Unit unit)
        {
            var cacheCfg = StartSceneConfigCategory.Instance.GetCache(unit.Zone());
            var message = new Other2Cache_UpdateCache() { UnitId = unit.Id, };
            message.EntityTypeList.Add(unit.GetType().FullName);
            message.EntityData.Add(unit.ToBson());

            foreach (var (id, entity) in unit.Components)
            {
                var t = entity.GetType();
                if (!t.IsAssignableTo(typeof (ICache)))
                {
                    continue;
                }

                message.EntityTypeList.Add(t.FullName);
                message.EntityData.Add(entity.ToBson());
            }

            scene.GetComponent<MessageSender>().Send(cacheCfg.ActorId, message);
        }

        public static async ETTask<Unit> GetCache(Scene gateScene, Scene scene, long unitId)
        {
            var cacheCfg = StartSceneConfigCategory.Instance.GetCache(scene.Zone());
            var resp = (Cache2Other_GetCache)await scene.GetComponent<MessageSender>()
                    .Call(cacheCfg.ActorId, new Other2Cache_GetCache() { UnitId = unitId });
            if (resp.Error != ErrorCode.ERR_Success || resp.Entitys.IsNullOrEmpty())
            {
                return default;
            }

            int index = resp.ComponentNameList.IndexOf(typeof (Unit).FullName);
            if (index == -1)
            {
                return default;
            }

            var unit = MongoHelper.Deserialize<Unit>(resp.Entitys[index]);
            if (unit == null)
            {
                return default;
            }

            gateScene.GetComponent<UnitComponent>().AddChild(unit);
            gateScene.GetComponent<UnitComponent>().Add(unit);
            foreach (var bytes in resp.Entitys)
            {
                if (bytes == null)
                {
                    continue;
                }

                Entity entity = MongoHelper.Deserialize<Entity>(bytes);
                if (entity == unit)
                {
                    continue;
                }

                unit.AddComponent(entity);
            }

            return unit;
        }

        public static async ETTask<T> GetComponentCache<T>(this T self) where T : Entity, ICache
        {
            var cacheCfg = StartSceneConfigCategory.Instance.GetCache(self.Zone());
            var message = new Other2Cache_GetCache() { UnitId = self.Id, };
            message.ComponentNameList.Add(typeof (T).Name);
            var resp = (Cache2Other_GetCache)await self.Scene().GetComponent<MessageSender>().Call(cacheCfg.ActorId, message);
            if (resp.Error == ErrorCode.ERR_Success && resp.Entitys.Count > 0)
            {
                return resp.Entitys[0] as T;
            }

            return default;
        }

        public static async ETTask DeleteCache(Scene scene, long unitId)
        {
            var cacheCfg = StartSceneConfigCategory.Instance.GetCache(scene.Zone());
            await scene.GetComponent<MessageSender>().Call(cacheCfg.ActorId, new Other2Cache_DeleteCache() { UnitId = unitId, });
        }
    }
}