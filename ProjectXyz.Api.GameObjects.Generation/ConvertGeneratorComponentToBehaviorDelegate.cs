using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace ProjectXyz.Api.GameObjects.Generation
{
    public delegate IEnumerable<IBehavior> ConvertGeneratorComponentToBehaviorDelegate(
        IFilterContext filterContext,
        IReadOnlyCollection<IBehavior> baseBehaviors,
        IGeneratorComponent generatorComponent);
}