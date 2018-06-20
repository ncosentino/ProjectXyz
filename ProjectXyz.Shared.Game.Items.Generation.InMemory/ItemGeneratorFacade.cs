using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Api.GameObjects.Items.Generation;

namespace ProjectXyz.Shared.Game.GameObjects.Items.Generation.InMemory
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

        public IEnumerable<IGameObject> GenerateItems(IGeneratorContext generatorContext)
        {
            var filteredGenerators = _attributeFilterer.Filter(
                _itemGenerators,
                generatorContext);
            var generator = filteredGenerators.RandomOrDefault(new Random());
            if (generator == null)
            {
                return Enumerable.Empty<IGameObject>();
            }

            var generatedItems = generator.GenerateItems(generatorContext);
            return generatedItems;
        }

        public IEnumerable<IGeneratorAttribute> SupportedAttributes => _itemGenerators
            .SelectMany(x => x.SupportedAttributes)
            .Distinct();
    }
}