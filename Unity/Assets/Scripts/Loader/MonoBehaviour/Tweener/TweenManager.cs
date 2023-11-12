using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    /// <summary>
    /// 动画管理器
    /// </summary>
    [Code]
    public class TweenManager: Singleton<TweenManager>, ISingletonAwake, ISingletonUpdate
    {
        /// <summary>
        /// 创建Tweener
        /// </summary>
        /// <returns></returns>
        public T CreateTweener<T>() where T : Tweener
        {
            var t = ObjectPool.Instance.Fetch(typeof (T));
            var tweenr = (T) t;
            tweenr.Init();
            return tweenr;
        }

        public void Awake()
        {
            fixedUpdatedList = new List<Tweener>();
            updatedList = new List<Tweener>();
        }

        /// <summary>
        /// 注册动画到动画更新字典中
        /// </summary>
        /// <param name="tweener">要注册的动画</param>
        internal void RegisterTweener(Tweener tweener)
        {
            Thrower.IsNotNull(tweener);
            if (tweener.UseFixedUpdate)
            {
                if (fixedUpdatedList.Contains(tweener))
                {
                    Log.Warning($"重复添加动画ID: {tweener.Id}");

                    return;
                }

                fixedUpdatedList.Add(tweener);
            }
            else
            {
                if (updatedList.Contains(tweener))
                {
                    Log.Warning($"重复添加动画ID: {tweener.Id}");

                    return;
                }

                updatedList.Add(tweener);
            }
        }

        /// <summary>
        /// 取消动画更新
        /// </summary>
        /// <param name="tweener">要取消更新的动画</param>
        internal bool UnRegisterTweener(Tweener tweener)
        {
            Thrower.IsNotNull(tweener);
            if (tweener.UseFixedUpdate)
            {
                return fixedUpdatedList.Remove(tweener);
            }

            return updatedList.Remove(tweener);
        }

        /// <summary>
        /// 销毁动画
        /// </summary>
        /// <param name="tweener">要销毁的动画</param>
        /// <param name="complete"></param>
        internal void Kill(Tweener tweener, bool complete)
        {
            tweener.Kill(complete);
        }

        internal void OnKill(Tweener tweener)
        {
            tweener.Reset();
            ObjectPool.Instance.Recycle(tweener);
        }

        internal Tweener GetTween(object target)
        {
            foreach (var tweener in updatedList)
            {
                if (tweener.Target == target)
                {
                    return tweener;
                }
            }

            foreach (var tweener in fixedUpdatedList)
            {
                if (tweener.Target == target)
                {
                    return tweener;
                }
            }

            return null;
        }

        internal Tweener GetTween(object target, TweenPropType propType)
        {
            foreach (var tweener in updatedList)
            {
                if (tweener is UTweener uTweener && uTweener.Target == target && propType == uTweener.PropType)
                {
                    return tweener;
                }
            }

            foreach (var tweener in fixedUpdatedList)
            {
                if (tweener is UTweener uTweener && uTweener.Target == target && propType == uTweener.PropType)
                {
                    return tweener;
                }
            }

            return null;
        }

        void ISingletonUpdate.Update()
        {
            var deltaTime = Time.deltaTime;
            var time = Time.time;
            for (int i = 0; i < updatedList.Count; i++)
            {
                var item = updatedList[i];
                try
                {
                    item.Update(deltaTime, time);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }

        internal void FixedUpdate()
        {
            var deltaTime = Time.fixedDeltaTime;
            var time = Time.time;
            for (int i = 0; i < fixedUpdatedList.Count; i++)
            {
                var item = fixedUpdatedList[i];
                try
                {
                    item.Update(deltaTime, time);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }

        public override void Dispose()
        {
            for (int i = 0; i < updatedList.Count; i++)
            {
                var item = updatedList[i];
                item.Kill();
            }

            for (int i = 0; i < fixedUpdatedList.Count; i++)
            {
                var item = updatedList[i];
                item.Kill();
            }

            fixedUpdatedList.Clear();
            updatedList.Clear();
            
            base.Dispose();
        }

        private List<Tweener> fixedUpdatedList;
        private List<Tweener> updatedList;
    }
}