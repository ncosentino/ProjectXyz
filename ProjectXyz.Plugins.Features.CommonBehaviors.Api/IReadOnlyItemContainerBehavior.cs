using System.Collections.Generic;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface IReadOnlyItemContainerBehavior : IBehavior
    {
        IIdentifier ContainerId { get; }

        IReadOnlyCollection<IGameObject> Items { get; }
    }
}