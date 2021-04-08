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

        public IPathFinder CreateForMap(IMap map)
        {
            var pathFinder = new AStarPathFinder(
                map,
                _collisionDetector);
            return pathFinder;
        }
    }
}