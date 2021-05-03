using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.Mapping.Api
{
    public interface IMapBehaviorsInterceptor
    {
        IEnumerable<IBehavior> Intercept(IReadOnlyCollection<IBehavior> baseBehaviors);
    }
}