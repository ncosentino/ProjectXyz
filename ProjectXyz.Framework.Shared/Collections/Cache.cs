using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

using NexusLabs.Contracts;

using ProjectXyz.Api.Framework.Collections;

namespace ProjectXyz.Shared.Framework.Collections
{
    public sealed class Cache<TKey, TValue> : ICache<TKey, TValue>
    {
        private readonly OrderedDictionary _cache;

        public Cache(int limit)
        {
            Contract.Requires(
                () => limit > 0,
                () => $"{nameof(limit)} must be >= 1.");

            _cache = new OrderedDictionary(StringComparer.OrdinalIgnoreCase);
            Limit = limit;
        }

        public TValue this[TKey key] => (TValue)_cache[key];

        public IEnumerable<TKey> Keys => _cache.Keys.Cast<TKey>();

        public IEnumerable<TValue> Values => _cache.Values.Cast<TValue>();

        public int Count => _cache.Count;

        public int Limit { get; }

        public void Invalidate(TKey key)
        {
            _cache.Remove(key);
        }

        public void AddOrUpdate(TKey key, TValue item)
        {
            while (_cache.Count >= Limit - 1)
            {
                _cache.RemoveAt(0);
            }

            _cache.Add(key, item);
        }

        public bool TryGetValue(TKey key, out TValue item)
        {
            if (_cache.Contains(key))
            {
                item = (TValue)_cache[key];
                return true;
            }

            item = default(TValue);
            return false;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => Keys
            .Select(x => new KeyValuePair<TKey, TValue>(x, this[x]))
            .GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public bool ContainsKey(TKey key) => _cache.Contains(key);
    }
}
