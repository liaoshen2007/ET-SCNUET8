using System;
using UnityEngine;

namespace ET
{
    /// <summary>
    /// Unity动画封装实现
    /// </summary>
    public class UTweener : Tweener
    {
        #region Properties
        /// <summary>
        /// 值是否保持整型
        /// </summary>
        public bool Snapping { get; set; }

        /// <summary>
        /// 动画路径
        /// </summary>
        public XPath Path { get; set; }

        /// <summary>
        /// 开始值
        /// </summary>
        public TweenValue StartValue { get; }

        /// <summary>
        /// 结束值
        /// </summary>
        public TweenValue EndValue { get; }

        /// <summary>
        /// 当前值
        /// </summary>
        public TweenValue Value { get; }

        /// <summary>
        /// 上一次动画与当前动画的差值
        /// </summary>
        public TweenValue DeltaValue { get; }

        /// <summary>
        /// 动画类型
        /// </summary>
        public TweenPropType PropType { get; set; }
        #endregion

        #region Methods
        public UTweener()
        {
            StartValue = new TweenValue();
            EndValue = new TweenValue();
            Value = new TweenValue();
            DeltaValue = new TweenValue();
        }
        #endregion

        #region Internal Methods
        /// <summary>
        /// 当更新时
        /// </summary>
        /// <param name="factor">采样因子 大小在0 - 1之间</param>
        /// <param name="currentTime"></param>
        protected override void OnUpdate(float factor, float currentTime)
        {
            base.OnUpdate(factor, currentTime);

            Value.SetZero();
            DeltaValue.SetZero();
            if (valueType == 5) //Double 类型动画
            {
                double d = StartValue.D + (EndValue.D - StartValue.D) * factor;
                if (Snapping)
                {
                    d = Math.Round(d);
                }

                DeltaValue.D = d - Value.D;
                Value.D = d;
                Value.X = (float)d;
            }
            else if (valueType == 6)
            {
                if (!IsComplete)
                {
                    Vector3 r = UnityEngine.Random.insideUnitSphere;
                    r.x = r.x > 0 ? 1 : -1;
                    r.y = r.y > 0 ? 1 : -1;
                    r.z = r.z > 0 ? 1 : -1;
                    r *= StartValue.W * (1 - factor);

                    DeltaValue.Vec3 = r;
                    Value.Vec3 = StartValue.Vec3 + r;
                }
                else
                {
                    Value.Vec3 = StartValue.Vec3;
                }
            }
            else if (Path != null)
            {
                var vec3 = Path.GetPointAt(factor);
                if (Snapping)
                {
                    vec3.x = Mathf.Round(vec3.x);
                    vec3.y = Mathf.Round(vec3.y);
                    vec3.z = Mathf.Round(vec3.z);
                }

                DeltaValue.Vec3 = vec3 - Value.Vec3;
                Value.Vec3 = vec3;
            }
            else
            {
                for (int i = 0; i < valueType; i++)
                {
                    float n1 = StartValue[i];
                    float n2 = EndValue[i];
                    float f = n1 + (n2 - n1) * factor;
                    if (Snapping)
                    {
                        f = Mathf.Round(f);
                    }

                    DeltaValue[i] = f - Value[i];
                    Value[i] = f;
                }

                Value.D = Value.X;
            }

            if (Target != null && PropType != TweenPropType.None)
            {
                TweenPropTypeUtils.SetProps(Target, PropType, Value);
            }
        }

        internal UTweener To(float start, float end, float duration)
        {
            valueType = 1;
            StartValue.X = start;
            EndValue.X = end;
            Value.X = start;
            Duration = duration;
            return this;
        }

        internal UTweener To(Vector2 start, Vector2 end, float duration)
        {
            valueType = 2;
            StartValue.Vec2 = start;
            EndValue.Vec2 = end;
            Value.Vec2 = start;
            Duration = duration;
            return this;
        }

        internal UTweener To(Vector3 start, Vector3 end, float duration)
        {
            valueType = 3;
            StartValue.Vec3 = start;
            EndValue.Vec3 = end;
            Value.Vec3 = start;
            Duration = duration;
            return this;
        }

        internal UTweener To(Vector4 start, Vector4 end, float duration)
        {
            valueType = 4;
            StartValue.Vec4 = start;
            EndValue.Vec4 = end;
            Value.Vec4 = start;
            Duration = duration;
            return this;
        }

        internal UTweener To(Color start, Color end, float duration)
        {
            valueType = 4;
            StartValue.Color = start;
            EndValue.Color = end;
            Value.Color = start;
            Duration = duration;
            return this;
        }

        internal UTweener To(double start, double end, float duration)
        {
            valueType = 5;
            StartValue.D = start;
            EndValue.D = end;
            Value.D = start;
            Duration = duration;
            return this;
        }

        internal UTweener Shake(Vector3 start, float amplitude, float duration)
        {
            valueType = 6;
            StartValue.Vec3 = start;
            StartValue.W = amplitude;
            Duration = duration;
            EaseType = Ease.Linear;
            return this;
        }
        #endregion

        #region Internal Fields
        /// <summary>
        /// 动画类型定义
        /// </summary>
        private int valueType;
        #endregion
    }
}