using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors.Api
{
    public interface IActorBehaviorsInterceptor
    {
        IEnumerable<IBehavior> Intercept(IReadOnlyCollection<IBehavior> behaviors);
    }
}
