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

        IEnumerable<Vector2> GetAdjacentPositionsToTile(
            Vector2 position,
            bool includeDiagonals);

        IEnumerable<Vector2> GetAdjacentPositionsToObject(
            Vector2 position,
            Vector2 size,
            bool includeDiagonals);
    }
}