namespace ET
{
    /// <summary>
    /// 
    /// </summary>
    public enum TweenPropType
    {
        None,
        X,
        Y,
        Z,
        XY,
        Position,
        Width,
        Height,
        Size,
        ScaleX,
        ScaleY,
        Scale,
        Rotation,
        RotationX,
        RotationY,
        Alpha,
    }

    internal class TweenPropTypeUtils
    {
        internal static void SetProps(object target, TweenPropType propType, TweenValue value)
        {
            XObject g = target as XObject;
            if (g == null)
            {
                return;
            }

            switch (propType)
            {
                case TweenPropType.X:
                    g.X = value.X;
                    break;

                case TweenPropType.Y:
                    g.Y = value.X;
                    break;

                case TweenPropType.Z:
                    g.Z = value.X;
                    break;

                case TweenPropType.XY:
                    g.XY = value.Vec2;
                    break;

                case TweenPropType.Position:
                    g.Position = value.Vec3;
                    break;

                case TweenPropType.Width:
                    g.Width = value.X;
                    break;

                case TweenPropType.Height:
                    g.Height = value.X;
                    break;

                case TweenPropType.Size:
                    g.Size = value.Vec2;
                    break;

                case TweenPropType.ScaleX:
                    g.ScaleX = value.X;
                    break;

                case TweenPropType.ScaleY:
                    g.ScaleY = value.X;
                    break;

                case TweenPropType.Scale:
                    g.Scale = value.Vec2;
                    break;

                case TweenPropType.Rotation:
                    g.Rotation = value.X;
                    break;

                case TweenPropType.RotationX:
                    g.RotationX = value.X;
                    break;

                case TweenPropType.RotationY:
                    g.RotationY = value.X;
                    break;

                case TweenPropType.Alpha:
                    g.Alpha = value.X;
                    break;
            }
        }
    }
}