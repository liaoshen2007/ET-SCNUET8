using System;
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
    }
}