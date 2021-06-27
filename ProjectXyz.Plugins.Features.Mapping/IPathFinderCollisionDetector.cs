using System.Collections.Generic;
using System.Numerics;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.Mapping
{
    public interface IPathFinderCollisionDetector
    {
        void Reset(IEnumerable<Vector4> collidersToIgnore);

        bool CollisionsAlongPath(
            Vector2 source,
            Vector2 target);

        IEnumerable<IGameObject> GameObjectsIntersectingArea(Vector4 area);
    }
}