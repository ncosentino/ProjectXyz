using System.Collections.Generic;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Generation.Default
{
    public sealed class BehaviorGeneratorComponentToBehaviorConverter : IDiscoverablePredicateGeneratorComponentToBehaviorConverter
    {
        public bool CanConvert(IGeneratorComponent generatorComponent) => generatorComponent is IBehaviorGeneratorComponent;

        public IEnumerable<IBehavior> Convert(
            IFilterContext filterContext,
            IEnumerable<IBehavior> baseBehaviors,
            IGeneratorComponent generatorComponent)
        {
            var behaviorGeneratorComponent = (IBehaviorGeneratorComponent)generatorComponent;
            return behaviorGeneratorComponent.Behaviors;
        }
    }
}