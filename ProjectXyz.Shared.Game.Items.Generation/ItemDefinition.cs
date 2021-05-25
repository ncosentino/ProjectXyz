using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation
{
    public sealed class ItemDefinition : IItemDefinition
    {
        public ItemDefinition(
            IEnumerable<IFilterAttribute> attributes,
            IEnumerable<IGeneratorComponent> filterComponents)
        {
            SupportedAttributes = attributes.ToArray();
            GeneratorComponents = filterComponents.ToArray();
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; }

        public IEnumerable<IGeneratorComponent> GeneratorComponents { get; }
    }
}