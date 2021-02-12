using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors.Api
{
    public interface IActorBehaviorsProvider
    {
        IEnumerable<IBehavior> GetBehaviors(IReadOnlyCollection<IBehavior> baseBehaviors);
    }
}
