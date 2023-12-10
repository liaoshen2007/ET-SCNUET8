using System;

namespace UnityEngine.UI
{
    public interface LoopScrollDataSource
    {
        void ProvideData(Transform transform, int idx);

        string ProvidePrefab(int idx);
    }

    public class LoopScrollDataSourceInstance: LoopScrollDataSource
    {
        public Action<Transform, int> ScrollMoveEvent;

        public Func<int, string> PrefabEvent;

        public void ProvideData(Transform transform, int idx)
        {
            this.ScrollMoveEvent?.Invoke(transform, idx);
        }

        public string ProvidePrefab(int idx)
        {
            return this.PrefabEvent?.Invoke(idx);
        }
    }
}