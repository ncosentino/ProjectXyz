namespace ProjectXyz.Plugins.Features.Mapping.Api
{
    public interface IPathFinderFactory
    {
        IPathFinder CreateForMap(IMap map);
    }
}