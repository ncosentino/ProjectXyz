using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Items.Generation;
using ProjectXyz.Api.Items.Generation.Attributes;
using ProjectXyz.Framework.Extensions.Collections;

namespace ProjectXyz.Shared.Game.Items.Generation.InMemory
{
    public sealed class ItemGeneratorFacade : IItemGenerator
    {
        private readonly IReadOnlyCollection<IItemGenerator> _itemGenerators;
        private readonly IAttributeFilterer _attributeFilterer;

        public ItemGeneratorFacade(
            IAttributeFilterer attributeFilterer,
            IEnumerable<IItemGenerator> itemGenerators)
        {
            _attributeFilterer = attributeFilterer;
            _itemGenerators = itemGenerators.ToArray();
        }

        public IEnumerable<IGameObject> GenerateItems(IItemGeneratorContext itemGeneratorContext)
        {
            var filteredGenerators = _attributeFilterer.Filter(
                _itemGenerators,
                itemGeneratorContext);
            var generator = filteredGenerators.RandomOrDefault(new Random());
            if (generator == null)
            {
                return Enumerable.Empty<IGameObject>();
            }

            var generatedItems = generator.GenerateItems(itemGeneratorContext);
            return generatedItems;
        }

        public IEnumerable<IItemGeneratorAttribute> SupportedAttributes => _itemGenerators
            .SelectMany(x => x.SupportedAttributes)
            .Distinct();
    }
}