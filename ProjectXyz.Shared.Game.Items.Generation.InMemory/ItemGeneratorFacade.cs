using System;
using System.Collections.Generic;
using System.Linq;

using NexusLabs.Framework;

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
        private readonly IRandom _random;

        public ItemGeneratorFacade(
            IAttributeFilterer attributeFilterer,
            IRandom random,
            IEnumerable<IDiscoverableItemGenerator> itemGenerators)
        {
            _attributeFilterer = attributeFilterer;
            _random = random;
            _itemGenerators = new List<IItemGenerator>(itemGenerators);
        }

        public IEnumerable<IGameObject> GenerateItems(IGeneratorContext generatorContext)
        {
            var totalCount = GetGenerationCount(
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
            var elligibleGenerators = new HashSet<IItemGenerator>(filteredGenerators);
            while (generatedCount < totalCount)
            {
                if (elligibleGenerators.Count < 1)
                {
                    throw new InvalidOperationException(
                        "Could not find elligible item generators with the " +
                        "provided context. Investigate the conditions on the " +
                        "context along with the item generators.");
                }

                var generator = elligibleGenerators.RandomOrDefault(_random);
                var currentContext = new GeneratorContext(
                    1,
                    1, // totalCount - generatedCount, // FIXME: do we hurt randomization by allowing initially selected generators to generate more?
                    generatorContext.Attributes);

                var generatedItems = generator.GenerateItems(currentContext);
                var generatedAtLeastOne = false;
                foreach (var generatedItem in generatedItems)
                {
                    generatedCount++;
                    generatedAtLeastOne = true;
                    yield return generatedItem;
                }

                if (!generatedAtLeastOne)
                {
                    elligibleGenerators.Remove(generator);
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

        private int GetGenerationCount(
            int itemCountMinimum,
            int itemCountMaximum)
        {
            var count = _random.Next(
                itemCountMinimum,
                itemCountMaximum == int.MaxValue
                    ? int.MaxValue
                    : itemCountMaximum + 1);
            return count;
        }
    }
}