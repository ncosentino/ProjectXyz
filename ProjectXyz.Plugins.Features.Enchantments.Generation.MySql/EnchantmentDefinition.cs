using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Plugins.Features.Enchantments.Generation.MySql
{
    public sealed class EnchantmentDefinition : IEnchantmentDefinition
    {
        public EnchantmentDefinition(
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
