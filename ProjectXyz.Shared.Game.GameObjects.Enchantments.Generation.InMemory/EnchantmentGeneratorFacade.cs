using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Shared.Game.GameObjects.Enchantments.Generation.InMemory
{
    public sealed class EnchantmentGeneratorFacade : IEnchantmentGeneratorFacade
    {
        private readonly List<IEnchantmentGenerator> _enchantmentGenerators;
        private readonly IAttributeFilterer _attributeFilterer;
        private readonly IRandomNumberGenerator _randomNumberGenerator;

        public EnchantmentGeneratorFacade(
            IAttributeFilterer attributeFilterer,
            IRandomNumberGenerator randomNumberGenerator)
        {
            _attributeFilterer = attributeFilterer;
            _randomNumberGenerator = randomNumberGenerator;
            _enchantmentGenerators = new List<IEnchantmentGenerator>();
        }

        public IEnumerable<IEnchantment> GenerateEnchantments(IGeneratorContext generatorContext)
        {
            var filteredGenerators = _attributeFilterer.Filter(
                _enchantmentGenerators,
                generatorContext);
            var generator = filteredGenerators.RandomOrDefault(_randomNumberGenerator);
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

        public void Register(IEnchantmentGenerator enchantmentGenerator)
        {
            _enchantmentGenerators.Add(enchantmentGenerator);
        }
    }
}