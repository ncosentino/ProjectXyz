using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.Mapping
{
    public interface IMapBehaviorsProvider
    {
        IEnumerable<IBehavior> GetBehaviors(IReadOnlyCollection<IBehavior> baseBehaviors);
    }
}