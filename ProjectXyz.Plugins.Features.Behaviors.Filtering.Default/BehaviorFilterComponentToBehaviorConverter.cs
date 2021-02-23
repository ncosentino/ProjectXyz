using System;
using System.Collections.Generic;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Behaviors.Filtering;

namespace ProjectXyz.Plugins.Features.Behaviors.Filtering.Default
{
    public sealed class BehaviorFilterComponentToBehaviorConverter : IDiscoverableFilterComponentToBehaviorConverter
    {
        public Type ComponentType => typeof(IBehaviorFilterComponent);

        public IEnumerable<IBehavior> Convert(IFilterComponent filterComponent)
        {
            var behaviorFilterComponent = (IBehaviorFilterComponent)filterComponent;
            return behaviorFilterComponent.Behaviors;
        }
    }
}