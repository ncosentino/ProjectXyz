using System.Collections.Generic;
using System.Numerics;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.Mapping
{
    public static class IPathFinderExtensions
    {
        public static IEnumerable<IGameObject> GetIntersectingGameObjectsAtTile(
            this IPathFinder pathFinder,
            Vector2 position)
        {
            var gameObjects = pathFinder.GetIntersectingGameObjects(
                position,
                Vector2.One);
            return gameObjects;
        }

        public static IEnumerable<IGameObject> GetGameObjectsAtTile(
            this IPathFinder pathFinder,
            Vector2 position)
        {
            // use a small enough size to get objects that are centered at
            // this tile... but admittedly if there are big objects
            // overlapping this region they'll also get picked up
            var gameObjects = pathFinder.GetIntersectingGameObjects(
                position,
                new Vector2(0.5f, 0.5f));
            return gameObjects;
        }
    }
}