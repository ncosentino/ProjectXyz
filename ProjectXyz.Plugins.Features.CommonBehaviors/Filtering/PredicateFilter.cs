using System;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Filtering
{
    public sealed class PredicateFilter : IFilterAttributeValue
    {
        public PredicateFilter(Func<IGameObject, bool> predicate)
        {
            Predicate = predicate;
        }

        public Func<IGameObject, bool> Predicate { get; }
    }
}
