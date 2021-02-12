using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors
{
    public sealed class NoAdditionalActorBehaviorsProvider : IDiscoverableActorBehaviorsProvider
    {
        public IEnumerable<IBehavior> GetBehaviors(IReadOnlyCollection<IBehavior> baseBehaviors)
        {
            yield break;
        }
    }
}
