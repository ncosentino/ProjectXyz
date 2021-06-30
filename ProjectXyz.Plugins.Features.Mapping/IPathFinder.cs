using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.Mapping
{
    public interface IPathFinder
    {
        IEnumerable<IGameObject> GetIntersectingGameObjects(
            Vector2 position,
            Vector2 size);

        Task<IPath> FindPathAsync(
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