using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    /// <summary>
    /// 动画管理器
    /// </summary>
    internal class TweenManager: MonoBehaviour
    {
        #region Methods

        /// <summary>
        /// 创建Tweener
        /// </summary>
        /// <returns></returns>
        public static T CreateTweener<T>() where T : Tweener
        {
            var t = ObjectPool.Instance.Fetch(typeof (T));
            var tweenr = (T) t;
            tweenr.Init();
            return tweenr;
        }

        #endregion

        #region Internal Methods

        static TweenManager()
        {
            var tween = new GameObject("TweenManager");
            tween.AddComponent<TweenManager>();
            DontDestroyOnLoad(tween);

            fixedUpdatedList = new List<Tweener>();
            updatedList = new List<Tweener>();

            CodeLoader.Instance.OnApplicationQuit += OnQuit;
        }

        /// <summary>
        /// 注册动画到动画更新字典中
        /// </summary>
        /// <param name="tweener">要注册的动画</param>
        internal static void RegisterTweener(Tweener tweener)
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
                tweenCount++;
            }
            else
            {
                if (updatedList.Contains(tweener))
                {
                    Log.Warning($"重复添加动画ID: {tweener.Id}");

                    return;
                }

                updatedList.Add(tweener);
                tweenCount++;
            }
        }

        /// <summary>
        /// 取消动画更新
        /// </summary>
        /// <param name="tweener">要取消更新的动画</param>
        internal static bool UnRegisterTweener(Tweener tweener)
        {
            Thrower.IsNotNull(tweener);
            bool success;
            if (tweener.UseFixedUpdate)
            {
                success = fixedUpdatedList.Remove(tweener);
                if (success)
                {
                    tweenCount--;
                }

                return success;
            }

            success = updatedList.Remove(tweener);
            if (success)
            {
                tweenCount--;
            }

            return success;
        }

        /// <summary>
        /// 销毁动画
        /// </summary>
        /// <param name="tweener">要销毁的动画</param>
        /// <param name="complete"></param>
        internal static void Kill(Tweener tweener, bool complete)
        {
            tweener.Kill(complete);
        }

        internal static void OnKill(Tweener tweener)
        {
            tweener.Reset();
            ObjectPool.Instance.Recycle(tweener);
        }

        internal static Tweener GetTween(object target)
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

        internal static Tweener GetTween(object target, TweenPropType propType)
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

        private void Update()
        {
            for (int i = 0; i < updatedList.Count; i++)
            {
                var item = updatedList[i];
                try
                {
                    item.Update(Time.deltaTime, Time.time);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }

            if (Application.isEditor)
            {
                playerCount = tweenCount;
                cacheCount = ObjectPool.Instance.GetCount(typeof (Tweener));
                totalCount = tweenCount + this.playerCount;
            }
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < fixedUpdatedList.Count; i++)
            {
                var item = fixedUpdatedList[i];
                try
                {
                    item.Update(Time.deltaTime, Time.time);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }

        private static void OnQuit()
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
        }

        #endregion

        #region Internal Fields

        [SerializeField]
        private int playerCount;

        [SerializeField]
        private int cacheCount;

        [SerializeField]
        private int totalCount;

        private static int tweenCount;

        private static List<Tweener> fixedUpdatedList;
        private static List<Tweener> updatedList;

        #endregion
    }
}