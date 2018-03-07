using System;
using System.Collections.Generic;

namespace ProjectXyz.Framework.Interface.Collections
{
    public static class DictionaryExtensions
    {
        #region Methods
        public static IReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return new Dictionary<TKey, TValue>(dictionary);
        }

        public static TValue GetValueOrDefault<TKey, TValue>(
            this Dictionary<TKey, TValue> dictionary,
            TKey key,
            Func<TValue> defaultValueCallback)
        {
            TValue value;
            return dictionary.TryGetValue(key, out value)
                ? value
                : defaultValueCallback();
        }

        public static TValue GetValueOrDefault<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key,
            Func<TValue> defaultValueCallback)
        {
            TValue value;
            return dictionary.TryGetValue(key, out value)
                ? value
                : defaultValueCallback();
        }

        public static TValue GetValueOrDefault<TKey, TValue>(
            this IReadOnlyDictionary<TKey, TValue> dictionary,
            TKey key,
            Func<TValue> defaultValueCallback)
        {
            TValue value;
            return dictionary.TryGetValue(key, out value)
                ? value
                : defaultValueCallback();
        }

        public static TValue GetValueOrDefault<TKey, TValue>(
            this Dictionary<TKey, TValue> dictionary,
            TKey key,
            TValue defaultValue)
        {
            TValue value;
            return dictionary.TryGetValue(key, out value)
                ? value
                : defaultValue;
        }

        public static TValue GetValueOrDefault<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key,
            TValue defaultValue)
        {
            TValue value;
            return dictionary.TryGetValue(key, out value)
                ? value
                : defaultValue;
        }

        public static TValue GetValueOrDefault<TKey, TValue>(
            this IReadOnlyDictionary<TKey, TValue> dictionary,
            TKey key,
            TValue defaultValue)
        {
            TValue value;
            return dictionary.TryGetValue(key, out value)
                ? value
                : defaultValue;
        }

        public static TValue GetValueOrDefault<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key)
        {
            return GetValueOrDefault(dictionary, key, default(TValue));
        }

        public static TValue GetValueOrDefault<TKey, TValue>(
            this IReadOnlyDictionary<TKey, TValue> dictionary,
            TKey key)
        {
            return GetValueOrDefault(dictionary, key, default(TValue));
        }
        #endregion
    }
}