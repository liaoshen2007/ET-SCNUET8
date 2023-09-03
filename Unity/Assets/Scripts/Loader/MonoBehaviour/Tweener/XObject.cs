using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ET
{
    /// <summary>
    /// 对UGUI物体的简单封装
    /// </summary>
    public class XObject : UIBehaviour,
        IPointerEnterHandler,
        IPointerExitHandler,
        IBeginDragHandler,
        IDragHandler,
        IEndDragHandler,
        IPointerClickHandler,
        IPointerDownHandler,
        IPointerUpHandler,
        IScrollHandler
    {
        #region Events
        /// <summary>
        /// 当UI被点击时触发
        /// </summary>
        public Action<XObject, PointerEventData> OnClick;

        /// <summary>
        /// 当鼠标或手指移入UI对象时触发
        /// </summary>
        public Action<XObject, PointerEventData> OnRollOver;

        /// <summary>
        /// 当鼠标或手指移出UI对象时触发
        /// </summary>
        public Action<XObject, PointerEventData> OnRollOut;

        /// <summary>
        /// 当位置改变时触发
        /// </summary>
        public Action<XObject, PointerEventData> OnPositionChanged;

        /// <summary>
        /// 当大小改变时触发
        /// </summary>
        public Action<XObject, PointerEventData> OnSizeChanged;

        /// <summary>
        /// 开始拖拽时触发
        /// </summary>
        public Action<XObject, PointerEventData> OnDragStart;

        /// <summary>
        /// 拖拽中
        /// </summary>
        public Action<XObject, PointerEventData> OnDragMove;

        /// <summary>
        /// 拖拽结束触发
        /// </summary>
        public Action<XObject, PointerEventData> OnDragEnd;

        /// <summary>
        /// 当鼠标按下时触发
        /// </summary>
        public Action<XObject, PointerEventData> OnMouseDown;

        /// <summary>
        /// 当鼠标抬起时触发
        /// </summary>
        public Action<XObject, PointerEventData> OnMouseUp;

        /// <summary>
        /// 当鼠标滚轮滚动时触发
        /// </summary>
        public Action<XObject, PointerEventData> OnMouseWheel;
        #endregion

        #region Properties
        /// <summary>
        /// 用户自定义数据
        /// </summary>
        public object UserData { get; set; }

        public RectTransform CacheTransform { get; private set; }

        public int SortingOrder
        {
            get => transform.GetSiblingIndex();
            set => transform.SetSiblingIndex(value);
        }

        /// <summary>
        /// 所属父物体列表
        /// </summary>
        public XObject Parent { get; internal set; }

        /// <summary>
        /// The x coordinate of the object relative to the local coordinates of the parent.
        /// </summary>
        public float X
        {
            get => x;
            set => SetPosition(value, y, z);
        }

        /// <summary>
        /// The Y coordinate of the object relative to the local coordinates of the parent.
        /// </summary>
        public float Y
        {
            get => y;
            set => SetPosition(x, value, z);
        }

        /// <summary>
        /// The Z coordinate of the object relative to the local coordinates of the parent.
        /// </summary>
        public float Z
        {
            get => z;
            set => SetPosition(x, y, value);
        }

        /// <summary>
        /// The x and Y coordinates of the object relative to the local coordinates of the parent.
        /// </summary>
        public Vector2 XY
        {
            get => new Vector2(x, y);
            set => SetPosition(value.x, value.y, z);
        }

        /// <summary>
        /// The x,Y,Z coordinates of the object relative to the local coordinates of the parent.
        /// </summary>
        public Vector3 Position
        {
            get => new Vector3(x, y, z);
            set => SetPosition(value.x, value.y, value.z);
        }

        /// <summary>
        /// The Width of the object in pixels.
        /// </summary>
        public float Width
        {
            get => width;
            set => SetSize(value, rawHeight);
        }

        /// <summary>
        /// The Height of the object in pixels.
        /// </summary>
        public float Height
        {
            get => height;
            set => SetSize(rawWidth, value);
        }

        /// <summary>
        /// The Size of the object in pixels.
        /// </summary>
        public Vector2 Size
        {
            get => new Vector2(Width, Height);
            set => SetSize(value.x, value.y);
        }

        /// <summary>
        /// ActualWidth = Width * scalex
        /// </summary>
        public float ActualWidth => Width * scaleX;

        /// <summary>
        /// ActualHeight = Height * ScaleY
        /// </summary>
        public float ActualHeight => Height * scaleY;

        /// <summary>
        /// </summary>
        public float XMin
        {
            get => pivotAsAnchor ? (x - width * pivotX) : x;
            set
            {
                if (pivotAsAnchor)
                    SetPosition(value + width * pivotX, y, z);
                else
                    SetPosition(value, y, z);
            }
        }

        /// <summary>
        /// </summary>
        public float YMin
        {
            get => pivotAsAnchor ? (y - height * pivotY) : y;
            set
            {
                if (pivotAsAnchor)
                    SetPosition(x, value + height * pivotY, z);
                else
                    SetPosition(x, value, z);
            }
        }

        /// <summary>
        /// The Horizontal Scale factor. '1' means no Scale, cannt be negative.
        /// </summary>
        public float ScaleX
        {
            get => scaleX;
            set => SetScale(value, scaleY);
        }

        /// <summary>
        /// The Vertical Scale factor. '1' means no Scale, cannt be negative.
        /// </summary>
        public float ScaleY
        {
            get => scaleY;
            set => SetScale(scaleX, value);
        }

        /// <summary>
        /// The Scale factor.
        /// </summary>
        public Vector2 Scale
        {
            get => new Vector2(scaleX, scaleY);
            set => SetScale(value.x, value.y);
        }

        /// <summary>
        /// The x coordinate of the object's origin in its own coordinate space.
        /// </summary>
        public float PivotX
        {
            get => pivotX;
            set => SetPivot(value, pivotY, pivotAsAnchor);
        }

        /// <summary>
        /// The Y coordinate of the object's origin in its own coordinate space.
        /// </summary>
        public float PivotY
        {
            get => pivotY;
            set => SetPivot(pivotX, value, pivotAsAnchor);
        }

        /// <summary>
        /// The x and Y coordinates of the object's origin in its own coordinate space.
        /// </summary>
        public Vector2 Pivot
        {
            get => new Vector2(pivotX, pivotY);
            set => SetPivot(value.x, value.y, pivotAsAnchor);
        }

        public bool PivotAsAnchor
        {
            get => pivotAsAnchor;
            set => SetPivot(pivotX, pivotY, value);
        }

        /// <summary>
        /// The Rotation around the Z axis of the object in degrees.
        /// </summary>
        public float Rotation
        {
            get => rotation;
            set
            {
                rotation = value;
                rotateVec = CacheTransform.localEulerAngles;
                rotateVec.z = -value;
                CacheTransform.localEulerAngles = rotateVec;
            }
        }

        /// <summary>
        /// The Rotation around the x axis of the object in degrees.
        /// </summary>
        public float RotationX
        {
            get => rotationX;
            set
            {
                rotationX = value;
                rotateVec = CacheTransform.localEulerAngles;
                rotateVec.x = value;
                CacheTransform.localEulerAngles = rotateVec;
            }
        }

        /// <summary>
        /// The Rotation around the Y axis of the object in degrees.
        /// </summary>
        public float RotationY
        {
            get => rotationY;
            set
            {
                rotationY = value;
                rotateVec = CacheTransform.localEulerAngles;
                rotateVec.y = value;
                CacheTransform.localEulerAngles = rotateVec;
            }
        }

        /// <summary>
        /// The opacity of the object. 0 = transparent, 1 = opaque.
        /// </summary>
        public float Alpha
        {
            get => alpha;

            set
            {
                alpha = value;
                HandleAlphaChanged();
            }
        }

        /// <summary>
        /// The visibility of the object. An invisible object will be untouchable.
        /// </summary>
        public bool Visible
        {
            get => visible;

            set
            {
                if (visible != value)
                {
                    visible = value;
                    HandleVisibleChanged();
                }
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Change the x and Y coordinates of the object's origin in its own coordinate space.
        /// </summary>
        /// <param name="xv">x Value in ratio</param>
        /// <param name="yv">Y Value in ratio</param>
        public void SetPivot(float xv, float yv)
        {
            SetPivot(xv, yv, false);
        }

        /// <summary>
        /// Change the x and Y coordinates of the object's origin in its own coordinate space.
        /// </summary>
        /// <param name="xv">      x Value in ratio</param>
        /// <param name="yv">      Y Value in ratio</param>
        /// <param name="asAnchor">If use the Pivot as the anchor Position</param>
        public void SetPivot(float xv, float yv, bool asAnchor)
        {
            if (pivotX != xv || pivotY != yv || pivotAsAnchor != asAnchor)
            {
                pivotX = xv;
                pivotY = yv;
                pivotAsAnchor = asAnchor;
                CacheTransform.pivot = new Vector2(pivotX, pivotY);
                if (!pivotAsAnchor) //displayObject的轴心参考宽高与GObject的参看宽高不一样的情况下，需要调整displayObject的位置
                {
                    HandlePositionChanged();
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="xv"></param>
        /// <param name="yv"></param>
        public void SetXY(float xv, float yv)
        {
            if (pivotAsAnchor)
            {
                SetPosition(xv + pivotX * width, yv + pivotY * height, z);
            }
            else
            {
                SetPosition(xv, yv, z);
            }
        }

        /// <summary>
        /// change the x,Y,Z coordinates of the object relative to the local coordinates of the parent.
        /// </summary>
        /// <param name="xv">x Value.</param>
        /// <param name="yv">Y Value.</param>
        /// <param name="zv">Z Value.</param>
        public void SetPosition(float xv, float yv, float zv)
        {
            if (x != xv || y != yv || z != zv)
            {
                x = xv;
                y = yv;
                z = zv;

                HandlePositionChanged();
                OnPositionChanged?.Invoke(this, null);
            }
        }

        /// <summary>
        /// Change Size.
        /// </summary>
        /// <param name="wv">Width Value.</param>
        /// <param name="hv">Height Value.</param>
        public void SetSize(float wv, float hv)
        {
            SetSize(wv, hv, false);
        }

        /// <summary>
        /// Change Size.
        /// </summary>
        /// <param name="wv">         Width Value.</param>
        /// <param name="hv">         Height Value.</param>
        /// <param name="ignorePivot">
        /// If Pivot is set, the object's positon will change when its Size change. Set
        /// ignorePivot=true to keep the Position.
        /// </param>
        public void SetSize(float wv, float hv, bool ignorePivot)
        {
            if (rawWidth != wv || rawHeight != hv)
            {
                rawWidth = wv;
                rawHeight = hv;
                float dWidth = wv - width;
                float dHeight = hv - height;
                width = wv;
                height = hv;

                HandleSizeChanged();
                if (pivotX != 0 || pivotY != 0)
                {
                    if (!pivotAsAnchor)
                    {
                        if (!ignorePivot)
                        {
                            SetXY(x - pivotX * dWidth, y - pivotY * dHeight);
                        }
                        else
                        {
                            HandlePositionChanged();
                        }
                    }
                    else
                    {
                        HandlePositionChanged();
                    }
                }
            }
        }

        /// <summary>
        /// Change the Scale factor.
        /// </summary>
        /// <param name="wv">The Horizontal Scale factor.</param>
        /// <param name="hv">The Vertical Scale factor</param>
        public void SetScale(float wv, float hv)
        {
            if (scaleX != wv || scaleY != hv)
            {
                scaleX = wv;
                scaleY = hv;
                HandleScaleChanged();
            }
        }

        #endregion

        #region Internal Methods

        protected override void Awake()
        {
            base.Awake();
            CacheTransform = GetComponent<RectTransform>();

            visible = true;
            x = CacheTransform.localPosition.x;
            y = CacheTransform.localPosition.y;
            z = CacheTransform.localPosition.z;
            width = CacheTransform.sizeDelta.x;
            height = CacheTransform.sizeDelta.y;
            scaleX = CacheTransform.localScale.x;
            scaleY = CacheTransform.localScale.y;
            pivotX = CacheTransform.pivot.x;
            pivotY = CacheTransform.pivot.y;
            rotateVec = CacheTransform.localEulerAngles;
            rotation = -CacheTransform.localEulerAngles.z;
            rotationX = CacheTransform.localEulerAngles.x;
            rotationY = CacheTransform.localEulerAngles.y;
        }

        protected void SetSizeDirectly(float wv, float hv)
        {
            rawWidth = wv;
            rawHeight = hv;
            if (wv < 0)
            {
                wv = 0;
            }

            if (hv < 0)
            {
                hv = 0;
            }

            width = wv;
            height = hv;
        }

        protected virtual void HandlePositionChanged()
        {
            if (transform != null)
            {
                float xv = x;
                float yv = y;
                if (!pivotAsAnchor)
                {
                    xv += width * pivotX;
                    yv += height * pivotY;
                }

                CacheTransform.localPosition = new Vector3(xv, yv, z);
            }
        }

        protected virtual void HandleSizeChanged()
        {
            CacheTransform.sizeDelta = new Vector2(width, height);
        }

        protected virtual void HandleScaleChanged()
        {
            CacheTransform.localScale = new Vector3(scaleX, scaleY);
        }

        protected virtual void HandleAlphaChanged()
        {
            //TODO
        }

        protected internal virtual void HandleVisibleChanged()
        {
            gameObject.SetActive(visible);
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            OnRollOver?.Invoke(this, eventData);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            OnRollOut?.Invoke(this, eventData);
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            OnDragStart?.Invoke(this, eventData);
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            OnDragMove?.Invoke(this, eventData);
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            OnDragEnd?.Invoke(this, eventData);
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(this, eventData);
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            OnMouseDown?.Invoke(this, eventData);
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            OnMouseUp?.Invoke(this, eventData);
        }

        protected override void OnRectTransformDimensionsChange()
        {
            base.OnRectTransformDimensionsChange();

            OnSizeChanged?.Invoke(this, null);
        }

        void IScrollHandler.OnScroll(PointerEventData eventData)
        {
            OnMouseWheel?.Invoke(this, eventData);
        }

        #endregion

        #region Internal Fields
        private float x;
        private float y;
        private float z;
        private float pivotX;
        private float pivotY;
        private bool pivotAsAnchor;
        private float alpha;
        private float rotation;
        private float rotationX;
        private float rotationY;
        private bool visible;
        private float scaleX;
        private float scaleY;

        internal float width;
        internal float height;
        internal float rawWidth;
        internal float rawHeight;
        private Vector3 rotateVec;
        #endregion

        #region Tween Helpers
        //
        // public Tweener TweenMove(Vector2 EndValue, float duration)
        // {
        //     return Tween.To(XY, EndValue, duration).SetTarget(this, TweenPropType.XY);
        // }
        //
        // public Tweener TweenMoveX(float EndValue, float duration)
        // {
        //     return Tween.To(x, EndValue, duration).SetTarget(this, TweenPropType.X);
        // }
        //
        // public Tweener TweenMoveY(float EndValue, float duration)
        // {
        //     return Tween.To(y, EndValue, duration).SetTarget(this, TweenPropType.Y);
        // }
        //
        // public Tweener TweenScale(Vector2 EndValue, float duration)
        // {
        //     return Tween.To(Scale, EndValue, duration).SetTarget(this, TweenPropType.Scale);
        // }
        //
        // public Tweener TweenScaleX(float EndValue, float duration)
        // {
        //     return Tween.To(scaleX, EndValue, duration).SetTarget(this, TweenPropType.ScaleX);
        // }
        //
        // public Tweener TweenScaleY(float EndValue, float duration)
        // {
        //     return Tween.To(scaleY, EndValue, duration).SetTarget(this, TweenPropType.ScaleY);
        // }
        //
        // public Tweener TweenResize(Vector2 EndValue, float duration)
        // {
        //     return Tween.To(Size, EndValue, duration).SetTarget(this, TweenPropType.Size);
        // }
        //
        // public Tweener TweenFade(float EndValue, float duration)
        // {
        //     return Tween.To(alpha, EndValue, duration).SetTarget(this, TweenPropType.Alpha);
        // }
        //
        // public Tweener TweenRotate(float EndValue, float duration)
        // {
        //     return Tween.To(rotation, EndValue, duration).SetTarget(this, TweenPropType.Rotation);
        // }

        #endregion
    }
}