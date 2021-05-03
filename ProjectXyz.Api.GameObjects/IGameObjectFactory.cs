using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Api.GameObjects
{
    public interface IGameObjectFactory
    {
        IGameObject Create(IEnumerable<IBehavior> behaviors);
    }
}