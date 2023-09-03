﻿using ET;
using UnityEngine;

namespace ET
{
    /// <summary>
    /// 位置动画
    /// </summary>
    public class TweenPosition : UComTweener
    {
        #region Internal Methods
        /// <summary>
        /// 当更新时
        /// </summary>
        /// <param name="factor">采样因子 大小在0 - 1之间</param>
        protected override void OnUpdate(float factor, float currentTime)
        {
            if (isLocal)
            {
                transform.localPosition = Vector3.Lerp(startValue, endValue, factor);
            }
            else
            {
                transform.position = Vector3.Lerp(startValue, endValue, factor);
            }
        }
        #endregion

        #region Internal Fields
        [SerializeField]
        private Vector3 startValue = Vector3.zero;

        [SerializeField]
        private Vector3 endValue = Vector3.zero;

        [SerializeField]
        private bool isLocal = false;
        #endregion
    }
}