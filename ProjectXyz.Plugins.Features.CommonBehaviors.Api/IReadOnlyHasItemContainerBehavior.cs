using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface IReadOnlyHasItemContainersBehavior : IBehavior
    {
        IReadOnlyCollection<IItemContainerBehavior> ItemContainers { get; }
    }
}