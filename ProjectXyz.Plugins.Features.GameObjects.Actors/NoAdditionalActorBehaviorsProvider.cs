using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors
{
    public sealed class NoAdditionalActorBehaviorsProvider : IAdditionalActorBehaviorsProvider
    {
        public IEnumerable<IBehavior> GetBehaviors()
        {
            yield break;
        }
    }
}
