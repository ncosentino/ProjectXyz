using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Api.Items.Generation;

namespace ProjectXyz.Shared.Game.GameObjects.Items.Generation.InMemory
{
    public sealed class InMemoryItemDefinitionRepository : IItemDefinitionRepository
    {
        private readonly Lazy<IReadOnlyCollection<IItemDefinition>> _lazyItemDefinitions;
        private readonly IAttributeFilterer _attributeFilterer;

        public InMemoryItemDefinitionRepository(
            IAttributeFilterer attributeFilterer,
            IEnumerable<IItemDefinition> itemDefinitions)
        {
            _attributeFilterer = attributeFilterer;
            _lazyItemDefinitions = new Lazy<IReadOnlyCollection<IItemDefinition>>(itemDefinitions.ToArray);
        }

        private IReadOnlyCollection<IItemDefinition> ItemDefinitions => _lazyItemDefinitions.Value;

        public IEnumerable<IItemDefinition> LoadItemDefinitions(IGeneratorContext generatorContext)
        {
            var filteredItemDefinitions = _attributeFilterer.Filter(
                ItemDefinitions,
                generatorContext);
            foreach (var filteredItemDefinition in filteredItemDefinitions)
            {
                // TODO: ensure we have all of the item generation components
                // NOTE: this includes:
                // - Fixed ones attached to the item definition
                // - Filter-applies ones that aren't attached to the item definition but can be applied by filter requirement being met   

                yield return filteredItemDefinition;
            }
        }

        public IEnumerable<IGeneratorAttribute> SupportedAttributes => ItemDefinitions
            .SelectMany(x => x.SupportedAttributes)
            .Distinct();
    }
}