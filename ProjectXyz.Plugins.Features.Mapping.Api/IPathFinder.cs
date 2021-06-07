using System.Collections.Generic;
using System.Numerics;

namespace ProjectXyz.Plugins.Features.Mapping.Api
{
    public interface IPathFinder
    {
        IPath FindPath(
            Vector2 startPosition,
            Vector2 endPosition,
            Vector2 size,
            bool includeDiagonals);

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

        IEnumerable<Vector2> GetAllowedPathDestinations(
            Vector2 position,
            Vector2 size,
            double maximumDistance,
            bool includeDiagonals);
    }
}