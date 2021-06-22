using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.Mapping
{
    public interface IPathFinderFactory
    {
        IPathFinder CreateForMap(IGameObject map);
    }
}