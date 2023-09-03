using System.Collections.Generic;

namespace ET
{
    /// <summary>
    /// 动画序列 播放全是顺序进行
    /// </summary>
    public sealed class Sequence : Tweener
    {
        #region Methods
        public Sequence Append(Tweener tweener)
        {
            if (Verify(tweener))
            {
                Duration += tweener.Delay + tweener.Duration;
                tweener.OnComplete += OnCompleted;
                tweenerList.Add(tweener);
            }

            return this;
        }

        public Sequence Insert(int index, Tweener tweener)
        {
            if (Verify(tweener))
            {
                Duration += tweener.Delay + tweener.Duration;
                tweener.OnComplete += OnCompleted;
                tweenerList.Insert(index, tweener);
            }

            return this;
        }

        public Sequence Remove(Tweener tweener)
        {
            if (Verify(tweener))
            {
                Duration -= tweener.Delay + tweener.Duration;
                tweener.OnComplete -= OnCompleted;
                tweenerList.Remove(tweener);
            }

            return this;
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// 验证是否可以添加或移除
        /// </summary>
        /// <param name="tweener"></param>
        /// <returns></returns>
        private bool Verify(Tweener tweener)
        {
            if (tweener == null)
            {
                Log.Error("添加空对象");

                return false;
            }

            if (IsPlaying || tweener.IsPlaying)
            {
                Log.Warning("不允许运行时添加或移除");

                return false;
            }

            if (kill)
            {
                Log.Error("已经销毁的动画对象");

                return false;
            }

            return true;
        }

        protected override void OnStarted()
        {
            base.OnStarted();

            if (IsReverse)
            {
                tweenerList.Reverse();
                currentTweener = tweenerList[0];
                tweenerList.RemoveAt(0);
                currentTweener.PlayReverse();
            }
            else
            {
                currentTweener = tweenerList[0];
                tweenerList.RemoveAt(0);
                currentTweener.PlayForward();
            }
        }

        protected override void OnPaused()
        {
            base.OnPaused();

            if (currentTweener != null)
            {
                currentTweener.IsPaused = true;
            }
        }

        protected override void OnKilled()
        {
            base.OnKilled();

            if (tweenerList.Count > 0)
            {
                foreach (var tweener in tweenerList)
                {
                    tweener.Kill();
                }

                tweenerList.Clear();
            }
        }

        /// <summary>
        /// 当动画完成时
        /// </summary>
        private void OnCompleted(Tweener tweener)
        {
            if (tweenerList.Count > 0)
            {
                currentTweener = tweenerList[0];
                tweenerList.RemoveAt(0);
                if (IsReverse)
                {
                    currentTweener.PlayReverse();
                }
                else
                {
                    currentTweener.PlayForward();
                }
            }
        }
        #endregion

        #region Internal Fields
        private Tweener currentTweener;
        private List<Tweener> tweenerList = new List<Tweener>();
        #endregion
    }
}