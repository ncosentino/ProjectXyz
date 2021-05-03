using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Mapping.Api;

namespace ProjectXyz.Plugins.Features.Mapping.Default.PathFinding
{
    public sealed class AStarPathFinderFactory : IPathFinderFactory
    {
        private readonly IPathFinderCollisionDetector _collisionDetector;

        public AStarPathFinderFactory(IPathFinderCollisionDetector collisionDetector)
        {
            _collisionDetector = collisionDetector;
        }

        public IPathFinder CreateForMap(IGameObject map)
        {
            var pathFinder = new AStarPathFinder(
                map,
                _collisionDetector);
            return pathFinder;
        }
    }
}