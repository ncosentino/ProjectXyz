using System;
using System.Collections.Generic;
using System.Linq;

using NexusLabs.Framework;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.Framework.Collections;

namespace ProjectXyz.Plugins.Features.Enchantments.Generation
{
    public sealed class EnchantmentGeneratorFacade : IEnchantmentGeneratorFacade
    {
        private readonly List<IEnchantmentGenerator> _enchantmentGenerators;
        private readonly IAttributeFilterer _attributeFilterer;
        private readonly IRandom _random;

        public EnchantmentGeneratorFacade(
            IAttributeFilterer attributeFilterer,
            IRandom random,
            IEnumerable<IDiscoverableEnchantmentGenerator> discoverableEnchantmentGenerators)
        {
            _attributeFilterer = attributeFilterer;
            _random = random;
            _enchantmentGenerators = new List<IEnchantmentGenerator>(discoverableEnchantmentGenerators);
        }

        public IEnumerable<IEnchantment> GenerateEnchantments(IFilterContext filterContext)
        {
            if (!_enchantmentGenerators.Any())
            {
                throw new InvalidOperationException(
                    $"There are no enchantment generators registered to this " +
                    $"facade. Did you forget to call '{nameof(Register)}()'?");
            }

            var filteredGenerators = _attributeFilterer.Filter(
                _enchantmentGenerators,
                filterContext);
            var generator = filteredGenerators.RandomOrDefault(_random);
            if (generator == null)
            {
                return Enumerable.Empty<IEnchantment>();
            }

            var generatedEnchantments = generator.GenerateEnchantments(filterContext);
            return generatedEnchantments;
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes => _enchantmentGenerators
            .SelectMany(x => x.SupportedAttributes)
            .Distinct();

        public void Register(IEnchantmentGenerator enchantmentGenerator)
        {
            _enchantmentGenerators.Add(enchantmentGenerator);
        }
    }
}