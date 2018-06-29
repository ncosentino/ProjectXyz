using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation.InMemory
{
    public sealed class ItemGeneratorFacade : IItemGeneratorFacade
    {
        private readonly List<IItemGenerator> _itemGenerators;
        private readonly IAttributeFilterer _attributeFilterer;
        private readonly IRandomNumberGenerator _randomNumberGenerator;

        public ItemGeneratorFacade(
            IAttributeFilterer attributeFilterer,
            IRandomNumberGenerator randomNumberGenerator)
        {
            _attributeFilterer = attributeFilterer;
            _randomNumberGenerator = randomNumberGenerator;
            _itemGenerators = new List<IItemGenerator>();
        }

        public IEnumerable<IGameObject> GenerateItems(IGeneratorContext generatorContext)
        {
            var filteredGenerators = _attributeFilterer.Filter(
                _itemGenerators,
                generatorContext);
            var generator = filteredGenerators.RandomOrDefault(_randomNumberGenerator);
            if (generator == null)
            {
                throw new InvalidOperationException(
                    $"There are no item generators that match the context '{generatorContext}'.");
            }

            var generatedItems = generator.GenerateItems(generatorContext);
            return generatedItems;
        }

        public IEnumerable<IGeneratorAttribute> SupportedAttributes => _itemGenerators
            .SelectMany(x => x.SupportedAttributes)
            .Distinct();

        public void Register(IItemGenerator itemGenerator)
        {
            _itemGenerators.Add(itemGenerator);
        }
    }
}