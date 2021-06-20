using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Filtering
{
    public sealed class PredicateFilterHandler
    {
        public PredicateFilterHandler()
        {
            Matcher = (filter, obj) =>
            {
                var match = filter.Predicate(obj);
                return match;
            };
        }

        public GenericAttributeValueMatchDelegate<PredicateFilter, IGameObject> Matcher;
    }
}
