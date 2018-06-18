using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Items.Generation;
using ProjectXyz.Api.Items.Generation.Attributes;

namespace ProjectXyz.Shared.Game.Items.Generation.InMemory
{
    public sealed class ItemDefinition : IItemDefinition
    {
        public ItemDefinition(
            IEnumerable<IItemGeneratorAttribute> attributes,
            IEnumerable<IItemGeneratorComponent> generatorComponents)
        {
            SupportedAttributes = attributes.ToArray();
            GeneratorComponents = generatorComponents.ToArray();
        }

        public IEnumerable<IItemGeneratorAttribute> SupportedAttributes { get; }

        public IEnumerable<IItemGeneratorComponent> GeneratorComponents { get; }
    }
}