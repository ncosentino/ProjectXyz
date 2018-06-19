using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Api.Enchantments.Generation;

namespace ProjectXyz.Shared.Game.GameObjects.Enchantments.Generation.InMemory
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