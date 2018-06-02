using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors
{
    public sealed class NoAdditionalActorBehaviorsProvider : IAdditionalActorBehaviorsProvider
    {
        public IEnumerable<IBehavior> GetBehaviors(IGameObject gameObject)
        {
            yield break;
        }
    }
}
