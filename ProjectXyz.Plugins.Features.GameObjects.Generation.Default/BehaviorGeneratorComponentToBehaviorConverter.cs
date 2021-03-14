using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;

namespace ProjectXyz.Plugins.Features.GameObjects.Generation.Default
{
    public sealed class BehaviorGeneratorComponentToBehaviorConverter : IDiscoverablePredicateGeneratorComponentToBehaviorConverter
    {
        public bool CanConvert(IGeneratorComponent generatorComponent) => generatorComponent is IBehaviorGeneratorComponent;

        public IEnumerable<IBehavior> Convert(
            IEnumerable<IBehavior> baseBehaviors,
            IGeneratorComponent generatorComponent)
        {
            var behaviorGeneratorComponent = (IBehaviorGeneratorComponent)generatorComponent;
            return behaviorGeneratorComponent.Behaviors;
        }
    }
}