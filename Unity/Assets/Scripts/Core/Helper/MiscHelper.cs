using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ET
{
    public static class MiscHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmpty(this ICollection collection)
        {
            if (collection == null || collection.Count == 0)
            {
                return true;
            }

            return false;
        }

        public static string[] ToStringArray<T>(this IEnumerable<T> itor)
        {
            var list = new List<string>();
            foreach (var obj in itor)
            {
                list.Add(obj.ToString());
            }

            return list.ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Get<T>(this IList<T> collection, int index, T def = default)
        {
            if (collection.Count <= index || index < 0)
            {
                return def;
            }

            return collection[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasIndex<T>(this IList<T> collection, int index)
        {
            if (collection.Count <= index || index < 0)
            {
                return false;
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static V Get<T, V>(this Dictionary<T, V> dict, T key, V def = default)
        {
            if (dict.TryGetValue(key, out V v))
            {
                return v;
            }

            return default;
        }

        public static bool Exists<T>(this IEnumerable<T> itor, T key)
        {
            foreach (T t in itor)
            {
                if (t.Equals(key))
                {
                    return true;
                }
            }

            return false;
        }

        public static int ToInt(this string str, int def = 0)
        {
            if (int.TryParse(str, out int v))
            {
                return v;
            }

            return def;
        }

        public static long ToLong(this string str, long def = 0)
        {
            if (long.TryParse(str, out long v))
            {
                return v;
            }

            return def;
        }

        public static float ToFloat(this string str, float def = 0)
        {
            if (float.TryParse(str, out float v))
            {
                return v;
            }

            return def;
        }
    }
}