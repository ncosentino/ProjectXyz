using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation
{
    public sealed class ItemDefinition : IItemDefinition
    {
        public ItemDefinition(
            IEnumerable<IFilterAttribute> attributes,
            IEnumerable<IFilterComponent> filterComponents)
        {
            SupportedAttributes = attributes.ToArray();
            FilterComponents = filterComponents.ToArray();
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; }

        public IEnumerable<IFilterComponent> FilterComponents { get; }
    }
}