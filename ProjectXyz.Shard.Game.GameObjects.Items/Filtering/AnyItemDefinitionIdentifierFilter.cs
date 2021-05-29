using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Filtering
{
    public sealed class AnyItemDefinitionIdentifierFilter : IFilterAttributeValue
    {
        public AnyItemDefinitionIdentifierFilter(params IIdentifier[] itemDefinitionIds)
            : this((IEnumerable<IIdentifier>)itemDefinitionIds)
        {
        }

        public AnyItemDefinitionIdentifierFilter(IEnumerable<IIdentifier> itemDefinitionIds)
        {
            ItemDefinitionIds = itemDefinitionIds.ToArray();
        }

        public IReadOnlyCollection<IIdentifier> ItemDefinitionIds { get; }
    }
}
