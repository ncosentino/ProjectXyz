using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Api.GameObjects
{
    public interface IGameObject
    {
        IReadOnlyCollection<IBehavior> Behaviors { get; }
    }
}