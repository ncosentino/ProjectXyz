using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors.Api
{
    public interface IAdditionalActorBehaviorsProvider
    {
        IEnumerable<IBehavior> GetBehaviors(IGameObject gameObject);
    }
}
