using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;

namespace ProjectXyz.Api.GameObjects
{
    public interface IGameObjectsForBehavior
    {
        IEnumerable<IGameObject> GetChildren(IBehavior behavior);
    }
}