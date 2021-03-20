namespace ProjectXyz.Api.Systems
{
    public interface IDiscoverableSystem : ISystem
    {
        int? Priority { get; }
    }
}