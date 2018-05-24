namespace ProjectXyz.Framework.Interface.Collections
{
    public interface ICache<TKey, TValue> : IReadOnlyCache<TKey, TValue>
    {
        void Add(TKey key, TValue item);
    }
}
