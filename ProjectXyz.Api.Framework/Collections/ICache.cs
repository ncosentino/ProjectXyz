namespace ProjectXyz.Api.Framework.Collections
{
    public interface ICache<TKey, TValue> : IReadOnlyCache<TKey, TValue>
    {
        void AddOrUpdate(TKey key, TValue item);

        void Invalidate(TKey key);
    }
}
