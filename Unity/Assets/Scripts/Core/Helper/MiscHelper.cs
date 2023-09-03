using System.Collections;

namespace ET
{
    public static class MiscHelper
    {
        public static bool IsNullOrEmpty(this ICollection collection)
        {
            if (collection == null || collection.Count == 0)
            {
                return true;
            }

            return false;
        }
    }
}