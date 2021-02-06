﻿using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Enchantments.Generation;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;

namespace ProjectXyz.Shared.Game.GameObjects.Enchantments.Generation.InMemory
{
    public sealed class InMemoryEnchantmentDefinitionRepository : IReadOnlyEnchantmentDefinitionRepository
    {
        private readonly List<IEnchantmentDefinition> _enchantmentDefinitions;
        private readonly IAttributeFilterer _attributeFilterer;

        public InMemoryEnchantmentDefinitionRepository(
            IAttributeFilterer attributeFilterer,
            IEnumerable<IEnchantmentDefinition> enchantmentDefinitions)
        {
            _attributeFilterer = attributeFilterer;
            _enchantmentDefinitions = new List<IEnchantmentDefinition>(enchantmentDefinitions);
        }

        public IEnumerable<IEnchantmentDefinition> ReadEnchantmentDefinitions(IGeneratorContext generatorContext)
        {
            if (!_enchantmentDefinitions.Any())
            {
                throw new InvalidOperationException(
                    $"There are no {typeof(IEnchantmentDefinition)} instances " +
                    $"for this repository. Did you forget to construct " +
                    $"{GetType()} with these instances or register them with " +
                    $"your dependency injection framework?");
            }

            var filteredEnchantmentDefinitions = _attributeFilterer.Filter(
                _enchantmentDefinitions,
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

        public IEnumerable<IGeneratorAttribute> SupportedAttributes => _enchantmentDefinitions
            .SelectMany(x => x.SupportedAttributes)
            .Distinct();
    }
}