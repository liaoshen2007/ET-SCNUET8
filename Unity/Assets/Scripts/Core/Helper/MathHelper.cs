using System;
using System.Runtime.CompilerServices;
using Random = Unity.Mathematics.Random;

namespace ET
{
    public static class MathHelper
    {
        public static long Ceil(this double value)
        {
            return (long) Math.Ceiling(value);
        }

        public static long Ceil(this float value)
        {
            return (long) Math.Ceiling(value);
        }

        public static long Ceil(this long value)
        {
            return value;
        }

        public static int Ceil(this int value)
        {
            return value;
        }

        public static bool IsHit(this long value)
        {
            var r = new Random();
            return value >= r.NextInt(10000);
        }

        public static bool IsHit(this int value)
        {
            var r = new Random();
            return value >= r.NextInt(10000);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ToInt(this string value)
        {
            if (int.TryParse(value, out int v))
            {
                return v;
            }

            return 0;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long ToLong(this string value)
        {
            if (long.TryParse(value, out long v))
            {
                return v;
            }

            return 0;
        }
    }
}