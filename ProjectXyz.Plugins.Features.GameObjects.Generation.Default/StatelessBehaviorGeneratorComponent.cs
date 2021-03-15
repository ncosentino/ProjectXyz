using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Generation.Default
{
    public sealed class StatelessBehaviorGeneratorComponent : IBehaviorGeneratorComponent
    {
        public StatelessBehaviorGeneratorComponent(
            params IBehavior[] behaviors)
            : this((IEnumerable<IBehavior>)behaviors)
        {
        }

        public StatelessBehaviorGeneratorComponent(
            IEnumerable<IBehavior> behaviors)
        {
            Behaviors = behaviors.ToArray();
        }

        public IReadOnlyCollection<IBehavior> Behaviors { get; }
    }
}