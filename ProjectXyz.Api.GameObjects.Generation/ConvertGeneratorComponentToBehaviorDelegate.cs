using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Api.GameObjects.Generation
{
    public delegate IEnumerable<IBehavior> ConvertGeneratorComponentToBehaviorDelegate(
        IReadOnlyCollection<IBehavior> baseBehaviors,
        IGeneratorComponent generatorComponent);
}