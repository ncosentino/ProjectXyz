using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Api.Enchantments.Generation;

namespace ProjectXyz.Shared.Game.GameObjects.Enchantments.Generation.InMemory
{
    public sealed class InMemoryEnchantmentDefinitionRepository : IEnchantmentDefinitionRepository
    {
        private readonly Lazy<IReadOnlyCollection<IEnchantmentDefinition>> _lazyEnchantmentDefinitions;
        private readonly IAttributeFilterer _attributeFilterer;

        public InMemoryEnchantmentDefinitionRepository(
            IAttributeFilterer attributeFilterer,
            IEnumerable<IEnchantmentDefinition> enchantmentDefinitions)
        {
            _attributeFilterer = attributeFilterer;
            _lazyEnchantmentDefinitions = new Lazy<IReadOnlyCollection<IEnchantmentDefinition>>(enchantmentDefinitions.ToArray);
        }

        private IReadOnlyCollection<IEnchantmentDefinition> EnchantmentDefinitions => _lazyEnchantmentDefinitions.Value;

        public IEnumerable<IEnchantmentDefinition> LoadEnchantmentDefinitions(IGeneratorContext generatorContext)
        {
            var filteredEnchantmentDefinitions = _attributeFilterer.Filter(
                EnchantmentDefinitions,
                generatorContext);
            foreach (var filteredEnchantmentDefinition in filteredEnchantmentDefinitions)
            {
                // TODO: ensure we have all of the Enchantment generation components
                // NOTE: this includes:
                // - Fixed ones attached to the Enchantment definition
                // - Filter-applies ones that aren't attached to the Enchantment definition but can be applied by filter requirement being met   

                yield return filteredEnchantmentDefinition;
            }
        }

        public IEnumerable<IGeneratorAttribute> SupportedAttributes => EnchantmentDefinitions
            .SelectMany(x => x.SupportedAttributes)
            .Distinct();
    }
}