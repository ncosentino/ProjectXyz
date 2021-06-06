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

        IEnumerable<Vector2> GetAllAdjacentPositionsToTile(
            Vector2 position,
            bool includeDiagonals);

        IEnumerable<Vector2> GetFreeAdjacentPositionsToTile(
            Vector2 position,
            bool includeDiagonals);

        IEnumerable<Vector2> GetAllAdjacentPositionsToObject(
            Vector2 position,
            Vector2 size,
            bool includeDiagonals);

        IEnumerable<Vector2> GetFreeAdjacentPositionsToObject(
            Vector2 position,
            Vector2 size,
            bool includeDiagonals);

        IEnumerable<Vector2> GetFreeTilesInRadius(
            Vector2 position,
            int radiusInTiles);
    }
}