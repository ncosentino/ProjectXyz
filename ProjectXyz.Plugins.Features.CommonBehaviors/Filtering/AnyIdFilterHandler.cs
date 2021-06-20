using System.Linq;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Filtering
{
    public sealed class AnyIdFilterHandler
    {
        public AnyIdFilterHandler()
        {
            Matcher = (filter, obj) =>
            {
                if (!obj.TryGetFirst<IIdentifierBehavior>(out var identifierBehavior))
                {
                    return false;
                }

                var match = filter.Ids.Contains(identifierBehavior.Id);
                return match;
            };
        }

        public GenericAttributeValueMatchDelegate<AnyIdFilter, IGameObject> Matcher;
    }
}
