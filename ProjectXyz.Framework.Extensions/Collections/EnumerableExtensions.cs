using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Framework.Extensions.Collections
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

        public static IEnumerable<T2> TakeTypes<T2>(this IEnumerable<object> enumerable)
        {
            return enumerable.Where(x => x is T2).Cast<T2>();
        }

        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> enumerable)
        {
            return enumerable.ToDictionary(
                x => x.Key,
                x => x.Value);
        }

        public static IEnumerable<T> Yield<T>(this T obj)
        {
            yield return obj;
        }

        public static T[] AsArray<T>(this T obj)
        {
            return obj.Yield().ToArray();
        }

        public static IEnumerable<T> Repeat<T>(this T obj, int times)
        {
            for (var i = 0; i < times; ++i)
            {
                yield return obj;
            }
        }

        public static IEnumerable<T> Append<T>(this IEnumerable<T> enumerable, T obj)
        {
            return enumerable.Concat(obj.Yield());
        }

        public static IReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.ToArray();
        }

        public static IEnumerable<TProperty> SingleOrDefault<T, TProperty>(
            this IEnumerable<T> enumerable,
            Func<T, IEnumerable<TProperty>> selector)
        {
            return SingleOrDefault(
                enumerable,
                _ => true,
                selector);
            ;
        }

        public static IEnumerable<TProperty> SingleOrDefault<T, TProperty>(
            this IEnumerable<T> enumerable,
            Func<T, bool> predicate,
            Func<T, IEnumerable<TProperty>> selector)
        {
            var item = enumerable.SingleOrDefault(predicate);
            return item == null
                ? Enumerable.Empty<TProperty>()
                : selector(item);
        }

        public static TResult Single<TResult>(this IEnumerable<object> enumerable)
        {
            return enumerable.TakeTypes<TResult>().Single();
        }
    }
}