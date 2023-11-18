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
    }
}