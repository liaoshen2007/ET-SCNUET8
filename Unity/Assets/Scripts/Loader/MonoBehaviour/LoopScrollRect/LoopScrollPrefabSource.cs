using System;
using System.Collections.Generic;
using ET;

namespace UnityEngine.UI
{
    public interface LoopScrollPrefabSource
    {
        GameObject GetObject(string prefabName = default);

        void ReturnObject(Transform trans, bool isDestroy = false);
    }

    [Serializable]
    public class ScrollPrefabItem
    {
        public string prefabName;
        public GameObject prefab;
        public int poolSize = 5;

        [NonSerialized]
        public bool inited = false;
    }

    [Serializable]
    public class LoopScrollPrefabSourceInstance: LoopScrollPrefabSource
    {
        public List<ScrollPrefabItem> PrefabList;

        private bool inited = false;
        private Dictionary<string, ScrollPrefabItem> prefabDict;

        private void Init()
        {
            if (this.inited)
            {
                return;
            }

            this.inited = true;
            prefabDict = new Dictionary<string, ScrollPrefabItem>();
            for (int i = 0; i < PrefabList.Count; i++)
            {
                var item = PrefabList[i];
                prefabDict.Add(item.prefabName, item);
            }
        }

        public GameObject GetObject(string prefabName = default)
        {
            try
            {
                this.Init();
                if (this.PrefabList.Count == 0)
                {
                    Log.Error("滑动列表没有配置滑动项1");
                    return default;
                }

                if (prefabName.IsNullOrEmpty())
                {
                    prefabName = PrefabList[0].prefabName;
                }

                if (!this.prefabDict.TryGetValue(prefabName, out ScrollPrefabItem item))
                {
                    Log.Error("滑动列表没有配置滑动项2");
                    return default;
                }

                if (item.inited)
                {
                    return GameObjectPoolHelper.GetObjectFromPool(prefabName);
                }

                GameObjectPoolHelper.InitPool(prefabName, item.prefab, item.poolSize);
                item.inited = true;

                return GameObjectPoolHelper.GetObjectFromPool(prefabName);
            }
            catch (Exception e)
            {
                Log.Error(e);
                return null;
            }
        }

        public virtual void ReturnObject(Transform go, bool isDestroy = false)
        {
            try
            {
                if (isDestroy)
                {
                    Object.Destroy(go.gameObject);
                }
                else
                {
                    GameObjectPoolHelper.ReturnTransformToPool(go);
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}