using System.Linq;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Filtering
{
    public sealed class AnyItemDefinitionIdentifierFilterHandler
    {
        public AnyItemDefinitionIdentifierFilterHandler()
        {
            Matcher = (filter, item) =>
            {
                if (!item.TryGetFirst<IItemDefinitionIdentifierBehavior>(out var itemDefinitionIdentifierBehavior))
                {
                    return false;
                }

                var match = filter.ItemDefinitionIds.Any(id => itemDefinitionIdentifierBehavior.ItemDefinitionId.Equals(id));
                return match;
            };
        }

        public GenericAttributeValueMatchDelegate<AnyItemDefinitionIdentifierFilter, IGameObject> Matcher;
    }
}
