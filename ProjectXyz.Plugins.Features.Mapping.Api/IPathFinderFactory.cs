using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.Mapping.Api
{
    public interface IPathFinderFactory
    {
        IPathFinder CreateForMap(IGameObject map);
    }
}