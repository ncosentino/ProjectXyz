namespace ProjectXyz.Api.Systems
{
    public interface IDiscoverableSystemUpdateComponentCreator : ISystemUpdateComponentCreator
    {
        int? Priority { get; }
    }
}
