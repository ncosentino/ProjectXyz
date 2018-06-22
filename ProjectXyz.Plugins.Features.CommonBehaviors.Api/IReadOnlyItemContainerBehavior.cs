using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface IReadOnlyItemContainerBehavior : IBehavior
    {
        IIdentifier ContainerId { get; }

        IReadOnlyCollection<IGameObject> Items { get; }
    }
}