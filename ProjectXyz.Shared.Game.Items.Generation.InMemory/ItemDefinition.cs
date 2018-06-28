using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation.InMemory
{
    public sealed class ItemDefinition : IItemDefinition
    {
        public ItemDefinition(
            IEnumerable<IGeneratorAttribute> attributes,
            IEnumerable<IGeneratorComponent> generatorComponents)
        {
            SupportedAttributes = attributes.ToArray();
            GeneratorComponents = generatorComponents.ToArray();
        }

        public IEnumerable<IGeneratorAttribute> SupportedAttributes { get; }

        public IEnumerable<IGeneratorComponent> GeneratorComponents { get; }
    }
}