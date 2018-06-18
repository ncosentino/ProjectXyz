using System.Collections.Generic;

namespace ProjectXyz.Api.Framework.Collections
{
    public interface IReadOnlyCache<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    {
        int Limit { get; }
    }
}