using System.Collections.Generic;

namespace ProjectXyz.Framework.Interface.Collections
{
    public interface IReadOnlyCache<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    {
        int Limit { get; }
    }
}