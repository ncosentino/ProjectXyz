using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Api.Enchantments.Generation;

namespace ProjectXyz.Shared.Game.GameObjects.Enchantments.Generation.InMemory
{
    public sealed class EnchantmentGeneratorFacade : IEnchantmentGenerator
    {
        private readonly IReadOnlyCollection<IEnchantmentGenerator> _enchantmentGenerators;
        private readonly IAttributeFilterer _attributeFilterer;

        public EnchantmentGeneratorFacade(
            IAttributeFilterer attributeFilterer,
            IEnumerable<IEnchantmentGenerator> enchantmentGenerators)
        {
            _attributeFilterer = attributeFilterer;
            _enchantmentGenerators = enchantmentGenerators.ToArray();
        }

        public IEnumerable<IEnchantment> GenerateEnchantments(IGeneratorContext generatorContext)
        {
            var filteredGenerators = _attributeFilterer.Filter(
                _enchantmentGenerators,
                generatorContext);
            var generator = filteredGenerators.RandomOrDefault(new Random());
            if (generator == null)
            {
                return Enumerable.Empty<IEnchantment>();
            }

            var generatedEnchantments = generator.GenerateEnchantments(generatorContext);
            return generatedEnchantments;
        }

        public IEnumerable<IGeneratorAttribute> SupportedAttributes => _enchantmentGenerators
            .SelectMany(x => x.SupportedAttributes)
            .Distinct();
    }
}