using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors.Api
{
    public interface IAdditionalActorBehaviorsProvider
    {
        IEnumerable<IBehavior> GetBehaviors();
    }
}
