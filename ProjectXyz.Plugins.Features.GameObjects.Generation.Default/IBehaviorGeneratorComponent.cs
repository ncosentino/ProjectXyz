using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;

namespace ProjectXyz.Plugins.Features.GameObjects.Generation.Default
{
    public interface IBehaviorGeneratorComponent : IGeneratorComponent
    {
        IReadOnlyCollection<IBehavior> Behaviors { get; }
    }
}