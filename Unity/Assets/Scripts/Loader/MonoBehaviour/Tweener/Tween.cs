using UnityEngine;

namespace ET
{
    /// <summary>
    /// 动画接口
    /// </summary>
    public static class Tween
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startValue"></param>
        /// <param name="endValue"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static UTweener To(float startValue, float endValue, float duration)
        {
            return CreateTween().To(startValue, endValue, duration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startValue"></param>
        /// <param name="endValue"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static UTweener To(Vector2 startValue, Vector2 endValue, float duration)
        {
            return CreateTween().To(startValue, endValue, duration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startValue"></param>
        /// <param name="endValue"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static UTweener To(Vector3 startValue, Vector3 endValue, float duration)
        {
            return CreateTween().To(startValue, endValue, duration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startValue"></param>
        /// <param name="endValue"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static UTweener To(Vector4 startValue, Vector4 endValue, float duration)
        {
            return CreateTween().To(startValue, endValue, duration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startValue"></param>
        /// <param name="endValue"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static UTweener To(Color startValue, Color endValue, float duration)
        {
            return CreateTween().To(startValue, endValue, duration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startValue"></param>
        /// <param name="endValue"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static UTweener ToDouble(double startValue, double endValue, float duration)
        {
            return CreateTween().To(startValue, endValue, duration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startValue"></param>
        /// <param name="amplitude"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static UTweener Shake(Vector3 startValue, float amplitude, float duration)
        {
            return CreateTween().Shake(startValue, amplitude, duration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="complete"></param>
        public static void Kill(object target, bool complete = false)
        {
            var tween = GetTween(target);
            if (tween != null)
            {
                TweenManager.Kill(tween, complete);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static Tweener GetTween(object target)
        {
            return TweenManager.GetTween(target, TweenPropType.None);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="propType"></param>
        /// <returns></returns>
        public static Tweener GetTween(object target, TweenPropType propType)
        {
            return TweenManager.GetTween(target, propType);
        }

        private static UTweener CreateTween()
        {
            return TweenManager.CreateTweener<UTweener>();
        }
    }
}