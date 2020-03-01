using System;
using System.Collections.Generic;
using System.Linq;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Api.GameObjects.Generation.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Items.Api.Generation;
using ProjectXyz.Shared.Game.GameObjects.Generation;

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
            var totalCount = GetCount(
                generatorContext.MinimumGenerateCount,
                generatorContext.MaximumGenerateCount);
            if (totalCount < 1)
            {
                yield break;
            }

            var filteredGenerators = _attributeFilterer
                .Filter(
                    _itemGenerators,
                    generatorContext)
                .ToArray();
            if (!filteredGenerators.Any())
            {
                throw new InvalidOperationException(
                    $"There are no item generators that match the context '{generatorContext}'.");
            }

            var generatedCount = 0;
            while (generatedCount < totalCount)
            {
                var generator = filteredGenerators.RandomOrDefault(_randomNumberGenerator);
                var currentContext = new GeneratorContext(
                    1,
                    1, // totalCount - generatedCount, // FIXME: do we hurt randomization by allowing initially selected generators to generate more?
                    generatorContext.Attributes);

                var generatedItems = generator.GenerateItems(currentContext);
                foreach (var generatedItem in generatedItems)
                {
                    generatedCount++;
                    yield return generatedItem;
                }
            }
        }

        public IEnumerable<IGeneratorAttribute> SupportedAttributes => _itemGenerators
            .SelectMany(x => x.SupportedAttributes)
            .Distinct();

        public void Register(IItemGenerator itemGenerator)
        {
            _itemGenerators.Add(itemGenerator);
        }

        private int GetCount(
            int itemCountMinimum,
            int itemCountMaximum)
        {
            var count = _randomNumberGenerator.NextInRange(
                itemCountMinimum,
                itemCountMaximum);
            return count;
        }
    }
}