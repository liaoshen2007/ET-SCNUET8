using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ET
{
    public class TweenButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        #region Internal Methods

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            // 不可交互或没有缩放对象
            if (target != null && !target.IsInteractable() || !scaleTarget || scaleDuration <= 0)
            {
                return;
            }

            if (scaleTweener == null)
            {
                scaleTweener = Tween.To(Vector3.one, Vector3.one * scaleFactor, scaleDuration);
                scaleTweener.AutoKill = false;
                scaleTweener.OnUpdated += OnUpdate;
            }

            scaleTweener.PlayForward();
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            scaleTweener?.PlayReverse();
        }

        protected void OnDestroy()
        {
            scaleTweener?.Kill();
        }

        private void OnUpdate(Tweener tween)
        {
            if (tween is UTweener ut)
            {
                scaleTarget.localScale = ut.Value.Vec3;
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (target == null)
            {
                target = GetComponentInChildren<Selectable>();
            }

            if (target)
            {
                target.transition = Selectable.Transition.None;
                if (scaleTarget == null)
                {
                    scaleTarget = target.transform;
                }
            }
        }

        private void Reset()
        {
            OnValidate();
        }
#endif
        #endregion

        #region Internal Fields
        [SerializeField]
        private Selectable target;

        [SerializeField]
        private Transform scaleTarget; // 缩放动画target

        [SerializeField]
        private float scaleFactor = 1.1f;

        [SerializeField]
        private float scaleDuration = 0.1f;

        private UTweener scaleTweener;
        #endregion
    }
}