using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.Generation.InMemory
{
    public sealed class InMemoryEnchantmentDefinitionRepository : IDiscoverableReadOnlyEnchantmentDefinitionRepository
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

        public IEnumerable<IEnchantmentDefinition> ReadEnchantmentDefinitions(IFilterContext filterContext)
        {
            if (!_enchantmentDefinitions.Any())
            {
                throw new InvalidOperationException(
                    $"There are no {typeof(IEnchantmentDefinition)} instances " +
                    $"for this repository. Did you forget to construct " +
                    $"{GetType()} with these instances or register them with " +
                    $"your dependency injection framework?");
            }

            var filteredEnchantmentDefinitions = _attributeFilterer.BidirectionalFilter(
                _enchantmentDefinitions,
                filterContext.Attributes);
            foreach (var filteredEnchantmentDefinition in filteredEnchantmentDefinitions)
            {
                // TODO: ensure we have all of the Enchantment generation components
                // NOTE: this includes:
                // - Fixed ones attached to the Enchantment definition
                // - Filter-applies ones that aren't attached to the Enchantment definition but can be applied by filter requirement being met   

                yield return filteredEnchantmentDefinition;
            }
        }

        public IEnumerable<IFilterAttribute> SupportedAttributes => _enchantmentDefinitions
            .SelectMany(x => x.SupportedAttributes)
            .Distinct();
    }
}