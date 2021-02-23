using System;
using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;

namespace ProjectXyz.Plugins.Features.Behaviors.Filtering.Default
{
    public sealed class BehaviorFilterComponentToBehaviorConverter : IDiscoverablePredicateFilterComponentToBehaviorConverter
    {
        public bool CanConvert(IFilterComponent filterComponent) => filterComponent is IBehaviorFilterComponent;

        public IEnumerable<IBehavior> Convert(IFilterComponent filterComponent)
        {
            var behaviorFilterComponent = (IBehaviorFilterComponent)filterComponent;
            return behaviorFilterComponent.Behaviors;
        }
    }
}