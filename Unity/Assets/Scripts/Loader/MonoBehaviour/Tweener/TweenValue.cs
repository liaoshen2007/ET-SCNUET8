using UnityEngine;

namespace ET
{
    public class TweenValue
    {
        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float Z { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float W { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double D { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Vector2 Vec2
        {
            get => new Vector2(X, Y);
            set
            {
                X = value.x;
                Y = value.y;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Vector3 Vec3
        {
            get => new Vector3(X, Y, Z);
            set
            {
                X = value.x;
                Y = value.y;
                Z = value.z;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Vector4 Vec4
        {
            get => new Vector4(X, Y, Z, W);
            set
            {
                X = value.x;
                Y = value.y;
                Z = value.z;
                W = value.w;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Color Color
        {
            get => new Color(X, Y, Z, W);
            set
            {
                X = value.r;
                Y = value.g;
                Z = value.b;
                W = value.a;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return X;
                    case 1:
                        return Y;
                    case 2:
                        return Z;
                    case 3:
                        return W;
                    default:
                        throw new System.Exception("Index out of bounds: " + index);
                }
            }

            set
            {
                switch (index)
                {
                    case 0:
                        X = value;
                        break;
                    case 1:
                        Y = value;
                        break;
                    case 2:
                        Z = value;
                        break;
                    case 3:
                        W = value;
                        break;
                    default:
                        throw new System.Exception("Index out of bounds: " + index);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetZero()
        {
            X = Y = Z = W = 0;
            D = 0;
        }
        #endregion
    }
}