using System.Collections.Generic;
using System.Numerics;

namespace ProjectXyz.Plugins.Features.Mapping.Api
{
    public interface IPathFinder
    {
        IEnumerable<Vector2> FindPath(
            Vector2 startPosition,
            Vector2 endPosition,
            Vector2 size);

        IEnumerable<Vector2> GetAdjacentPositions(
            Vector2 position,
            bool includeDiagonals);
    }
}