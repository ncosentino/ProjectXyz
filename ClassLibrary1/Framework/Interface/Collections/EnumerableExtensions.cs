using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassLibrary1.Framework.Interface.Collections
{
    public static class EnumerableExtensions
    {
        public static void Foreach<T>(
            this IEnumerable<T> enumerable, 
            Action<T> perItemCallback)
        {
            foreach (var item in enumerable)
            {
                perItemCallback(item);
            }
        }

        public static IEnumerable<T2> TakeTypes<T1, T2>(this IEnumerable<T1> enumerable)
        {
            return enumerable.Where(x => x is T2).Cast<T2>();
        }

        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> enumerable)
        {
            return enumerable.ToDictionary(
                x => x.Key,
                x => x.Value);
        }
    }
}