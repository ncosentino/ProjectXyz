using System;
using System.Collections.Generic;
using System.Linq;

using NexusLabs.Framework;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Default.Attributes;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Generation.InMemory
{
    public sealed class ItemGeneratorFacade : IItemGeneratorFacade
    {
        private readonly List<IItemGenerator> _itemGenerators;
        private readonly IAttributeFilterer _attributeFilterer;
        private readonly IRandom _random;
        private readonly IFilterContextFactory _filterContextFactory;

        public ItemGeneratorFacade(
            IAttributeFilterer attributeFilterer,
            IRandom random,
            IEnumerable<IDiscoverableItemGenerator> itemGenerators,
            IFilterContextFactory filterContextFactory)
        {
            _attributeFilterer = attributeFilterer;
            _random = random;
            _filterContextFactory = filterContextFactory;
            _itemGenerators = new List<IItemGenerator>(itemGenerators);
        }

        public IEnumerable<IGameObject> GenerateItems(IFilterContext filterContext)
        {
            var totalCount = GetGenerationCount(
                filterContext.MinimumCount,
                filterContext.MaximumCount);
            if (totalCount < 1)
            {
                yield break;
            }

            // when picking a generator, since it's the "middleman", anything "
            // required" on our context becomes optional just for selecting the
            // generator. generation itself will enforce requirements.
            var filteredGenerators = _attributeFilterer
                .BidirectionalFilter(
                    _itemGenerators,
                    filterContext.Attributes.Select(x => x.CopyWithRequired(false)))
                .ToArray();
            if (!filteredGenerators.Any())
            {
                throw new InvalidOperationException(
                    $"There are no item generators that match the context '{filterContext}'.");
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
                var currentContext = _filterContextFactory.CreateFilterContextForSingle(filterContext.Attributes);

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

        public IEnumerable<IFilterAttribute> SupportedAttributes => _itemGenerators
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